using BuyStockAdviser.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyStockAdviser
{
    public class StockAnalyzer
    {
        public void Analyze()
        {
            /*get unique symbol from stockitems where decision is null
             *loop each symbol
             *get latest record from stockitems
             * check if it has entry in stockbuydecision
             * 
             * 
             * 
             */

        }

        public void AddStockBuyDecision(List<StockItem> items)
        {
            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {

            }
        }
    }
}
