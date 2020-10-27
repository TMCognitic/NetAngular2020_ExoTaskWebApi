using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ExoTaskWebApi.Models.Entities;
using ExoTaskWebApi.Models.Interfaces;
using ExoTaskWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Tools.Connection.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExoTaskWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskRepository<Task> _repository;

        public TaskController()
        {
            Connection connection = new Connection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoTaskWebApi;Integrated Security=True;", SqlClientFactory.Instance);
            _repository = new TaskRepository(connection);
        }

        // GET: api/Task
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _repository.Get();
        }

        // POST api/Task
        [HttpPost]
        public Task Post([FromBody] Task entity)
        {
            return _repository.Insert(entity);
        }

        // PUT api/Task/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Task entity)
        {
            return _repository.Update(id, entity);
        }

        // DELETE api/Task/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
