using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels.BookEntityModel;
using TheBookCave.Models;

namespace TheBookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void SeedData()
        {
            var db = new DataContext();

            if(!db.Books.Any())
            {
                var initialBooks = new List<BookEntityModel>()
                {
                    new BookEntityModel { Title = "", Author = "", ISBN10 = "", ISBN13 = "", Description = "", NumberOfPages = 0, DatePublished = new DateModel(2000, 31, 08), Publisher = "", Rating = 4.5, NumberOfRatings = 13, NumberOfCopiesSold = 120, InStock = 8, Genre="", Price = 9.99},
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
