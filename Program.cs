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
            StockDownloader stockDownloader = new StockDownloader();
            stockDownloader.Download();

            StockAnalyzer stockAnalyzer = new StockAnalyzer();
            stockAnalyzer.Analyze();

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