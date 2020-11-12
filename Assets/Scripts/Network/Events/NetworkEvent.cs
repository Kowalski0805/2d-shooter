using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Network.Events
{
    public interface NetworkEvent
    {
        byte[] Serialize();

        NetworkEvent CreateEvent(byte[] data);
    }
}
