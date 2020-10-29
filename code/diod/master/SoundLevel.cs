using System;
using System.Linq;
using Client.code.diod.master.optimal;
using Client.code.objects;
using NAudio.CoreAudioApi;

namespace Client.code.diod.master {
	public class SoundLevel : AFunctions, IComandGenerator {
		public SoundLevel() { Run(); }

		private void Run() {
			int soundLevel = GetSoundLevel();

			int part = soundLevel / 10;
			int spec = 0;
			int spec2 = 0;
			Console.WriteLine($"{soundLevel} => {part}");
			for (int i = 0; i < part; i++) {
				if (part > 20) {
					spec *= part.ToString()[0];
					if (part > 150) {
						spec2 = (spec2 + 30) * part.ToString()[0];
						spec2 += soundLevel;
					}
				}

				int red = part + Cut(soundLevel) - Cut(spec2);
				int green = part;
				int blue = part + spec;
				base.AddDiod(new Diod(red, 0, blue));
				// Console.WriteLine($" [{red}] [{green}] [{blue}] ");
			}

			int diodCount = Config.DiodCount - part;
			for (int i = 0; i < diodCount; i++) {
				base.AddDiod(Diod.Off());
			}
		}

		int Cut(int bt) {if (bt > 255) { return 255; } else if (bt < 0) {return 0;} return bt;}

		private int GetSoundLevel() {
			MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
			MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
			float masterPeakValue = defaultDevice.AudioMeterInformation.MasterPeakValue;
			int peakValue = (int) (masterPeakValue * 1000);
			Console.WriteLine($"Level => [{peakValue}]");
			return peakValue;
		}


		public byte[] GetCommands() => base.GetCommands(base.GetDiod());
	}
}