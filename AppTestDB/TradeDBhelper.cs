using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using TeaTime;
//using PoloniexBot;

namespace BotsDBhelperDecimal
{
    public static class DecimalTradeDBhelper
    {
        internal const string baseFileName = "CurrencyPairs_.tea";
        internal const string fileNameCommand = "Commands.tea";
        internal const string fileNameCommandResponse = "ResponseCommands.tea";
        

        /// <summary>
        /// Получаем последнюю команду от нейросети
        /// </summary>
        /// <returns></returns>
        public static TradeTaskRequest ReadLastCommand()
        {
            if (!File.Exists(fileNameCommand))
            {
                InitDbCommand(fileNameCommand);
            }
            TradeTaskRequest lastCommand = new TradeTaskRequest();
            try
            {
                Stopwatch watch = Stopwatch.StartNew();
                var teaCurrencyPairs = new List<TradeTaskRequest>();
                using (var tf = TeaFile<TradeTaskRequest>.OpenRead(fileNameCommand))
                {
                    lastCommand = tf.Items.Last();
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Произошла ошибка! Не удалось прочитать последнюю команду от нейросети");
            }
            return lastCommand;
        }

        /// <summary>
        /// Сохраняем в хранилище полученные пару валют за текущее время
        /// </summary>
        /// <param name="currentPair">Пара валют</param>
        /// <param name="fileName">Названии файла</param>
        public static void WriteTick(TimeCurrencyPair currentPair, MoneyPair pair)
        {
            var fileName = CreateFileName(pair);
            if (!File.Exists(fileName))
            {
                InitDb(fileName);
            }
            try
            {
                using (var tf = TeaFile<TimeCurrencyPair>.Append(fileName))
                {
                    tf.Write(currentPair);
                }
            }
            catch (Exception ex)
            {
               //Logger.Log.Error("Произошла ошибка! База недоступна. Информация не записана");
            }
        }

        /// <summary>
        /// Сохраняем информацию о балансе после выполнения команды от нейросети
        /// </summary>
        /// <returns></returns>
        public static void WriteResponseLastCommand(TradeTaskResponse response)
        {
            var fileName = CreateFileName(response.currencyPair);
            if (!File.Exists(fileName))
            {
                InitDbCommandResponse(fileName);
            }
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
                //Logger.Log.Error("Произошла ошибка! Не удалось сохранить информацию о балансе после выполнения команды");
            }
        }

        internal static string CreateFileName(MoneyPair pair)
        {
            return baseFileName.Insert(14, pair.ToString());
        }

        internal static MoneyPair ParseMoneyPair(string fileName)
        {
            MoneyPair pair;
            string[] partsName = fileName.Split('.');
            string pairStr = partsName[0].Replace("CurrencyPair_", "");
            Enum.TryParse<MoneyPair>(pairStr, out pair);
            return pair;
        }


        internal static void InitDb(string fileName)
        {
            try
            {
                TimeCurrencyPair initPair = new TimeCurrencyPair()
                {
                    currentTime = DateTime.Now,
                    highestBid = 8080,
                    last = 8080,
                    lowestAsk = 8080,
                    lastbaseVolume = 8080,
                    quoteVolume = 8080,
                    isFrozen = 0
                };
                using (var tf = TeaFile<TimeCurrencyPair>.Create(fileName))
                {
                    tf.Write(initPair);
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Ошибка! Не удалось создать файл базы данных тикеров");
            }

        }

        internal static void InitDbCommand(string fileNameCommand)
        {
            TradeTaskRequest lastCommand = new TradeTaskRequest()
            {
                command = CommandType.Wait,
                currencyPair = MoneyPair.btc_usdt,
                Id = 1,
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
                //Logger.Log.Error("Ошибка! Не удалось создать файл базы данных нейросети");
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
                //Logger.Log.Error("Ошибка! Не удалось создать файл базы данных кошелька");
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
                //Logger.Log.Error("Ошибка! Не удалось завершить тестовое создание пары тикеров");
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
                        currencyPair = MoneyPair.btc_usdt,
                        requestTime = time,
                        command = (CommandType)random_value.Next(3),
                    });
                }
                return currencyPairs;
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Ошибка! Не удалось завершить тестовое создание команды");
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
                //Logger.Log.Error("Ошибка! Невозможно прочитать базу данных");
            }
        }

        
    }
}
