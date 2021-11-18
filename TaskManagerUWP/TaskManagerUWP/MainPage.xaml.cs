using System.Collections.Generic;
using Library.TaskManager.Communication;
using Library.TaskManager.Models;
using Newtonsoft.Json;
using TaskManagerUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TaskManagerUWP {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            //DataContext = new MainViewModel();

            //var handler = new WebRequestHandler();
            //var items = JsonConvert.DeserializeObject<List<TMItem>>(handler.Get("http://localhost/TaskManagerAPI/Task/GetItem").Result);
            //var context = DataContext as MainViewModel;

            //items.ForEach(context.FilteredTMItems.Add);
            var mainViewModel = new MainViewModel();
            var taskString = new WebRequestHandler().Get("http://localhost:13791/Task").Result;
            var tasks = JsonConvert.DeserializeObject<List<TMTask>>(taskString);
            if(tasks != null) {
                tasks.ForEach(t => mainViewModel.FilteredTMItems.Add(t));
            }
            var appointmentString = new WebRequestHandler().Get("http://localhost:13791/Appointment").Result;
            var appointments = JsonConvert.DeserializeObject<List<TMAppointment>>(appointmentString);
            if(appointments != null) {
                appointments.ForEach(a => mainViewModel.FilteredTMItems.Add(a));
            }

            DataContext = mainViewModel;
            (DataContext as MainViewModel).RefreshList();
        }

        public event DependencyPropertyChangedEventHandler PropertyChanged;

        private async void AddNew_Click(object sender, RoutedEventArgs e) {
            await (DataContext as MainViewModel).AddTMItem();
            (DataContext as MainViewModel).RefreshList();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e) {
            await (DataContext as MainViewModel).EditTMItem();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            (DataContext as MainViewModel).DeleteTMItem();
		}

        private async void Details_Click(object sender, RoutedEventArgs e) {
            await (DataContext as MainViewModel).Details();
        }

        private void Search_Click(object sender, RoutedEventArgs e) {
            (DataContext as MainViewModel).RefreshList();
		}

        private void Sort_Click(object sender, RoutedEventArgs e) {
            (DataContext as MainViewModel).RefreshList();
        }

        private void SaveLoad_Click(object sender, RoutedEventArgs e) {
            (DataContext as MainViewModel).SaveLoad();
        }

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}
	}
}
