using ExchangeSharp;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using aymk_engine.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using aymk_engine.Business;
using static aymk_engine.Util.Constants;

namespace aymk_engine
{
    public partial class Form1 : Form
    {
        public static string connectionString = "mongodb://localhost:27017";
        //public static string connectionString = "mongodb://aymk:aymk2018@ds125368.mlab.com:25368/aymkcoin";
        public static string databaseName = "aymkCoin";
        public static int previousMinute = 5;

        public List<CatalogExchange> catalogExchange;
        public List<CatalogMarket> catalogMarket;
        Repository<CatalogMarket> repoCatalogMarket;
        Repository<CatalogExchange> repoCatalogExc;
        Thread threadMarketData;
        System.Windows.Forms.Timer timerMarketData;
        bool isBusyMarketData = false;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            repoCatalogExc = new Repository<CatalogExchange>();
            repoCatalogMarket = new Repository<CatalogMarket>();


            catalogExchange = repoCatalogExc.GetAllList(); 
            catalogMarket = repoCatalogMarket.GetAllList();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", richTextBox1.Text);

                if (threadMarketData != null)
                    threadMarketData.Abort();
            }
            catch (Exception ex)
            {
                File.WriteAllText(Application.StartupPath + "\\Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", ex.Message + Environment.NewLine + richTextBox1.Text);

            }

        }
        void addLog(string message)
        {
            richTextBox1.AppendText(string.Format("{1:HH:mm:ss} {2}{0}",
                                                Environment.NewLine,
                                                DateTime.Now,
                                                message));
        }

        #region Market Data
        private void btnMarketDataStart_Click(object sender, EventArgs e)
        {
            threadMarketData = new Thread(delegate ()
            {
                // Invoke your control like this
                this.btnMarketDataStart.Invoke(new MethodInvoker(delegate ()
                {
                    timerMarketData = new System.Windows.Forms.Timer();
                    timerMarketData.Enabled = true;
                    timerMarketData.Tick += TimerMarketData_Tick;
                    timerMarketData.Start();
                }));
            });
            threadMarketData.Start();
            addLog("Market data service starting");
        }

        private void TimerMarketData_Tick(object sender, EventArgs e)
        {
            timerMarketData.Interval = (int)numericUpDown1.Value * 1000;

            try
            {
                catalogMarket = repoCatalogMarket.GetAllList();


                if (isBusyMarketData == false)
                {
                    DateTime start = DateTime.UtcNow;
                    DateTime lastPreviousDate = start.AddMinutes(-previousMinute);
                    try
                    {
                        isBusyMarketData = true;
                        ExchangeBittrexAPI api = new ExchangeBittrexAPI();
                        CatalogMarket market;
                        DateTime now = DateTime.UtcNow;
                        DateTime previousDate = now.AddMinutes(-previousMinute);
                        var tickers = api.GetTickers();
                       
                        foreach (var ticker in tickers)
                        {
                            market = catalogMarket.Where(p => p.Code == ticker.Key).SingleOrDefault();
                            if (market != null)
                            {
                                //if (market.HistoricalData == null)
                                //    market.HistoricalData = new List<ExchangeTicker>();

                                //List<ExchangeTicker> data = market.HistoricalData.Where(q => q.Volume.Timestamp > lastPreviousDate).ToList();
                                //market.HistoricalData.Clear();
                                //market.HistoricalData.AddRange(data);
                                //market.HistoricalData.Add(ticker.Value);

                                try
                                {
                                    //var update = Builders<CatalogMarket>.Update
                                    //                                  .Set("ticker", ticker.Value)
                                    //                                  .Set("historicalData", market.HistoricalData)
                                    //                                  .Set("updatedAt", DateTime.Now)
                                    //                                  .Inc("runCount", 1);
                                    var update = Builders<CatalogMarket>.Update
                                                                       .Set("active",true)
                                                                      .Set("ticker", ticker.Value) 
                                                                      .Set("updatedAt", DateTime.Now)
                                                                      .Inc("runCount", 1);
                                    repoCatalogMarket.UpdateItem(market.Id, update);
                                }
                                catch (Exception ex)
                                {
                                    throw new ArgumentException(market.Code + " : " + ex.Message);
                                }
                            }
                            else
                            {
                                addLog(string.Format("Market not found: {0}", ticker.Key));
                            }



                        }


                        isBusyMarketData = false;
                        DateTime completed = DateTime.UtcNow;
                        addLog(string.Format("Market data OK. Duration: {0:0.00}", completed.Subtract(start).TotalSeconds));

                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }


                }
                else
                {
                    addLog("Market data BUSY");
                }
            }
            catch (Exception ex)
            {
                isBusyMarketData = false;
                addLog("Market data ERROR : " + ex.Message);
            }
        }

        private void btnMarketDataShow_Click(object sender, EventArgs e)
        {
            catalogMarket = repoCatalogMarket.GetAllList();

            dataGridView1.DataSource = catalogMarket;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Refresh();
        }

        private void btnMarketDataStop_Click(object sender, EventArgs e)
        {
            if (timerMarketData != null)
                timerMarketData.Enabled = false;
            if (threadMarketData != null)
                threadMarketData.Abort();

            addLog("Market data service stopped");
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            repoCatalogMarket.DeleteAllItems();
            JToken result = new ExchangeBittrexAPI().GetSymbolsWithAllField();
            JArray array = result as JArray;

            foreach (JToken trade in array)
            {

                CatalogMarket cm = new CatalogMarket();
                cm.Active = true;
                cm.Code = trade["MarketName"].ToString();
                cm.ExchangeId = new ObjectId("5a5a76f667d6713bb4a10d69");
                cm.Exchange = catalogExchange.Where(p => p.Id == cm.ExchangeId).SingleOrDefault();
                cm.FirstCode = trade["BaseCurrency"].ToString();
                cm.SecondCode = trade["MarketCurrency"].ToString();
                cm.LogoUrl = trade["LogoUrl"].ToString();
                cm.Name = trade["MarketCurrencyLong"].ToString();
                cm.RunCount = 0;
                cm.CreatedAt = DateTime.Now;

                repoCatalogMarket.Insert(cm);
            }

            catalogMarket = repoCatalogMarket.GetAllList();
        }

        private async  void button2_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                await AlertBL.GetInstance().Run();
                addLog("Bitti : " + DateTime.Now.Subtract(now).TotalMilliseconds);
            }
         


        }
    }
}
