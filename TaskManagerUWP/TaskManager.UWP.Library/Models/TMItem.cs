using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerUWP.Library.Models {
	public class TMItem {
		// Stores the current ID that all new items will be set to.
		//private static int currentID = 1;
		// Sets the ID to whatever the private current ID is and increments it.
		//public TMItem() {
		//	ID = currentID++;
		//}
		// All public variables used throughout the program.
		// ID no longer necessary.
		//public int ID { set; get; }
		// Stores priority (1 - 3) and the string ver. ([!  ] to [!!!])
		public int Priority { set; get; }
		public string PriorityString { set; get; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsCompleted { get; set; }
		public string IsCompletedString { get; set; }
		// Int that stores the number of completed items for the outstanding items list (no longer needed)
		//public static int completedNum = 0;
		//public void setCompletedNum() {
		//	completedNum++;
		//}

		// Temp. ver. for testing purposes.
		public override string ToString() {
			return $"[ITEM] {Name} - {Description}";
		}
	}
}
