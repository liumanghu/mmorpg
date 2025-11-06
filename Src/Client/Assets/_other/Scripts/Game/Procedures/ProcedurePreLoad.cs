using System.Collections.Generic;
using System.IO;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using Google.Protobuf;
using MMORPG.DataTable;
using MMORPG.Game.Network.Packets;
using UnityGameFramework.Runtime;

namespace MMORPG.Game.Procedures
{
    public class ProcedurePreLoad : ProcedureBase
    {
        //资源加载标志
        private Dictionary<string, bool> _loadedFlag = new Dictionary<string, bool>();
        
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            Log.Info("进入资源加载流程");
            SubscribeEvents();
            
            //加载数据表
            PreLoadDataTables();
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            UnSubscribeEvents();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            if (_loadedFlag.Count == 0) return;
            //判断是否左右的资源全部加载完成
            foreach (var loadedFlag in _loadedFlag.Values)
            {
                if (!loadedFlag) return;
            }
            
            //切换下一个状态
            ChangeState<ProcedureStart>(procedureOwner);
        }

        private void SubscribeEvents()
        {
            GameLauncher.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadTableSuccess);
            GameLauncher.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadTableFailure);
        }

        private void UnSubscribeEvents()
        {
            GameLauncher.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadTableSuccess);
            GameLauncher.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadTableFailure);
        }

        /// <summary>
        /// 预加载数据表
        /// </summary>
        public void PreLoadDataTables()
        {
            //判断路径是否存在
            if (!Directory.Exists(AssetUtility.AssetDataTablePath))
            {
                Log.Error($"数据表预加载失败，对应路径不存在:{AssetUtility.AssetDataTablePath}");
            }

            var tableFiles = Directory.GetFiles(AssetUtility.AssetDataTablePath);
            foreach (var tablePath in tableFiles)
            {
                if (tablePath.EndsWith("meta")) continue;
                var tableName = tablePath.Remove(0, AssetUtility.AssetDataTablePath.Length + 1);
                //添加加载标志位
                _loadedFlag.Add(tablePath, false);
                GameLauncher.DataTable.LoadDataTable(tableName, tablePath, this);
            }
        }

        private void OnLoadTableSuccess(object sender, GameEventArgs e)
        {
            var ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            _loadedFlag[ne.DataTableAssetName] = true;
            // Log.Info("Load table '{0}' OK.", ne.DataTableAssetName);
        }

        private void OnLoadTableFailure(object sender, GameEventArgs e)
        {
            var ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }

        
    }
}