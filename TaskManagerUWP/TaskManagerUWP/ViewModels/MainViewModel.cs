using TaskManagerUWP.Dialogs;
using TaskManagerUWP.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerUWP.ViewModels {
	public class MainViewModel : INotifyPropertyChanged {
		public TMItem SelectedTMItem { get; set; }
		public ObservableCollection<TMItem> TMItems { get; set; }
		private ObservableCollection<TMItem> filteredTMItems;
		// In-class example that allows the search function to work by returning the filtered items when searching and all otherwise.
		public ObservableCollection<TMItem> FilteredTMItems { get {
				if(string.IsNullOrWhiteSpace(Query)) {
					return TMItems;
				} else {
					filteredTMItems = new ObservableCollection<TMItem>(TMItems
						.Where(s => s.Name.ToUpper().Contains(Query.ToUpper())
						|| s.Description.ToUpper().Contains(Query.ToUpper())
						|| (s as TMAppointment != null
						&& (s as TMAppointment).Attendees.ToUpper().Contains(Query.ToUpper()))).ToList());
					return filteredTMItems;
				}
			}
		}
		public string Query { get; set; }

		// These must be for saving eventually.
		private string persistencePath;
		private JsonSerializerSettings serializationSettings;

		public MainViewModel() {
			TMItems = new ObservableCollection<TMItem>();
			persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			foreach(var item in TMItems) {
				if(item is TMTask) {
					FilteredTMItems.Add(item as TMTask);
				} else {
					FilteredTMItems.Add(item as TMAppointment);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Edit function, skips the prompting dialog because it already knows what kind of item the selection is.
		public async Task EditTicket() {
			if (SelectedTMItem is TMTask) {
				var diag = new TMTaskDialog(TMItems, SelectedTMItem as TMTask);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
			} else if (SelectedTMItem is TMAppointment) {
				var diag = new TMApptDialog(TMItems, SelectedTMItem as TMAppointment);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
			}
		}

		// Simply removes the item as long as it isn't already null (in which case exits without doing anything).
		public void Remove() {
			if (SelectedTMItem == null) {
				return;
			}
			TMItems.Remove(SelectedTMItem);
		}

		// Marks the selected item as completed as long as it is not null.
		public void Complete() {
			if (SelectedTMItem == null) {
				return;
			}
			SelectedTMItem.IsCompleted = true;
			SelectedTMItem.IsCompletedString = "COMPLETE";
		}

		// Refresh list.
		public void RefreshList() {
			NotifyPropertyChanged("FilteredTMItems");
		}
	}
}