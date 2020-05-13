using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace signalrservices.Hubs
{
    public class MoveViewhub : Hub
    {
        public async Task MoveViewFromServer(float newX, float newY)
        {
            await Clients.Others.SendAsync("ReceiveNewPosition", newX, newY);
        }
    }
}
