using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTestDB
{
    class Program
    {
        // Внутри TeaTimeCurrencyPair не храниться название текущей пары валют,
        // пары_валют сохраняется в названии файла хранилища см. пример ниже.
        // Для каждой пары свое файл-хранилище.

        //static string fileName = "CurrencyPairs_btc_usd.tea";
        //static string fileNameCommand = "Commands.tea";
        //static string fileNameCommandResponse = "ResponseCommands.tea";

        /// <summary>
        /// Пример использования TeaFile
        /// </summary>
        /// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    if (!File.Exists(fileName))
        //    {
        //        DBHelper.InitDb(fileName);
        //    }
        //    TimeCurrencyPair currentPair = new TimeCurrencyPair()
        //    {
        //        currentTime = DateTime.Now,
        //        highestBid = 333,
        //        last = 33,
        //        lowestAsk = 303,
        //        lastbaseVolume = 8888,
        //        quoteVolume = 8888,
        //        isFrozen = 0
        //    };
            
        //    DBHelper.WriteTick(currentPair, fileName);// записал пару
        //    DBHelper.TESTReadDb(fileName);
        //    //Console.ReadKey();
        //    Console.WriteLine("---- Debug read line ----");

        //    if (!File.Exists(fileNameCommand))
        //    {
        //        DBHelper.InitDbCommand(fileNameCommand);
        //    }

        //    DBHelper.TESTCreate_TeaTimeCommands();
        //    var tradeTaskRequest = DBHelper.ReadLastCommand(fileNameCommand); // прочитал команду
        //    //
        //    // -- Выполнил команду --
        //    // -- -------------------
        //    //
        //    var tradeTaskResponse = new TradeTaskResponse()
        //    {
        //        balanceCrypto = 939393,
        //        Id = tradeTaskRequest.Id,

        //        //инициализовать поля
        //    };
        //    if (!File.Exists(fileNameCommandResponse))
        //    {
        //        DBHelper.InitDbCommandResponse(fileNameCommandResponse);
        //    }
        //    // записал response
        //    DBHelper.WriteResponseLastCommand(tradeTaskResponse, fileNameCommandResponse);
        //    //Console.ReadLine();
        //}

    }
}
