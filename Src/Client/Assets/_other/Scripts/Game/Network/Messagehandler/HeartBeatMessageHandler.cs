using SkillBridge.Message;
using UnityGameFramework.Runtime;

namespace MMORPG.Game.Network.Messagehandler
{
    [MessageInfo(MessageType = typeof(HeartBeatResponse))]
    public class HeartBeatMessageHandler : MessageHandlerBase<HeartBeatResponse>
    {
        protected override HeartBeatResponse GetMessage(NetMessageResponse messageResponse)
        {
            return messageResponse.HeartResponse;
        }

        protected override void Handle(HeartBeatResponse message)
        {
            Log.Info("收到了来自服务器的心跳数据");
        }
    }
}