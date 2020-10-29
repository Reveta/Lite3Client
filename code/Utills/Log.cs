using System;
using System.Collections.Generic;
using System.Threading;

namespace NTireParkMaster.DataAccess.Utills {
	public class Log {
		private static string pathLogFile = "log.txt";

		private static List<int> TypeLevels = new List<int>() {0};
		private static object _lockObject = new object();


		public static void ToLog(Level level, string logMessage) { ToLog(level.ToString(), logMessage, "", 0); }

		public static void ToLog(Level level, string logMessage, string className = "") {
			ToLog(level.ToString(), logMessage, className, 0);
		}

		public static void ToLog(Level level, string logMessage, int typeLevel) {
			ToLog(level, logMessage, null, typeLevel);
		}

		public static void ToLog(Level level, string logMessage, Type className = null) {
			ToLog(level.ToString(), logMessage, className.ToString());
		}

		public static void ToLog(Level level, string logMessage, Type className = null, int typeLevel = 0) {
			ToLog(level.ToString(), logMessage, className != null ? className.ToString() : "", typeLevel);
		}


		public static void ToLog(MessageLevel level, string logMessage, string className = "") {
			ToLog(level.ToString(), logMessage, className);
		}

		private static void ToLog(string level, string logMessage, string className = "", int typeLevel = 0) {
			lock (_lockObject) {
				TypeLevels.ForEach(type => {
					if (typeLevel == type) {
						string time = $"{DateTime.Now:g}";
						string message = $"{level} : {time} : {className} -> {logMessage}";
						
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine(message);
					}
				});
			}
		}

		public static void CheckIsNull(Object[] objects, Type className = null) {
			foreach (object objectOne in objects) {
				CheckIsNull(objectOne, className);
			}
		}

		public static void LogTypeLevelToShow(int[] levels) {
			TypeLevels.Remove(0);
			TypeLevels.AddRange(levels);
		}

		public static void ToLogState(Type stateFirst, Type stateSecond) {
			ToLog(Level.INFO, $"StateChange: {stateFirst.Name} TO {stateSecond.Name}");
			Thread.Sleep(1000);
		}

		public static void ToLogState(string stateFirst, string stateSecond) {
			ToLog(Level.INFO, $"StateChange: {stateFirst} TO {stateSecond}");
			Thread.Sleep(1000);
		}

		public static void CheckIsNull(object objectOne, Type className = null) {
			string obj = "object ";
			string nl = "\n";
			string nul = "null ";
			string messge = "";
			string classNameString = className != null ? className.Name : "";

			bool isNull = objectOne != null;
			if (isNull) {
				messge += $"{nl} {objectOne.GetType().Name} is {obj}";
			} else {
				messge += $"{nl} {objectOne} is {nul}";
			}

			ToLog(Level.DEBUG, messge, classNameString);
		}

		public static void SetPathLogFile(string path) { pathLogFile = path; }

		public enum Level {
			INFO,
			DEBUG,
			WARNING,
			ERROR,
			FATAL,
		}

		public enum MessageLevel {
			MESSAGE,
			PHOTO,
			NEWUSER,
			NEWSESSION,
			ENDSESSION
		}

		private struct LogObject {
			private Level Level { get; set; }
			private MessageLevel MessageLevel { get; set; }

			private string LogMessage { get; set; }
			private Type ClassName { get; set; }

			private int TypeLevel { get; set; }
		}
	}
}