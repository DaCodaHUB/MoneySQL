using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for EmailForm.xaml
    /// </summary>
    public partial class EmailForm : Window
    {
        public EmailForm()
        {
            InitializeComponent();
            DataContext = new TextFields();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (UsernameTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Username doesn't meet requirement");
                return;
            }

            var email = SqlConnect.GetEmail(UsernameTxt.Text);
            if (email.Length == 0)
            {
                MessageBox.Show("Username doesn't exist.");
                return;
            }

            Hide();
            var mailCode = new MailCode(SqlConnect.GetEmail(UsernameTxt.Text));
            var resetForm = new ResetPassword(UsernameTxt.Text);
            resetForm.Closed += (s, args) => Close();
            resetForm.Show();
        }
    }
}