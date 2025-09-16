using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CudaAudio.Core
{
	public class AudioCollection
	{
		public ConcurrentDictionary<Guid, AudioObj> tracks { get; private set; } = [];
		public IReadOnlyList<AudioObj> Tracks => this.tracks.Values.OrderBy(t => t.CreatedAt).ToList();
		public BindingList<AudioObj> TracksBindingList = [];

		/*
		public static DedicatedThreadScheduler StretchingScheduler { get; } = new(
			Math.Clamp(Environment.ProcessorCount / 2, 2, 8),
			ThreadPriority.AboveNormal);*/

		// NEU: UI-Synchronisierung (für BindingList-Änderungen)
		private SynchronizationContext? uiCtx;

		public int Count => this.Tracks.Count;
		public string[] Entries => this.Tracks.Select(t => t.Name).ToArray();
		public string[] Playing => this.tracks.Values.Where(t => t.Playing).Select(t => t.Name).ToArray();

		public Color GraphColor { get; set; } = SystemColors.ActiveCaption;
		public Color BackColor { get; set; } = Color.White;

		public AudioObj? this[Guid guid]
		{
			get => this.tracks[guid];
		}

		public AudioObj? this[string name]
		{
			get => this.tracks.Values.FirstOrDefault(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public AudioObj? this[int index]
		{
			get => index >= 0 && index < this.Count ? this.tracks.Values.ElementAt(index) : null;
		}

		public AudioObj? this[IntPtr pointer]
		{
			get => pointer != IntPtr.Zero ? this.tracks.Values.FirstOrDefault(t => t.Pointer == pointer) : null;
		}

		public AudioCollection()
		{
			this.uiCtx = SynchronizationContext.Current; // kann zu Beginn null sein (Program.Main erstellt vor Application.Run)
		}

		public AudioCollection(Color? graphColor = null)
		{
			this.GraphColor = graphColor ?? this.GraphColor;
			this.uiCtx = SynchronizationContext.Current;
		}

		// NEU: UI-Context binden, am besten im UI (z.B. im WindowMain-Konstruktor) aufrufen
		public void BindToUiContext(SynchronizationContext? ctx = null)
		{
			this.uiCtx = ctx ?? SynchronizationContext.Current ?? this.uiCtx;
		}

		public void SetRemoveAfterPlayback(bool remove)
		{
			foreach (var track in this.tracks.Values)
			{
				track.RemoveAfterPlayback = remove;
			}
		}

		public async Task<AudioObj?> ImportAsync(string filePath, bool linearLoad = true)
		{
			// UI-Context ggf. spät binden
			this.uiCtx ??= SynchronizationContext.Current;

			AudioObj? obj = null;
			if (linearLoad)
			{
				await Task.Run(() =>
				{
					obj = new AudioObj(filePath, true);
				});
			}
			else
			{
				obj = await AudioObj.CreateAsync(filePath);
			}
			if (obj == null)
			{
				return null;
			}

			// Objekt auf UI-Context mappen (für Events/PropertyChanged)
			obj.BindToUiContext(this.uiCtx);

			// In Collection aufnehmen
			if (!this.tracks.TryAdd(obj.Id, obj))
			{
				obj.Dispose();
				return null;
			}

			// NEU: Bei RemoveRequested das Objekt aus der Collection entfernen
			obj.RemoveRequested += async (_, __) =>
			{
				await this.RemoveAsync(obj);
			};

			// BindingList-Add garantiert im UI-Thread
			if (this.uiCtx != null && SynchronizationContext.Current != this.uiCtx)
			{
				var tcs = new TaskCompletionSource();
				this.uiCtx.Post(_ =>
				{
					try
					{
						this.TracksBindingList.Add(obj);
						tcs.SetResult();
					}
					catch (Exception ex)
					{
						tcs.SetException(ex);
					}
				}, null);
				await tcs.Task.ConfigureAwait(false);
			}
			else
			{
				this.TracksBindingList.Add(obj);
			}

			return obj;
		}

		public async Task RemoveAsync(AudioObj? obj)
		{
			if (obj == null)
			{
				return;
			}

			// Entfernen aus Dictionary (threadsafe)
			if (this.tracks.TryRemove(obj.Id, out var removed))
			{
				// BindingList-Remove im UI-Thread
				if (this.uiCtx != null && SynchronizationContext.Current != this.uiCtx)
				{
					var tcs = new TaskCompletionSource();
					this.uiCtx.Post(_ =>
					{
						try
						{
							this.TracksBindingList.Remove(removed);
							tcs.SetResult();
						}
						catch (Exception ex)
						{
							tcs.SetException(ex);
						}
					}, null);
					await tcs.Task.ConfigureAwait(false);
				}
				else
				{
					this.TracksBindingList.Remove(removed);
				}

				removed.Dispose();
			}
		}

		public void StopAll(bool remove = false)
		{
			foreach (var track in this.tracks.Values.ToList())
			{
				bool wasPlaying = track.Playing; // Merken, ob Track lief
				track.Stop();

				if (remove && wasPlaying)
				{
					if (this.tracks.TryRemove(track.Id, out var t))
					{
						if (this.uiCtx != null && SynchronizationContext.Current != this.uiCtx)
						{
							this.uiCtx.Post(_ => this.TracksBindingList.Remove(t), null);
						}
						else
						{
							this.TracksBindingList.Remove(t);
						}
						t?.Dispose();
					}
				}
			}
		}

		public void SetMasterVolume(float percentage)
		{
			percentage = Math.Clamp(percentage, 0.0f, 1.0f);

			foreach (var track in this.tracks.Values)
			{
				int volume = (int) (track.Volume * percentage);
				track.SetVolume(volume);
			}
		}

		public async Task DisposeAsync()
		{
			var items = this.tracks.Values.ToList();
			this.tracks.Clear();

			// BindingList-Clear im UI-Thread
			if (this.uiCtx != null && SynchronizationContext.Current != this.uiCtx)
			{
				var tcs = new TaskCompletionSource();
				this.uiCtx.Post(_ =>
				{
					try
					{
						this.TracksBindingList.Clear();
						tcs.SetResult();
					}
					catch (Exception ex)
					{
						tcs.SetException(ex);
					}
				}, null);
				await tcs.Task.ConfigureAwait(false);
			}
			else
			{
				this.TracksBindingList.Clear();
			}

			foreach (var track in items)
			{
				track.Dispose();
			}
		}

		public static async Task<AudioObj?> LevelAudioFileAsync(string filePath, float duration = 1.0f, float normalize = 1.0f)
		{
			AudioObj? obj = await AudioObj.CreateAsync(filePath);
			if (obj == null)
			{
				return null;
			}

			await obj.Level(duration, normalize);

			return obj;
		}
	}
}
