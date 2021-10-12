using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerUWP.Library.Models {
	public class TMAppointment : TMItem {
		// Unique variables that store the appointment start and end times.
		public DateTime Start { set; get; }
		public DateTime Stop { set; get; }
		// List of strings that stores the attendees of the appointment.
		public string Attendees { set; get; }

		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][APPT] {Name} - {Description}";
		}
	}
}
