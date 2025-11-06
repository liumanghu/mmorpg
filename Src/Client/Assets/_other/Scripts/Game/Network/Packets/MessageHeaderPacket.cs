using GameFramework;
using GameFramework.Network;

namespace MMORPG.Game.Network.Packets
{
    public class MessageHeaderPacket : IPacketHeader, IReference
    {
        private int _packetLength;

        public int PacketLength
        {
            set => _packetLength = value;
            get => _packetLength;
        }

        public static MessageHeaderPacket GetNew(int packetLength)
        {
            var packetHeader = ReferencePool.Acquire<MessageHeaderPacket>();
            packetHeader.PacketLength = packetLength;

            return packetHeader;
        }

        public void Clear()
        {
            _packetLength = 0;
        }
    }
}