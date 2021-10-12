using TaskManagerUWP.Library.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// Is either navigated to from the prompt dialog, or straight to when editing. Adds/edits a task item.

namespace TaskManagerUWP.Dialogs {
	public sealed partial class TMTaskDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public TMTaskDialog(IList<TMItem> TMItems) {
			InitializeComponent();
			//DataContext = new TMTask();
			DataContext = new TMItem();
			this.TMItems = TMItems;
		}
		//public TMTaskDialog(IList<TMItem> TMItems, TMTask item) {
		public TMTaskDialog(IList<TMItem> TMItems, TMItem item) {
			InitializeComponent();
			DataContext = item;
			this.TMItems = TMItems;
		}

		// Adds the task (or, if it already exists, edits it by removing the original and adding the new one in its spot).
		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			//var itemToEdit = DataContext as TMTask;
			var itemToEdit = DataContext as TMItem;
			var i = TMItems.IndexOf(itemToEdit);
			if (i >= 0) {
				TMItems.Remove(itemToEdit);
				TMItems.Insert(i, itemToEdit);
			}
			else {
				TMItems.Add(itemToEdit);
			}
		}

		// This is just to exit the dialog without doing anything, remains empty.
		private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
		}
	}
}
