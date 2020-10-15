using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema_Database.ImportDTO
{
   public class MovieDTO
    {
        
        [MaxLength(20), MinLength(3)]
        [Required]
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }
        [Required]
        public string Genre { get; set; }
        [Range(3, 20)]
        [Required]
        public double Rating { get; set; }
        [MaxLength(20), MinLength(3)]
        [Required]
        public string Director { get; set; }

   }
}
