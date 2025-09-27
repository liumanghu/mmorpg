using Common;
using Google.Protobuf;
using Network;
using SkillBridge.Message;

namespace GameServer.Network.Handler
{
    public interface IPacketHandler
    {
        void Handle(NetConnection sender, NetMessageRequest messageRequest);
    }
    public abstract class PacketHandlerBase<T> : IPacketHandler where T : class, IMessage
    {
        public void Handle(NetConnection sender, NetMessageRequest messageRequest)
        {
            var packet = GetPacket(messageRequest);
            if (packet == null) return;
            Handle(sender, packet);
        }

        protected abstract T GetPacket(NetMessageRequest messageRequest);

        protected abstract void Handle(NetConnection sender, T message);
    }
}