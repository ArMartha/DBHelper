using BotsDBhelperDecimal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBotsDBhelper
{
    class Program
    {
        // Пример использования TeaFile
        // Внутри TeaTimeCurrencyPair не храниться название текущей пары валют,
        // пары_валют сохраняется в названии файла хранилища см. пример ниже.
        // Для каждой пары свое файл-хранилище.


        static void Main(string[] args)
        {
            // просмотр содержимого 
            TimeCurrencyPair[] pairs = AnnDBhelper.ReadHistory(DateTime.MinValue, DateTime.Now, MoneyPair.btc_usdt);
            

            TimeCurrencyPair currentPair = new TimeCurrencyPair()
            {
                //currentTime = DateTime.Now,
                highestBid = 333,
                last = 33,
                lowestAsk = 303,
                lastbaseVolume = 8888,
                quoteVolume = 8888,
                isFrozen = 0
            };

            DecimalTradeDBhelper.WriteTick(currentPair, MoneyPair.btc_usdt);// записал пару
            DecimalTradeDBhelper.TESTReadDb("CurrencyPairs_btc_usdt.tea");
            //Console.ReadKey();
            Console.WriteLine("---- Debug read line ----");

            DecimalTradeDBhelper.TESTCreate_TeaTimeCommands();
            var tradeTaskRequest = DecimalTradeDBhelper.ReadLastCommand(); // прочитал команду
            //
            // -- Выполнил команду --
            // -- -------------------
            //
            var tradeTaskResponse = new TradeTaskResponse()
            {
                balanceCrypto = 939393,
                Id = tradeTaskRequest.Id,
                //инициализовать поля
            };

            // записал response
            //TradeDBhelper.WriteResponseLastCommand(tradeTaskResponse);
            Console.ReadLine();

        }

    }
}
