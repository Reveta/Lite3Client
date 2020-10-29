using System;
using System.Collections.Generic;
using Client.code.objects;

namespace Client.code.diod.master {
	public class MasterDebug : AMaster, IComandGenerator {
		private List<Diod> _diods = new List<Diod>();

		public MasterDebug() {
			_diods.Clear();
			for (int i = 0; i < 16; i++) {
				int color = new Random().Next(1, 3);
				// int color = 1;
				_diods.Add(new Diod(
					color,
					color,
					color)
				);
			}
		}

		public byte[] GetCommands() => base.GetCommands(_diods);
	}
}