
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema_Database.ImportDTO
{
    [XmlType("Tickets")]
   public class TicketsDTO
    {
        public float Price { get; set; }

        [Required]
        [XmlElement("ProjectionId")]
        public int ProjectionsId { get; set; }
    }
}
