using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using API.TaskManager.Persistence;
using Library.TaskManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.ToDoApplication.Controllers {
	[ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase {
        private object _lock = new object();
        
        [HttpGet("GetItem")]
        public TMItem GetTestItem() {
            return new TMAppointment();
        }

        [HttpGet]
        public IEnumerable<TMAppointment> Get() {
            return Database.TMAppointments;
        }

        [HttpPost("AddOrUpdate")]
        public TMAppointment AddOrUpdate([FromBody] TMAppointment appointment) {
            if (appointment.Id <= 0) {
                lock (_lock) {
                    if (Database.TMAppointments.Any()) {
                        var lastUsedId = Database.TMAppointments.Select(t => t.Id).Max();
                        appointment.Id = lastUsedId + 1;
                        Database.TMAppointments.Add(appointment);
                    }
                    else {
                        var lastUsedId = 0;
                        appointment.Id = lastUsedId + 1;
                        Database.TMAppointments.Add(appointment);
                    }
                }
            }
            else {
                var item = Database.TMAppointments.FirstOrDefault(t => t.Id == appointment.Id);
                var index = Database.TMAppointments.IndexOf(item);
                Database.TMAppointments.RemoveAt(index);
                Database.TMAppointments.Insert(index, appointment);
            }

            return appointment;
        }

        [HttpPost("Delete")]
        public TMAppointment Delete([FromBody] TMAppointment appt) {
            var apptToRemove = Database.TMAppointments.FirstOrDefault(a => a.Id == appt.Id);
            Database.TMAppointments.Remove(apptToRemove);
            return appt;
        }

        [HttpPost("Query")]
        public ObservableCollection<TMAppointment> Search([FromBody] string Query) {
            var filteredTMAppointments = new ObservableCollection<TMAppointment>(Database.TMAppointments
                        .Where(s => s.Name.ToUpper().Contains(Query.ToUpper())
                        || s.Description.ToUpper().Contains(Query.ToUpper())
                        || (s as TMAppointment != null
                        && (s as TMAppointment).Attendees.ToUpper().Contains(Query.ToUpper()))).ToList());
            return filteredTMAppointments;
        }
    }
}
