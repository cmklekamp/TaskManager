using Library.TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.TaskManager.Persistence {
    public static class Database {
        public static ObservableCollection<TMTask> TMTasks { get; set; } = new ObservableCollection<TMTask>();
        public static ObservableCollection<TMAppointment> TMAppointments { get; set; } = new ObservableCollection<TMAppointment>();




    }
}