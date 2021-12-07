using TaskManagerUWP.Dialogs;
using Library.TaskManager.Communication;
using Library.TaskManager.Models;
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
					var filteredTaskString = new WebRequestHandler().Post("http://localhost:13791/Task/Query", Query).Result;
					var filteredTasks = JsonConvert.DeserializeObject<ObservableCollection<TMTask>>(filteredTaskString);
					filteredTMItems = new ObservableCollection<TMItem>();
					if (filteredTasks != null) {
						foreach(TMItem i in filteredTasks) {
							filteredTMItems.Add(i);
						}
					}
					var filteredApptString = new WebRequestHandler().Post("http://localhost:13791/Appointment/Query", Query).Result;
					var filteredAppointments = JsonConvert.DeserializeObject<List<TMAppointment>>(filteredApptString);
					if (filteredAppointments != null) {
						foreach (TMAppointment i in filteredAppointments) {
							filteredTMItems.Add(i);
						}
					}
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
		public async Task AddTMItem() {
			var diag = new TMPromptDialog(TMItems);
			await diag.ShowAsync();
			RefreshList();
		}

		// Edit function, skips the prompting dialog because it already knows what kind of item the selection is.
		public async Task EditTMItem() {
			if (SelectedTMItem is TMTask) {
				var diag = new TMTaskDialog(TMItems, SelectedTMItem as TMTask);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
				RefreshList();
			}
			else if (SelectedTMItem is TMAppointment) {
				var diag = new TMApptDialog(TMItems, SelectedTMItem as TMAppointment);
				NotifyPropertyChanged("SelectedTMItem");
				await diag.ShowAsync();
				RefreshList();
			}
		}

		// Simply removes the item as long as it isn't already null (in which case exits without doing anything).
		public void DeleteTMItem() {
			if (SelectedTMItem == null) {
				return;
			}
			if (SelectedTMItem is TMTask) {
				var taskString =  new WebRequestHandler().Post("http://localhost:13791/Task/Delete", SelectedTMItem).Result;
				var taskServer = JsonConvert.DeserializeObject<TMTask>(taskString);
				var task = TMItems.FirstOrDefault(t => t is TMTask && t._id == taskServer._id);
				TMItems.Remove(task);
				RefreshList();
			}
			else if (SelectedTMItem is TMAppointment) {
				var apptString = new WebRequestHandler().Post("http://localhost:13791/Appointment/Delete", SelectedTMItem).Result;
				var apptServer = JsonConvert.DeserializeObject<TMAppointment>(apptString);
				var appt = TMItems.FirstOrDefault(t => t is TMAppointment && t._id == apptServer._id);
				TMItems.Remove(appt);
				RefreshList();
			}
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

		// Save or load to file.
		public async void SaveLoad() {
			var diag = new SaveLoadDialog(TMItems);
			await diag.ShowAsync();
		}
	}
}