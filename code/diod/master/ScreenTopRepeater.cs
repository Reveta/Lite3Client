using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Client.code.diod.master.optimal;
using Client.code.objects;

namespace Client.code.diod.master {
	public class ScreenTopRepeater : AFunctions, IComandGenerator {

		private int screenWidth;
		public ScreenTopRepeater(int screenWidth) {
			this.screenWidth = screenWidth;
			Run();
		}

		private void Run() {
			// Bitmap takeScreenShot = TakeScreenShot();
			// Console.WriteLine(takeScreenShot.Height);
			
			using var bitmap = new Bitmap(this.screenWidth, 200);
			Console.WriteLine(bitmap.Height);
			using (var g = Graphics.FromImage(bitmap)) {
				g.CopyFromScreen(0, 0, 0, 0,
					bitmap.Size, CopyPixelOperation.SourceCopy);
			}
			
			List<Diod> diodsListByScreen = GetDiodsListByScreen(bitmap);
			diodsListByScreen.ForEach(diod => base.AddDiod(diod));
		}

		private Bitmap TakeScreenShot() {
			using var bitmap = new Bitmap(this.screenWidth, 200);
			Console.WriteLine(bitmap.Height);
			using (var g = Graphics.FromImage(bitmap)) {
				g.CopyFromScreen(0, 0, 0, 0,
					bitmap.Size, CopyPixelOperation.SourceCopy);
			}
			Console.WriteLine(bitmap.Height);

			return bitmap;
		}

		private List<Diod> GetDiodsListByScreen(Bitmap bitmap) {
			var diods = new List<Diod>();
			int diodCount = this.screenWidth / Config.DiodCount;
			for (var i = 0; i < Config.DiodCount && diodCount < this.screenWidth; i++) {
				Color pixel = bitmap.GetPixel(i * diodCount, 100);
				byte pixelR = pixel.R;
				byte pixelG = pixel.G;
				byte pixelB = pixel.B;
				Console.WriteLine($"{pixelR} + {pixelG} + {pixelB} => {diodCount}");
				
				var item = new Diod(pixelR * 2, pixelG * 2, pixelB *2);
				diods.Add(item);
			}

			return diods;
		} 

		public byte[] GetCommands() => base.GetCommands(base.GetDiod());
	}
}