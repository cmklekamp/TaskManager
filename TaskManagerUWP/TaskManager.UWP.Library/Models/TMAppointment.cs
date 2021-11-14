using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.TaskManagerUWP.Models {
	public class TMAppointment : TMItem {
		// Initial constructor.
		public TMAppointment() : base() {
			BoundDateStart = DateTime.Now;
			BoundDateStop = DateTime.Now;
		}
		// Variables that handle DateTime stuff.
		// Starting time variables.
		public DateTime Start { set; get; }
		private DateTimeOffset boundDateStart;
		public DateTimeOffset BoundDateStart {
			get {
				return boundDateStart;
			}
			set {
				boundDateStart = value;
				Start = boundDateStart.Date;
				NotifyPropertyChanged("Start");
			}
		}
		// Stop time variables.
		public DateTime Stop { set; get; }
		private DateTimeOffset boundDateStop;
		public DateTimeOffset BoundDateStop {
			get {
				return boundDateStop;
			}
			set {
				boundDateStop = value;
				Stop = boundDateStop.Date;
				NotifyPropertyChanged("Stop");
			}
		}
		// Event handler stuff.
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		// Input string that stores the attendees of the appointment.
		public string Attendees { set; get; }
		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][Appt] {Name} - {Description}";
		}
	}
}
