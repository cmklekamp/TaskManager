using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using API.TaskManager.Persistence;
using Library.TaskManager.Models;
using Library.TaskManager.Persistence;
using Library.TaskManager.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.TaskManager.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class TaskController : ControllerBase {
		private readonly ILogger<TaskController> _logger;

		[HttpGet]
		public IEnumerable<TMTask> Get() {
			return Database.Current.TMTasks;
		}

		[HttpGet("GetItem")]
		public TMItem GetTestItem() {
			return new TMTask();
		}

		[HttpPost("AddOrUpdate")]
		public TMTask Receive([FromBody] TMTask task) {
			Database.Current.AddOrUpdate(task);
			return task;
		}

		[HttpPost("Delete")]
		public string Delete([FromBody] string id) {
			Database.Current.Delete("TMTask", id);
			return id;
		}

		[HttpPost("Query")]
		public IEnumerable<TMTask> Search(string query) {
			return Database.Current.TMTasks.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
						|| s.Description.ToUpper().Contains(query.ToUpper()));
		}
	}
}
