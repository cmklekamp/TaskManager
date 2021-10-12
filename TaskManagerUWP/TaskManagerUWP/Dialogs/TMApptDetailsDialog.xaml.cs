using System.Collections.Generic;
using TaskManagerUWP.Library.Models;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerUWP.Dialogs {
	public sealed partial class TMApptDetailsDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public TMApptDetailsDialog(IList<TMItem> TMItems, TMAppointment item) {
			InitializeComponent();
			DataContext = item;
			this.TMItems = TMItems;
		}
	}
}
