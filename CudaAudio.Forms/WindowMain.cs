using CudaAudio.Core;
using CudaAudio.Runtime;
using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Diagnostics;

namespace CudaAudio.Forms
{
	public partial class WindowMain : Form
	{
		private AudioCollection audioCollection = new();
		private CudaService cudaService = new();

		public AudioObj? SelectedTrack => this.audioCollection[(this.GetSelectedTrackItemSafe()?.ToString() ?? "???")];

		private System.Threading.Timer? uiTimer;
		private Dictionary<Guid, CancellationTokenSource> playbackCancellationTokens = [];
		private float volume => (100f - this.vScrollBar_volume.Value) / 100f;

		private int chunkSize => (int) this.numericUpDown_chunkSize.Value;
		private double overlap => (double) this.numericUpDown_overlap.Value;
		private int overlapSize => (int) (this.overlap * this.overlap);
		private double initialBpm => (double) this.numericUpDown_initialBpm.Value;
		private double targetBpm => (double) this.numericUpDown_targetBpm.Value;
		private double stretchFactor => (double) this.numericUpDown_stretchFactor.Value;
		private double calculatedStretchFactor => this.initialBpm / this.targetBpm;

		public WindowMain()
		{
			this.InitializeComponent();

			this.DragEnter += this.WindowMain_DragEnter;
			this.DragDrop += this.WindowMain_DragDrop;

			this.audioCollection.BindToUiContext();
			this.listBox_tracks.DataSource = this.audioCollection.TracksBindingList;
			this.audioCollection.TracksBindingList.ListChanged += this.TracksBindingList_ListChanged;
			this.SetupContextMenuForListBox();

			this.uiTimer = this.SetupUiTimer();

			this.FillDevicesComboBox(this.comboBox_devices);
		}




		private async Task UiTimerCallback()
		{
			// Always update timestamp for selected track
			if (this.SelectedTrack != null)
			{
				this.textBox_timeStamp.Text = this.SelectedTrack.CurrentTime.ToString(@"h\:mm\:ss\.fff");
			}

			await this.UpdateSingleWaveform();
		}

		private async Task UpdateSingleWaveform()
		{
			if (this.IsDisposed || !this.IsHandleCreated)
			{
				return;
			}

			if (this.InvokeRequired)
			{
				this.BeginInvoke(new Action(async () => await this.UpdateSingleWaveform()));
				return;
			}

			try
			{
				var track = this.SelectedTrack;
				if (track != null)
				{
					Color graph = Color.FromArgb(200, 100, 200, 255); // Semi-transparent blue

					this.pictureBox_waveform.Image = await track.GetWaveformImageSimpleAsync(
						null,
						this.pictureBox_waveform.Width,
						this.pictureBox_waveform.Height,
						(int) this.numericUpDown_zoom.Value,
						graphColor: graph,
						backgroundColor: this.audioCollection.BackColor
					);
				}
				else
				{
					this.pictureBox_waveform.Image = new Bitmap(this.pictureBox_waveform.Width, this.pictureBox_waveform.Height);
				}

				this.pictureBox_waveform.Invalidate();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error updating single waveform: {ex.Message}");
			}
		}

		private void SetupContextMenuForListBox()
		{
			ContextMenuStrip contextMenu = new();
			ToolStripMenuItem removeItem = new("Remove");

			removeItem.Click += async (sender, e) =>
			{
				if (this.SelectedTrack != null)
				{
					await this.audioCollection.RemoveAsync(this.SelectedTrack);
				}
			};

			contextMenu.Items.Add(removeItem);

			// Diese Lösung behält das ContextMenuStrip bei
			this.listBox_tracks.MouseDown += (sender, e) =>
			{
				try
				{
					if (e.Button == MouseButtons.Right)
					{
						int index = this.listBox_tracks.IndexFromPoint(e.Location);
						if (index != ListBox.NoMatches)
						{
							this.listBox_tracks.SelectedIndex = index;
							// Kurze Verzögerung für bessere UX
							Task.Delay(50).ContinueWith(_ =>
							{
								this.listBox_tracks.Invoke((MethodInvoker) delegate
								{
									Point center = new Point(Cursor.Position.X - 15, Cursor.Position.Y - 10);
									contextMenu.Show(center);
								});
							}, TaskScheduler.FromCurrentSynchronizationContext());
						}
					}
				}
				catch (ObjectDisposedException)
				{
					// Form is disposed, ignore
				}
				catch (InvalidOperationException)
				{
					// Handle if invoke cannot be performed
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"Error showing context menu: {ex.Message}");
				}
			};
		}

