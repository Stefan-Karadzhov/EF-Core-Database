using Cinema_Database.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema_Database.ImportDTO
{
    [XmlType("Projection")]
   public class ProjectionDTO
    { 
        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }
        [Required]
        [XmlElement("MovieId")]
        public int MovieId { get; set; }

        [Required]
        [XmlElement("HallId")]
        public int HallsId { get; set; }

    }
}
