using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SensorGUI.wpf
{
  
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void CreatedUser_Button(object sender, RoutedEventArgs e)
        {
           
                string useranme = newUserTextBox.Text;
                string password = newPasswordTextBox.Password;
                
                if (newPasswordTextBox.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    
                }
               
                else
                {
                    errormessage.Text = "";
                    
                    
                    
                    errormessage.Text = "You have Registered successfully.";
                    

                    LoginScreen p = new LoginScreen();
                    p.Show();
                    Close();
                }
            
        }
    }
}