		private System.Threading.Timer SetupUiTimer(int interval = 50)
		{
			var timer = new System.Threading.Timer(async (state) =>
			{
				if (this.InvokeRequired)
				{
					this.BeginInvoke(new Action(async () =>
					{
						await this.UiTimerCallback();
					}));
				}
				else
				{
					await this.UiTimerCallback();
				}
			}, null, 0, interval);

			if (this.uiTimer != null)
			{
				this.uiTimer.Dispose();
				this.uiTimer = timer;
			}

			return timer;
		}

		private void FillDevicesComboBox(ComboBox comboBox)
		{
			comboBox.Items.Clear();

			comboBox.Items.AddRange(this.cudaService.DeviceEntries.ToArray());

			// Event
			comboBox.SelectedIndexChanged += (sender, e) =>
			{
				try
				{
					if (comboBox.SelectedIndex >= 0 && comboBox.SelectedIndex < this.cudaService.DeviceEntries.Count)
					{
						this.cudaService.Initialize(comboBox.SelectedIndex);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"CUDA Initialization Error: {ex.Message}", "CUDA Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			};
		}


		private void TracksBindingList_ListChanged(object? sender, ListChangedEventArgs e)
		{
			// Invoke on UI thread if necessary
			if (this.InvokeRequired)
			{
				this.BeginInvoke(new Action(() => this.TracksBindingList_ListChanged(sender, e)));
				return;
			}

			try
			{
				this.listBox_tracks.Refresh();
			}
			catch (ObjectDisposedException)
			{
				// Form is disposed, ignore
			}
			catch (InvalidOperationException)
			{
				// Handle if invoke cannot be performed
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error refreshing listBox_tracks: {ex.Message}");
			}
		}

		private object? GetSelectedTrackItemSafe()
		{
			if (this.listBox_tracks.InvokeRequired)
			{
				return this.listBox_tracks.Invoke(new Func<object?>(() => this.listBox_tracks.SelectedItem));
			}
			else
			{
				return this.listBox_tracks.SelectedItem;
			}
		}





		private void UpdateInfo()
		{
			// Update playback button
			if (this.SelectedTrack != null && this.SelectedTrack.Playing)
			{
				this.button_playStop.Text = this.button_playStop.Tag?.ToString() ?? "■";
			}
			else
			{
				this.button_playStop.Text = "▶";
			}

			// FFT button
			if (this.SelectedTrack?.Form == "f")
			{
				this.button_fourier.Text = "FFT";
			}
			else if (this.SelectedTrack?.Form == "c")
			{
				this.button_fourier.Text = "IFFT";
			}
			else
			{
				this.button_fourier.Text = "Fourier";
			}

			// Set pointer
			this.label_pointer.Text = this.SelectedTrack != null ? $"Pointer: <{this.SelectedTrack.Pointer}>" : "Pointer: N/A";

			// Set initial bpm
			if (this.SelectedTrack != null && this.SelectedTrack.Bpm > 10)
			{
				this.numericUpDown_initialBpm.Value = (decimal) Math.Clamp(this.SelectedTrack.Bpm, (double) this.numericUpDown_initialBpm.Minimum, (double) this.numericUpDown_initialBpm.Maximum);
			}

			// Set times textbox
			this.textBox_metrics.Text = this.SelectedTrack?.GetMetricsString() ?? "N/A";
		}


		private void listBox_tracks_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateInfo();
		}


		private async void button_import_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new()
			{
				Title = "Import Audio File",
				Filter = "Audio Files|*.wav;*.mp3;*.flac",
				Multiselect = true,
				RestoreDirectory = true
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				var tasks = ofd.FileNames.Select(file => this.audioCollection.ImportAsync(file, true)).ToArray();

				await Task.WhenAll(tasks);
			}

			this.UpdateInfo();
		}

		private void button_export_Click(object sender, EventArgs e)
		{

		}

		private void WindowMain_DragEnter(object? sender, DragEventArgs e)
		{
			if (e.Data is IDataObject data && data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[]?) data.GetData(DataFormats.FileDrop) ?? [];
				if (files.Any(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
								   f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
								   f.EndsWith(".flac", StringComparison.OrdinalIgnoreCase)))
				{
					e.Effect = DragDropEffects.Copy;
					return;
				}
			}
			e.Effect = DragDropEffects.None;
		}

		private async void WindowMain_DragDrop(object? sender, DragEventArgs e)
		{
			if (e.Data is IDataObject data && data.GetDataPresent(DataFormats.FileDrop))
			{
				string[]? files = (string[]?) data.GetData(DataFormats.FileDrop ?? "");
				var audioFiles = files?.Where(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
												  f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
												  f.EndsWith(".flac", StringComparison.OrdinalIgnoreCase)).ToArray();

				if (audioFiles != null && audioFiles.Length > 0)
				{
					var tasks = audioFiles.Select(file => this.audioCollection.ImportAsync(file, true)).ToArray();
					await Task.WhenAll(tasks);
				}

				this.UpdateInfo();
			}
		}


