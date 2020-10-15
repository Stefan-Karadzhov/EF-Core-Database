

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Halls
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        public ICollection<Projections> Projections { get; set; } = new HashSet<Projections>();


        public ICollection<Seats> Seats { get; set; } = new HashSet<Seats>();

    }
}
