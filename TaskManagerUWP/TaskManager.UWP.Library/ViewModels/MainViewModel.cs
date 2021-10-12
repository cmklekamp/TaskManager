using TaskManagerUWP.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerUWP.Library.ViewModels {
	public class MainViewModel {
		public List<TMItem> TMItems { get; set; }
		public TMItem SelectedTMItem { get; set; }

		public MainViewModel() {
			TMItems = new List<TMItem>();
		}

		public void AddTMItem() {
			if (SelectedTMItem == null) {
				TMItems.Add(new TMItem());
			}
		}

		public void DeleteTMItem() {
			if(SelectedTMItem != null) {
				TMItems.Remove(SelectedTMItem);
			}
		}
	}
}