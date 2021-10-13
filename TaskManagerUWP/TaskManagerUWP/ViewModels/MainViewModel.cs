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
		// In-class example that allows the search function to work by returning the filtered items when searching.
		// If not searching, also checks if the sort type is default, and otherwise returns the other type of sort.
		public ObservableCollection<TMItem> FilteredTMItems { get {
				if(string.IsNullOrWhiteSpace(Query)) {
					if (SortType == "Only Incomplete") {
						filteredTMItems = new ObservableCollection<TMItem>(TMItems
							.Where(s => s.IsCompletedString.Contains("Incomplete")));
						return filteredTMItems;
					} else if (SortType == "By Priority") {
						filteredTMItems = new ObservableCollection<TMItem>(TMItems
							.OrderByDescending(s => s.Priority).ToList());
						return filteredTMItems;
					} else {
						return TMItems;
					}
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
		// String responsible for filtering the search.
		public string Query { get; set; }
		// String responsible for filtering based on the sort ComboBox.
		public string SortType { get; set; }

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

		// Add function, calls the "task or appointment" prompt and passes in the list to pass on.
		public async Task AddTicket() {
			var diag = new TMPromptDialog(TMItems);
			await diag.ShowAsync();
		}

		// Edit function, skips the prompting dialog because it already knows what kind of item the selection is.
		public async Task EditTicket() {
			if (SelectedTMItem is TMTask) {
				var diag = new TMTaskDialog(TMItems, SelectedTMItem as TMTask);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
			}
			else if (SelectedTMItem is TMAppointment) {
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

		// Calls a dialogue that shows the details of the item.
		public async Task Details() {
			if (SelectedTMItem is TMTask) {
				var diag = new TMTaskDetailsDialog(TMItems, SelectedTMItem as TMTask);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
			}
			else if (SelectedTMItem is TMAppointment) {
				var diag = new TMApptDetailsDialog(TMItems, SelectedTMItem as TMAppointment);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
			}
		}

		// Refresh list.
		public void RefreshList() {
			NotifyPropertyChanged("FilteredTMItems");
		}
	}
}