using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema_Database.ImportDTO
{
    [XmlType("Customer")]
   public class CustomerTicketsDTO
    {
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
        public decimal? Balance { get; set; }
        [XmlArray("Tickets"), XmlArrayItem("Ticket")]
        public HashSet<TicketsDTO> Tickets { get; set; }
    }
}
