using GameServer.Core;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Entities
{
    class Monster : CharacterBase
    {
        public Monster(int tid, int level, Vector3Int pos, Vector3Int dir) : base(CHARACTER_TYPE.Monster, tid, level, pos, dir)
        {

        }
    }
}
