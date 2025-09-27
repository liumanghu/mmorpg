using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace MMORPG.Game.Procedures
{
    public class ProcedureLaunch : ProcedureBase
    {
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            
            ChangeState<ProcedureSplash>(procedureOwner);
        }
    }
}
