using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Cinema_Database.DbCinemaContext;
using Cinema_Database.ExportDTO;
using Cinema_Database.Model;
using Newtonsoft.Json;

namespace Cinema_Database
{
   public class Serializer
    {
        public void JsonMovieSerializer(DbCimenaContext db)
        {
                
            var movies = db.Movies
                .Where(m => m.Rating >= 5 && db.Tickets.Any(t => t.Projection.MovieId == m.Id))
                .Select(mv => new
                {
                    rating = mv.Rating,
                    director = mv.Director,
                    movieName = mv.Title,
                    customers = db.Customers
                        .Where(c => db.Tickets.Any(t => t.CustomerId == c.Id && t.Projection.MovieId == mv.Id))
                        .Select(c => new
                        {
                            firstName = c.FirstName,
                            lastName = c.LastName,
                            balance = c.Balance
                        }).ToList()

                })
                .ToList();
                var serializer = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"G:\Repository\EF-Core-Database\Cinema_Database/Serializer-Movie.json", serializer);
        }



        public void XmlCustomerSerializer(DbCimenaContext db,int customerAge)
        {
            var customers = db.Customers
                .Where(c => c.Age >= customerAge)
                .Select(c => new ExportCustomerDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Balance = c.Balance
                }).ToList();
            var serializer = new XmlSerializer(typeof(List<ExportCustomerDTO>),new XmlRootAttribute("Customers"));
            TextWriter writer = new StreamWriter(@"G:\Repository\EF-Core-Database\Cinema_Database/XmlSerializer-Customer.xml");
            serializer.Serialize(writer, customers);
            writer.Close();
           
        }


    }
}
