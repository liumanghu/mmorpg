using System;

namespace MMORPG.Game.Network.Messagehandler
{
    public class MessageInfo : Attribute
    {
        public Type MessageType;
    }
}