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
	public class TMTaskController : ControllerBase {
		private readonly ILogger<TMTaskController> _logger;

		[HttpGet]
		public IEnumerable<TMTask> Get() {
			return Database.TMTasks;
		}

		[HttpGet("GetItem")]
		public TMItem GetTestItem() {
			return new TMTask();
		}

		[HttpPost("AddOrUpdate")]
		public TMItem Recieve([FromBody] TMTask task) {
			return task;
		}
	}
}
