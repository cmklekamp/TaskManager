using API.TaskManager.Persistence;
using Library.TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.TaskManager.Controllers {
	[ApiController]
	[Route("[controller")]
	public class TaskController : ControllerBase {
		private readonly ILogger<TaskController> _logger;

		[HttpGet]
		public IEnumerable<Task> Get() {
			return Database.Tasks;
		}

		[HttpGet("GetItem")]
		public Item GetTestItem() {
			return new Task();
		}

		[HttpPost("AddOrUpdate")]
		public Item Recieve([FromBody] Task task) {
			return task;
		}
	}
}
