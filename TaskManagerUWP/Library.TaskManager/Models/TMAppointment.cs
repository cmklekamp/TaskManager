using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.TaskManager.Persistence;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Library.TaskManager.Models {
	[JsonConverter(typeof(ItemJsonConverter))]
	public class TMAppointment : TMItem, INotifyPropertyChanged {
		// Initial constructor.
		public TMAppointment() : base() {
			BoundDateStart = DateTime.Now;
			BoundDateStop = DateTime.Now;
		}
		// Variables that handle DateTime stuff.
		// Starting time variables.
		[BsonElement("Start")]
		public DateTime Start { set; get; }
		[BsonIgnore]
		private DateTimeOffset boundDateStart;
		[BsonIgnore]
		public DateTimeOffset BoundDateStart {
			get {
				return boundDateStart;
			}
			set {
				boundDateStart = value;
				Start = boundDateStart.Date;
				NotifyPropertyChanged("Start");
			}
		}
		// Stop time variables.
		[BsonElement("Stop")]
		public DateTime Stop { set; get; }
		[BsonIgnore]
		private DateTimeOffset boundDateStop;
		[BsonIgnore]
		public DateTimeOffset BoundDateStop {
			get {
				return boundDateStop;
			}
			set {
				boundDateStop = value;
				Stop = boundDateStop.Date;
				NotifyPropertyChanged("Stop");
			}
		}
		// Event handler stuff.
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		// Input string that stores the attendees of the appointment.
		[BsonElement("Attendees")]
		public string Attendees { set; get; }
		// ToString override that formats the items being printed to the screen.
		public override string ToString() {
			return $"{PriorityString}[{IsCompletedString}][Appt] {Name} - {Description}";
		}
	}
}
