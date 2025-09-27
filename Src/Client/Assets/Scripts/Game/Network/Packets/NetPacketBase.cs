using GameFramework;
using GameFramework.Network;
using Google.Protobuf;

namespace MMORPG.Game.Network.Packets
{
    public abstract class NetPacketBase : Packet
    {
        public IMessage Protocol { get; set; }
    }
    
    /// <summary>
    /// packet包装protobuf基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NetPacketBase<T> : NetPacketBase, IReference where T : class, IMessage
    {
        /// <summary>
        /// 网络消息
        /// </summary>
        public T Message
        {
            set => Protocol = value;
            get => Protocol as T;
        }
        
        
        public override void Clear()
        {
            Protocol = default;
        }
    }
}