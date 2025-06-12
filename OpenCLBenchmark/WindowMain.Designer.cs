namespace OpenCLBenchmark
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
			this.button_info = new Button();
			this.listBox_log = new ListBox();
			this.listBox_pointers = new ListBox();
			this.listBox_images = new ListBox();
			this.panel_view = new Panel();
			this.pictureBox_view = new PictureBox();
			this.button_import = new Button();
			this.button_export = new Button();
			this.button_reset = new Button();
			this.button_move = new Button();
			this.numericUpDown_zoom = new NumericUpDown();
			this.label_meta = new Label();
			this.checkBox_log = new CheckBox();
			this.comboBox_kernelNames = new ComboBox();
			this.groupBox_kernels = new GroupBox();
			this.button_kernelLoad = new Button();
			this.checkBox_newImage = new CheckBox();
			this.button_execute = new Button();
			this.checkBox_invariables = new CheckBox();
			this.panel_kernelArguments = new Panel();
			this.comboBox_kernelVersions = new ComboBox();
			this.groupBox_benchmark = new GroupBox();
			this.button_makeGif = new Button();
			this.button_benchmarkColor = new Button();
			this.label2 = new Label();
			this.numericUpDown_benchmarkX = new NumericUpDown();
			this.label1 = new Label();
			this.numericUpDown_benchmarkY = new NumericUpDown();
			this.label_info_frames = new Label();
			this.numericUpDown_benchmarkFrames = new NumericUpDown();
			this.button_runBenchmark = new Button();
			this.progressBar_benchmark = new ProgressBar();
			this.numericUpDown_gifFps = new NumericUpDown();
			this.panel_view.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.pictureBox_view).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_zoom).BeginInit();
			this.groupBox_kernels.SuspendLayout();
			this.groupBox_benchmark.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkX).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkY).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkFrames).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_gifFps).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox_devices
			// 
			this.comboBox_devices.FormattingEnabled = true;
			this.comboBox_devices.Location = new Point(12, 12);
			this.comboBox_devices.Name = "comboBox_devices";
			this.comboBox_devices.Size = new Size(400, 23);
			this.comboBox_devices.TabIndex = 0;
			this.comboBox_devices.Text = "Select OpenCL-Device to initialize ...";
			// 
			// button_info
			// 
			this.button_info.Location = new Point(418, 12);
			this.button_info.Name = "button_info";
			this.button_info.Size = new Size(23, 23);
			this.button_info.TabIndex = 1;
			this.button_info.Text = "i";
			this.button_info.UseVisualStyleBackColor = true;
			this.button_info.Click += this.button_info_Click;
			// 
			// listBox_log
			// 
			this.listBox_log.FormattingEnabled = true;
			this.listBox_log.ItemHeight = 15;
			this.listBox_log.Location = new Point(12, 500);
			this.listBox_log.Name = "listBox_log";
			this.listBox_log.Size = new Size(1000, 169);
			this.listBox_log.TabIndex = 2;
			// 
			// listBox_pointers
			// 
			this.listBox_pointers.FormattingEnabled = true;
			this.listBox_pointers.ItemHeight = 15;
			this.listBox_pointers.Location = new Point(506, 323);
			this.listBox_pointers.Name = "listBox_pointers";
			this.listBox_pointers.Size = new Size(180, 169);
			this.listBox_pointers.TabIndex = 3;
			// 
			// listBox_images
			// 
			this.listBox_images.FormattingEnabled = true;
			this.listBox_images.ItemHeight = 15;
			this.listBox_images.Location = new Point(1172, 500);
			this.listBox_images.Name = "listBox_images";
			this.listBox_images.Size = new Size(240, 169);
			this.listBox_images.TabIndex = 4;
			// 
			// panel_view
			// 
			this.panel_view.Controls.Add(this.pictureBox_view);
			this.panel_view.Location = new Point(692, 12);
			this.panel_view.Name = "panel_view";
			this.panel_view.Size = new Size(720, 480);
			this.panel_view.TabIndex = 5;
			// 
			// pictureBox_view
			// 
			this.pictureBox_view.BackColor = Color.Black;
			this.pictureBox_view.Location = new Point(3, 3);
			this.pictureBox_view.Name = "pictureBox_view";
			this.pictureBox_view.Size = new Size(714, 474);
			this.pictureBox_view.TabIndex = 0;
			this.pictureBox_view.TabStop = false;
			// 
			// button_import
			// 
			this.button_import.Location = new Point(1106, 500);
			this.button_import.Name = "button_import";
			this.button_import.Size = new Size(60, 23);
			this.button_import.TabIndex = 6;
			this.button_import.Text = "Import";
			this.button_import.UseVisualStyleBackColor = true;
			this.button_import.Click += this.button_import_Click;
			// 
			// button_export
			// 
			this.button_export.Location = new Point(1106, 529);
			this.button_export.Name = "button_export";
			this.button_export.Size = new Size(60, 23);
			this.button_export.TabIndex = 7;
			this.button_export.Text = "Export";
			this.button_export.UseVisualStyleBackColor = true;
			this.button_export.Click += this.button_export_Click;
			// 
			// button_reset
			// 
			this.button_reset.Location = new Point(1106, 646);
			this.button_reset.Name = "button_reset";
			this.button_reset.Size = new Size(60, 23);
			this.button_reset.TabIndex = 8;
			this.button_reset.Text = "Reset";
			this.button_reset.UseVisualStyleBackColor = true;
			this.button_reset.Click += this.button_reset_Click;
			// 
			// button_move
			// 
			this.button_move.Location = new Point(1106, 586);
			this.button_move.Name = "button_move";
			this.button_move.Size = new Size(60, 23);
			this.button_move.TabIndex = 9;
			this.button_move.Text = "Move";
			this.button_move.UseVisualStyleBackColor = true;
			this.button_move.Click += this.button_move_Click;
			// 
			// numericUpDown_zoom
			// 
			this.numericUpDown_zoom.Location = new Point(606, 12);
			this.numericUpDown_zoom.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
			this.numericUpDown_zoom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown_zoom.Name = "numericUpDown_zoom";
			this.numericUpDown_zoom.Size = new Size(80, 23);
			this.numericUpDown_zoom.TabIndex = 10;
			this.numericUpDown_zoom.Value = new decimal(new int[] { 100, 0, 0, 0 });
			// 
			// label_meta
			// 
			this.label_meta.AutoSize = true;
			this.label_meta.Location = new Point(12, 482);
			this.label_meta.Name = "label_meta";
			this.label_meta.Size = new Size(167, 15);
			this.label_meta.TabIndex = 11;
			this.label_meta.Text = "No image meta data available.";
			// 
			// checkBox_log
			// 
			this.checkBox_log.AutoSize = true;
			this.checkBox_log.Checked = true;
			this.checkBox_log.CheckState = CheckState.Checked;
			this.checkBox_log.Location = new Point(549, 14);
			this.checkBox_log.Name = "checkBox_log";
			this.checkBox_log.Size = new Size(51, 19);
			this.checkBox_log.TabIndex = 12;
			this.checkBox_log.Text = "Log?";
			this.checkBox_log.UseVisualStyleBackColor = true;
			// 
			// comboBox_kernelNames
			// 
			this.comboBox_kernelNames.FormattingEnabled = true;
			this.comboBox_kernelNames.Location = new Point(6, 22);
			this.comboBox_kernelNames.Name = "comboBox_kernelNames";
			this.comboBox_kernelNames.Size = new Size(346, 23);
			this.comboBox_kernelNames.TabIndex = 13;
			this.comboBox_kernelNames.Text = "Select kernel base name ...";
			this.comboBox_kernelNames.SelectedIndexChanged += this.comboBox_kernelNames_SelectedIndexChanged;
			// 
			// groupBox_kernels
			// 
			this.groupBox_kernels.Controls.Add(this.button_kernelLoad);
			this.groupBox_kernels.Controls.Add(this.checkBox_newImage);
			this.groupBox_kernels.Controls.Add(this.button_execute);
			this.groupBox_kernels.Controls.Add(this.checkBox_invariables);
			this.groupBox_kernels.Controls.Add(this.panel_kernelArguments);
			this.groupBox_kernels.Controls.Add(this.comboBox_kernelVersions);
			this.groupBox_kernels.Controls.Add(this.comboBox_kernelNames);
			this.groupBox_kernels.Location = new Point(12, 41);
			this.groupBox_kernels.Name = "groupBox_kernels";
			this.groupBox_kernels.Size = new Size(429, 438);
			this.groupBox_kernels.TabIndex = 14;
			this.groupBox_kernels.TabStop = false;
			this.groupBox_kernels.Text = "OpenCL imaging kernels";
			// 
			// button_kernelLoad
			// 
			this.button_kernelLoad.Location = new Point(358, 51);
			this.button_kernelLoad.Name = "button_kernelLoad";
			this.button_kernelLoad.Size = new Size(65, 23);
			this.button_kernelLoad.TabIndex = 19;
			this.button_kernelLoad.Text = "Reload";
			this.button_kernelLoad.UseVisualStyleBackColor = true;
			this.button_kernelLoad.Click += this.button_kernelLoad_Click;
			// 
			// checkBox_newImage
			// 
			this.checkBox_newImage.AutoSize = true;
			this.checkBox_newImage.Checked = true;
			this.checkBox_newImage.CheckState = CheckState.Checked;
			this.checkBox_newImage.Location = new Point(261, 412);
			this.checkBox_newImage.Name = "checkBox_newImage";
			this.checkBox_newImage.Size = new Size(91, 19);
			this.checkBox_newImage.TabIndex = 18;
			this.checkBox_newImage.Text = "New image?\r\n";
			this.checkBox_newImage.UseVisualStyleBackColor = true;
			// 
			// button_execute
			// 
			this.button_execute.Location = new Point(358, 409);
			this.button_execute.Name = "button_execute";
			this.button_execute.Size = new Size(65, 23);
			this.button_execute.TabIndex = 17;
			this.button_execute.Text = "Execute";
			this.button_execute.UseVisualStyleBackColor = true;
			this.button_execute.Click += this.button_execute_Click;
			// 
			// checkBox_invariables
			// 
			this.checkBox_invariables.AutoSize = true;
			this.checkBox_invariables.Location = new Point(6, 413);
			this.checkBox_invariables.Name = "checkBox_invariables";
			this.checkBox_invariables.Size = new Size(119, 19);
			this.checkBox_invariables.TabIndex = 16;
			this.checkBox_invariables.Text = "Show invariables?";
			this.checkBox_invariables.UseVisualStyleBackColor = true;
			this.checkBox_invariables.CheckedChanged += this.checkBox_invariables_CheckedChanged;
			// 
			// panel_kernelArguments
			// 
			this.panel_kernelArguments.Location = new Point(6, 51);
			this.panel_kernelArguments.Name = "panel_kernelArguments";
			this.panel_kernelArguments.Size = new Size(346, 340);
			this.panel_kernelArguments.TabIndex = 15;
			// 
			// comboBox_kernelVersions
			// 
			this.comboBox_kernelVersions.FormattingEnabled = true;
			this.comboBox_kernelVersions.Location = new Point(358, 22);
			this.comboBox_kernelVersions.Name = "comboBox_kernelVersions";
			this.comboBox_kernelVersions.Size = new Size(65, 23);
			this.comboBox_kernelVersions.TabIndex = 14;
			this.comboBox_kernelVersions.Text = "Version";
			this.comboBox_kernelVersions.SelectedIndexChanged += this.comboBox_kernelVersions_SelectedIndexChanged;
			// 
			// groupBox_benchmark
			// 
			this.groupBox_benchmark.Controls.Add(this.numericUpDown_gifFps);
			this.groupBox_benchmark.Controls.Add(this.button_makeGif);
			this.groupBox_benchmark.Controls.Add(this.button_benchmarkColor);
			this.groupBox_benchmark.Controls.Add(this.label2);
			this.groupBox_benchmark.Controls.Add(this.numericUpDown_benchmarkX);
			this.groupBox_benchmark.Controls.Add(this.label1);
			this.groupBox_benchmark.Controls.Add(this.numericUpDown_benchmarkY);
			this.groupBox_benchmark.Controls.Add(this.label_info_frames);
			this.groupBox_benchmark.Controls.Add(this.numericUpDown_benchmarkFrames);
			this.groupBox_benchmark.Controls.Add(this.button_runBenchmark);
			this.groupBox_benchmark.Location = new Point(506, 41);
			this.groupBox_benchmark.Name = "groupBox_benchmark";
			this.groupBox_benchmark.Size = new Size(183, 249);
			this.groupBox_benchmark.TabIndex = 15;
			this.groupBox_benchmark.TabStop = false;
			this.groupBox_benchmark.Text = "Benchmark";
			// 
			// button_makeGif
			// 
			this.button_makeGif.Location = new Point(122, 83);
			this.button_makeGif.Name = "button_makeGif";
			this.button_makeGif.Size = new Size(55, 23);
			this.button_makeGif.TabIndex = 18;
			this.button_makeGif.Text = "-> GIF";
			this.button_makeGif.UseVisualStyleBackColor = true;
			this.button_makeGif.Click += this.button_makeGif_Click;
			// 
			// button_benchmarkColor
			// 
			this.button_benchmarkColor.BackColor = Color.Black;
			this.button_benchmarkColor.ForeColor = Color.White;
			this.button_benchmarkColor.Location = new Point(154, 22);
			this.button_benchmarkColor.Name = "button_benchmarkColor";
			this.button_benchmarkColor.Size = new Size(23, 23);
			this.button_benchmarkColor.TabIndex = 17;
			this.button_benchmarkColor.Text = "C";
			this.button_benchmarkColor.UseVisualStyleBackColor = false;
			this.button_benchmarkColor.Click += this.button_benchmarkColor_Click;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 114);
			this.label2.Name = "label2";
			this.label2.Size = new Size(83, 15);
			this.label2.TabIndex = 16;
			this.label2.Text = "X-Coordinates";
			// 
			// numericUpDown_benchmarkX
			// 
			this.numericUpDown_benchmarkX.DecimalPlaces = 15;
			this.numericUpDown_benchmarkX.Location = new Point(6, 132);
			this.numericUpDown_benchmarkX.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
			this.numericUpDown_benchmarkX.Name = "numericUpDown_benchmarkX";
			this.numericUpDown_benchmarkX.Size = new Size(171, 23);
			this.numericUpDown_benchmarkX.TabIndex = 5;
			this.numericUpDown_benchmarkX.Value = new decimal(new int[] { 18335, 0, 0, -2147155968 });
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 158);
			this.label1.Name = "label1";
			this.label1.Size = new Size(83, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Y-Coordinates";
			// 
			// numericUpDown_benchmarkY
			// 
			this.numericUpDown_benchmarkY.DecimalPlaces = 15;
			this.numericUpDown_benchmarkY.Location = new Point(6, 176);
			this.numericUpDown_benchmarkY.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
			this.numericUpDown_benchmarkY.Name = "numericUpDown_benchmarkY";
			this.numericUpDown_benchmarkY.Size = new Size(171, 23);
			this.numericUpDown_benchmarkY.TabIndex = 3;
			this.numericUpDown_benchmarkY.Value = new decimal(new int[] { 64939, 0, 0, -2147155968 });
			// 
			// label_info_frames
			// 
			this.label_info_frames.AutoSize = true;
			this.label_info_frames.Location = new Point(6, 202);
			this.label_info_frames.Name = "label_info_frames";
			this.label_info_frames.Size = new Size(45, 15);
			this.label_info_frames.TabIndex = 2;
			this.label_info_frames.Text = "Frames";
			// 
			// numericUpDown_benchmarkFrames
			// 
			this.numericUpDown_benchmarkFrames.Location = new Point(6, 220);
			this.numericUpDown_benchmarkFrames.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
			this.numericUpDown_benchmarkFrames.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown_benchmarkFrames.Name = "numericUpDown_benchmarkFrames";
			this.numericUpDown_benchmarkFrames.Size = new Size(65, 23);
			this.numericUpDown_benchmarkFrames.TabIndex = 1;
			this.numericUpDown_benchmarkFrames.Value = new decimal(new int[] { 100, 0, 0, 0 });
			// 
			// button_runBenchmark
			// 
			this.button_runBenchmark.Location = new Point(127, 220);
			this.button_runBenchmark.Name = "button_runBenchmark";
			this.button_runBenchmark.Size = new Size(50, 23);
			this.button_runBenchmark.TabIndex = 0;
			this.button_runBenchmark.Text = "Run";
			this.button_runBenchmark.UseVisualStyleBackColor = true;
			this.button_runBenchmark.Click += this.button_runBenchmark_Click;
			// 
			// progressBar_benchmark
			// 
			this.progressBar_benchmark.Location = new Point(506, 296);
			this.progressBar_benchmark.Name = "progressBar_benchmark";
			this.progressBar_benchmark.Size = new Size(180, 21);
			this.progressBar_benchmark.TabIndex = 16;
			// 
			// numericUpDown_gifFps
			// 
			this.numericUpDown_gifFps.Location = new Point(6, 83);
			this.numericUpDown_gifFps.Maximum = new decimal(new int[] { 144, 0, 0, 0 });
			this.numericUpDown_gifFps.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown_gifFps.Name = "numericUpDown_gifFps";
			this.numericUpDown_gifFps.Size = new Size(65, 23);
			this.numericUpDown_gifFps.TabIndex = 19;
			this.numericUpDown_gifFps.Value = new decimal(new int[] { 20, 0, 0, 0 });
			// 
			// WindowMain
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(1424, 681);
			this.Controls.Add(this.progressBar_benchmark);
			this.Controls.Add(this.groupBox_benchmark);
			this.Controls.Add(this.groupBox_kernels);
			this.Controls.Add(this.checkBox_log);
			this.Controls.Add(this.label_meta);
			this.Controls.Add(this.numericUpDown_zoom);
			this.Controls.Add(this.button_move);
			this.Controls.Add(this.button_reset);
			this.Controls.Add(this.button_export);
			this.Controls.Add(this.button_import);
			this.Controls.Add(this.panel_view);
			this.Controls.Add(this.listBox_images);
			this.Controls.Add(this.listBox_pointers);
			this.Controls.Add(this.listBox_log);
			this.Controls.Add(this.button_info);
			this.Controls.Add(this.comboBox_devices);
			this.MaximumSize = new Size(1440, 720);
			this.MinimumSize = new Size(1440, 720);
			this.Name = "WindowMain";
			this.Text = "OpenCL Benchmark";
			this.panel_view.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.pictureBox_view).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_zoom).EndInit();
			this.groupBox_kernels.ResumeLayout(false);
			this.groupBox_kernels.PerformLayout();
			this.groupBox_benchmark.ResumeLayout(false);
			this.groupBox_benchmark.PerformLayout();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkX).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkY).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_benchmarkFrames).EndInit();
			((System.ComponentModel.ISupportInitialize) this.numericUpDown_gifFps).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private ComboBox comboBox_devices;
		private Button button_info;
		private ListBox listBox_log;
		private ListBox listBox_pointers;
		private ListBox listBox_images;
		private Panel panel_view;
		private Button button_import;
		private Button button_export;
		private Button button_reset;
		private Button button_move;
		private NumericUpDown numericUpDown_zoom;
		private PictureBox pictureBox_view;
		private Label label_meta;
		private CheckBox checkBox_log;
		private ComboBox comboBox_kernelNames;
		private GroupBox groupBox_kernels;
		private CheckBox checkBox_newImage;
		private Button button_execute;
		private CheckBox checkBox_invariables;
		private Panel panel_kernelArguments;
		private ComboBox comboBox_kernelVersions;
		private Button button_kernelLoad;
        private GroupBox groupBox_benchmark;
        private Button button_runBenchmark;
        private Label label_info_frames;
        private NumericUpDown numericUpDown_benchmarkFrames;
        private Label label2;
        private NumericUpDown numericUpDown_benchmarkX;
        private Label label1;
        private NumericUpDown numericUpDown_benchmarkY;
        private Button button_benchmarkColor;
        private ProgressBar progressBar_benchmark;
		private Button button_makeGif;
		private NumericUpDown numericUpDown_gifFps;
	}
}
