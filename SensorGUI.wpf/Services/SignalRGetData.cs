using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorGUI.wpf.Services
{
    public class SignalRGetData
    {
        private readonly HubConnection _connection;

        //data recieved from connection
        //public event Action<Veichle>

        public SignalRGetData(HubConnection connection)
        {
            _connection = connection;
        }
    }
}
