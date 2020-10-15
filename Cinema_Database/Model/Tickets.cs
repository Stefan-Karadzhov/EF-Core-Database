
using System.ComponentModel.DataAnnotations;

namespace Cinema_Database.Model
{
   public class Tickets
    {
        public int Id { get; set; }

        public float Price { get; set; }
        [Required]
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
        [Required]
        public int ProjectionsId { get; set; }

        public Projections Projection { get; set; }
    }
}
