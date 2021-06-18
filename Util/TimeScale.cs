using System;
using System.Linq;

namespace ProsperousAssistant.Util
{
	public class TimeScale
	{
		public static readonly TimeScale SECONDS = new TimeScale("Seconds", 1);
		public static readonly TimeScale MINUTES = new TimeScale("Minutes", 60);
		public static readonly TimeScale HOURS = new TimeScale("Hours", 60 * 60);
		public static readonly TimeScale DAYS = new TimeScale("Days", 60 * 60 * 24);
		public static readonly TimeScale WEEKS = new TimeScale("Weeks", 60 * 60 * 24 * 7);
		public static readonly TimeScale MONTHS = new TimeScale("Months", 60 * 60 * 24 * 30);
		public static readonly TimeScale[] ALL = { SECONDS, MINUTES, HOURS, DAYS, WEEKS, MONTHS };

		private TimeScale(string name, int scaleFactor)
		{
			Name = name;
			ScaleFactor = scaleFactor;
		}

		public string Name { get; }
		public int ScaleFactor { get; }

		public static TimeScale Parse(string str)
		{
			return ALL.Where(mode => mode.Name.Equals(str, StringComparison.OrdinalIgnoreCase)).Single();
		}
	}
}
