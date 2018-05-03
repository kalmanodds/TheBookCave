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
                    new BookEntityModel { Title = "Test", Author = "Atli Gislason", ISBN10 = "1234", ISBN13 = "1234", Description = "yeet", NumberOfPages = 5, DatePublished = new DateModel(1999, 12, 5), Publisher = "Atli Publishers", Rating = 4.7, NumberOfRatings = 100, NumberOfCopiesSold = 10, InStock = 3 },
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
