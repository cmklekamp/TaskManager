using Library.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.TaskManager.Persistence {
    public static class Database {
        public static ObservableCollection<TMTask> TMTasks { get; set; } = new ObservableCollection<TMTask> {
            new TMTask { Name = "Task 1",
                       Description = "Task Desc 1"},
            new TMTask { Name = "Task 2",
                       Description = "Task Desc 2"}
        };
        public static ObservableCollection<TMAppointment> TMAppointments { get; set; } = new ObservableCollection<TMAppointment>
        {
            new TMAppointment { Name = "Appt 1",
                       Description = "Appt Desc 1"},
            new TMAppointment { Name = "Appt 2",
                       Description = "Appt Desc 2"}
        };




    }
}