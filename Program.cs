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

namespace TheBookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

/*
        public static void SeedData()
        {
            var db = new DataContext();

            if(!db.Books.Any())
            {
                var initialBooks = new List<BookEntityModel>()
                {
                    new BookEntityModel { parameters }
                }

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
*/
    }
}
