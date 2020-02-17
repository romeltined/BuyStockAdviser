using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyStockAdviser
{
    //public class EFContext : DbContext
    //{
    //    private string connectionString;
    //    //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";
    //    public EFContext()
    //    {
    //        IConfiguration config = new ConfigurationBuilder()
    //        .AddJsonFile("appsettings.json", true, true)
    //        .Build();

    //        connectionString = config["BuyStockAdviserContext"];
    //    }
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }

    //    public DbSet<StockSymbol> StockSymbols { get; set; }
    //    public DbSet<StockData> StockDatas { get; set; }

    //}
    //public class StockDbContext : DbContext
    //{
    //    private string Connection;
    //    public DbSet<StockSymbol> StockSymbols { get; set; }
    //    public DbSet<StockData> StockDatas { get; set; }

    //    public StockDbContext(string connection)
    //    {
    //        Connection = connection;
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer(Connection);
    //    }

    //}

    public class StockItem
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public string Symbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Last { get; set; }
        public double Volume { get; set; }
    }

    public class StockSymbol
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
    }

    public class StockBuyDecision
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Datetime { get; set; }
        public string Decision { get; set; }
        public double Last { get; set; }

        public double SlopeLeft { get; set; }

        public double SlopeRight { get; set; }
    }


    public class TiingoItem
    {
        public DateTime date { get; set; }
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double open { get; set; }
        public double volume { get; set; }
        public double adjClose { get; set; }
        public double adjHigh { get; set; }
        public double adjVolume { get; set; }
        public double divCash { get; set; }
        public double splitFactor { get; set; }
    }
}
