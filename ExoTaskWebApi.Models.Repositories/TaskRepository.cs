using ExoTaskWebApi.Models.Entities;
using ExoTaskWebApi.Models.Interfaces;
using ExoTaskWebApi.Models.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tools.Connection.Database;

namespace ExoTaskWebApi.Models.Repositories
{
    public class TaskRepository : ITaskRepository<Task>
    {
        private readonly Connection _connection;

        public TaskRepository(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Task> Get()
        {
            Command command = new Command("Select Id, Title, Done, Deleted, Created, LastModified from Task;");
            return _connection.ExecuteReader(command, dr => dr.ToTask());
        }

        public Task Insert(Task entity)
        {
            Command command = new Command("insert into Task (Title) output inserted.* values (@Title);");
            command.AddParameter("Title", entity.Title);
            return _connection.ExecuteReader(command, dr => dr.ToTask()).Single();
        }

        public bool Update(int id, Task entity)
        {
            Command command = new Command("update Task Set Title = @Title, Done = @Done, LastModified = GETDATE() where Id = @Id;");
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Done", entity.Done);
            command.AddParameter("Id", id);

            return _connection.ExecuteNonQuery(command) == 1;             
        }

        public bool Delete(int id)
        {
            Command command = new Command("Delete from Task where Id = @Id;");
            command.AddParameter("Id", id);

            return _connection.ExecuteNonQuery(command) == 1;
        }
    }
}
