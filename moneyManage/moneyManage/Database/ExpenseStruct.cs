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
            private decimal money;
           

            public Expense(string category, decimal money)
            {
                this.category = category;
                this.money = money;
              
            }
        }

        private List<Expense> expenseList;
        private SqlConnect sql;

        public ExpenseStruct()
        {
            sql = new SqlConnect();
            expenseList = new List<Expense>();
        }

        public void Insert(int userid, string category, decimal money)
        {
            Expense data = new Expense(category, money);

            expenseList.Add(data);
//            sql.InsertMoneyExpense(userid, category, money);
        }

        public List<Expense> ExpnseList { get; set; }
        public int UserID { get; set; }

        public int getLength()
        {
            return expenseList.Count;
        }
    }
}