using Google.Protobuf;
using SkillBridge.Message;

namespace MMORPG.Game.Network.Messagehandler
{
    public interface IMessageHandler
    {
        void Handle(NetMessageResponse messageResponse);
    }
    
    public abstract class MessageHandlerBase<T> : IMessageHandler where T : class, IMessage
    {
        public void Handle(NetMessageResponse messageResponse)
        {
            var message = GetMessage(messageResponse);
            if (message == null) return;
            Handle(message);
        }

        protected abstract T GetMessage(NetMessageResponse messageResponse);

        protected abstract void Handle(T message);
    }
}