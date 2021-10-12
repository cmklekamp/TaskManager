﻿using TaskManagerUWP.Library.Models;
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