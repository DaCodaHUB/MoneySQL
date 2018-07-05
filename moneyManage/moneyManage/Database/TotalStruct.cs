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

            public Total(decimal money)
            {
                this.money = money;
            }
        }

        private List<Total> totalList;
        private Total current;
        private SqlConnect sql;

        public TotalStruct()
        {
            totalList = new List<Total>();
            current = new Total( 0);
        }

        public void Insert(int userid, decimal money)
        {
            current = new Total(money);
            totalList.Add(current);
            sql.InsertMoneyTotal(userid,money);

        }

        public Total Current { get; set; }
        public List<Total> TotalList { get; set; }
    }
}