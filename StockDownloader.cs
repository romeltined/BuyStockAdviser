﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuyStockAdviser
{
    public class StockDownloader
    {
        private List<StockItem> _symbols;
        private string _Url;
        private string _userName;
        private string _password;
        public StockDownloader(List<StockItem> symbols, string Url, string userName, string password)
        {
            _symbols = symbols;
            _Url = Url;
            _userName = userName;
            _password = password;
        }

        public List<StockItem> Download()
        {
            List<StockItem> result = new List<StockItem>();

            foreach(StockItem c in _symbols)
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
                        foreach(TiingoItem t in model)
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

        public string DateFormatter(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
 

    }


}
