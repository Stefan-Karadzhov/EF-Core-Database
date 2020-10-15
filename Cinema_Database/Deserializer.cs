using Cinema_Database.DbCinemaContext;
using Cinema_Database.ImportDTO;
using Cinema_Database.Model;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace Cinema_Database
{
   public class Deserializer
    {

        public static bool IsValid(object obj)
        {
            var validatorResult = new List<ValidationResult>();
            var validator = new ValidationContext(obj);

            var res = Validator.TryValidateObject(obj, validator, validatorResult, validateAllProperties: true);
            return res;
        }

        public void DataImportMovies(string inputJson, DbCimenaContext db)
        {
            var ReadedFile = File.ReadAllText(inputJson);
            var jsonFile = JsonConvert.DeserializeObject<MovieDTO[]>(ReadedFile);
            foreach (var dto in jsonFile)
            {
                if (IsValid(dto))
                {
                    var movie = new Movies
                    {
                        Title = dto.Title,
                        TimeSpan = dto.Duration,
                        Rating= dto.Rating,
                        Director = dto.Director,
                    };
                    db.Movies.Add(movie);
                }
            }
            db.SaveChanges();
        }

        public void DataImport(string inputJson,DbCimenaContext db)
        {
            var ReadedFile = File.ReadAllText(inputJson);
            var jsonFile = JsonConvert.DeserializeObject<HallDTO[]>(ReadedFile);
            foreach (var dto in jsonFile)
            {
                if (IsValid(dto))
                {
                    var hallObject = new Halls
                    {
                        Name = dto.Name,
                        Is3D = dto.Is3D,
                        Is4Dx = dto.Is4Dx,
                    };
                    db.Halls.Add(hallObject);
                    db.SaveChanges();
                    ImportSeats(db, hallObject.Id, dto.Seats);
                    
                }
            }
            db.SaveChanges();
        }

        private static void ImportSeats(DbCimenaContext db, int hallid, int seatsCount)
        {
            var seatList = new List<Seats>();
            for (int i = 0; i < seatsCount; i++)
            {
                seatList.Add(new Seats { HallId = hallid });
            }
            db.Seats.AddRange(seatList);
           
        }

        public void DataImportXml(string inputXML, DbCimenaContext db)
        {
            var serializer = new XmlSerializer(typeof(ProjectionDTO[]),new XmlRootAttribute("Projections"));
            var objects = (ProjectionDTO[])serializer.Deserialize(new StreamReader(inputXML));
            var projections = new List<Projections>();
            foreach (var dto in objects)
            {
                if (IsValid(dto) && IsValidMovieId(db,dto.MovieId) && IsValidHallId(db,dto.HallsId))
                {
                    var projection = new Projections()
                    {
                        MovieId = dto.MovieId,
                        HallId = dto.HallsId,
                        DateTime = DateTime.ParseExact(dto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    };

                    projections.Add(projection);
                }
            }
            db.Projections.AddRange(projections);
            db.SaveChanges();
        }

        public void DataImportXmlTickets(string inputXML, DbCimenaContext db)
        {

            var serializer = new XmlSerializer(typeof(CustomerTicketsDTO[]),new XmlRootAttribute("Customers"));
            var objects = (CustomerTicketsDTO[])serializer.Deserialize(new StreamReader(inputXML));
            var customers = new List<Customers>();
            foreach (var dto in objects)
            {
                if (IsValid(dto))
                {
                    var customer = new Customers
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        Balance = dto.Balance,
                    };
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    ImportTickets(db, customer.Id,dto.Tickets);
                }
            }


        }

        private void ImportTickets(DbCimenaContext db, int id, HashSet<TicketsDTO> tickets)
        {
            foreach (var ticketObj in tickets)
            {
                if (IsValid(ticketObj) && IsValidProjectionId(db,ticketObj.ProjectionsId))
                {
                    var ticket = new Tickets
                    {
                        Price = ticketObj.Price,
                        ProjectionsId = ticketObj.ProjectionsId,
                        CustomerId = id,
                    };
                    db.Tickets.Add(ticket);
                }
                
            }
            db.SaveChanges();
        }

        private bool IsValidProjectionId(DbCimenaContext context, int projectionsId)
        {
            return context.Projections.Any(p => p.Id == projectionsId);
        }

        private bool IsValidMovieId(DbCimenaContext context, int MovieId)
        {
            return context.Movies.Any(m => m.Id == MovieId);
        }

        private bool IsValidHallId(DbCimenaContext context, int HallId)
        {
            return context.Halls.Any(h => h.Id == HallId);
        }
    }
}
