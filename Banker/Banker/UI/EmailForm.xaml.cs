using System;
using System.Windows;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for EmailForm.xaml
    /// </summary>
    public partial class EmailForm
    {
        private SqlConnect.User _user;

        public EmailForm()
        {
            InitializeComponent();
            DataContext = new TextFields();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            _user = SqlConnect.GetEmail(UsernameTxt.Text);

            if (UsernameTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Username doesn't meet requirement");
                return;
            }


            if (_user.Email.Length == 0)
            {
                MessageBox.Show("Username doesn't exist.");
                return;
            }

            if (_user.Timestamp.Day < DateTime.Today.Day && _user.IsReset == 0)
            {
                SqlConnect.UpdateMode(_user, "ResetStatus");
            }

            else if (_user.ResetTimes >= 4)
            {
                MessageBox.Show("You tried to reset 4 times already. Please come back tomorrow.");
                return;
            }

            Hide();
            var mailCode = new MailCode(_user);
            var resetForm = new ResetPassword(_user);
            resetForm.Closed += (s, args) => Close();
            resetForm.Show();
        }

        private void GotCode_OnClick(object sender, RoutedEventArgs e)
        {
            _user = SqlConnect.GetEmail(UsernameTxt.Text);
            if (UsernameTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Username doesn't meet requirement");
                return;
            }


            if (_user.Email.Length == 0)
            {
                MessageBox.Show("Username doesn't exist.");
                return;
            }

            Hide();
            var resetForm = new ResetPassword(_user);
            resetForm.Closed += (s, args) => Close();
            resetForm.Show();
        }
    }
}