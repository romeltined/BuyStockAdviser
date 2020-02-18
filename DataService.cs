using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BuyStockAdviser.Data;

namespace BuyStockAdviser
{
    public class DataService
    {
        public void AddStockItem(List<StockItem> list)
        {
            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {
                foreach (StockItem s in list)
                {
                    context.StockItems.Add(new StockItem
                    {
                        Symbol = s.Symbol,
                        Last = s.Last,
                        High = s.High,
                        Datetime = s.Datetime,
                        Volume = s.Volume,
                        Open = s.Open
                    });
                }
                context.SaveChanges();
            }
        }

        internal object GetNewStockItems(object stockSymbols)
        {
            throw new NotImplementedException();
        }

        internal object GetStockSymbols()
        {
            throw new NotImplementedException();
        }
    }
}
