using Cinema_Database.DbCinemaContext;
using Cinema_Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Cinema_Database
{
    class Program
    {
        static void Main()
        {
            
            using (var db = new DbCimenaContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
               // db.Database.Migrate();
                
                var JsonDeserializer = new Deserializer();
                JsonDeserializer.DataImportMovies(@"C:\Users\admin\source\repos\Cinema_Database\Cinema_Database/movies.json", db);
                JsonDeserializer.DataImportHallAndSeats(@"C:\Users\admin\source\repos\Cinema_Database\Cinema_Database/halls-seats.json",db);
                var XmlDeserializer = new Deserializer();
                
                XmlDeserializer.DataImportProjectionsXml(@"C:\Users\admin\source\repos\Cinema_Database\Cinema_Database/projections.xml", db);
                XmlDeserializer.DataImportXmlTickets(@"C:\Users\admin\source\repos\Cinema_Database\Cinema_Database/customers-tickets.xml", db);
                
                var jsonSerializer = new Serializer();
                jsonSerializer.JsonMovieSerializer(db);
            }
          
        }
    }
}
