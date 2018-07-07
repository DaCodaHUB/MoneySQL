using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Banker.Database
{
    public class RandomListData
    {
        private Random _random;

        public RandomListData()
        {
            _random = new Random();
        }

        public List<SqlConnect.Bank> generate()
        {
            var list = new List<SqlConnect.Bank>();
            for (var i = 0; i < 90; i++)
            {
                
                DateTime start = new DateTime(2018, 1, 1);
                var range = (DateTime.Today - start).Days;
                var daytime = start.AddDays(_random.Next(range));
                var money = _random.Next(2, 100);
                var data = new SqlConnect.Bank(money,daytime);
                list.Add(data);
            }
            return list;
        }
    }

}
