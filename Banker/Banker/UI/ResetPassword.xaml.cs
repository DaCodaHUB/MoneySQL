﻿using System;
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
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        private string _verifyString;
        
        public ResetPassword(string verifyString)
        {
            InitializeComponent();
            _verifyString = verifyString;
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            // Todo: change type to password

            // Todo: upload to database
            if(_verifyString.Equals(verify.Text) && newPass.Password.Equals(confirmPass.Password))
            {

            }
        }
    }
}
