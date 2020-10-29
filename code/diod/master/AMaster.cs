using System.Collections.Generic;
using Client.code.objects;

namespace Client.code.diod.master {
	public abstract class AMaster {

		public byte[] GetCommands(List<Diod> diods) {
			var bytes = new List<byte>();
			diods.ForEach(diod => {
				bytes.Add(diod.R);
				bytes.Add(diod.G);
				bytes.Add(diod.B);
			});
			return bytes.ToArray();
		}

	}
}