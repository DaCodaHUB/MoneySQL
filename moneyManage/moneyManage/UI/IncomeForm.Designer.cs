namespace moneyManage.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MoneyTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IncomeBtn = new System.Windows.Forms.Button();
            this.SpendBtn = new System.Windows.Forms.Button();
            this.catagoryList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CommentTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ReportBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CurrentMoney = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MoneyTxt
            // 
            this.MoneyTxt.Location = new System.Drawing.Point(52, 40);
            this.MoneyTxt.Name = "MoneyTxt";
            this.MoneyTxt.Size = new System.Drawing.Size(179, 22);
            this.MoneyTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(237, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "$";
            // 
            // IncomeBtn
            // 
            this.IncomeBtn.Location = new System.Drawing.Point(291, 34);
            this.IncomeBtn.Name = "IncomeBtn";
            this.IncomeBtn.Size = new System.Drawing.Size(151, 29);
            this.IncomeBtn.TabIndex = 2;
            this.IncomeBtn.Text = "Income";
            this.IncomeBtn.UseVisualStyleBackColor = true;
            this.IncomeBtn.Click += new System.EventHandler(this.Income_Click);
            // 
            // SpendBtn
            // 
            this.SpendBtn.Location = new System.Drawing.Point(467, 34);
            this.SpendBtn.Name = "SpendBtn";
            this.SpendBtn.Size = new System.Drawing.Size(153, 29);
            this.SpendBtn.TabIndex = 3;
            this.SpendBtn.Text = "Spend";
            this.SpendBtn.UseVisualStyleBackColor = true;
            this.SpendBtn.Click += new System.EventHandler(this.Spend_Click);
            // 
            // catagoryList
            // 
            this.catagoryList.FormattingEnabled = true;
            this.catagoryList.Items.AddRange(new object[] {
            "Education",
            "Entertainment",
            "Transportation",
            "Food and Drink",
            "Services",
            "Materials"});
            this.catagoryList.Location = new System.Drawing.Point(52, 118);
            this.catagoryList.Name = "catagoryList";
            this.catagoryList.Size = new System.Drawing.Size(179, 24);
            this.catagoryList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.3F);
            this.label2.Location = new System.Drawing.Point(52, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Categories";
            // 
            // CommentTxt
            // 
            this.CommentTxt.Location = new System.Drawing.Point(291, 118);
            this.CommentTxt.Name = "CommentTxt";
            this.CommentTxt.Size = new System.Drawing.Size(329, 22);
            this.CommentTxt.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.3F);
            this.label3.Location = new System.Drawing.Point(288, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Comments";
            // 
            // ReportBtn
            // 
            this.ReportBtn.Location = new System.Drawing.Point(52, 179);
            this.ReportBtn.Name = "ReportBtn";
            this.ReportBtn.Size = new System.Drawing.Size(179, 34);
            this.ReportBtn.TabIndex = 8;
            this.ReportBtn.Text = "View Reports";
            this.ReportBtn.UseVisualStyleBackColor = true;
            this.ReportBtn.Click += new System.EventHandler(this.Report_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(287, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current";
            // 
            // CurrentMoney
            // 
            this.CurrentMoney.Location = new System.Drawing.Point(350, 182);
            this.CurrentMoney.Name = "CurrentMoney";
            this.CurrentMoney.Size = new System.Drawing.Size(198, 22);
            this.CurrentMoney.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(555, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "$";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 254);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CurrentMoney);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReportBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CommentTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.catagoryList);
            this.Controls.Add(this.SpendBtn);
            this.Controls.Add(this.IncomeBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MoneyTxt);
            this.Name = "Form1";
            this.Text = "MoneySQL";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MoneyTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button IncomeBtn;
        private System.Windows.Forms.Button SpendBtn;
        private System.Windows.Forms.ComboBox catagoryList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CommentTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReportBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CurrentMoney;
        private System.Windows.Forms.Label label5;
        //private moneyManage.Database.TotalStruct totalData;
        //private moneyManage.Database.ExpenseStruct expenseData;
    }
}

