using System.Diagnostics;

namespace OpenCLBenchmark
{
	public partial class WindowMain : Form
	{
		// ----- ----- ----- ATTRIBUTES ----- ----- ----- \\
		public string Repopath => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));


		public ImageHandling ImgH { get; private set; }
		public OpenClService Service { get; private set; }



		// ----- ----- ----- CONSTRUCTORS ----- ----- ----- \\
		public WindowMain()
		{
			this.InitializeComponent();

			// Window position
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(0, 0);

			// Init. ImgH & Service
			this.ImgH = new ImageHandling(this.Repopath, this.listBox_images, this.pictureBox_view, this.numericUpDown_zoom, this.label_meta);
			this.Service = new OpenClService(this.Repopath, this.listBox_log, this.comboBox_devices, this.listBox_pointers);
			this.Service.SelectDeviceLike("Intel");

			// Fill kernels etc.
			this.FillKernels();
		}






		// ----- ----- ----- METHODS ----- ----- ----- \\
		public void FillKernels()
		{
			// Fill kernel base names in comboBox
			this.Service.FillKernelBaseNamesCombobox(this.comboBox_kernelNames);
		}

		public void LoadKernel()
		{
			string baseName = this.comboBox_kernelNames.SelectedItem?.ToString() ?? "NONE";
			string version = this.comboBox_kernelVersions.SelectedItem?.ToString() ?? "00";

			// Load kernel
			this.Service.KernelCompiler?.LoadKernel(baseName + version, "", this.panel_kernelArguments, this.checkBox_invariables.Checked);
		}

		public void ExecuteKernel()
		{
			// Check initialized
			if (this.Service.KernelCompiler == null)
			{
				MessageBox.Show(@"Kernel compiler is not initialized.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Create new obj if checkbox is checked
			ImageObject? obj = null;
			if (this.checkBox_newImage.Checked)
			{
				string name = this.ImgH.CurrentObject?.Name ?? "KernelImage";
				int width = this.pictureBox_view.Width;
				int height = this.pictureBox_view.Height;
				int index = this.listBox_images.Items.Count + 1; // Start from 1 for new images

				Bitmap image = this.ImgH.CurrentObject?.Img != null
					? new Bitmap(this.ImgH.CurrentObject.Img)
					: new Bitmap(width, height); // Create a new bitmap if no current image
				obj = new ImageObject(image, name + "_" + index.ToString("000"));
				this.ImgH.Images.Add(obj);
			}
			else
			{
				obj = this.ImgH.CurrentObject;
			}

			// Check if obj is null
			if (obj == null)
			{
				MessageBox.Show(@"No image selected or created.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Get kernel base name and version
			string baseName = this.comboBox_kernelNames.SelectedItem?.ToString() ?? "NONE";
			string version = this.comboBox_kernelVersions.SelectedItem?.ToString() ?? "00";


			// Call execute method
			this.Service.ExecuteImageKernel(obj, baseName, version, null, this.checkBox_log.Checked);

			// Refresh view
			this.ImgH.RefreshView();
		}




		// ----- ----- ----- EVENTS ----- ----- ----- \\
		private void button_info_Click(object sender, EventArgs e)
		{
			// If CTRL down, get platform info
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				this.Service.GetFullPlatformInfo(null, false, true);
			}
			else
			{
				this.Service.GetFullDeviceInfo(null, false, true);
			}
		}

		private void button_import_Click(object sender, EventArgs e)
		{
			this.ImgH.ImportImage();
		}

		private void button_export_Click(object sender, EventArgs e)
		{
			this.ImgH.CurrentObject?.Export();
		}

		private void button_move_Click(object sender, EventArgs e)
		{
			// Check obj
			if (this.ImgH.CurrentObject == null)
			{
				MessageBox.Show(@"No image selected.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Check initialized
			if (this.Service.MemorRegister == null)
			{
				MessageBox.Show(@"OpenCL service is not initialized.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.Service.MoveImage(this.ImgH.CurrentObject, this.checkBox_log.Checked);

			this.ImgH.RefreshView();
		}

		private void button_reset_Click(object sender, EventArgs e)
		{
			this.ImgH.CurrentObject?.ResetImage();
		}

		private void button_execute_Click(object sender, EventArgs e)
		{
			this.ExecuteKernel();
		}

		private void checkBox_invariables_CheckedChanged(object sender, EventArgs e)
		{
			this.LoadKernel();
		}

		private void comboBox_kernelNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedKernelBaseName = this.comboBox_kernelNames.SelectedItem?.ToString() ?? "NONE";
			this.Service.FillKernelVersionsCombobox(this.comboBox_kernelVersions, selectedKernelBaseName);
		}

		private void comboBox_kernelVersions_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.LoadKernel();
		}

		private void button_kernelLoad_Click(object sender, EventArgs e)
		{
			this.LoadKernel();
		}

		private async void button_runBenchmark_Click(object sender, EventArgs e)
		{
			BenchmarkProcessor processor = new(this.ImgH, this.Service);

			string kernelName = "mandelbrotPrecise01";
			Color col = this.button_benchmarkColor.BackColor;
			int framesCount = (int) this.numericUpDown_benchmarkFrames.Value;
			double xCoord = (double) this.numericUpDown_benchmarkX.Value;
			double yCoord = (double) this.numericUpDown_benchmarkY.Value;
			int iterCoeff = 256;

			// Stopwatch for timing
			Stopwatch stopwatch = Stopwatch.StartNew();

			List<long> times = await processor.ProcessAsync(kernelName, framesCount, xCoord, yCoord, 1.1D, iterCoeff, col, true, this.progressBar_benchmark, this.checkBox_log.Checked);
			long totalTime = times.Sum();
			double medianTime = times.Average();

			stopwatch.Stop();

			MessageBox.Show(@"Total: " + stopwatch.ElapsedMilliseconds + " ms\n\n" + @"Median: " + (stopwatch.ElapsedMilliseconds / framesCount) + " ms", $@"Benchmark finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button_benchmarkColor_Click(object sender, EventArgs e)
		{
			// Color pick dialog
			using ColorDialog colorDialog = new();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				// Ausgewählte Farbe
				Color selectedColor = colorDialog.Color;

				// Hintergrundfarbe des Buttons ändern
				Button? button = sender as Button;
				if (button != null)
				{
					button.BackColor = selectedColor;

					// Überprüfen, ob die Farbe zu dunkel ist
					if (selectedColor.GetBrightness() < 0.5f)
					{
						button.ForeColor = Color.White;
					}
					else
					{
						button.ForeColor = Color.Black;
					}
				}
			}
		}

		private async void button_makeGif_Click(object sender, EventArgs e)
		{
			string outPath = Path.Combine(this.Repopath, "Images", "OutputGif");
			int delay = (int) (1000 / this.numericUpDown_gifFps.Value);

			await GifMaker.CreateGifFromImagesAsync(this.ImgH.Images, outPath, delay, this.progressBar_benchmark);
		}
	}
}
