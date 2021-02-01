using System;
using System.Collections.Generic;
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
   
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {
            MainWindow p = new MainWindow();
            p.Show();
            
            Close();
           
        }

        private void Register_Button(object sender, RoutedEventArgs e)
        {
            Register p = new Register();
            p.Show();
            Close();
        }
    }
}
