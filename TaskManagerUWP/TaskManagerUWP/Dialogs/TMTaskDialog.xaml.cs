using System.Collections.Generic;
using System.Linq;
using Library.TaskManager.Communication;
using Library.TaskManager.Models;
using Newtonsoft.Json;
using TaskManagerUWP.ViewModels;
using Windows.UI.Xaml.Controls;


// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
// Is either navigated to from the prompt dialog, or straight to when editing. Adds/edits a task item.

namespace TaskManagerUWP.Dialogs {
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

			var taskString = new WebRequestHandler().Post("http://localhost:13791/Task/AddOrUpdate", itemToEdit).Result;
			var taskServer = JsonConvert.DeserializeObject<TMTask>(taskString);
			var task = TMItems.FirstOrDefault(t => t is TMTask && t._id == taskServer._id);
			if(task == null) {
				TMItems.Add(taskServer);
			} else {
				var index = TMItems.IndexOf(task);
				TMItems.Remove(task);
				TMItems.Insert(index, task);
			}
		}
	}
}
