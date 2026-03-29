using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Tracking.Application.Common.Hubs
{
    public class DangerHub : Hub
    {
        public async Task JoinTracking(string code)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, code);
        }
    }
}
