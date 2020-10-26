using System;

namespace ExoTaskWebApi.Models.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        // ou public Nullable<DateTime> LastModified { get; set; }
    }
}
