using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using ExoTaskWebApi.Api.Models;
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
        public IActionResult Post([FromBody] CreateTask entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.Title.ToUpper().Contains("SEX"))
                    return BadRequest(new { ErrorMessage = "Le titre contient des mots explicitement interdits par la bienséance" });

                return Ok(_repository.Insert(new Task() { Title = entity.Title }));                
            }
            return BadRequest();
        }

        // PUT api/Task/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditTask entity)
        {
            if (ModelState.IsValid)
            {
                if (id != entity.Id)
                    return Forbid();

                return Ok(_repository.Update(id, new Task() { Title = entity.Title, Done = entity.Done }));
            }

            return BadRequest();
        }

        // DELETE api/Task/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
