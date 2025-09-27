using System;

namespace GameServer.Network.Handler
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketInfo : Attribute
    {
        //对应的消息类型
        public Type PacketType;
    }
}