using BuyStockAdviser.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuyStockAdviser
{
    public class StockDownloader
    {
        private string _Url;
        private string _userName;
        private string _password;

        public StockDownloader()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            _Url = configuration["TiingoUrl"];
            _userName = configuration["UserName"];
            _password = configuration["Password"];
        }

        public void Download()
        {
            var stockItems = GetLatestStockItems();
            var result = GetFromTiingo(stockItems);
            AddStockItem(result);
        }

        public List<StockItem> GetFromTiingo(List<StockItem> stockItems)
        {
            List<StockItem> result = new List<StockItem>();

            foreach (StockItem c in stockItems)
            {
                var uRLrequest = string.Format(_Url, c.Symbol, DateFormatter(c.Datetime.AddDays(1)), DateFormatter(DateTime.Now));
                try
                {
                    {
                        WebRequest request = WebRequest.Create(uRLrequest);
                        request.Credentials = new NetworkCredential(_userName, _password);
                        WebResponse response = request.GetResponse();
                        Stream dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        var json = reader.ReadToEnd();
                        var model = JsonSerializer.Deserialize<List<TiingoItem>>(json);
                        foreach (TiingoItem t in model)
                        {
                            result.Add(new StockItem { Symbol = c.Symbol, Last = t.adjClose, High = t.adjHigh, Datetime = t.date, Volume = t.adjVolume, Open = t.open });
                        };
                    }
                    
                }
                catch
                {
                    return null;
                }
            }

            return result;
        }

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
                foreach (StockSymbol s in symbols)
                {
                    var stockItem = context.StockItems
                        .Where(c => c.Symbol == s.Symbol)
                        .OrderByDescending(c => c.Datetime)
                        .FirstOrDefault();

                    if (stockItem == null)
                    {
                        stockItem = new StockItem { Symbol = s.Symbol, Datetime = DateTime.Now.AddMonths(-3) };
                    }
                    result.Add(stockItem);
                }
            }
            return result;
        }

        public string DateFormatter(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
 

    }


}
