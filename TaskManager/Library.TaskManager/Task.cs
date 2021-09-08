using System;

namespace Library.TaskManager {
	public class Task {
		// Stores the current ID that all new tasks will be set to.
		private static int currentID = 1;
		// Sets the ID to whatever the private current ID is and increments it.
		public Task() {
			ID = currentID++;
		}
		// All public variables used throughout the program.
		public int ID { set; get; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Deadline { get; set; }
		public bool IsCompleted { get; set; }
		// Overrides ToString to make listing easier.
		public override string ToString() {
			return $"{ID}. {Name} - {Description} ({Deadline.ToString("MM/dd/yyyy")})";
		}
	}
}
