

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Customers
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string LastName { get; set; }
        [Range(12,110)]
        [Required]
        public int Age { get; set; }
        [Required]
        [Range(0.01,double.MaxValue)]
        public decimal? Balance { get; set; }

        public ICollection<Tickets> Tickets { get; set; } = new HashSet<Tickets>();


    }
}
