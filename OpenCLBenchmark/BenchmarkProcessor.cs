using Accessibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace OpenCLBenchmark
{
    public class BenchmarkProcessor
	{
		// ----- ----- ----- ATTRIBUTES ----- ----- ----- \\
		// Rename the property to avoid ambiguity
		public string RepoPath => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

		// Rename the property to avoid ambiguity
		public ImageHandling ImageHandler { get; private set; }
		public OpenClService OpenClService { get; private set; }

		// ----- ----- ----- CONSTRUCTORS ----- ----- ----- \\
		public BenchmarkProcessor(ImageHandling imageHandling, OpenClService service)
		{
			// Set attributes
			this.ImageHandler = imageHandling ?? throw new ArgumentNullException(nameof(imageHandling), @"ImageHandling cannot be null.");
			this.OpenClService = service ?? throw new ArgumentNullException(nameof(service), @"OpenClService cannot be null.");
		}

		// ----- ----- ----- METHODS ----- ----- ----- \\
		public async Task<List<long>> ProcessAsync(string kernelName = "mandelbrotPrecise01", int count = 100, double xCoord = 0.0, double yCoord = 0.0, double zoomInc = 1.05, int iterCoeff = 16, Color? col = null, bool outputImages = false, ProgressBar? pBar = null, bool log = false)
		{
			// Verify params
			col ??= Color.Black;
			pBar ??= new ProgressBar();
			pBar.Maximum = count;
			pBar.Value = 0;

			// Original empty images
			List<ImageObject> objects = new();
			List<Task<(long time, IntPtr outputPointer, ImageObject image)>> tasks = [];

			for (int i = 0; i < count; i++)
			{
				Bitmap bmp = new(1920, 1080);
				objects.Add(new ImageObject(bmp, "OriginalEmpty_" + i.ToString("00000")));
			}

			// Loop exec
			for (int i = 0; i < count; i++)
			{
				ImageObject currentImageObject = objects[i];

				object[] variableArgs =
				{
									0, 0, 0, 0,
									zoomInc * (i + 1),
									xCoord,
									yCoord,
									iterCoeff,
									(int)col.Value.R,
									(int)col.Value.G,
									(int)col.Value.B
								};

				tasks.Add(Task.Run(async () =>
				{
					Stopwatch sw = Stopwatch.StartNew();
					// Rufe die asynchrone Kernel-Ausführung auf.
					// 'ExecuteImageKernelAsync' erhält 'currentImageObject' als Parameter und modifiziert dessen Zustand (z.B. obj.Pointer).
					// Der Rückgabewert (hier 'processedImageObject') ist die modifizierte Instanz.
					ImageObject processedImageObject = await this.OpenClService.ExecuteImageKernelAsync(currentImageObject, kernelName, "", variableArgs, log);

					// Der Output-Pointer kann aus dem bearbeiteten Objekt entnommen werden.
					IntPtr outputPointer = processedImageObject.Pointer;

					sw.Stop();
					// Gebe die gemessene Zeit, den Output-Pointer und die bearbeitete ImageObject-Instanz zurück.
					// Die 'objects'-Liste wird *nicht* direkt in dieser Lambda-Expression modifiziert.
					return (sw.ElapsedMilliseconds, outputPointer, processedImageObject);
				}));
			}

			List<long> times = [];
			foreach (Task<(Int64 time, nint outputPointer, ImageObject image)> task in tasks)
			{
				(Int64 time, nint outputPointer, ImageObject image) result = await task;
				times.Add(result.time);
				pBar.Increment(1);
			}

			pBar.Value = 0;

			if (outputImages)
			{
				this.ImageHandler.Images.AddRange(objects);
				this.ImageHandler.RefreshView();
			}

			return times;
		}
	}
}
