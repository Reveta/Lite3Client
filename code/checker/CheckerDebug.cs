using System;
using Client.code.diod.master;
using NTireParkMaster.DataAccess.Utills;

namespace Client.code.checker {
	public class CheckerDebug : IChecker {
		public byte[] Check(byte[] bytes) {
			byte[] message = new MasterDebug().GetCommands();
			if (message.Length != Config.DiodCount * 3) {
				Log.ToLog(Log.Level.WARNING, "Debug command size != DiodCount * 3!", GetType());
			}

			if (bytes.Length != 0) {
				for (var i = 0; i < message.Length; i++) {
					byte byteDigit = bytes[i];
					if (byteDigit != 0) {
						message[i] = byteDigit;
					}
				}
			}

			if (message.Length != bytes.Length) {
				Console.WriteLine($"Warning! message size = [{bytes.Length}]," +
				                  $" but expected [{message.Length}]");
			}


			return message;
		}
	}
}