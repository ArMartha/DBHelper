using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaTime;
//using PoloniexBot;

namespace BotsDBhelperDecimal
{
    public static class AnnDBhelper
    {
        /// <summary>
        /// Чтение из файла БД данных о цене в заданный промежуток времени
        /// </summary>
        /// <param name="begin">врямя начала чтения</param>
        /// <param name="end">время окончания чтения</param>
        /// <param name="pair">пара валют, напр. BTC_USD </param>
        /// <returns></returns>
        public static TimeCurrencyPair[] ReadHistory(DateTime begin, DateTime end, MoneyPair pair)
        {
            Time teaTimeBegin = begin;
            Time teaTimeEnd = end;
            IEnumerable<TimeCurrencyPair> teaCurrencyPairs = new List<TimeCurrencyPair>();
            var fileName = DecimalTradeDBhelper.CreateFileName(pair);
           
            try
            {
                if (!File.Exists(fileName))
                {
                    DecimalTradeDBhelper.InitDb(fileName);
                }
                using (var tf = TeaFile<TimeCurrencyPair>.OpenRead(fileName))
                {
                    teaCurrencyPairs = tf.Items.Where(p => (p.currentTime  > teaTimeBegin)&&(p.currentTime < teaTimeEnd)).ToArray() ;
                    Console.WriteLine(teaCurrencyPairs);
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Произошла ошибка! Не удалось прочитать истрические данные");
                Console.Write(ex.Message);
            }

            return teaCurrencyPairs.ToArray();
        }

        /// <summary>
        /// Запись в БД Command, новой команды для бота и времени ёё выполнения, возвращает 1, если команда успешно записана.
        /// </summary>
        /// <param name="command">новая команда</param>
        /// <returns></returns>
        public static int WriteCommand(TradeTaskRequest command)
        {
            var fileName = DecimalTradeDBhelper.CreateFileName(command.currencyPair);
            try
            {
                if (!File.Exists(fileName))
                {
                    DecimalTradeDBhelper.InitDbCommandResponse(fileName);
                }

                using (var tf = TeaFile<TradeTaskRequest>.Append(fileName))
                {
                    // if the stream is a filestream that was opened in FileMode.Append, this call is redundant.
                    // this line is here for the allowed case were the stream and headerstream point to the same stream.
                    //https://github.com/discretelogics/TeaFiles.Net-time-series-storage-in-flat-files/blob/938e3fd46e9c55b80e81163495cdd30ebd0112c4/TeaFiles/TeaFileT.cs
                    //tf.SetFilePointerToEnd();
                    tf.Write(command);
                    // TODO транзакция
                }
                return 1;
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Произошла ошибка! Команда не была записана");
            }
            return 0;
        }

        /// <summary>
        /// Чтение из БД CommandResponse, информации об изменении баланса после выполнения команды
        /// </summary>
        /// <returns></returns>
        public static TradeTaskResponse ReadLastResponce()
        {

            TradeTaskResponse lastResponseCommand = new TradeTaskResponse();
            try
            {
                if (!File.Exists(DecimalTradeDBhelper.fileNameCommand))
                {
                    DecimalTradeDBhelper.InitDbCommand(DecimalTradeDBhelper.fileNameCommand);
                }
                using (var tf = TeaFile<TradeTaskResponse>.OpenRead(DecimalTradeDBhelper.fileNameCommand))
                {
                    lastResponseCommand = tf.Items.Last();
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error("Произошла ошибка! Не удалось прочитать информацию о балансе");
            }
            return lastResponseCommand;
        }
    }
}
