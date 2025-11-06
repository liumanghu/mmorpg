using System;
using System.IO;
using System.Text;
using MMORPG.Game.Network.PacketHandler;
using GameFramework.Network;
using GameFramework;
using GameFramework.Event;
using Google.Protobuf;
using MMORPG.Game.Network.Packets;
using SkillBridge.Message;
using UnityGameFramework.Runtime;

namespace MMORPG.Game.Network
{
    public class MmoNetworkChannelHelper : INetworkChannelHelper
    {
        private readonly MemoryStream m_CachedStream = new MemoryStream(1024 * 64);
        
        private INetworkChannel _networkChannel;
        
        /// <summary>
        /// 消息包头长度
        /// </summary>
        public int PacketHeaderLength => sizeof(int);
        
        
        /// <summary>
        /// 初始化网络频道辅助器
        /// </summary>
        /// <param name="networkChannel"></param>
        public void Initialize(INetworkChannel networkChannel)
        {
            _networkChannel = networkChannel;
            
            //注册消息处理事件
            _networkChannel.RegisterHandler(new NetMessagePacketHandler());
            
            //注册事件
            GameLauncher.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameLauncher.Event.Subscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameLauncher.Event.Subscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameLauncher.Event.Subscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameLauncher.Event.Subscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);
        }

        /// <summary>
        /// 关闭并清理网络频道辅助器
        /// </summary>
        public void Shutdown()
        {
            _networkChannel = null;
            
            GameLauncher.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameLauncher.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameLauncher.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameLauncher.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameLauncher.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);
        }

        public void PrepareForConnecting()
        {
            _networkChannel.Socket.ReceiveBufferSize = 1024 * 64;
            _networkChannel.Socket.SendBufferSize = 1024 * 64;
        }

        /// <summary>
        /// 发送心跳数据包
        /// </summary>
        /// <returns></returns>
        public bool SendHeartBeat()
        {
            var heartBeat = NetMessagePacket.GetHeartBeatPacket(0);
            if (heartBeat == null) return false;
            _networkChannel.Send(heartBeat);
            return true;
        }

        
        public bool Serialize<T>(T packet, Stream destination) where T : Packet
        {
            var packetImpl = packet as NetPacketBase;
            if (packetImpl == null)
            {
                Log.Warning("Packet is invalid.");
                return false;
            }
            
            //重设缓存区
            m_CachedStream.Position = 0;
            m_CachedStream.SetLength(0);
            
            //暂时序列化网络包，获取包体长度
            packetImpl.Protocol.WriteTo(m_CachedStream);
            var packetLength = (int)m_CachedStream.Length;
            
            //设置请求头
            destination.Write(BitConverter.GetBytes(packetLength));
            
            //正式序列化包体数据
            packetImpl.Protocol.WriteTo(destination);
            ReferencePool.Release(packet);
            
            return true;
        }

        /// <summary>
        /// 反序列化消息包头
        /// </summary>
        /// <param name="source"></param>
        /// <param name="customErrorData"></param>
        /// <returns></returns>
        public IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData)
        {
            // 注意：此函数并不在主线程调用！
            customErrorData = null;
            var stream = source as MemoryStream;
            if (stream == null)
            {
                Log.Error("网络字节流类型不对");
                return null;
            }
            var packetLength = BitConverter.ToInt32(stream.GetBuffer(), 0);
            var packetHeader = MessageHeaderPacket.GetNew(packetLength);
            return packetHeader;
        }

        /// <summary>
        /// 反序列化消息包。
        /// </summary>
        public Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData)
        {
            // 注意：此函数并不在主线程调用！
            customErrorData = null;
            
            var scPacketHeader = packetHeader as MessageHeaderPacket;
            if (scPacketHeader == null)
            {
                Log.Warning("Packet header is invalid.");
                return null;
            }
            
            var netMessage = new NetMessage();
            netMessage.MergeFrom(source);
            var packet = ReferencePool.Acquire<NetMessagePacket>();
            packet.Message = netMessage;
            
            ReferencePool.Release(scPacketHeader);
            
            return packet;
        }
        
        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne = (UnityGameFramework.Runtime.NetworkConnectedEventArgs)e;
            if (ne.NetworkChannel != _networkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' connected, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void OnNetworkClosed(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkClosedEventArgs ne = (UnityGameFramework.Runtime.NetworkClosedEventArgs)e;
            if (ne.NetworkChannel != _networkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' closed.", ne.NetworkChannel.Name);
        }

        private void OnNetworkMissHeartBeat(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs ne = (UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs)e;
            if (ne.NetworkChannel != _networkChannel)
            {
                return;
            }

            Log.Warning("Network channel '{0}' miss heart beat '{1}' times.", ne.NetworkChannel.Name, ne.MissCount.ToString());

            if (ne.MissCount < 2)
            {
                return;
            }

            ne.NetworkChannel.Close();
        }

        private void OnNetworkError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkErrorEventArgs)e;
            if (ne.NetworkChannel != _networkChannel)
            {
                return;
            }

            Log.Error("Network channel '{0}' error, error code is '{1}', error message is '{2}'.", ne.NetworkChannel.Name, ne.ErrorCode.ToString(), ne.ErrorMessage);

            ne.NetworkChannel.Close();
        }

        private void OnNetworkCustomError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkCustomErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkCustomErrorEventArgs)e;
            if (ne.NetworkChannel != _networkChannel)
            {
                return;
            }
        }
    }
}