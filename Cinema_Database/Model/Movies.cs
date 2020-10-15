
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Movies
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(20), MinLength(3)]
        [Required]
        public string Title { get; set; }

        public TimeSpan TimeSpan { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Range(3,20)]
        [Required]
        public double Rating { get; set; }
        [MaxLength(20), MinLength(3)]
        [Required]
        public string Director { get; set; }

        public ICollection<Projections> Projections { get; set; }
    }
}
