using Assets.Scripts.Network.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Network.Handlers
{
    public interface ServerNetworkEventHandler
    {
        NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player);
    }

    public interface ClientNetworkEventHandler
    {
        NetworkEvent Handle(Client client, NetworkEvent e);
    }
}
