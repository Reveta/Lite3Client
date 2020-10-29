using System.Collections.Generic;
using Client.code.objects;

namespace Client.code.diod.master {
	public class EmptyMaster : AMaster, IComandGenerator {
		private static List<Diod> _diods = new List<Diod>() {
			Diod.Off(), Diod.Off(), Diod.Off(), Diod.Off(),
			Diod.Off(), Diod.Off(), Diod.Off(), Diod.Off(),
			Diod.Off(), Diod.Off(), Diod.Off(), Diod.Off(),
			Diod.Off(), Diod.Off(), Diod.Off(), Diod.Off(),
		};

		public byte[] GetCommands() => base.GetCommands(_diods);
	}
}