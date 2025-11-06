using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MMORPG
{
    public class LoadDll : MonoBehaviour
    {
        private void Start()
        {
            // Editor环境下，HotUpdate.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
#if !UNITY_EDITOR
            Assembly hotfixAssembly = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/Hotfix.dll.bytes}"));
#else
            // Editor下无需加载，直接查找获得HotUpdate程序集
            Assembly hotFixAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Hotfix");
#endif

            Type type = hotFixAss.GetType("Hotfix.Hello");
            type.GetMethod("Run")?.Invoke(null, null);
        }
    }
}