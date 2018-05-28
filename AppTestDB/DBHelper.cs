using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaTime;

namespace AppTestDB
{
    public static class DBHelper
    {
        public static void InitDb(string fileName)
        {
            try
            {
                TimeCurrencyPair initPair = new TimeCurrencyPair()
                {
                    currentTime = DateTime.Now,
                    highestBid = 3333,
                    last = 3333,
                    lowestAsk = 3333,
                    lastbaseVolume = 8888,
                    quoteVolume = 8888,
                    isFrozen = 0
                };
                Stopwatch watch = Stopwatch.StartNew();
                using (var tf = TeaFile<TimeCurrencyPair>.Create(fileName))
                {
                    tf.Write(initPair);
                }
                watch.Stop();
            }
            catch (Exception ex)
            {
                // TODO log
            }

        }

        /// <summary>
        /// Получаем последнюю команду от нейросети
        /// </summary>
        /// <param name="fileName">Название файла-хранилища команд</param>
        /// <returns></returns>
        public static TradeTaskRequest ReadLastCommand(string fileName)
        {
            TradeTaskRequest lastCommand = new TradeTaskRequest();
            try
            {
                Stopwatch watch = Stopwatch.StartNew();
                var teaCurrencyPairs = new List<TradeTaskRequest>();
                using (var tf = TeaFile<TradeTaskRequest>.OpenRead(fileName))
                {
                    lastCommand = tf.Items.Last();
                }
            }
            catch (Exception ex)
            {
                // TODO log
            }
            return lastCommand;
        }

        internal static void InitDbCommand(string fileNameCommand)
        {
            TradeTaskRequest lastCommand = new TradeTaskRequest()
            {
                command = CommandType.Buy,
                currencyPair = MoneyPair.btc_usd,
                Id =1,
                requestTime = DateTime.Now
            };
            try
            {
                using (var tf = TeaFile<TradeTaskRequest>.Create(fileNameCommand))
                {
                    tf.Write(lastCommand);
                }
            }
            catch (Exception ex)
            {
                // TODO log
            }
        }

        internal static void InitDbCommandResponse(string fileNameCommandResponse)
        {
            TradeTaskResponse lastCommandResponse = new TradeTaskResponse();
            try
            {
                using (var tf = TeaFile<TradeTaskResponse>.Create(fileNameCommandResponse))
                {
                    tf.Write(lastCommandResponse);
                }
            }
            catch (Exception ex)
            {
                // TODO log
            }
        }

        /// <summary>
        /// Сохраняем в хранилище полученные пару валют за текущее время
        /// </summary>
        /// <param name="currentPair">Пара валют</param>
        /// <param name="fileName">Названии файла</param>
        public static void WriteTick(TimeCurrencyPair currentPair, string fileName)
        {
            try
            {
                using (var tf =  TeaFile<TimeCurrencyPair>.Append(fileName))
                {
                    tf.Write(currentPair);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Сохраняем статус аккаунта после выполниения команды от нейросети
        /// </summary>
        /// <param name="fileName">Название файла-хранилища </param>
        /// <returns></returns>
        public static void WriteResponseLastCommand(TradeTaskResponse response,string fileName)
        {
            TradeTaskResponse lastCommandResponse = new TradeTaskResponse();
            try
            {
                using (var tf = TeaFile<TradeTaskResponse>.Append(fileName))
                {
                    tf.Write(lastCommandResponse);
                }
            }
            catch (Exception ex)
            {
                // TODO log
            }
        }
        

        public static List<TimeCurrencyPair> TESTCreate_TeaTimeCurrencyPairs()
        {
            try
            {
                var random_price = new System.Random();
                int count_elements = 3;
                var currencyPairs = new List<TimeCurrencyPair>(count_elements);
                DateTime time = DateTime.ParseExact("01/01/2018 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                for (int i = 0; i < count_elements; i++)
                {
                    time = time.AddSeconds(5);
                    currencyPairs.Add(new TimeCurrencyPair()
                    {

                        currentTime = time,
                        highestBid = random_price.Next(18000), // random price 0-18000$
                        last = random_price.Next(18000),
                        lowestAsk = random_price.Next(18000),
                        lastbaseVolume = random_price.Next(18000),
                        quoteVolume = random_price.Next(18000),
                        isFrozen = 0
                    });
                }
                return currencyPairs;
            }
            catch (Exception ex)
            {
                // TODO log
            }
            return null;
        }
        
        public static List<TradeTaskRequest> TESTCreate_TeaTimeCommands()
        {
            try
            {
                var random_value = new System.Random();
                int count_elements = 3;
                var currencyPairs = new List<TradeTaskRequest>(count_elements);
                DateTime time = DateTime.ParseExact("01/01/2018 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                for (int i = 0; i < count_elements; i++)
                {
                    time = time.AddSeconds(5);
                    currencyPairs.Add(new TradeTaskRequest()
                    {
                        currencyPair = MoneyPair.btc_usd,
                        requestTime = time,
                        command = (CommandType)random_value.Next(3),
                    });
                }
                return currencyPairs;
            }
            catch (Exception ex)
            {
                // TODO log
            }
            return null;
        }

        //private static Random random = new Random();
        //public static char[] RandomPair()
        //{
        //    int length = 6;
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    string pair = new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //    return pair.Insert(3,"_").ToCharArray();
        //}

        public static void TESTReadDb(string fileName)
        {
            try
            {
                Stopwatch watch = Stopwatch.StartNew();
                var teaCurrencyPairs = new List<TimeCurrencyPair>();
                using (var tf = TeaFile<TimeCurrencyPair>.OpenRead(fileName))
                {
                    watch.Stop();
                    Console.WriteLine($"loaded CurrencyPairs {tf.Items.Count} - time:{watch.ElapsedMilliseconds} ms ");
                    // see for "how use to" 
                    // https://github.com/discretelogics/TeaFiles.Net-time-series-storage-in-flat-files/tree/master/Examples
                    // :)

                }
            }
            catch (Exception ex)
            {
               // TODO log
            }
        }

        
    }
}
