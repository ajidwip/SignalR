using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRRealTimeSQL
{
    public class MyHub : Hub
    {
        public async Task Show()
        {
            await Clients.All.displayStatus();
        }
    }
}