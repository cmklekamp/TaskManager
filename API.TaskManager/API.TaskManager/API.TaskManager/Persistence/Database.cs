using Library.TaskManager.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace API.TaskManager.Persistence {
    public class Database {
        private IMongoDatabase _database;

        private static Database instance;
        public static Database Current {
            get {
                if (instance == null) {
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://TMUser:tmuser@taskmanagerapi.sdve1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
                    var client = new MongoClient(settings);
                    var _db = client.GetDatabase("test");
                    instance = new Database(_db);
                }

                return instance;
            }
        }

        public void AddOrUpdate(TMItem item) {
            if (string.IsNullOrEmpty(item._id)) {
                item._id = ObjectId.GenerateNewId().ToString();
            }

            //mapping for collections
            IMongoCollection<BsonDocument> collection;
            TMItem itemToPersist;
            if (item is TMTask) {
                collection = _database.GetCollection<BsonDocument>("tmtasks");
                itemToPersist = item as TMTask;
            }
            else if (item is TMAppointment) {
                collection = _database.GetCollection<BsonDocument>("tmappointments");
                itemToPersist = item as TMAppointment;
            }
            else {
                throw new TypeNotSupportedException(item.GetType().ToString());
            }


            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(itemToPersist._id)));
            collection.InsertOne(itemToPersist.ToBsonDocument());
            return;
        }

        public bool Delete(string type, string id) {
            if(type == "TMTask") {
                try {
                    IMongoCollection<BsonDocument> collection;
                    collection = _database.GetCollection<BsonDocument>("tmtasks");
                    collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)));
                }
                catch (Exception) {
                    return false;
                }

                return true;
            } else if (type == "TMAppointment") {
                try {
                    IMongoCollection<BsonDocument> collection;
                    collection = _database.GetCollection<BsonDocument>("tmappointments");
                    collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)));
                }
                catch (Exception) {
                    return false;
                }

                return true;
            } else {
                return false;
			}
        }

        public List<TMTask> TMTasks {
            get {
                var taskBson = _database.GetCollection<BsonDocument>("tmtasks");
                var data = taskBson.Find(_ => true).ToList();
                var _tmtasks = new List<TMTask>();
                foreach (var item in data) {
                    var json = item.ToJson();
                    var obj = BsonSerializer.Deserialize<TMTask>(item);
                    _tmtasks.Add(obj);
                }
                return _tmtasks;
            }

        }

        public List<TMAppointment> TMAppointments {
            get {
                var appBson = _database.GetCollection<BsonDocument>("tmappointments");
                var data = appBson.Find(_ => true).ToList();
                var _tmappts = new List<TMAppointment>();
                foreach (var item in data) {
                    var json = item.ToJson();
                    var obj = BsonSerializer.Deserialize<TMAppointment>(item);
                    _tmappts.Add(obj);
                }
                return _tmappts;
            }

        }


        private Database(IMongoDatabase db) {
            _database = db;
        }

    }

    public class TypeNotSupportedException : Exception {
        private string _type;
        public TypeNotSupportedException(string type) {
            _type = type;
        }
        public override string Message => $"Attempt was made to persist an unsupported type: {_type}";
    }
}