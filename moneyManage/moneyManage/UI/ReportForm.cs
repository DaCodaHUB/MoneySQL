﻿using System;
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
        // Todo: Pass in TotalStruct and ExpenseStruct
        public Report(TotalStruct totalData, ExpenseStruct expenseData)
        {
            InitializeComponent();
            this.totalData = totalData;
            this.expenseData = expenseData;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {

        }
    }
}