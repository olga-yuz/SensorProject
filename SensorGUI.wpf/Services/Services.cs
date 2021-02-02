using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SensorGUI.wpf.Services
{
    /**
    public class Services
    {
        private static string BaseUrl = "http://localhost:81/api/Vehicles";
        internal async static Task<List<Vehicle>> GetAllVehicle()
        {
            var allVehiclesUrl = $"{BaseUrl}/all";
            using (HttpClient httpClient = new HttpClient())
            {
                var allVehiclesResponse = await httpClient.GetStringAsync(allVehiclesUrl);
                var allVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(allVehiclesResponse);
                return allVehicles;
            }
        }
        internal static async Task<Vehicle> AddVehicle(addVehicle VehicleAddModel)
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


        internal async static Task<Vehicle> UpdateVehicle(updateVeicle VehicleUpdateModel, Guid id)
        {
            var updateVehicleUrl = $"{BaseUrl}/{id}";
            using (HttpClient httpClient = new HttpClient())
            {
                //var url = new Uri(updateVehicleUrl);
                string jsonTranport = JsonConvert.SerializeObject(VehicleUpdateModel);
                var jsonPayload = new StringContent(jsonTranport, Encoding.UTF8, "application/json");
                var updateVehicleResponse = await httpClient.PutAsync(updateVehicleUrl, jsonPayload);
                var responseContent = await updateVehicleResponse.Content.ReadAsStringAsync();
                var updatedVehicle = JsonConvert.DeserializeObject<Vehicle>(responseContent);
                return updatedVehicle;
            }
        }

        
        ]internal async static Task<string> DeleteVehicle(Guid Id)
        {
            var deleteVehicleUrl = $"{BaseUrl}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var VehicleDeleteResponse = await httpClient.DeleteAsync(deleteVehicleUrl);
                var responseContent = await VehicleDeleteResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }**/
}
