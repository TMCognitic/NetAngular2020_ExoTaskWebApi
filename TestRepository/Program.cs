using ExoTaskWebApi.Models.Entities;
using ExoTaskWebApi.Models.Interfaces;
using ExoTaskWebApi.Models.Repositories;
using System;
using System.Data.SqlClient;
using Tools.Connection.Database;

namespace TestRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection connection = new Connection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoTaskWebApi;Integrated Security=True;", SqlClientFactory.Instance);
            ITaskRepository<Task> repository = new TaskRepository(connection);

            Task task = repository.Insert(new Task() { Title = "Prendre la pause de midi" });
            Console.WriteLine($"{task.Id} : {task.Title} (Created : {task.Created})");

            task.Done = true;
            repository.Update(task.Id, task);

            foreach(Task t in repository.Get())
                Console.WriteLine($"{t.Id} : {t.Title} {((t.Done)? "- Done" : "")} (Created : {t.Created} | LastModified : {t.LastModified}.{t.LastModified?.Millisecond})");

            repository.Delete(task.Id);

            foreach (Task t in repository.Get())
                Console.WriteLine($"{t.Id} : {t.Title} {((t.Deleted) ? "- Deleted" : "")} (Created : {t.Created} | LastModified : {t.LastModified}.{t.LastModified?.Millisecond})");



        }
    }
}
