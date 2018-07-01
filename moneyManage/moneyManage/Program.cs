using System;
using System.Windows.Forms;
using moneyManage.Database;

namespace moneyManage
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SignIn());

//            SqlConnect sql = new SqlConnect();
//            sql.CreateNewUser("s","sh");
//            sql.VerifyUser("ss","");
        }
    }
}
