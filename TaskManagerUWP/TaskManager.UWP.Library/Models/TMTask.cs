using System;

namespace TaskManagerUWP.Library.Models {
	public class TMTask : TMItem {
		// Unique variable that stores the task deadline.
		public DateTime Deadline { get; set; }
		// Overrides ToString to make listing easier.
		//public override string ToString() {
		//	return $"{ID}. [TASK] {Name} - {Description} ({Deadline.ToString()}) [{IsCompletedString}]";
		//}
		// Simplified ver. for testing purposes.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][TASK] {Name} - {Description}";
		}
	}
}
