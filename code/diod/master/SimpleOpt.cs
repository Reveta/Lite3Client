using System.Collections.Generic;
using System.Linq;
using Client.code.objects;

namespace Client.code.diod.master {
	public class DiodMasterOpt : AMaster, IComandGenerator {
		private int _index = 0;

		private readonly LinkedList<Diod> Diods = new LinkedList<Diod>();

		private void AddDiod(Diod diod) {
			diod.Index = _index;

			if (_index == Config.DiodCount) {
				_index = 0;
			} else {
				Diods.AddBefore(Diods.Find(new Diod(_index)), diod);
				Diods.RemoveLast();
			}
		}

		public DiodMasterOpt() {
			for (var i = 0; i < Config.DiodCount; i++) {
				Diod diod = Diod.Off();
				diod.Index = i;
				Diods.AddLast(diod);
			}
			
			Run();
		}

		public void Run() {
			for (var i = 0; i < 2; i++) {
				AddDiod(Diod.Green());
				AddDiod(Diod.Blue());
				AddDiod(Diod.Green());
			}
		}

		public byte[] GetCommands() => base.GetCommands(Diods.ToList());
	}
}