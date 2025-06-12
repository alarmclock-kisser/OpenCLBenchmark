using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;

namespace OpenCLBenchmark
{
	public static class GifMaker
	{
		public static async Task CreateGifFromImagesAsync(List<ImageObject> images, string outputPath, int delay = 100, System.Windows.Forms.ProgressBar? pBar = null)
		{
			if (images == null || images.Count == 0)
			{
				Console.WriteLine("No images provided to create GIF.");
				return;
			}

			// Sicherstellen, dass das Ausgabeverzeichnis existiert
			Directory.CreateDirectory(outputPath);

			// Dateiname basiert auf dem Namen des ersten Bildes
			string gifFileName = Path.Combine(outputPath, (images.First().Name + ".gif").Replace(" ", "_"));

			// ProgressBar initialisieren und auf dem UI-Thread aktualisieren (thread-safe)
			if (pBar != null)
			{
				if (pBar.InvokeRequired)
				{
					pBar.Invoke(new Action(() => { pBar.Maximum = images.Count; pBar.Value = 0; }));
				}
				else
				{
					pBar.Maximum = images.Count;
					pBar.Value = 0;
				}
			}

			// Den gesamten GIF-Erstellungsprozess in einen Threadpool-Thread verlagern
			await Task.Run(() =>
			{
				// Erstelle ein neues ImageSharp-Bild. Die Dimensionen werden die des ersten Bildes sein.
				// Das hier erzeugte 'gif'-Objekt wird die Sammlung von Frames enthalten.
				using (Image<Rgba32> gif = new(images.First().Width, images.First().Height))
				{
					// WICHTIGE KORREKTUR:
					// Anstatt gif.Frames.RemoveFrame(0) aufzurufen (was einen Fehler wirft, da es der letzte Frame ist),
					// werden wir den RootFrame, der durch den Konstruktor erstellt wurde, später ERSETZEN.
					// Er bleibt vorerst bestehen und wird mit dem ersten echten Frame überschrieben.

					int frameIndex = 0;
					foreach (ImageObject imgObject in images)
					{
						using (MemoryStream ms = new())
						{
							// Konvertiere System.Drawing.Bitmap zu MemoryStream (z.B. als PNG)
							// Sicherstellen, dass Img nicht null ist
							imgObject.Img?.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
							ms.Position = 0; // Stream-Position zurücksetzen, um vom Anfang zu lesen

							// Lade das Bild in das SixLabors.ImageSharp-Format
							// Das 'using' hier ist wichtig, um die Ressourcen von imageSharpImage zu verwalten.
							using Image<Rgba32> imageSharpImage = Image.Load<Rgba32>(ms);
							// NEUE KRITISCHE KORREKTUR: Klone das GESAMTE Image.
							// imageSharpImage.Clone() erstellt ein völlig neues Image-Objekt,
							// das unabhängig vom ursprünglichen imageSharpImage ist und seinen eigenen RootFrame enthält.
							// Dies stellt sicher, dass die Pixeldaten nicht vorzeitig entsorgt werden.
							using Image<Rgba32> clonedImage = imageSharpImage.Clone();
							// Setze Metadaten für den RootFrame des GEKLONTEN BILDES
							clonedImage.Frames.RootFrame.Metadata.GetGifMetadata().FrameDelay = delay / 10;

							// Füge den RootFrame des geklonten Bildes zum GIF hinzu.
							// Der RootFrame ist der eigentliche Frame des geklonten Bildes.
							if (frameIndex == 0)
							{
								// Ersetze den initialen (leeren) RootFrame von 'gif' durch unseren ersten echten Frame.
								// Der alte RootFrame wird dabei korrekt entsorgt.
								
														   // Alternative: gif.Frames.Clear() gefolgt von AddFrame.
								gif.Frames.AddFrame(clonedImage.Frames.RootFrame);
							}
							else
							{
								// Füge weitere Frames hinzu
								gif.Frames.AddFrame(clonedImage.Frames.RootFrame);
							}
							// clonedImage (und sein RootFrame) wird hier entsorgt,
							// aber die Frames in gif.Frames sind jetzt separate Kopien.
							// imageSharpImage (und seine ursprünglichen Frame-Daten) wird hier entsorgt.
						} // ms wird hier entsorgt.

						frameIndex++;
						// Fortschritt aktualisieren (MUSS thread-safe sein, da in Task.Run!)
						if (pBar != null)
						{
							if (pBar.InvokeRequired)
							{
								pBar.Invoke(new Action(() => pBar.Increment(1)));
							}
							else
							{
								pBar.Increment(1);
							}
						}
					}

					// Speichere das GIF. Jetzt verweisen die in 'gif' gespeicherten Frames auf gültigen, nicht entsorgten Speicher.
					gif.Save(gifFileName);
				} // 'gif' und alle seine Frames werden hier entsorgt.

				Console.WriteLine($"GIF created at: {gifFileName}");
			});

			// ProgressBar zurücksetzen, nachdem der Task abgeschlossen ist (thread-safe)
			if (pBar != null)
			{
				if (pBar.InvokeRequired)
				{
					pBar.Invoke(new Action(() => pBar.Value = 0));
				}
				else
				{
					pBar.Value = 0;
				}
			}
		}
	}
}


