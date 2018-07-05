using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyManage.Database
{
    public class TotalStruct
    {
        public struct Total
        {
            public decimal money;
            public DateTime Timestamp { get; }

            public Total(decimal money, DateTime timestamp)
            {
                this.money = money;
                this.Timestamp = timestamp;
            }
        }

        private List<Total> totalList;
        private Total current;
        //private SqlConnect sql;

        public TotalStruct()
        {
            totalList = new List<Total>();
            current = new Total(0, DateTime.Now);
        }

        public void Insert(decimal money, DateTime time)
        {
            current = new Total(money, time);
            totalList.Add(current);
            //sql.InsertMoneyTotal(userid, money);
        }

        public Total Current { get; set; }
        public List<Total> TotalList { get; set; }
    }
}