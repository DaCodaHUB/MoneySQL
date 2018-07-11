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
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            var mail = new MailCode(Email.Text);
            var resetForm = new ResetPassword(mail.VerifyString);
            resetForm.Closed += (s, args) => Close();
            resetForm.Show();
        }
    }
}
