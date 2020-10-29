using System;
using System.Collections.Generic;

namespace Client.code.objects {
	public class Diod {
		public int Index;
		public byte R;
		public byte G;
		public byte B;

		public Diod() { }

		public Diod(int index) { this.Index = index; }

		public Diod(byte r, byte g, byte b) {
			this.R = r;
			this.G = g;
			this.B = b;
		}
		
		public Diod(int r, int g, int b) {
			int Cut(int bt) {
				if (bt > 255) {
					return 255;
				}else if (bt < 0) {
					return 0;
				}

				return bt;
			}

			this.R = Convert.ToByte(Cut(r));
			this.G = Convert.ToByte(Cut(g));
			this.B = Convert.ToByte(Cut(b));
		}

		public static Diod Off() {
			return new Diod(0, 0, 0);
		} 

		public static Diod Red() {
			return new Diod(255, 0, 0);
		} 
		
		public static Diod Green() {
			return new Diod(0, 255, 0);
		} 
		
		public static Diod Blue() {
			return new Diod(0, 0, 255);
		}

		public override bool Equals(object? obj) {
			Diod diod = (Diod) obj;
			return diod != null && this.Index == diod.Index;
		}
	}
}