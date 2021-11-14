using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Library.TaskManagerUWP.Models {
	public class TMTask : TMItem, INotifyPropertyChanged {
		// Initial constructor.
		public TMTask() : base() {
			BoundDate = DateTime.Now;
		}

		// Variables that handle DateTime stuff.
		private DateTimeOffset boundDate;
		public DateTimeOffset BoundDate {
			get {
				return boundDate;
			}
			set {
				boundDate = value;
				Deadline = boundDate.Date;
				NotifyPropertyChanged("Deadline");
			}
		}
		public DateTime Deadline { get; set; }
		// Event handler stuff.
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][Task] {Name} - {Description}";
		}
	}
}
