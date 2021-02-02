
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorSignalR.Hubs
{ 
    public class DataHub: Hub
    {
        //Connection on login
        public async override Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Chat Users");
            await base.OnConnectedAsync();
        }
        //Discconection on Signout
        public async override Task OnDisconnectedAsync(Exception eexception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Chat");
            await base.OnDisconnectedAsync(eexception);
        }
        //sends message to all clients possibly use to send data stream
        public async Task SendTemp(int id, int temp)
        {
            await Clients.All.SendAsync("TempData", id, temp);
        }
        public async Task SendHumid(int id, int humid)
        {
            await Clients.All.SendAsync("HumidData", id, humid);
        }
        public async Task SendGPS(int id, int lat, int _long, DateTime time)
        {
            await Clients.All.SendAsync("GPSData", id, lat, _long, time);
        }
    }
}
