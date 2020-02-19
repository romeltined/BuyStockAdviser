using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyStockAdviser.Data
{
    public class BuyStockAdviserContext : DbContext
    {
        public BuyStockAdviserContext(DbContextOptions<BuyStockAdviserContext> options) : base(options)
        {
        }

        public DbSet<StockSymbol> StockSymbols { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

    }
}
