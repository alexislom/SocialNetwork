using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace PL.Hubs
{
    [Authorize]
    public class ConversationHub : Hub
    {
        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;
            Groups.Remove(Context.ConnectionId, name);

            return base.OnDisconnected(stopCalled);
        }
    }
}