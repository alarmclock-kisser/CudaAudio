namespace CudaAudio.Forms
{
    partial class WindowMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBox_devices = new ComboBox();
			this.pictureBox_waveform = new PictureBox();
			this.listBox_tracks = new ListBox();
			this.button_import = new Button();
			this.button_playStop = new Button();
			this.button_reset = new Button();
			this.textBox_timeStamp = new TextBox();
			this.numericUpDown_zoom = new NumericUpDown();
			this.label_pointer = new Label();
			this.groupBox_processing = new GroupBox();
			this.button_fourier = new Button();
			this.label_info_stretchFactor = new Label();
			this.label_info_targetBpm = new Label();
			this.label_info_initialBpm = new Label();
			this.label_info_overlap = new Label();
			this.label_info_chunkSize = new Label();
			this.numericUpDown_overlap = new NumericUpDown();
			this.numericUpDown_chunkSize = new NumericUpDown();
			this.button_stretch = new Button();
			this.numericUpDown_stretchFactor = new NumericUpDown();
			this.numericUpDown_targetBpm = new NumericUpDown();
			this.numericUpDown_initialBpm = new NumericUpDown();
			this.vScrollBar_volume = new VScrollBar();
			this.button_export = new Button();
			this.progressBar_processing = new ProgressBar();
			this.textBox_metrics = new TextBox();
			((System.ComponentModel.ISupportInitialize) this.pictureBox_waveform).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_zoom).BeginInit();
			this.groupBox_processing.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_overlap).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_chunkSize).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_stretchFactor).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_targetBpm).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_initialBpm).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox_devices
			// 
			this.comboBox_devices.FormattingEnabled = true;
			this.comboBox_devices.Location = new Point(12, 12);
			this.comboBox_devices.Name = "comboBox_devices";
			this.comboBox_devices.Size = new Size(320, 23);
			this.comboBox_devices.TabIndex = 0;
			this.comboBox_devices.Text = "Select CUDA Device ...";
			// 
			// pictureBox_waveform
			// 
			this.pictureBox_waveform.BackColor = Color.White;
			this.pictureBox_waveform.Location = new Point(12, 584);
			this.pictureBox_waveform.Name = "pictureBox_waveform";
			this.pictureBox_waveform.Size = new Size(663, 85);
			this.pictureBox_waveform.TabIndex = 1;
			this.pictureBox_waveform.TabStop = false;
			// 
			// listBox_tracks
			// 
			this.listBox_tracks.FormattingEnabled = true;
			this.listBox_tracks.ItemHeight = 15;
			this.listBox_tracks.Location = new Point(452, 409);
			this.listBox_tracks.Name = "listBox_tracks";
			this.listBox_tracks.Size = new Size(240, 169);
			this.listBox_tracks.TabIndex = 2;
			this.listBox_tracks.SelectedIndexChanged += this.listBox_tracks_SelectedIndexChanged;
			// 
			// button_import
			// 
			this.button_import.Location = new Point(617, 380);
			this.button_import.Name = "button_import";
			this.button_import.Size = new Size(75, 23);
			this.button_import.TabIndex = 3;
			this.button_import.Text = "Import";
			this.button_import.UseVisualStyleBackColor = true;
			this.button_import.Click += this.button_import_Click;
			// 
			// button_playStop
			// 
			this.button_playStop.Location = new Point(452, 380);
			this.button_playStop.Name = "button_playStop";
			this.button_playStop.Size = new Size(23, 23);
			this.button_playStop.TabIndex = 4;
			this.button_playStop.Tag = "◼";
			this.button_playStop.Text = "▶";
			this.button_playStop.UseVisualStyleBackColor = true;
			this.button_playStop.Click += this.button_playStop_Click;
			// 
			// button_reset
			// 
			this.button_reset.Location = new Point(617, 351);
			this.button_reset.Name = "button_reset";
			this.button_reset.Size = new Size(75, 23);
			this.button_reset.TabIndex = 5;
			this.button_reset.Text = "Reset";
			this.button_reset.UseVisualStyleBackColor = true;
			this.button_reset.Click += this.button_reset_Click;
			// 
			// textBox_timeStamp
			// 
			this.textBox_timeStamp.Location = new Point(12, 555);
			this.textBox_timeStamp.Name = "textBox_timeStamp";
			this.textBox_timeStamp.PlaceholderText = "0:00:00.000";
			this.textBox_timeStamp.Size = new Size(80, 23);
			this.textBox_timeStamp.TabIndex = 6;
			// 
			// numericUpDown_zoom
			// 
			this.numericUpDown_zoom.Location = new Point(386, 555);
			this.numericUpDown_zoom.Maximum = new decimal(new int[] { 65536, 0, 0, 0 });
			this.numericUpDown_zoom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown_zoom.Name = "numericUpDown_zoom";
			this.numericUpDown_zoom.Size = new Size(60, 23);
			this.numericUpDown_zoom.TabIndex = 7;
			this.numericUpDown_zoom.Value = new decimal(new int[] { 256, 0, 0, 0 });
			// 
			// label_pointer
			// 
			this.label_pointer.AutoSize = true;
			this.label_pointer.Location = new Point(12, 38);
			this.label_pointer.Name = "label_pointer";
			this.label_pointer.Size = new Size(73, 15);
			this.label_pointer.TabIndex = 8;
			this.label_pointer.Text = "Pointer: <0>";
			// 
			// groupBox_processing
			// 
			this.groupBox_processing.Controls.Add(this.button_fourier);
			this.groupBox_processing.Controls.Add(this.label_info_stretchFactor);
			this.groupBox_processing.Controls.Add(this.label_info_targetBpm);
			this.groupBox_processing.Controls.Add(this.label_info_initialBpm);
			this.groupBox_processing.Controls.Add(this.label_info_overlap);
			this.groupBox_processing.Controls.Add(this.label_info_chunkSize);
			this.groupBox_processing.Controls.Add(this.numericUpDown_overlap);
			this.groupBox_processing.Controls.Add(this.numericUpDown_chunkSize);
			this.groupBox_processing.Controls.Add(this.button_stretch);
			this.groupBox_processing.Controls.Add(this.numericUpDown_stretchFactor);
			this.groupBox_processing.Controls.Add(this.numericUpDown_targetBpm);
			this.groupBox_processing.Controls.Add(this.numericUpDown_initialBpm);
			this.groupBox_processing.Location = new Point(12, 56);
			this.groupBox_processing.Name = "groupBox_processing";
			this.groupBox_processing.Size = new Size(320, 186);
			this.groupBox_processing.TabIndex = 9;
			this.groupBox_processing.TabStop = false;
			this.groupBox_processing.Text = "Processing";
			// 
			// button_fourier
			// 
			this.button_fourier.Location = new Point(239, 22);
			this.button_fourier.Name = "button_fourier";
			this.button_fourier.Size = new Size(75, 23);
			this.button_fourier.TabIndex = 19;
			this.button_fourier.Text = "Fourier";
			this.button_fourier.UseVisualStyleBackColor = true;
			this.button_fourier.Click += this.button_fourier_Click;
			// 
			// label_info_stretchFactor
			// 
			this.label_info_stretchFactor.AutoSize = true;
			this.label_info_stretchFactor.Location = new Point(138, 139);
			this.label_info_stretchFactor.Name = "label_info_stretchFactor";
			this.label_info_stretchFactor.Size = new Size(80, 15);
			this.label_info_stretchFactor.TabIndex = 18;
			this.label_info_stretchFactor.Text = "Stretch Factor";
			// 
			// label_info_targetBpm
			// 
			this.label_info_targetBpm.AutoSize = true;
			this.label_info_targetBpm.Location = new Point(72, 139);
			this.label_info_targetBpm.Name = "label_info_targetBpm";
			this.label_info_targetBpm.Size = new Size(61, 15);
			this.label_info_targetBpm.TabIndex = 17;
			this.label_info_targetBpm.Text = "Dest. BPM";
			// 
			// label_info_initialBpm
			// 
			this.label_info_initialBpm.AutoSize = true;
			this.label_info_initialBpm.Location = new Point(6, 139);
			this.label_info_initialBpm.Name = "label_info_initialBpm";
			this.label_info_initialBpm.Size = new Size(55, 15);
			this.label_info_initialBpm.TabIndex = 16;
			this.label_info_initialBpm.Text = "Init. BPM";
			// 
			// label_info_overlap
			// 
			this.label_info_overlap.AutoSize = true;
			this.label_info_overlap.Location = new Point(72, 95);
			this.label_info_overlap.Name = "label_info_overlap";
			this.label_info_overlap.Size = new Size(48, 15);
			this.label_info_overlap.TabIndex = 15;
			this.label_info_overlap.Text = "Overlap";
			// 
			// label_info_chunkSize
			// 
			this.label_info_chunkSize.AutoSize = true;
			this.label_info_chunkSize.Location = new Point(6, 95);
			this.label_info_chunkSize.Name = "label_info_chunkSize";
			this.label_info_chunkSize.Size = new Size(54, 15);
			this.label_info_chunkSize.TabIndex = 14;
			this.label_info_chunkSize.Text = "Chunk S.";
			// 
			// numericUpDown_overlap
			// 
			this.numericUpDown_overlap.DecimalPlaces = 3;
			this.numericUpDown_overlap.Location = new Point(72, 113);
			this.numericUpDown_overlap.Maximum = new decimal(new int[] { 95, 0, 0, 131072 });
			this.numericUpDown_overlap.Name = "numericUpDown_overlap";
			this.numericUpDown_overlap.Size = new Size(60, 23);
			this.numericUpDown_overlap.TabIndex = 13;
			this.numericUpDown_overlap.Tag = "";
			this.numericUpDown_overlap.Value = new decimal(new int[] { 5, 0, 0, 65536 });
			this.numericUpDown_overlap.ValueChanged += this.numericUpDown_overlap_ValueChanged;
			// 
			// numericUpDown_chunkSize
			// 
			this.numericUpDown_chunkSize.Location = new Point(6, 113);
			this.numericUpDown_chunkSize.Maximum = new decimal(new int[] { 65536, 0, 0, 0 });
			this.numericUpDown_chunkSize.Minimum = new decimal(new int[] { 128, 0, 0, 0 });
			this.numericUpDown_chunkSize.Name = "numericUpDown_chunkSize";
			this.numericUpDown_chunkSize.Size = new Size(60, 23);
			this.numericUpDown_chunkSize.TabIndex = 12;
			this.numericUpDown_chunkSize.Tag = "2048";
			this.numericUpDown_chunkSize.Value = new decimal(new int[] { 2048, 0, 0, 0 });
			this.numericUpDown_chunkSize.ValueChanged += this.numericUpDown_chunkSize_ValueChanged;
			// 
			// button_stretch
			// 
			this.button_stretch.Location = new Point(274, 157);
			this.button_stretch.Name = "button_stretch";
			this.button_stretch.Size = new Size(40, 23);
			this.button_stretch.TabIndex = 10;
			this.button_stretch.Text = "GO";
			this.button_stretch.UseVisualStyleBackColor = true;
			this.button_stretch.Click += this.button_stretch_Click;
			// 
			// numericUpDown_stretchFactor
			// 
			this.numericUpDown_stretchFactor.DecimalPlaces = 15;
			this.numericUpDown_stretchFactor.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
			this.numericUpDown_stretchFactor.Location = new Point(138, 157);
			this.numericUpDown_stretchFactor.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			this.numericUpDown_stretchFactor.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
			this.numericUpDown_stretchFactor.Name = "numericUpDown_stretchFactor";
			this.numericUpDown_stretchFactor.Size = new Size(130, 23);
			this.numericUpDown_stretchFactor.TabIndex = 11;
			this.numericUpDown_stretchFactor.Value = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown_stretchFactor.ValueChanged += this.numericUpDown_stretchFactor_ValueChanged;
			// 
			// numericUpDown_targetBpm
			// 
			this.numericUpDown_targetBpm.DecimalPlaces = 3;
			this.numericUpDown_targetBpm.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
			this.numericUpDown_targetBpm.Location = new Point(72, 157);
			this.numericUpDown_targetBpm.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
			this.numericUpDown_targetBpm.Minimum = new decimal(new int[] { 40, 0, 0, 0 });
			this.numericUpDown_targetBpm.Name = "numericUpDown_targetBpm";
			this.numericUpDown_targetBpm.Size = new Size(60, 23);
			this.numericUpDown_targetBpm.TabIndex = 10;
			this.numericUpDown_targetBpm.Value = new decimal(new int[] { 150, 0, 0, 0 });
			this.numericUpDown_targetBpm.ValueChanged += this.numericUpDown_targetBpm_ValueChanged;
			// 
			// numericUpDown_initialBpm
			// 
			this.numericUpDown_initialBpm.DecimalPlaces = 3;
			this.numericUpDown_initialBpm.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
			this.numericUpDown_initialBpm.Location = new Point(6, 157);
			this.numericUpDown_initialBpm.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
			this.numericUpDown_initialBpm.Minimum = new decimal(new int[] { 40, 0, 0, 0 });
			this.numericUpDown_initialBpm.Name = "numericUpDown_initialBpm";
			this.numericUpDown_initialBpm.Size = new Size(60, 23);
			this.numericUpDown_initialBpm.TabIndex = 0;
			this.numericUpDown_initialBpm.Value = new decimal(new int[] { 150, 0, 0, 0 });
			this.numericUpDown_initialBpm.ValueChanged += this.numericUpDown_initialBpm_ValueChanged;
			// 
			// vScrollBar_volume
			// 
			this.vScrollBar_volume.Location = new Point(678, 581);
			this.vScrollBar_volume.Name = "vScrollBar_volume";
			this.vScrollBar_volume.Size = new Size(17, 88);
			this.vScrollBar_volume.TabIndex = 10;
			this.vScrollBar_volume.Scroll += this.vScrollBar_volume_Scroll;
			// 
			// button_export
			// 
			this.button_export.Location = new Point(617, 322);
			this.button_export.Name = "button_export";
			this.button_export.Size = new Size(75, 23);
			this.button_export.TabIndex = 11;
			this.button_export.Text = "Export";
			this.button_export.UseVisualStyleBackColor = true;
			this.button_export.Click += this.button_export_Click;
			// 
			// progressBar_processing
			// 
			this.progressBar_processing.Location = new Point(12, 248);
			this.progressBar_processing.Name = "progressBar_processing";
			this.progressBar_processing.Size = new Size(320, 15);
			this.progressBar_processing.TabIndex = 12;
			// 
			// textBox_metrics
			// 
			this.textBox_metrics.Location = new Point(512, 12);
			this.textBox_metrics.MaxLength = 6536;
			this.textBox_metrics.Multiline = true;
			this.textBox_metrics.Name = "textBox_metrics";
			this.textBox_metrics.ScrollBars = ScrollBars.Vertical;
			this.textBox_metrics.Size = new Size(180, 230);
			this.textBox_metrics.TabIndex = 13;
			// 
			// WindowMain
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(704, 681);
			this.Controls.Add(this.textBox_metrics);
			this.Controls.Add(this.progressBar_processing);
			this.Controls.Add(this.button_export);
			this.Controls.Add(this.vScrollBar_volume);
			this.Controls.Add(this.groupBox_processing);
			this.Controls.Add(this.label_pointer);
			this.Controls.Add(this.numericUpDown_zoom);
			this.Controls.Add(this.textBox_timeStamp);
			this.Controls.Add(this.button_reset);
			this.Controls.Add(this.button_playStop);
			this.Controls.Add(this.button_import);
			this.Controls.Add(this.listBox_tracks);
			this.Controls.Add(this.pictureBox_waveform);
			this.Controls.Add(this.comboBox_devices);
			this.MaximizeBox = false;
			this.MaximumSize = new Size(720, 720);
			this.MinimumSize = new Size(720, 720);
			this.Name = "WindowMain";
			this.Text = "CudaAudio (Forms)";
			((System.ComponentModel.ISupportInitialize) this.pictureBox_waveform).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_zoom).EndInit();
			this.groupBox_processing.ResumeLayout(false);
			this.groupBox_processing.PerformLayout();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_overlap).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_chunkSize).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_stretchFactor).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_targetBpm).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_initialBpm).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private ComboBox comboBox_devices;
		private PictureBox pictureBox_waveform;
		private ListBox listBox_tracks;
		private Button button_import;
		private Button button_playStop;
		private Button button_reset;
		private TextBox textBox_timeStamp;
		private NumericUpDown numericUpDown_zoom;
		private Label label_pointer;
		private GroupBox groupBox_processing;
		private Button button_stretch;
		private NumericUpDown numericUpDown_stretchFactor;
		private NumericUpDown numericUpDown_targetBpm;
		private NumericUpDown numericUpDown_initialBpm;
		private VScrollBar vScrollBar_volume;
		private NumericUpDown numericUpDown_chunkSize;
		private NumericUpDown numericUpDown_overlap;
		private Label label_info_chunkSize;
		private Label label_info_stretchFactor;
		private Label label_info_targetBpm;
		private Label label_info_initialBpm;
		private Label label_info_overlap;
		private Button button_fourier;
		private Button button_export;
		private ProgressBar progressBar_processing;
		private TextBox textBox_metrics;
	}
}
