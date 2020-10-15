
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Seats
    {
        public int Id { get; set; }
        [Required]
        public int HallId { get; set; }

        public Halls Halls { get; set; }
    }
}
