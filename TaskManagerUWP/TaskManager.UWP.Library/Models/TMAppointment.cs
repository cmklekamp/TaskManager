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
		// Overrides ToString to make listing easier.
		//public override string ToString() {
		//	return $"{ID}. [APPT] {Name} - {Description} ({Start.ToString()} - {Stop.ToString()}) [{IsCompletedString}]" +
		//		$"\n\tAttendees: {Attendees}";
		//}

		// Simplified ver. for testing purposes.
		public override string ToString() {
			return $"[APPT] {Name} - {Description}";
		}
	}
}
