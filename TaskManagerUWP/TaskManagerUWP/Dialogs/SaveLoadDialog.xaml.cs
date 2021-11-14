using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Library.TaskManager.Models;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerUWP.Dialogs {
	public sealed partial class SaveLoadDialog : ContentDialog {
		private IList<TMItem> TMItems;
		public SaveLoadDialog(IList<TMItem> TMItems) {
			InitializeComponent();
			DataContext = this;
			this.TMItems = TMItems;
		}

		// Variables to store the save/load file path.
		public string Filename { get; set; }

		// Saves the list to a JSON file.
		private void ContentDialog_SaveClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Filename);
			File.WriteAllText($"{path}.json", JsonConvert.SerializeObject(TMItems, settings));
		}

		// Loads the list from a JSON file.
		private void ContentDialog_LoadClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Filename);
			if (File.Exists($"{path}.json")) {
				JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
				var items = JsonConvert.DeserializeObject<IList<TMItem>>(File.ReadAllText($"{path}.json"), settings);
				TMItems.Clear();
				foreach(var item in items) {
					TMItems.Add(item);
				}
			}
		}
	}
}
