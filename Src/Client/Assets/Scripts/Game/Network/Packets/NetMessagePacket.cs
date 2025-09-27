using GameFramework;
using SkillBridge.Message;

namespace MMORPG.Game.Network.Packets
{
    public sealed class NetMessagePacket : NetPacketBase<NetMessage>
    {
        public override int Id => NetPacketIds.NetMessageId;

        /// <summary>
        /// 获取心跳包
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static NetMessagePacket GetHeartBeatPacket(int num)
        {
            var packet = ReferencePool.Acquire<NetMessagePacket>();
            var netMessage = new NetMessage();
            var netRequest = new NetMessageRequest();
            var heartBeatRequest = new HeartBeatRequest();
            netRequest.HeartRequest = heartBeatRequest;
            netMessage.Request = netRequest;
            packet.Message = netMessage;
            return packet;
        }
    }
}