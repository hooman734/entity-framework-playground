using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Trace)
                .AddDbContext<EntityDbContext>(x =>
                    x.UseSqlite("Data Source=/Users/hooman/RiderProjects/entity-framework-playground/App/test.sqlite"))
                .BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<EntityDbContext>();

            var page = new Page {Content = "Some content"};
            var book = new Book {Name = "Amir", Pages = new List<Page> {page}};
            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            Console.WriteLine("Before change");
            foreach (var dbContextBook in dbContext.Books.Include(x => x.Pages))
            {
                Console.WriteLine(dbContextBook);
            }

            var entity = dbContext.Books.Where(x => x.Name == "Amir").FirstOrDefault() ?? throw new Exception("null");
            entity.Name = "Hooman";
            dbContext.SaveChanges();

            Console.WriteLine("After change");
            foreach (var dbContextBook in dbContext.Books.Include(x => x.Pages))
            {
                Console.WriteLine(dbContextBook);
            }

            Console.WriteLine("Hello World!");
        }
    }
}