using API.TaskManager.Persistence;
using Library.TaskManager.Models;
using Library.TaskManager.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.ToDoApplication.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase {
        private object _lock;
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
                    var lastUsedId = Database.TMAppointments.Select(a => a.Id).Max();
                    appointment.Id = lastUsedId + 1;
                    Database.TMAppointments.Add(appointment);
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
    }
}
