using System;
using Library.TaskManager;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager {
	class Program {
		static void Main(string[] args) {
			// List to hold the tasks.
			var taskList = new List<Task>();
			// Introduces the user to the Task Manager program.
			Console.WriteLine("Welcome to the Task Manager!");
			Console.WriteLine("Here, you can keep track of tasks by storing, editing, and deleting them.");

			// Main menu loop.
			bool cont = true;
			while (cont) {
				// Prints the main menu and all of its options.
				Console.WriteLine("MAIN MENU:");
				Console.WriteLine("1. Create a new task.");
				Console.WriteLine("2. Delete an existing task.");
				Console.WriteLine("3. Edit an existing task.");
				Console.WriteLine("4. Complete a task.");
				Console.WriteLine("5. List all tasks.");
				Console.WriteLine("6. List all outstanding tasks.");
				Console.WriteLine("7. Exit Task Manager.");

				// Reads in the user input and executes the correct option.
				if (int.TryParse(Console.ReadLine(), out int choice)) {
					switch (choice) {
						case 1:
							// Create a new task.
							var newTask = new Task();
							Console.WriteLine("What is the name of the task?");
							newTask.Name = Console.ReadLine();
							Console.WriteLine("What is the task description?");
							newTask.Description = Console.ReadLine();
							Console.WriteLine("What is the task's deadline?");
							DateTime.TryParse(Console.ReadLine(), out DateTime Deadline);
							newTask.Deadline = Deadline;
							newTask.IsCompleted = false;
							taskList.Add(newTask);
							Console.WriteLine("Task created.");
							break;
						case 2:
							// Delete an existing task.
							Console.WriteLine("Which task would you like to delete?");
							foreach (var task in taskList) {
								Console.WriteLine(task.ToString());
							}
							if (int.TryParse(Console.ReadLine(), out int deleteChoice)) {
								var deleteTask = taskList.FirstOrDefault(t => t.ID == deleteChoice);
								taskList.Remove(deleteTask);
								Console.WriteLine("Task deleted.");
							} else {
								Console.WriteLine("Invalid choice, try again.");
							}
							break;
						case 3:
							// Edit an existing task.
							Console.WriteLine("Which task would you like to edit?");
							foreach (var task in taskList) {
								Console.WriteLine(task.ToString());
							}
							if (int.TryParse(Console.ReadLine(), out int editChoice)) {
								var editTask = taskList.FirstOrDefault(t => t.ID == editChoice);
								if (editTask != null) {
									Console.WriteLine("What would you like the new name to be?");
									editTask.Name = Console.ReadLine();
									Console.WriteLine("What would you like the new description to be?");
									editTask.Description = Console.ReadLine();
									Console.WriteLine("What would you like the new deadline to be?");
									DateTime.TryParse(Console.ReadLine(), out DateTime editDeadline);
									editTask.Deadline = editDeadline;
									Console.WriteLine("Task edited.");
								} else {
									Console.WriteLine("Task does not exist.");
								}
							}
							else {
								Console.WriteLine("Invalid choice, try again.");
							}
							break;
						case 4:
							// Complete a task.
							Console.WriteLine("Which task would you like to complete?");
							foreach (var task in taskList) {
								Console.WriteLine(task.ToString());
							}
							if (int.TryParse(Console.ReadLine(), out int completeChoice)) {
								var completeTask = taskList.FirstOrDefault(t => t.ID == completeChoice);
								if (completeTask != null) {
									completeTask.IsCompleted = true;
									Console.WriteLine("Task completed.");
								} else {
									Console.WriteLine("Task does not exist.");
								}
							}
							else {
								Console.WriteLine("Invalid choice, try again.");
							}
							break;
						case 5:
							// List all tasks.
							foreach (var task in taskList) {
								Console.WriteLine(task.ToString());
							}
							break;
						case 6:
							// List all outstanding tasks.
							foreach (var task in taskList) {
								if (!task.IsCompleted)
									Console.WriteLine(task.ToString());
							}
							break;
						case 7:
							// Exit Task Manager.
							cont = false;
							break;
						default:
							Console.WriteLine("Invalid choice, try again.");
							break;
					}
				} else {
					Console.WriteLine("Invalid choice, try again.");
				}
			}

			Console.WriteLine("Thank you for using the Task Manager! Goodbye!");
		}
	}
}
