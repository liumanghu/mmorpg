using UnityEngine;

namespace MMORPG.Game
{
    public partial class GameLauncher : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
            InitCustomDebuggers();
        }
    }
}