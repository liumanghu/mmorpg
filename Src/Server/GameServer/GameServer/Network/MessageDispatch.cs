//WARNING: DON'T EDIT THIS FILE!!!

using System;
using System.Collections.Generic;
using System.Reflection;
using Common;
using GameServer.Network.Handler;
using Network;
using SkillBridge.Message;

namespace GameServer.Network
{
    public class MessageDispatch : Singleton<MessageDispatch>
    {
        private const string HandlerNameSpace = "GameServer.Network.Handler";
        private readonly List<IPacketHandler> _handlers = new List<IPacketHandler>();

        public MessageDispatch()
        {
            //通过反射获取所有的包处理对象
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.Namespace != HandlerNameSpace
                    || type.IsAbstract
                    || type.IsInterface
                    || type.GetInterface("IPacketHandler") == null) continue;

                var packetInfo = type.GetCustomAttribute<PacketInfo>();
                if (packetInfo == null)
                {
                    Log.Error($"类型{type}没有设置packetInfo");
                    continue;
                }
                
                _handlers.Add((IPacketHandler)Activator.CreateInstance(type));
            }
        }

        public void Dispatch(NetConnection sender, NetMessageRequest message)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(sender, message);
            }
            // if (message.HeartRequest != null) {MessageDistributer<T>.Instance.RaiseEvent(sender,message.HeartRequest);}
            // if (message.UserRegister != null) { MessageDistributer<T>.Instance.RaiseEvent(sender,message.UserRegister); }
            // if (message.UserLogin != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.UserLogin); }
            // if (message.CreateChar != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.CreateChar); }
            // if (message.GameEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.GameEnter); }
            // if (message.GameLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.GameLeave); }
            // if (message.mapCharacterEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapCharacterEnter); }
            // if (message.mapEntitySync != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapEntitySync); }
            // if (message.mapTeleport != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapTeleport); }
            // if (message.itemBuy != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemBuy); }
            // if (message.itemEquip != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemEquip); }
            // if (message.questReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questReq); }
            //
            // if (message.friendAddReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendAddReq); }
            // if (message.friendAddRes != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendAddRes); }
            // if (message.friendList != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendList); }
            // if (message.friendRemove != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendRemove); }
            //
            // if (message.teamCreate != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamCreate); }
            // if (message.teamInviteReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteReq); }
            // if (message.teamInviteRes != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteRes); }
            // if (message.teamInfo != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInfo); }
            // if (message.teamLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamLeave); }
            // if (message.teamJoinReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamJoinReq); }
            // if (message.teamJoinRes != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamJoinRes); }
            // if (message.teamTransfer != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamTransfer); }
            //
            // if (message.guildCreate != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildCreate); }
            // if (message.guildJoinReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildJoinReq); }
            // if (message.guildJoinRes != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildJoinRes); }
            // if (message.Guild != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.Guild); }
            // if (message.guildLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildLeave); }
            // if (message.guildList != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildList); }
            // if (message.guildAdmin != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.guildAdmin); }
            //
            // if (message.Chat != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.Chat); }
            // if (message.friendChat != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendChat); }
            // if (message.skillCast != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.skillCast); }
            // if (message.userResurgence != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.userResurgence); }
            // if (message.arenaChallengeReq != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.arenaChallengeReq); }
            //
            // if (message.arenaChallengeRes != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.arenaChallengeRes); }
            // if (message.arenaReady != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.arenaReady); }
            // if (message.storyStart != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.storyStart); }
            // if (message.storyEnd != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.storyEnd); }
        }
    }
}