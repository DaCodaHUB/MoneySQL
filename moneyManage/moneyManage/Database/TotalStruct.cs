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
            public int userid;
            public DateTime time;
            public int money;

            public Total(int userid, DateTime time, int money)
            {
                this.userid = userid;
                this.time = time;
                this.money = money;
            }
        }

        public List<Total> totalList;
        public Total current;

        public TotalStruct()
        {
            totalList = new List<Total>();
            current = new Total(0, DateTime.Now, 0);
        }

        public void Insert(int userid, DateTime time, int money)
        {
            current = new Total(userid, time, money);
            totalList.Add(current);
        }

        public Total Current { get; set; }
        public List<Total> TotalList { get; set; }
    }
}
