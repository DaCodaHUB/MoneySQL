using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace moneyManage.Database
{
    public class TotalStruct
    {
        public struct Total
        {
            public decimal money;
            public DateTime Timestamp;
            //public DateTime Timestamp { get; set; }

            public Total(decimal money, DateTime timestamp)
            {
                this.money = money;
                this.Timestamp = timestamp;
            }

            public Total(decimal money)
            {
                this.money = money;
            }
        }

        private List<Total> _totalList;
        private Total _current;
        private SqlConnect sql;
        private int userid;

        public TotalStruct()
        {
            _totalList = new List<Total>();
            _current = new Total(0, DateTime.Now);
        }

        public TotalStruct(int userid)
        {
            _totalList = new List<Total>();
            _current = new Total(0, DateTime.Now);
            sql = new SqlConnect();
            this.userid = userid;
        }

        public void Insert(decimal money, DateTime time)
        {
            //_current = new Total(money, time);
            _current.money = money;
            _current.Timestamp = time;
            _totalList.Add(_current);
            sql.InsertMoneyTotal(userid, money);
        }

        public void InsertData(decimal money, DateTime time)
        {
            _current.money = money;
            _current.Timestamp = time;
            _totalList.Add(_current);

            Debug.WriteLine(_current.money);
        }

        public Total Current {
            get
            {
                return _current;
            }
        }

        public List<Total> TotalList {
            get {
                return _totalList;
            }
        }
    }
}