using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskManager {
	public class Appointment : Item {
		// Unique variables that store the appointment start and end times.
		public DateTime Start { set; get; }
		public DateTime Stop { set; get; }
		// List of strings that stores the attendees of the appointment.
		public List<string> Attendees = new List<string>();
		// Overrides ToString to make listing easier.
		public override string ToString() {
			return $"{ID}. [APPT] {Name} - {Description} ({Start.ToString()} - {Stop.ToString()}) [{IsCompletedString}]" +
				$"\n\tAttendees: {String.Join(", ", Attendees)}";
		}
	}
}
