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

            public int money;

            public Total(int userid, int money)
            {
                this.userid = userid;

                this.money = money;
            }
        }

        public List<Total> totalList;
        public Total current;

        public TotalStruct()
        {
            totalList = new List<Total>();
            current = new Total(0, 0);
        }

        public void Insert(int userid, int money)
        {
            current = new Total(userid, money);
            totalList.Add(current);
        }

        public Total Current { get; set; }
        public List<Total> TotalList { get; set; }
    }
}