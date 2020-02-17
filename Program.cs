using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using BuyStockAdviser.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace BuyStockAdviser
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            var Url = configuration["TiingoUrl"];
            var userName = configuration["UserName"];
            var password = configuration["Password"];

            List<StockItem> stockItems = new List<StockItem>();
            stockItems.Add(new StockItem { Datetime = DateTime.Parse("2020-02-13"), Symbol = "AMD" });
            stockItems.Add(new StockItem { Datetime = DateTime.Parse("2020-01-13"), Symbol = "ACN" });


            StockDownloader stockDownloader = new StockDownloader(stockItems, Url, userName, password);
            var result = stockDownloader.Download();


        }
    }
}




//using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
//{
//    var releases = context.StockSymbols.Take(5).ToList();

//    foreach (var release in releases)
//    {
//        Console.WriteLine(release.Symbol);
//    }
//}