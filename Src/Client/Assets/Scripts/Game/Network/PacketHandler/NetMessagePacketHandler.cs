using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Network;
using MMORPG.Game.Network.Messagehandler;
using MMORPG.Game.Network.Packets;
using UnityGameFramework.Runtime;

namespace MMORPG.Game.Network.PacketHandler
{
    public class NetMessagePacketHandler : IPacketHandler
    {
        private const string HandlerNameSpace = "MMORPG.Game.Network.Messagehandler";
        
        private readonly List<IMessageHandler> _handlers = new List<IMessageHandler>();
        
        public int Id => NetPacketIds.NetMessageId;

        public NetMessagePacketHandler()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.Namespace != HandlerNameSpace
                    || type.IsAbstract
                    || type.IsInterface
                    || type.GetInterface("IMessageHandler") == null) continue;

                var messageInfo = type.GetCustomAttribute<MessageInfo>();
                if (messageInfo == null)
                {
                    Log.Error($"类型{type}没有设置messageInfo");
                    continue;
                }
                
                _handlers.Add((IMessageHandler)Activator.CreateInstance(type));
            }
        }
        
        public void Handle(object sender, Packet packet)
        {
            if (packet is not NetMessagePacket messagePacket) return;

            foreach (var messageHandler in _handlers)
            {
                messageHandler.Handle(messagePacket.Message.Response);
            }
        }
    }
}