using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyStockAdviser.Data
{
    public class BuyStockAdviserDbContextFactory : IDesignTimeDbContextFactory<BuyStockAdviserContext>
    {
        private static string _connectionString;

        public BuyStockAdviserContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public BuyStockAdviserContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<BuyStockAdviserContext>();
            builder.UseSqlServer(_connectionString);

            return new BuyStockAdviserContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
