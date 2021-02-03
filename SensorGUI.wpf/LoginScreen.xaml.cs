using Authorization;
using Authorization.Models.Request;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace SensorGUI.wpf
{
   
    public partial class LoginScreen : Window
    {
        private HubConnection _connection;
        //public AuthDbContext dbContext;
        private Login details = new Login();
        public LoginScreen()
        {
            
           InitializeComponent();
            /** _connection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:58067/TestHub")
                 .Build();
             _connection.Closed += async (error) =>
             {
                 await Task.Delay(new Random().Next(0, 5) * 1000);
                 await _connection.StartAsync();
             };**/
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
