using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema_Database.ExportDTO
{
    [XmlType("Customer")]
    public class ExportCustomerDTO
    {
        public string FirstName { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
       
        public string LastName { get; set; }
       
        [Range(0.01, double.MaxValue)]
        public decimal? Balance { get; set; }
    }
}
