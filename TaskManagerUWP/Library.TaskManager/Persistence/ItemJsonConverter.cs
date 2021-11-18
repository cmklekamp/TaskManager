using Library.TaskManager.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TaskManager.Persistence {
    public class ItemJsonConverter : JsonCreationConverter<TMItem> {
        protected override TMItem Create(Type objectType, JObject jObject) {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["Attendees"] != null || jObject["attendees"] != null) {
                return new TMAppointment();
            }
            else if (jObject["Deadline"] != null || jObject["deadline"] != null) {
                return new TMTask();
            }
            else {
                return new TMItem();
            }
        }
    }
}