		private void button_reset_Click(object sender, EventArgs e)
		{
			if (this.SelectedTrack == null)
			{
				return;
			}

			this.SelectedTrack.LoadAudioFile();
			this.UpdateInfo();
		}

		private async void button_playStop_Click(object sender, EventArgs e)
		{
			if (this.SelectedTrack == null)
			{
				return;
			}

			try
			{
				if (this.SelectedTrack.Playing)
				{
					if (this.audioCollection.Tracks.Count() <= 1)
					{
						this.SelectedTrack.RemoveAfterPlayback = false;
					}


					this.SelectedTrack.Stop();
					if (this.SelectedTrack == null)
					{
						return;
					}

					if (this.playbackCancellationTokens.TryGetValue(this.SelectedTrack.Id, out CancellationTokenSource? cts))
					{
						cts.Cancel();
						this.playbackCancellationTokens.Remove(this.SelectedTrack.Id);
					}
				}
				else
				{
					if (this.playbackCancellationTokens.ContainsKey(this.SelectedTrack.Id))
					{
						this.playbackCancellationTokens[this.SelectedTrack.Id].Cancel();
						this.playbackCancellationTokens.Remove(this.SelectedTrack.Id);
					}
					CancellationTokenSource cts = new();
					this.playbackCancellationTokens[this.SelectedTrack.Id] = cts;

					await this.SelectedTrack.Play(cts.Token, null, this.volume);
				}

				this.button_playStop.Text = this.SelectedTrack.Playing ? this.button_playStop.Tag?.ToString() ?? "■" : "▶";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Playback error: {ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.SelectedTrack?.Stop();
				this.button_playStop.Text = "▶";
			}

		}

		private void vScrollBar_volume_Scroll(object sender, ScrollEventArgs e)
		{
			this.SelectedTrack?.SetVolume(this.volume);
		}



		// Processing
		private void numericUpDown_initialBpm_ValueChanged(object sender, EventArgs e)
		{
			this.numericUpDown_stretchFactor.Value = (decimal) Math.Clamp(this.calculatedStretchFactor, (double) this.numericUpDown_stretchFactor.Minimum, (double) this.numericUpDown_stretchFactor.Maximum);

			this.numericUpDown_initialBpm.Tag = this.initialBpm;
		}

		private void numericUpDown_targetBpm_ValueChanged(object sender, EventArgs e)
		{
			this.numericUpDown_stretchFactor.Value = (decimal) Math.Clamp(this.calculatedStretchFactor, (double) this.numericUpDown_stretchFactor.Minimum, (double) this.numericUpDown_stretchFactor.Maximum);

			this.numericUpDown_targetBpm.Tag = this.targetBpm;
		}

		private void numericUpDown_stretchFactor_ValueChanged(object sender, EventArgs e)
		{
			this.numericUpDown_targetBpm.Value = (decimal) Math.Clamp((double) (this.initialBpm / this.stretchFactor), (double) this.numericUpDown_targetBpm.Minimum, (double) this.numericUpDown_targetBpm.Maximum);
		}

		private void numericUpDown_chunkSize_ValueChanged(object sender, EventArgs e)
		{
			int previousValue = int.Parse(this.numericUpDown_chunkSize.Tag?.ToString() ?? "16384");
			if (this.chunkSize > previousValue)
			{
				this.numericUpDown_chunkSize.Value = Math.Clamp(previousValue * 2, (int) this.numericUpDown_chunkSize.Minimum, (int) this.numericUpDown_chunkSize.Maximum);
			}
			else if (this.chunkSize < previousValue)
			{
				this.numericUpDown_chunkSize.Value = Math.Clamp(previousValue / 2, (int) this.numericUpDown_chunkSize.Minimum, (int) this.numericUpDown_chunkSize.Maximum);
			}

			this.numericUpDown_chunkSize.Tag = this.chunkSize;
		}

		private void numericUpDown_overlap_ValueChanged(object sender, EventArgs e)
		{

		}





		// CUDA
		private async void button_stretch_Click(object sender, EventArgs e)
		{

		}

		private async void button_fourier_Click(object sender, EventArgs e)
		{
			if (this.SelectedTrack == null)
			{
				return;
			}

			var audio = this.SelectedTrack;

			int fftSize = (int) this.numericUpDown_chunkSize.Value;
			float overlap = (float) this.numericUpDown_overlap.Value;

			var result = await this.cudaService.FourierTransformAsync(audio, fftSize, overlap, false, false, true, true);

			this.UpdateInfo();
		}



	}
}
