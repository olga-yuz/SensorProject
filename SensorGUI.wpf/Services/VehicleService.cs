using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleLib;

namespace SensorGUI.wpf.Services
{
    
    public class VehicleService
    {
        private static string TempUrl = "https://localhost:44319/api/Temp";
        private static string HumidUrl = "https://localhost:44319/api/Humid";
        private static string LocationUrl = "https://localhost:44319/api/LocationAPI";
        private static string BaseUrl = "https://localhost:44319/api/Vehicle";
        private static string AlertUrl = "https://localhost:44319/api/RedAlert";

        internal async static Task<List<VehicleTemp>> GetAllVehicleTemp()
        {
            List<VehicleTemp> VehicleViewTemp = new List<VehicleTemp>();
            var tempVehiclesUrl = $"{TempUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var tempVehiclesResponse = await httpClient.GetStringAsync(tempVehiclesUrl);
                var allVehiclesTemp = JsonConvert.DeserializeObject<List<VehicleTemp>>(tempVehiclesResponse);
                
                return allVehiclesTemp;
            }
        }
        internal async static Task<List<VehicleHumid>> GetAllVehicleHumid()
        {
            List<VehicleHumid> VehicleViewHumid = new List<VehicleHumid>();
            var humidVehiclesUrl = $"{HumidUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var humidVehiclesResponse = await httpClient.GetStringAsync(humidVehiclesUrl);
                var allVehiclesHumid = JsonConvert.DeserializeObject<List<VehicleHumid>>(humidVehiclesResponse);

                return allVehiclesHumid;
            }
        }
        internal async static Task<List<Location>> GetAllVehicleLocation()
        {
            List<Location> VehicleViewLocation = new List<Location>();
            var locationVehiclesUrl = $"{LocationUrl}/alllocations";
            using (HttpClient httpClient = new HttpClient())
            {
                var locationVehiclesResponse = await httpClient.GetStringAsync(locationVehiclesUrl);
                var allVehiclesLocation = JsonConvert.DeserializeObject<List<Location>>(locationVehiclesResponse);

                return allVehiclesLocation;
            }
        }



        internal static async Task<Vehicle> AddVehicle(AddVehicle VehicleAddModel)
        {
            var addVehicleUrl = $"{BaseUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var url = new Uri(addVehicleUrl);
                string jsonTranport = JsonConvert.SerializeObject(VehicleAddModel);
                var jsonPayload = new StringContent(jsonTranport, Encoding.UTF8, "application/json");
                var updateVehicleResponse = await httpClient.PostAsync(url, jsonPayload);
                var responseContent = await updateVehicleResponse.Content.ReadAsStringAsync();
                var updatedVehicle = JsonConvert.DeserializeObject<Vehicle>(responseContent);
                return updatedVehicle;
            }
        }
        internal static async Task<Location> AddLocation(Location location)
        {
            var addLocationUrl = $"{LocationUrl}/addlocation";
            using (HttpClient httpClient = new HttpClient())
            {
                var url = new Uri(addLocationUrl);
                string jsonTranport = JsonConvert.SerializeObject(location);
                var jsonPayload = new StringContent(jsonTranport, Encoding.UTF8, "application/json");
                var updateVehicleLocation = await httpClient.PostAsync(url, jsonPayload);
                var responseContent = await updateVehicleLocation.Content.ReadAsStringAsync();
                var updatedLocation = JsonConvert.DeserializeObject<Location> (responseContent);
                return updatedLocation;
            }
        }
        internal async static Task<List<RedAlert>> RedAlertVehiclesTemp()
        {
            
            var alertUrl = $"{AlertUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var alertVehiclesResponse = await httpClient.GetStringAsync(alertUrl);
                var alertVehicles = JsonConvert.DeserializeObject<List<RedAlert>>(alertVehiclesResponse);

                return alertVehicles;
            }
        }

        internal async static Task<Vehicle> UpdateVehicle(UpdateVehicle VehicleUpdateModel, int id)
        {
            var updateVehicleUrl = $"{BaseUrl}/{id}";
            using (HttpClient httpClient = new HttpClient())
            {
               // var url = new Uri(updateVehicleUrl);
                string jsonTranport = JsonConvert.SerializeObject(VehicleUpdateModel);
                var jsonPayload = new StringContent(jsonTranport, Encoding.UTF8, "application/json");
                var updateVehicleResponse = await httpClient.PutAsync(updateVehicleUrl, jsonPayload);
                var responseContent = await updateVehicleResponse.Content.ReadAsStringAsync();
                var updatedVehicle = JsonConvert.DeserializeObject<Vehicle>(responseContent);
                return updatedVehicle;
            }
        }

        
        internal async static Task<string> DeleteVehicle(int Id)
        {
            var deleteVehicleUrl = $"{BaseUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var VehicleDeleteResponse = await httpClient.DeleteAsync(deleteVehicleUrl);
                var responseContent = await VehicleDeleteResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        internal async static Task<List<VehicleTemp>> GetVehicleTemp(int Id)
        {
            
            var tempVehiclesUrl = $"{TempUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var tempVehiclesGet = await httpClient.GetStringAsync(tempVehiclesUrl);
                var getVehiclesTemp = JsonConvert.DeserializeObject<List<VehicleTemp>>(tempVehiclesGet);

                return getVehiclesTemp;
            }
        }
        internal async static Task<List<VehicleHumid>> GetVehicleHumid(int Id)
        {

            var humidVehiclesUrl = $"{HumidUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var humidVehiclesGet = await httpClient.GetStringAsync(humidVehiclesUrl);
                var getVehiclesHumid = JsonConvert.DeserializeObject<List<VehicleHumid>>(humidVehiclesGet);

                return getVehiclesHumid;
            }
        }
        internal async static Task<List<Location>> GetVehicleLocation(int Id)
        {

            var locationVehiclesUrl = $"{LocationUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var locationVehiclesGet = await httpClient.GetStringAsync(locationVehiclesUrl);
                var getVehiclesLocation = JsonConvert.DeserializeObject<List<Location>>(locationVehiclesGet);

                return getVehiclesLocation;
            }
        }
    }
}

