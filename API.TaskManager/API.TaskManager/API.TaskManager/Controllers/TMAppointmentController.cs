using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using API.TaskManager.Persistence;
using Library.TaskManager.Models;

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
            return Database.Current.TMAppointments;
        }

        [HttpPost("AddOrUpdate")]
        public TMAppointment Receive([FromBody] TMAppointment appointment) {
            Database.Current.AddOrUpdate(appointment);
            return appointment;
        }

        [HttpGet("Delete/{id}")]
        public string Delete(string id) {
            Database.Current.Delete("TMAppointment", id);
            return id;
        }

		[HttpPost("Query")]
		public IList<TMAppointment> Search(QueryDTO query) {
			return Database.Current.TMAppointments.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
						|| s.Description.ToUpper().Contains(query.ToUpper())
						|| (s as TMAppointment != null
						&& (s as TMAppointment).Attendees.ToUpper().Contains(query.ToUpper()))).ToList();
		}
	}
}
