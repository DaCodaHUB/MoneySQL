using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace moneyManage.Database
{
    public class ExpenseStruct
    {
        public struct Expense
        {
            private string category;
            public decimal Money { get; }
            public DateTime Timestamp { get; }


            public Expense(string category, decimal money, DateTime timestamp)
            {
                this.category = category;
                this.Money = money;
                this.Timestamp = timestamp;
            }
        }

        private List<Expense> expenseList;
        private SqlConnect sql;
        private int userid;

        public ExpenseStruct()
        {
            sql = new SqlConnect();
            expenseList = new List<Expense>();
        }

        public ExpenseStruct(int userid)
        {
            sql = new SqlConnect();
            expenseList = new List<Expense>();
            this.userid = userid;
        }

        public void Insert(string category, decimal money, DateTime time)
        {
            Expense data = new Expense(category, money, time);

            expenseList.Add(data);
            sql.InsertMoneyExpense(userid, category, money);
        }

        public void InsertData(string category, decimal money, DateTime time)
        {
            Expense data = new Expense(category, money, time);

            expenseList.Add(data);
        }

        public List<Expense> ExpnseList { get; set; }
        public int UserID { get; set; }

        public int getLength()
        {
            return expenseList.Count;
        }
    }
}