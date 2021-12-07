using System.Collections.Generic;
using System.Linq;
using Library.TaskManager.Communication;
using Library.TaskManager.Models;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// Is either navigated to from the prompt dialog, or straight to when editing. Adds/edits an appointment item.

namespace TaskManagerUWP.Dialogs {
	public sealed partial class TMApptDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public TMApptDialog(IList<TMItem> TMItems) {
			InitializeComponent();
			DataContext = new TMAppointment();
			this.TMItems = TMItems;
		}
		public TMApptDialog(IList<TMItem> TMItems, TMAppointment item) {
			InitializeComponent();
			DataContext = item;
			this.TMItems = TMItems;
		}

		// Adds the appointment (or, if it already exists, edits it by removing the original and adding the new one in its spot).
		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			var itemToEdit = DataContext as TMAppointment;

			var apptString = new WebRequestHandler().Post("http://localhost:13791/Appointment/AddOrUpdate", itemToEdit).Result;
			var apptServer = JsonConvert.DeserializeObject<TMAppointment>(apptString);
			var appt = TMItems.FirstOrDefault(a => a is TMAppointment && a._id == apptServer._id);
			if (appt == null) {
				TMItems.Add(apptServer);
			}
			else {
				var index = TMItems.IndexOf(appt);
				TMItems.Remove(appt);
				TMItems.Insert(index, appt);
			}
		}
	}
}
