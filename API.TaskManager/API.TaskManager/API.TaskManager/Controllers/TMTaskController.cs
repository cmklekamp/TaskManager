using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using API.TaskManager.Persistence;
using Library.TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.TaskManager.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class TaskController : ControllerBase {
		private object _lock = new object();
		private readonly ILogger<TaskController> _logger;

		[HttpGet]
		public IEnumerable<TMTask> Get() {
			return Database.TMTasks;
		}

		[HttpGet("GetItem")]
		public TMItem GetTestItem() {
			return new TMTask();
		}

		[HttpPost("AddOrUpdate")]
		public TMTask AddOrUpdate([FromBody] TMTask task) {
			if (task.Id <= 0) {
				lock (_lock) {
					if(Database.TMTasks.Any()) {
						var lastUsedId = Database.TMTasks.Select(t => t.Id).Max();
						task.Id = lastUsedId + 1;
						Database.TMTasks.Add(task);
					} else {
						var lastUsedId = 0;
						task.Id = lastUsedId + 1;
						Database.TMTasks.Add(task);
					}
				}
			}
			else {
				var item = Database.TMTasks.FirstOrDefault(t => t.Id == task.Id);
				var index = Database.TMTasks.IndexOf(item);
				Database.TMTasks.RemoveAt(index);
				Database.TMTasks.Insert(index, task);
			}

			return task;
		}

		[HttpPost("Delete")]
		public TMTask Delete([FromBody] TMTask task) {
			var taskToRemove = Database.TMTasks.FirstOrDefault(t => t.Id == task.Id);
			Database.TMTasks.Remove(taskToRemove);
			return task;
		}

		[HttpPost("Query")]
		public ObservableCollection<TMTask> Search([FromBody] string Query) {
			var filteredTMTasks = new ObservableCollection<TMTask>(Database.TMTasks
						.Where(s => s.Name.ToUpper().Contains(Query.ToUpper())
						|| s.Description.ToUpper().Contains(Query.ToUpper())));
			return filteredTMTasks;
		}
	}
}
