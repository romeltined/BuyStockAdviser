using BuyStockAdviser.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Meta.Numerics.Statistics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
            UpdateStockDecision();
            PrintStockDecision();

        }

        public void UpdateStockDecision()
        {
            double[] dataright;
            double[] dataleft;
            double slopeleft = 0;
            double sloperight = 0;
            string decision = null;

            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {
                var stockItems = context.StockItems
                    .Where(s => s.Decision == null).ToList();

                foreach (StockItem s in stockItems)
                {
                    double[] data =
                    (from d in context.StockItems
                     where d.Symbol == s.Symbol
                     where d.Datetime <= s.Datetime
                     orderby d.Datetime descending
                     select d.Last).Take(7).ToArray();

                    dataright = data.Take(4).ToArray();
                    Array.Reverse(dataright);
                    Array.Reverse(data);
                    dataleft = data.Take(4).ToArray();

                    double[] x = { 1, 2, 3, 4 };

                    slopeleft = 0;
                    sloperight = 0;

                    if (dataleft.Length == 4 && dataright.Length == 4)
                    {
                        LinearRegressionResult resultleft = dataleft.LinearRegression(x);
                        //Console.WriteLine($"y = ({resultleft.Intercept}) + ({resultleft.Slope}) x");

                        LinearRegressionResult resultright = dataright.LinearRegression(x);
                        //Console.WriteLine($"y = ({resultright.Intercept}) + ({resultright.Slope}) x");

                        slopeleft = resultleft.Slope.Value;
                        sloperight = resultright.Slope.Value;
                        //double degreesleft = Math.Atan(slopeleft) * 180 / Math.PI;
                        //double degreesright = Math.Atan(sloperight) * 180 / Math.PI;

                        if ((slopeleft < 0 && sloperight > 0) || (slopeleft > 0 && sloperight > 0))
                        {
                            //Console.WriteLine(s.Name + ": Buy");
                            decision = "BUY";
                        }
                        else
                        {
                            decision = "NA";
                        }
                    }
                    else
                    {
                        decision = "NA";
                    }

                    s.Decision = decision;
                    s.SlopeLeft = slopeleft;
                    s.SlopeRight = sloperight;
                    context.Entry(s).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
        }

        public void PrintStockDecision()
        {
            string path = Directory.GetCurrentDirectory() + @"\Result";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Result folder created!");
                //Console.ReadLine();
                //return;
            }

            string variable = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace(".", "");
            TextWriter tw = new StreamWriter(path + @"\StockAdviseReport_" + variable + ".csv");
            //tw.WriteLine("Symbol,Name,Price,SlopeLeft,SlopeRight,DegreesLeft,DegreesRight");

            using (var context = new BuyStockAdviserDbContextFactory().CreateDbContext())
            {
                var symbols = context.StockItems.Select(s => s.Symbol).Distinct();

                foreach (string s in symbols)
                {
                    var stockItems = context.StockItems
                        .Where(d => d.Symbol == s)
                        .OrderByDescending(d => d.Datetime)
                        .Take(40).ToList();

                    var writeDate = "";
                    var writeDecision = "";
                    var writeLast = "";
                    var writeSymbol = "";

                    foreach (StockItem c in stockItems)
                    {
                        writeDate =  writeDate + c.Datetime.ToShortDateString() + ",";
                        writeDecision =  writeDecision + c.Decision + ",";
                        writeLast =  writeLast + c.Last.ToString() + "," ;
                        writeSymbol = writeSymbol + c.Symbol + ",";
                    }
                    tw.WriteLine(writeSymbol);
                    tw.WriteLine(writeDecision);
                    tw.WriteLine(writeLast);
                    tw.WriteLine(writeDate);
                    tw.WriteLine("");

                }
            }

            tw.Close();
        }
    }
}
