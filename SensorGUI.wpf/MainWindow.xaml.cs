using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VehicleLib;
using SensorGUI.wpf.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SensorGUI.wpf
{

    public partial class MainWindow : Window
    {
        // private HubConnection connection;
        private HubConnection connection;
        private List<VehicleTemp> allTempVehicles, getTempVehicles;
        private List<VehicleHumid> allHumidVehicles, getHumidVehicles;
        private List<Location> allVehicleLocations, getLocationVehicles;
        private List<RedAlert> redAlertVehicles;
        private Vehicle selectedVehicle = new Vehicle();
        private Location location = new Location();
        public MainWindow()
        {
            InitializeComponent();
            
            GetAllVehiclesTemp();
            GetAllVehiclesHumid();
            GetAllVehiclesLocation();
           
        }

        private async void GetAllVehiclesTemp()
        {
            allTempVehicles = await VehicleService.GetAllVehicleTemp();
            TempListView.ItemsSource = allTempVehicles;
            
        }
       private async void GetAllVehiclesHumid()
        {
            allHumidVehicles = await VehicleService.GetAllVehicleHumid();
            HumidListView.ItemsSource = allHumidVehicles;
        }
        private async void GetAllVehiclesLocation()
        {
            allVehicleLocations = await VehicleService.GetAllVehicleLocation();
            GPSListView.ItemsSource = allVehicleLocations;
        }


        private void SearchClick_button(object sender, SelectionChangedEventArgs e)
        {
            TableStackPanel.Visibility = Visibility.Hidden;
            SingleTableStackPanel.Visibility = Visibility.Visible;
        }

        private async void Create_Button(object sender, RoutedEventArgs e)
        {

            var VehicleAddModel = new AddVehicle
            {
                temp = int.Parse(TempTextBox.Text),
                humidity = int.Parse(HumidTextBox.Text),
            };
            //var newLocation = new Location
            //{

           //     latitude = int.Parse(LatTextBox.Text),
           //     longitude = int.Parse(LongTextBox.Text),
           // };
            var addVehicleResponse = await VehicleService.AddVehicle(VehicleAddModel);
            //var addVehicleLocation = await VehicleService.AddLocation(newLocation);
            if (addVehicleResponse != null)
            {
                MessageBox.Show($"New Vehicle Record has been added.");
            }
            GetAllVehiclesTemp();
            GetAllVehiclesHumid();
            GetAllVehiclesLocation();


        }
        private async void Update_Button(object sender, RoutedEventArgs e)
        {
            var selectedVehicle = int.Parse(IdUpdate.Text);
            var vehicleUpdate = new UpdateVehicle
            {
                temp = int.Parse(TempUpdate.Text),
                humidity = int.Parse(HumidUpdate.Text),
                
            };
            var carUpdateResponse = await VehicleService.UpdateVehicle(vehicleUpdate, selectedVehicle);
            if (carUpdateResponse != null)
            {
                GetAllVehiclesTemp();
                GetAllVehiclesHumid();
                GetAllVehiclesLocation();
            }
            
        }
        private async void Delete_Button(object sender, RoutedEventArgs e)
        {
           var selectedVehicle = int.Parse(IdDelete.Text);
           var carDeleteResponse = await VehicleService.DeleteVehicle(selectedVehicle);
            MessageBox.Show(carDeleteResponse);
            GetAllVehiclesTemp();
            GetAllVehiclesHumid();
            GetAllVehiclesLocation();

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
        
        private async void RedPanel_Check(object sender, RoutedEventArgs e)
        {
            TableStackPanel.Visibility = Visibility.Hidden;
            RedAlertStackPanel.Visibility = Visibility.Visible;
            redAlertVehicles = await VehicleService.RedAlertVehiclesTemp();
            AlertListView.ItemsSource = redAlertVehicles;
        }
        private void RedPanel_Unchecked(object sender, RoutedEventArgs e)
        {
            RedAlertStackPanel.Visibility = Visibility.Hidden;
            TableStackPanel.Visibility = Visibility.Visible;  
            
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
        private void Refresh_Button(object sender, RoutedEventArgs e)
        {
            GetAllVehiclesTemp();
            GetAllVehiclesHumid();
            GetAllVehiclesLocation();

        }
        private void TableView_Button(object sender, RoutedEventArgs e)
        {
            TableStackPanel.Visibility = Visibility.Visible;
            GraphStackPanel.Visibility = Visibility.Hidden;

        }
        private async void Search_Button(object sender, RoutedEventArgs e)
        {
            
            getTempVehicles = await VehicleService.GetVehicleTemp(int.Parse(SearchBar.Text));
            getHumidVehicles = await VehicleService.GetVehicleHumid(int.Parse(SearchBar.Text));
            getLocationVehicles = await VehicleService.GetVehicleLocation(int.Parse(SearchBar.Text));
            
            TempListView.ItemsSource = getTempVehicles;
            HumidListView.ItemsSource = getHumidVehicles;
            GPSListView.ItemsSource = getLocationVehicles;


        }
        





    }
}

