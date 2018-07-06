using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using moneyManage.Database;

namespace moneyManage
{
    public partial class Report : Form
    {
        private List<SqlConnect.Bank> _total;
        private List<SqlConnect.Bank> _expense;

        // Todo: Pass in TotalStruct and ExpenseStruct
        public Report(List<SqlConnect.Bank> totalData, List<SqlConnect.Bank> expenseData)
        {
            InitializeComponent();
            this._total = totalData;
            this._expense = expenseData;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {

        }
    }
}
