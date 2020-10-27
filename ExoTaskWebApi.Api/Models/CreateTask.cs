using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExoTaskWebApi.Api.Models
{
    public class CreateTask
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
    }
}
