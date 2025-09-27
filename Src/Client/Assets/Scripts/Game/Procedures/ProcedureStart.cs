using GameFramework.Fsm;
using GameFramework.Procedure;
using MMORPG.DataTable;
using MMORPG.Game.Network;
using UnityGameFramework.Runtime;

namespace MMORPG.Game.Procedures
{
    public class ProcedureStart : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            var buffDefineTable = GameLauncher.DataTable.GetDataTable<BuffDefine_Buffs>();
            var buff = buffDefineTable.GetDataRow(1);
            Log.Info($"{buff.Description}  {buff.Target}");

            //创建网络频道
            ChannelManager.Instance.Connect();
        }
    }
}