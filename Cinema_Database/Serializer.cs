using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cinema_Database.DbCinemaContext;
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
                var serializer = JsonConvert.SerializeObject(movies,Formatting.Indented);
            File.WriteAllText(@"G:\Repository\EF-Core-Database\Cinema_Database/Serializer-Movie.json", serializer);
        }
    }
}
