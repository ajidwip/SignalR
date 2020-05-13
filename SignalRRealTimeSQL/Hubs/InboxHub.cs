using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRRealTimeSQL.Hubs
{
    public class InboxHub: Hub
    {
        public async Task Inbox(string masterkey)
        {
            await Clients.All.GetInbox(masterkey);
        }
    }
}