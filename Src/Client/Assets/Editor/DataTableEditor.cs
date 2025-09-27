using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Editor
{
    public static class DataTableEditor
    {
        [MenuItem("MMOTool/GeneratorData")]
        public static void GeneratorData()
        {
            var parentPath = Application.dataPath.Remove(Application.dataPath.Length - 14, 14);
            var workPath = $"{parentPath}/Data/osx-arm64";
            var codePath = $"{workPath}/DataTable2TypeTool";
            
            //如果时win平台则执行exe程序
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                workPath = $@"{parentPath}\Data\win";
                codePath = $@"{workPath}\DataTable2TypeTool.exe";
            }
            
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = codePath,
                    WorkingDirectory = workPath,
                    RedirectStandardOutput = true, // 设置为true以读取标准输出
                    RedirectStandardError = true, // 根据需要设置为true以读取标准错误
                    UseShellExecute = false, // 必须设置为false以重定向流
                    CreateNoWindow = false // 通常设置为true以避免创建额外的窗口
                };
                Debug.Log("process工作目录为:" + startInfo.WorkingDirectory);
            
                var process = new Process { StartInfo = startInfo };
                process.Start();
            
                // 等待进程退出（可选）
                process.WaitForExit();
                Debug.Log($"Process exited with code: {process.ExitCode}");
                
                // 读取标准输出
                string output = process.StandardOutput.ReadToEnd();
                Debug.Log($"Process output: {output}");
                
                string errorOutput = process.StandardError.ReadToEnd();
                Debug.Log($"Process Error: {errorOutput}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error running executable: {ex}");
            }
        }
    }
}