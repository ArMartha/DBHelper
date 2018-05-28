using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaTime;

namespace BotsDBhelperDecimal
{
    /// <summary>
    /// для сохранения исторических данных 
    /// </summary>
    public struct TimeCurrencyPair
    {
        public Time currentTime;
        public decimal last;
        public decimal lowestAsk;
        public decimal highestBid;
        public decimal lastbaseVolume;
        public decimal quoteVolume;
        public byte isFrozen;
    }

    /// <summary>
    /// запрос задачи от нейросети
    /// </summary>
    public struct TradeTaskRequest
    {
        public int Id;
        public MoneyPair currencyPair;
        public Time requestTime;
        public CommandType command;
    }

    public enum CommandType
    {
        Sell = 0, // Продать
        Buy = 1, // Купить
        Wait = 2 // Ждем след. команды
    }

    public enum MoneyPair
    {
        // нужно расширить до нужного количества 
        btc_usdt = 0,
    }

    /// <summary>
    /// сохранение статуса выполненной задачи 
    /// </summary>
    public struct TradeTaskResponse
    {
        public int Id;
        public MoneyPair currencyPair;
        public Time responseTime;
        public byte command;
        public decimal balanceMoney;
        public decimal balanceCrypto;
    }

}
