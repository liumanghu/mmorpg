using System.Net;
using Common;
using GameFramework.Network;

namespace MMORPG.Game.Network
{
    public class ChannelManager : Singleton<ChannelManager>
    {
        private readonly INetworkChannel m_TcpChannel =
            GameLauncher.Network.CreateNetworkChannel("GameMain", ServiceType.Tcp, new MmoNetworkChannelHelper());

        private readonly IPAddress m_IPAddress = IPAddress.Parse("192.168.31.138");

        private readonly int m_ProtId = 8000;


        public void Connect()
        {
            m_TcpChannel.HeartBeatInterval = 5f;
            m_TcpChannel.Connect(m_IPAddress, m_ProtId);
        }
    }
}