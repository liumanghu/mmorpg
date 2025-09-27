using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Managers;
using GameServer.Network;
using Network;
using SkillBridge.Message;

namespace GameServer.Services
{
    class MapService : Singleton<MapService>
    {
        public MapService()
        {
            // MessageDistributer<NetConnection>.Instance.Subscribe<MapCharacterEnterRequest>(this.OnMapCharacterEnter);
            // MessageDistributer<NetConnection>.Instance.Subscribe<MapEntitySyncRequest>(this.OnMapEntitySync);
        }

        public void Init()
        {
            MapManager.Instance.Init();
        }

        private void OnMapEntitySync(NetConnection sender, MapEntitySyncRequest message)
        {
            Log.InfoFormat("OnMapEntitySync: characterID:{0}:{1} Entity.Id{2} Evt:{3} Entity:{4}",sender.Session.Character.Id,
                sender.Session.Character.Info.Name,
                message.EntitySync.Id,
                message.EntitySync.Entity,
                message.EntitySync.Entity.String());
            MapManager.Instance[sender.Session.Character.Id].UpdateEntity(message.EntitySync);
        }

        private void OnMapCharacterEnter(NetConnection sender, MapCharacterEnterRequest message)
        {
            throw new NotImplementedException();
        }

        internal void SendEntutyUpdate(NetConnection connection, NEntitySync entitySync)
        {
            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.MapEntitySync = new MapEntitySyncResponse();
            message.Response.MapEntitySync.EntitySyncs.Add(entitySync);

            byte[] data = PackageHandler.PackMessage(message);
            connection.SendData(data,0,data.Length);
        }
    }
}
