using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BuyStockAdviser.Data;

namespace BuyStockAdviser
{
    public class DataServices
    {
        public void AddStockItem(List<StockItem> list)
        {
            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {
                foreach (StockItem s in list)
                {
                    context.StockItems.Add(s);
                }
                context.SaveChanges();
            }
        }

        public List<StockItem> GetLatestStockItems()
        {
            List<StockItem> result = new List<StockItem>();
            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {
                var symbols = context.StockSymbols.ToList();
                foreach(StockSymbol s in symbols)
                {
                    var stockItem = context.StockItems
                        .Where(c => c.Symbol == s.Symbol)
                        .OrderByDescending(c => c.Datetime)
                        .FirstOrDefault();

                    if(stockItem == null)
                    {
                        stockItem = new StockItem { Symbol = s.Symbol, Datetime = DateTime.Now.AddMonths(-3) };
                    }
                    result.Add(stockItem);
                }
            }
            return result;
        }
    }
}
