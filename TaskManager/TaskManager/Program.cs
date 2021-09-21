using System;
using Library.TaskManager;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace TaskManager {
	class Program {
		static void Main(string[] args) {
			// List to hold the items.
			List<Item> itemList = null;

			// Loads the last save data or creates a new list if no save data found.
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if (File.Exists($"{path}\\SaveData.json")) {
				JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
				itemList = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText($"{path}\\SaveData.json"), settings);
			} else {
				itemList = new List<Item>();
			}

			// List to hold the items.
			/// List<Item> itemList = new List<Item>();

			// Creates a list navigator to assist with the paging function.
			var itemNavigator = new ListNavigator<Item>(itemList, 5);

			// Introduces the user to the Task Manager program.
			Console.WriteLine("Welcome to the Task Manager!");
			Console.WriteLine("Here, you can keep track of tasks and appointments by storing, editing, and deleting them.");

			// Main menu loop.
			bool cont = true;
			while (cont) {
				// Prints the main menu and all of its options.
				Console.WriteLine("MAIN MENU:");
				Console.WriteLine("1. Create a new item.");
				Console.WriteLine("2. Delete an existing item.");
				Console.WriteLine("3. Edit an existing iteem.");
				Console.WriteLine("4. Complete an item.");
				Console.WriteLine("5. List all items.");
				Console.WriteLine("6. List all outstanding items.");
				Console.WriteLine("7. Search the list.");
				Console.WriteLine("8. Exit Task Manager.");

				// Reads in the user input and executes the correct option.
				if (int.TryParse(Console.ReadLine(), out int choice)) {
					switch (choice) {
						case 1:
							// Create an item.
							CreateOrEditItem(itemList);
							break;
						case 2:
							// Delete an existing item.
							Console.WriteLine("Which item would you like to delete?");
							PrintList(itemNavigator);
							if (int.TryParse(Console.ReadLine(), out int deleteChoice)) {
								var deleteItem = itemList.FirstOrDefault(t => t.ID == deleteChoice);
								itemList.Remove(deleteItem);
								Console.WriteLine("Item deleted.");
							} else {
								Console.WriteLine("Invalid choice, try again.");
							}
							break;
						case 3:
							// Edit an item.
							Console.WriteLine("Which item would you like to edit?");
							PrintList(itemNavigator);
							if (int.TryParse(Console.ReadLine(), out int editChoice)) {
								var editItem = itemList.FirstOrDefault(t => t.ID == editChoice);
								CreateOrEditItem(itemList, editItem);
							}
							break;
						case 4:
							// Complete an item.
							Console.WriteLine("Which item would you like to complete?");
							PrintList(itemNavigator);
							if (int.TryParse(Console.ReadLine(), out int completeChoice)) {
								var completeItem = itemList.FirstOrDefault(t => t.ID == completeChoice);
								if (completeItem != null) {
									completeItem.IsCompleted = true;
									completeItem.IsCompletedString = "COMPLETE";
									Console.WriteLine("Item completed.");
								} else {
									Console.WriteLine("Item does not exist.");
								}
							}
							else {
								Console.WriteLine("Invalid choice, try again.");
							}
							break;
						case 5:
							// List all items.
							if (itemList.Count != 0) {
								Console.WriteLine("All Items:");
								PrintList(itemNavigator);
							} else {
								Console.WriteLine("There are no items currently in this list.");
							}
							break;
						case 6:
							// List completed items.
							// Creates a temporary new list of only currently incomplete items.
							List<Item> newIncompleteList = new List<Item>();
							foreach (var item in itemList) {
								if (!(item.IsCompleted))
									newIncompleteList.Add(item);
							}
							var IncompleteNav = new ListNavigator<Item>(newIncompleteList, 5);
							PrintList(IncompleteNav);
							break;
						case 7:
							// Search the list.
							// Records the string the user wants to search for and searches it.
							// Code inspired by https://www.tutorialsteacher.com/linq/linq-query-syntax article.
							Console.WriteLine("What would you like to search for?");
							var searchTerm = Console.ReadLine();
							var result = from s in itemList
										 where s.Name.Contains(searchTerm)
										 || s.Description.Contains(searchTerm)
										 || (s as Appointment != null
										 && (s as Appointment).Attendees.Contains(searchTerm))
										 select s;
							// Prints the results of the attempted search.
							if (result.Count() != 0) {
								foreach (var item in result) {
									Console.WriteLine(item);
								}
							} else {
								Console.WriteLine("No items matched your search.");
							}
							break;
						case 8:
							// Exit Task Manager and save list to a JSON file.
							cont = false;
							Console.WriteLine("Saving...\n");
							JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
							File.WriteAllText($"{path}\\SaveData.json", JsonConvert.SerializeObject(itemList, settings));
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

		// Function to create or edit an item.
		public static void CreateOrEditItem(List<Item> itemList, Item item = null) {
			// Treats the function differently if the item is new or being edited.
			bool isNewItem = false;
			if (item == null) {
				isNewItem = true;
			}
			// Bool to check if task or appointment.
			bool isTask = false;
			// If isNewItem is true, ask if task or appointment and create the correct one, recording the type.
			if (isNewItem) {
				Console.WriteLine("Task or appointment?");
				Console.WriteLine("1. Task");
				Console.WriteLine("2. Appointment");
				if (int.TryParse(Console.ReadLine(), out int createChoice)) {
					switch (createChoice) {
						case 1:
							item = new Task();
							isTask = true;
							break;
						case 2:
							item = new Appointment();
							break;
						default:
							Console.WriteLine("Invalid choice, try again.");
							return;
					}
				}
			// If isNewItem is false, check the type of the given item.
			} else {
				if (item is Task) {
					isTask = true;
				}
			}
			// Now inputs the data based on if the create/edit item is a task or an appointment.
			if (isTask) {
				Console.WriteLine("What is the desired name?");
				item.Name = Console.ReadLine();
				Console.WriteLine("What is the desired description?");
				item.Description = Console.ReadLine();
				item.IsCompleted = false;
				item.IsCompletedString = "INCOMPLETE";
				Console.WriteLine("What is the desired deadline?");
				DateTime.TryParse(Console.ReadLine(), out DateTime taskDeadline);
				(item as Task).Deadline = taskDeadline;
				if (isNewItem) {
					itemList.Add(item);
				}
				Console.WriteLine("Task created.");
			} else {
				Console.WriteLine("What is the desired name?");
				item.Name = Console.ReadLine();
				Console.WriteLine("What is the desired description?");
				item.Description = Console.ReadLine();
				item.IsCompleted = false;
				item.IsCompletedString = "INCOMPLETE";
				Console.WriteLine("What is the desired start time?");
				DateTime.TryParse(Console.ReadLine(), out DateTime apptStart);
				(item as Appointment).Start = apptStart;
				Console.WriteLine("What is the desired stop time?");
				DateTime.TryParse(Console.ReadLine(), out DateTime apptStop);
				(item as Appointment).Stop = apptStop;
				Console.WriteLine("What are the names of the attendees?");
				Console.WriteLine("Enter DONE to finish adding.");
				bool isDone = false;
				while (!isDone) {
					string attendee = Console.ReadLine();
					if (String.Equals(attendee, "DONE", StringComparison.OrdinalIgnoreCase)) {
						isDone = true;
					}
					else {
						(item as Appointment).Attendees.Add(attendee);
					}
				}
				if (isNewItem) {
					itemList.Add(item);
				}
				Console.WriteLine("Appointment created.");
			}
		}

		// Prints the items in a list, either including completed items or not.
		public static void PrintList(ListNavigator<Item> itemNavigator, int incComplete = 1) {
			bool isNavigating = true;
			while (isNavigating) {
				var page = itemNavigator.GetCurrentPage();
				foreach (var item in page) {
					Console.WriteLine(item.ToString());
				}

				// Handles prompts and input for previous and next pages.
				// If there is no next or previous page, just complete viewing the list to avoid confusion.
				if (itemNavigator.HasPreviousPage || itemNavigator.HasNextPage) {
					// Displays "previous" and/or "next" page prompt.
					if (itemNavigator.HasPreviousPage) {
						Console.WriteLine("P. Previous Page");
					}
					if (itemNavigator.HasNextPage) {
						Console.WriteLine("N. Next Page");
					}
					
					// Handles user selection.
					var selection = Console.ReadLine();
					if (selection.Equals("P", StringComparison.InvariantCultureIgnoreCase)) {
						itemNavigator.GoBackward();
					} else if (selection.Equals("N", StringComparison.InvariantCultureIgnoreCase)) {
						itemNavigator.GoForward();
					} else {
						isNavigating = false;
					}
				} else {
					isNavigating = false;
				}
			}
		}
	}
}

