using Common;
using SkillBridge.Message;

namespace GameServer.Network
{
    public class MessageFactory : Singleton<MessageFactory>
    {
        public NetMessage GetHeartBeatRequest()
        {
            var message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.HeartResponse = new HeartBeatResponse();

            return message;
        }
    }
}