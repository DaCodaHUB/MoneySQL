using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyManage.Database
{
    class ExpenseStruct
    {
        public struct Expense
        {
            public int userid;
            public DateTime time;
            public string category;
            public int money;

            public Expense(int userid, DateTime time, string category, int money)
            {
                this.userid = userid;
                this.time = time;
                this.category = category;
                this.money = money;
            }
        }

        public List<Expense> expenseList;
        public int userid;

        public ExpenseStruct()
        {
            expenseList = new List<Expense>();
        }

        public void Insert(int userid, DateTime time, string category, int money)
        {
            Expense data = new Expense(userid, time, category, money);
            expenseList.Add(data);
            this.userid = userid;
        }

        public List<Expense> ExpnseList { get; set; }
        public int UserID { get; set; }

        public int getLength()
        {
            return expenseList.Count;
        }
    }
}
