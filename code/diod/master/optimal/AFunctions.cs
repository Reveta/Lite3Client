using System.Collections.Generic;
using System.Linq;
using Client.code.objects;

namespace Client.code.diod.master.optimal {
	public abstract class AFunctions : AMaster {
		private int _index = 0;

		private readonly LinkedList<Diod> Diods = new LinkedList<Diod>();

		public AFunctions() {
			for (var i = 0; i < Config.DiodCount; i++) {
				Diod diod = Diod.Off();
				diod.Index = i;
				Diods.AddLast(diod);
			}
		}
		protected List<Diod> GetDiod() => this.Diods.ToList();

		protected void AddDiod(Diod diod) {
			diod.Index = _index;

			if (_index == Config.DiodCount) {
				_index = 0;
			} else {
				Diods.AddBefore(Diods.Find(new Diod(_index)), diod);
				Diods.RemoveLast();
			}
		}

		protected Diod GetDiod(int index) { return Diods.Find(new Diod(index))?.Value; }
	}
}