using System;

namespace TaskManagerUWP.Library.Models {
	public class TMTask : TMItem {
		// Unique variable that stores the task deadline.
		public DateTime Deadline { get; set; }

		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][Task] {Name} - {Description}";
		}
	}
}
