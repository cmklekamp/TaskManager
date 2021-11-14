using System;
using System.Collections.Generic;
using Library.TaskManager.Models;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// Reads in which kind of item the user wants to make and opens the appropriate next dialog to add the item.

namespace TaskManagerUWP.Dialogs {
	public sealed partial class TMPromptDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public TMPromptDialog(IList<TMItem> TMItems) {
			InitializeComponent();
			DataContext = new TMItem();
			this.TMItems = TMItems;
		}

		// Issue here with linking to the next dialog, I don't think the above is necessary and is actively screwing things up.
		private async void ContentDialog_AddTask(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			sender.Hide();
			var diag = new TMTaskDialog(TMItems);
			await diag.ShowAsync();
		}

		// Issue here with linking to the next dialog, I don't think the above is necessary and is actively screwing things up.
		private async void ContentDialog_AddAppt(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			sender.Hide();
			var diag = new TMApptDialog(TMItems);
			await diag.ShowAsync();
		}
	}
}
