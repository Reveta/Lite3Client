using System.Collections.Generic;
using System.Linq;
using Client.code.objects;

namespace Client.code.diod.master {
	public class DiodMaster : AMaster, IComandGenerator {
		private int _index = 0;

		private readonly List<Diod> Diods = new List<Diod>();

		private void AddDiod(Diod diod) {
			if (_index == Config.DiodCount) {
				_index = 0;
			}

			Diods[_index++] = diod;
		}

		public DiodMaster() {
			if (Diods.Count == 0) {
				for (var i = 0; i < Config.DiodCount; i++) {
					Diods.Add(Diod.Off());
				}
			}
			
			Run();
		}

		public void Run() {
			for (var i = 0; i < 1; i++) {
				AddDiod(Diod.Green());
				AddDiod(Diod.Blue());
				AddDiod(Diod.Blue());
				AddDiod(Diod.Green());
				AddDiod(Diod.Red());
			}
		}

		public byte[] GetCommands() => base.GetCommands(Diods.ToList());
	}
}