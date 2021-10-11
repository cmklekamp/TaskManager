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
// Is either navigated to from the prompt dialog, or straight to when editing. Adds/edits a task item.

namespace TaskManagerUWP.Library.Dialogs {
	public sealed partial class TMTaskDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public TMTaskDialog(IList<TMItem> TMItems) {
			InitializeComponent();
			DataContext = new TMTask();
			this.TMItems = TMItems;
		}
		public TMTaskDialog(IList<TMItem> TMItems, TMTask item) {
			InitializeComponent();
			DataContext = item;
			this.TMItems = TMItems;
		}

		// Adds the task (or, if it already exists, edits it by removing the original and adding the new one in its spot).
		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			var itemToEdit = DataContext as TMTask;
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
