using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManager.Models {
	public class TMItem {
		public int Id { get; set; }
		public int Priority { set; get; }
		public string PriorityString {
			get {
				if (Priority == 1) {
					return "[!]  ";
				}
				else if (Priority == 2) {
					return "[!!] ";
				}
				else {
					return "[!!!]";
				}
			}
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public string IsCompletedString { get; set; }
		public override string ToString() {
			return $"[ITEM] {Name} - {Description}";
		}
	}
}

