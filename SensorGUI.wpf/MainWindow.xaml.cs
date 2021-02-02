using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VehicleLib;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SensorGUI.wpf
{

    public partial class MainWindow : Window
    {
        private HubConnection connection;
       // private List<Vehicle> allVehicles;
        private Vehicle selectedVehicle = new Vehicle();
        private Location location = new Location();
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:58067/TestHub")
                .Build();
            connection.Closed += async (error) =>
           {
               await Task.Delay(new Random().Next(0, 5) * 1000);
               await connection.StartAsync();
           };
        }
        
       
        private void SearchClick_button(object sender, SelectionChangedEventArgs e)
        {
            TableStackPanel.Visibility = Visibility.Hidden;
            SingleTableStackPanel.Visibility = Visibility.Visible;
        }

        private async void Create_Button(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                TempListView.Items.Add("Connection started");
               
            }
            catch (Exception ex)
            {
                TempListView.Items.Add(ex.Message);
                HumidListView.Items.Add(ex.Message);
                GPSListView.Items.Add(ex.Message);
            }

            connection.On<int, int>("ReceiveDataTemp", (id, temp) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newTemp = $"{id}: {temp}";
                    TempListView.Items.Add(newTemp);
                });
            });
            connection.On<int, int>("ReceiveDataHumid", (id, humid) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newHumid = $"{id}: {humid}";
                    HumidListView.Items.Add(newHumid);
                });
            });
            connection.On<int, int, int, int>("HumidData", (id, lat, _long, time) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newGPS = $"{id}: {lat}, {_long}, {time}";
                    GPSListView.Items.Add(newGPS);
                });
            });

            try
            {
                await connection.InvokeAsync("TempData",
                    IdTextBox.Text, TempTextBox.Text);
                await connection.InvokeAsync("HumidData",
                    IdTextBox.Text, HumidTextBox.Text);
                await connection.InvokeAsync("GPSData",
                    IdTextBox.Text, LatTextBox.Text, LongTextBox.Text);
            }
            catch (Exception ex)
            {
                TempListView.Items.Add(ex.Message);
            }


        }
        private void Update_Button(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_Button(object sender, RoutedEventArgs e)
        {

        }
        private void AddPanel_Button(object sender, RoutedEventArgs e)
        {
            CreatePanel.Visibility = Visibility.Visible;
            UpdatePanel.Visibility = Visibility.Hidden;
            DeletePanel.Visibility = Visibility.Hidden;
        }
        private void UpdatePanel_Button(object sender, RoutedEventArgs e)
        {
            UpdatePanel.Visibility = Visibility.Visible;
            CreatePanel.Visibility = Visibility.Hidden;
            DeletePanel.Visibility = Visibility.Hidden;
        }
        private void DeletePanel_Button(object sender, RoutedEventArgs e)
        {
            DeletePanel.Visibility = Visibility.Visible;
            UpdatePanel.Visibility = Visibility.Hidden;
            CreatePanel.Visibility = Visibility.Hidden;
        }
        

        private void SignOut_Button(object sender, RoutedEventArgs e)
        {

            LoginScreen n = new LoginScreen();
            n.Show();
            Close();

        }
        private void GraphView_Button(object sender, RoutedEventArgs e)
        {
            GraphStackPanel.Visibility = Visibility.Visible;
            TableStackPanel.Visibility = Visibility.Hidden;

        }
        private void TableView_Button(object sender, RoutedEventArgs e)
        {
            TableStackPanel.Visibility = Visibility.Visible;
            GraphStackPanel.Visibility = Visibility.Hidden;

        }
        private void TempListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count >= 1)
            {
                selectedVehicle = (Vehicle)e.AddedItems[0];
                VehicleTempID.Text = selectedVehicle.vehicleId.ToString();
                VehicleTemp.Text = selectedVehicle.temp.ToString();



            }
        }
        private void HumidListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count >= 1)
            {
                selectedVehicle = (Vehicle)e.AddedItems[0];
                VehicleHumidID.Text = selectedVehicle.vehicleId.ToString();
                VehicleHumid.Text = selectedVehicle.humidity.ToString();
            }

        }
        private void GPSListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count >= 1)
            {
                location = (Location)e.AddedItems[0];
                VehicleGPSID.Text = location.vehicleId.ToString();
                VehicleGPSLat.Text = location.latitude.ToString();
                VehicleGPSLat.Text = location.longitude.ToString();
                VehicleGPSTime.Text = location.time.ToString();
            }
        }
        
        
           

    }
}
