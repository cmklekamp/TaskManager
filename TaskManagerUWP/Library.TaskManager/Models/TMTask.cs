using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.TaskManager.Persistence;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Library.TaskManager.Models {
	[JsonConverter(typeof(ItemJsonConverter))]
	public class TMTask : TMItem, INotifyPropertyChanged {
		// Initial constructor.
		public TMTask() : base() {
			BoundDate = DateTime.Now;
		}

		// Variables that handle DateTime stuff.
		[BsonIgnore]
		private DateTimeOffset boundDate;
		[BsonIgnore]
		public DateTimeOffset BoundDate {
			get {
				return boundDate;
			}
			set {
				boundDate = value;
				Deadline = boundDate.Date;
				NotifyPropertyChanged("Deadline");
			}
		}
		[BsonElement("deadline")]
		public DateTime Deadline { get; set; }
		// Event handler stuff.
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][Task] {Name} - {Description}";
		}
	}
}

