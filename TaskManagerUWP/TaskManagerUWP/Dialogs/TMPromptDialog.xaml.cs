using TaskManagerUWP.Library.Models;
using TaskManagerUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
