using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Network;
using SkillBridge.Message;
using GameServer.Entities;
using GameServer.Managers;
using GameServer.Network;

namespace GameServer.Services
{
    class UserService : Singleton<UserService>
    {

        public UserService()
        {
            // MessageDistributer<NetConnection>.Instance.Subscribe<UserLoginRequest>(this.OnLogin);
            // MessageDistributer<NetConnection>.Instance.Subscribe<UserRegisterRequest>(this.OnRegister);
            // MessageDistributer<NetConnection>.
            //     Instance.Subscribe<UserCreateCharacterRequest>(this.OnCharacterCreate);
            // MessageDistributer<NetConnection>.Instance.Subscribe<UserGameEnterRequest>(this.OnGameEnter);
            // MessageDistributer<NetConnection>.Instance.Subscribe<UserGameLeaveRequest>(this.OnGameLeave);
        }

        public void Init()
        {

        }

        void OnLogin(NetConnection sender,UserLoginRequest request)
        {
            Log.InfoFormat("UserLoginRequest: User:{0}  Pass:{1}", request.User, request.Passward);

            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.UserLogin = new UserLoginResponse();


            TUser user = DBService.Instance.Entities.Users.Where(u => u.Username == request.User).FirstOrDefault();
            if(user==null)
            {
                message.Response.UserLogin.Result = RESULT.Failed;
                message.Response.UserLogin.Errormsg = "用户不存在";
            }
            else if(user.Password != request.Passward)
            {
                message.Response.UserLogin.Result = RESULT.Failed;
                message.Response.UserLogin.Errormsg = "密码错误";
            }
            else
            {
                sender.Session.User = user;

                message.Response.UserLogin.Result = RESULT.Success;
                message.Response.UserLogin.Errormsg = "None";
                message.Response.UserLogin.Userinfo = new NUserInfo();
                message.Response.UserLogin.Userinfo.Id = (int)user.ID;
                message.Response.UserLogin.Userinfo.Player = new NPlayerInfo();
                message.Response.UserLogin.Userinfo.Player.Id = user.Player.ID;
                foreach(var c in user.Player.Characters)
                {
                    NCharacterInfo info = new NCharacterInfo();
                    info.Id = c.ID;
                    info.Name = c.Name;
                    info.Class = (CHARACTER_CLASS)c.Class;
                    info.MapId = c.MapID;
                    message.Response.UserLogin.Userinfo.Player.Characters.Add(info);
                }
               
            }
            byte[]  data = PackageHandler.PackMessage(message);
            sender.SendData(data, 0, data.Length);
        }

        void OnRegister(NetConnection sender, UserRegisterRequest request)
        {
            Log.InfoFormat("UserRegisterRequest: User:{0}  Pass:{1}", request.User, request.Passward);

            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.UserRegister = new UserRegisterResponse();


            TUser user = DBService.Instance.Entities.Users.Where(u => u.Username == request.User).FirstOrDefault();
            if (user != null)
            {
                message.Response.UserRegister.Result = RESULT.Failed;
                message.Response.UserRegister.Errormsg = "用户已存在.";
            }
            else
            {
                TPlayer player = DBService.Instance.Entities.Players.Add(new TPlayer());
                DBService.Instance.Entities.Users.Add(new TUser() { Username = request.User, Password = request.Passward, Player = player });
                DBService.Instance.Entities.SaveChanges();
                message.Response.UserRegister.Result = RESULT.Success;
                message.Response.UserRegister.Errormsg = "None";
            }

            byte[] data = PackageHandler.PackMessage(message);
            sender.SendData(data, 0, data.Length);
        }
        void OnCharacterCreate(NetConnection sender,UserCreateCharacterRequest request)
        {
            Log.InfoFormat("UserCharacterRequest: Name:{0}  Class:{1}", request.Name, request.Class);

            TCharacter character = new TCharacter()
            {
                Name = request.Name,
                Class = (int)request.Class,
                TID = (int)request.Class,
                MapID = 1,
                MapPosX = 5000,
                MapPosY = 4000,
                MapPosZ = 820,
            };
            character = DBService.Instance.Entities.Characters.Add(character);
            sender.Session.User.Player.Characters.Add(character);
            DBService.Instance.Entities.SaveChanges();

            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.CreateChar = new UserCreateCharacterResponse();
            message.Response.CreateChar.Result = RESULT.Success;
            message.Response.CreateChar.Errormsg = "None";

            foreach (var c in sender.Session.User.Player.Characters)
            {
                NCharacterInfo info = new NCharacterInfo();
                info.Id = c.ID;
                info.Name = c.Name;
                info.Type = CHARACTER_TYPE.Player;
                info.Class = (CHARACTER_CLASS)c.Class;
                // info.Tid = c.TID;
                message.Response.CreateChar.Characters.Add(info);
            }

            byte[] data = PackageHandler.PackMessage(message);
            sender.SendData(data, 0, data.Length);
        }
        void OnGameEnter(NetConnection sender, UserGameEnterRequest request)
        {
            TCharacter dbchar = sender.Session.User.Player.Characters.ElementAt(request.CharacterIdx);
            Log.InfoFormat("UserGameEnterRequest: characterID:{0}:{1} Map:{2}",dbchar.ID,dbchar.Name,dbchar.MapID);
            Character character = CharacterManager.Instance.AddCharacter(dbchar);

            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.GameEnter = new UserGameEnterResponse();
            message.Response.GameEnter.Result = RESULT.Success;
            message.Response.GameEnter.Errormsg = "None";

            byte[] data = PackageHandler.PackMessage(message);
            sender.SendData(data,0,data.Length);
            sender.Session.Character = character;
            MapManager.Instance[dbchar.MapID].CharacterEnter(sender,character);

        }
        private void OnGameLeave(NetConnection sender, UserGameLeaveRequest request)
        {
            Character character = sender.Session.Character;
            Log.InfoFormat("UserGameLeaveRequest : characterID:{0}:{1} Map:{2}", character.Id,
                character.Info.Name, character.Info.MapId);

            CharacterLeave(character);
            NetMessage message = new NetMessage();
            message.Response = new NetMessageResponse();
            message.Response.GameLeave = new UserGameLeaveResponse();
            message.Response.GameLeave.Result = RESULT.Success;
            message.Response.GameLeave.Errormsg = "None";

            byte[] data = PackageHandler.PackMessage(message);
            sender.SendData(data, 0, data.Length);
        }

        public void CharacterLeave(Character character)
        {
            CharacterManager.Instance.RemoveCharacter(character.Id);
            MapManager.Instance[character.Info.MapId].CharacterLeave(character);
        }
    }
}
