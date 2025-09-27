using Common;
using Network;
using SkillBridge.Message;

namespace GameServer.Network.Handler
{
    [PacketInfo(PacketType = typeof(HeartBeatRequest))]
    public class HeartBeatPacketHandler : PacketHandlerBase<HeartBeatRequest>
    {
        protected override HeartBeatRequest GetPacket(NetMessageRequest messageRequest)
        {
            return messageRequest.HeartRequest;
        }

        protected override void Handle(NetConnection sender, HeartBeatRequest heartBeatRequest)
        {
            var ip = ((NetConnection.State)sender.EventArgs.UserToken).socket.RemoteEndPoint;
            Log.Info($"收到了来自客户端[{ip}]的心跳数据");
            
            var heartBeatMessage = MessageFactory.Instance.GetHeartBeatRequest();
            byte[] data = PackageHandler.PackMessage(heartBeatMessage);
            sender.SendData(data, 0, data.Length);
        }
    }
}