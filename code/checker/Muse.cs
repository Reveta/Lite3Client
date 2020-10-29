using System;
using System.Collections.Generic;

namespace Client.code.checker {
	public class Muse : IChecker{
		// private static List<byte[]> _list = new List<byte[]>();
		private static byte[] _bytesBuffer = new byte[Config.DiodCount * 3];


		public byte[] Check(byte[] bytes) {
			byte[] message = new byte[Config.DiodCount * 3];
			if (SumArray(_bytesBuffer) != 0) {
				for (int i = 0; i < bytes.Length; i++) {
					byte b1 = bytes[i];
					byte b2 = _bytesBuffer[i];
					int res = 0;
					if (b1 > b2) {
						res = (b1 - b2) / 2;
					} else {
						res = (b2 - b1) / 2;
					}
					message[i] = (byte) res;
				}
				
				
				
			} else {
				_bytesBuffer = bytes;
				return bytes;
			}

			return message;
		}

		private int SumDiod(byte b1, byte b2, byte b3) {
			return (int)b1 + (int)b2 + (int)b3;
		}

		private int SumArray(byte[] bytes) {
			int sum = 0;
			foreach (byte b in bytes) {
				sum += (int) b;
			}

			return sum;
		}
	}
}