using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_apps
{
    class MessagesLog
    {
        /*Привет, для сохранения истории курсов
        валют нужны поля в базе :
1)currency_pair - текущая пара валюты 
2)last 
3)lowestAsk 
3)highestBid 
4)baseVolume 
5)quoteVolume 
6)isFrozen 
выше указанные поля получают значения из запроса 
https://poloniex.com/public?command=returnTicker 
7)current_time (текущее время на сервере)
 Сергей
Сергей 22:39
Это была таблица history_data, теперь таб. results
currency_pair(string 
command_time(datetime) 
command(string or enum(sell,bye,nothing)) 
balance_money(decimal) 
balance_crypto(decimal)*/
    }
}
