using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.TaskManager.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Library.TaskManager.Models {
	[JsonConverter(typeof(ItemJsonConverter))]
	public class TMItem : INotifyPropertyChanged {
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Priority { set; get; }
		public string PriorityString {
			get {
				if (Priority == 1) {
					return "[!]  ";
				}
				else if (Priority == 2) {
					return "[!!] ";
				}
				else {
					return "[!!!]";
				}
			}
		}
		[BsonElement("name")]
		private string name;
		public event PropertyChangedEventHandler PropertyChanged;
		[BsonIgnore]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				NotifyPropertyChanged();
			}
		}
		[BsonElement("description")]
		private string description;
		[BsonIgnore]
		public string Description { 
			get {
				return description;
			}
			set {
				description = value;
				NotifyPropertyChanged();
			}
		}
		public virtual string IsCompletedString { get; set; }
		public override string ToString() {
			return $"[ITEM] {Name} - {Description}";
		}

		internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

