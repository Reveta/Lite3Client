using System;

namespace Client.code.checker {
	public class ByteFiller : IChecker {
		public byte[] Check(byte[] bytes) {
			var message = new byte[16 * 3];

			for (var i = 0; i < message.Length; i++) {
				message[i] = 0;
			}

			for (var i = 0; i < message.Length; i++) {
				message[i] = bytes[i];
			}

			if (message.Length != bytes.Length) {
				Console.WriteLine($"Warning! message size = [{bytes.Length}]," +
				                  $" but expected {message.Length}");
			}


			return message;
		}
	}
}