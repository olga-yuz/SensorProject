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
        private static string BaseUrl = "https://localhost:44319/vehicle";

        internal async static Task<List<VehicleTemp>> GetAllVehicleTemp()
        {
            List<VehicleTemp> VehicleViewTemp = new List<VehicleTemp>();
            var allVehiclesUrl = $"{TempUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var allVehiclesResponse = await httpClient.GetStringAsync(allVehiclesUrl);
                var allVehiclesTemp = JsonConvert.DeserializeObject<List<VehicleTemp>>(allVehiclesResponse);
                
                return allVehiclesTemp;
            }
        }
        internal async static Task<List<VehicleHumid>> GetAllVehicleHumid()
        {
            List<VehicleHumid> VehicleViewHumid = new List<VehicleHumid>();
            var allVehiclesUrl = $"{HumidUrl}";
            using (HttpClient httpClient = new HttpClient())
            {
                var allVehiclesResponse = await httpClient.GetStringAsync(allVehiclesUrl);
                var allVehiclesHumid = JsonConvert.DeserializeObject<List<VehicleHumid>>(allVehiclesResponse);

                return VehicleViewHumid;
            }
        }
        internal async static Task<List<Location>> GetAllVehicleLocation()
        {
            List<Location> VehicleViewLocation = new List<Location>();
            var allVehiclesUrl = $"{LocationUrl}/alllocations";
            using (HttpClient httpClient = new HttpClient())
            {
                var allVehiclesResponse = await httpClient.GetStringAsync(allVehiclesUrl);
                var allVehiclesLocation = JsonConvert.DeserializeObject<List<Location>>(allVehiclesResponse);

                return VehicleViewLocation;
            }
        }



        internal static async Task<Vehicle> AddVehicle(AddVehicle VehicleAddModel)
        {
            var addVehicleUrl = $"{BaseUrl}/add";
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


        internal async static Task<Vehicle> UpdateVehicle(UpdateVehicle VehicleUpdateModel, Guid id)
        {
            var updateVehicleUrl = $"{BaseUrl}/{id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var url = new Uri(updateVehicleUrl);
                string jsonTranport = JsonConvert.SerializeObject(VehicleUpdateModel);
                var jsonPayload = new StringContent(jsonTranport, Encoding.UTF8, "application/json");
                var updateVehicleResponse = await httpClient.PutAsync(updateVehicleUrl, jsonPayload);
                var responseContent = await updateVehicleResponse.Content.ReadAsStringAsync();
                var updatedVehicle = JsonConvert.DeserializeObject<Vehicle>(responseContent);
                return updatedVehicle;
            }
        }

        
        internal async static Task<string> DeleteVehicle(Guid Id)
        {
            var deleteVehicleUrl = $"{BaseUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var VehicleDeleteResponse = await httpClient.DeleteAsync(deleteVehicleUrl);
                var responseContent = await VehicleDeleteResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
}
