using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManager {
	public class Item {
		// Stores the current ID that all new items will be set to.
		private static int currentID = 1;
		// Sets the ID to whatever the private current ID is and increments it.
		public Item() {
			ID = currentID++;
		}
		// All public variables used throughout the program.
		public int ID { set; get; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsCompleted { get; set; }
		public string IsCompletedString { get; set; }
		// Int that stores the number of completed items for the outstanding items list.
		public static int completedNum = 0;
		public void setCompletedNum() {
			completedNum++;
		}
	}
}
