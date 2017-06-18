using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace PL.Hubs
{
    [Authorize]
    public class ConversationHub : Hub
    {
        public override Task OnConnected()
        {
            Groups.Add(Context.ConnectionId, Context.User.Identity.Name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Groups.Remove(Context.ConnectionId, Context.User.Identity.Name);

            return base.OnDisconnected(stopCalled);
        }
    }
}