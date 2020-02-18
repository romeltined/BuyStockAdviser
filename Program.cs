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
            DataServices dataServices = new DataServices();
            StockDownloader stockDownloader = new StockDownloader();

            var stockItems = dataServices.GetLatestStockItems();
            var result = stockDownloader.Download(stockItems);
            dataServices.AddStockItem(result);
            


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