

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Projections
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        [Required]
        public int MovieId { get; set; }

        public Movies Movie { get; set; }
        [Required]
        public int HallId { get; set; }

        public Halls Halls { get; set; }

        public ICollection<Tickets> Tickets { get; set; }
    }
}
