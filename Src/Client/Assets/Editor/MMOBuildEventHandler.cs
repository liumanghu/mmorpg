using System.IO;
using System.Runtime.InteropServices;
using GameFramework;
using UnityEditor; 
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;

namespace Editor
{
    public class MMOBuildEventHandler : IBuildEventHandler
    {
        public bool ContinueOnFailure => false;

        public void OnPreprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
            string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName,
            bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName,
            string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions, string workingPath,
            bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath,
            bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
            var outPutPath = Application.streamingAssetsPath;
            //获取目录下所有文件
            var fileNames = Directory.GetFiles(outPutPath, "*", SearchOption.AllDirectories);
            //删除旧有的文件
            foreach (var fileName in fileNames)
            {
                File.Delete(fileName);
            }

            Utility.Path.RemoveEmptyDirectory(outPutPath);
        }

        public void OnPreprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath,
            bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath)
        {
        }

        public void OnBuildAssetBundlesComplete(Platform platform, string workingPath, bool outputPackageSelected,
            string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected,
            string outputPackedPath, AssetBundleManifest assetBundleManifest)
        {
        }

        public void OnOutputUpdatableVersionListData(Platform platform, string versionListPath, int versionListLength,
            int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode)
        {
        }

        public void OnPostprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath,
            bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, bool isSuccess)
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.iOS:
                    if (platform != Platform.IOS) break;
                    CopyAssetFiles(outputPackagePath);
                    break;
                case BuildTarget.Android:
                    if (platform != Platform.Android) break;
                    CopyAssetFiles(outputPackagePath);
                    break;
            }
        }

        private void CopyAssetFiles(string outputDirectory)
        {
            //将新生成的文件拷贝到streaming目录下
            var streamingPath = Application.streamingAssetsPath;

            var allFileNames = Directory.GetFiles(outputDirectory, "*", SearchOption.AllDirectories);
            foreach (var fileName in allFileNames)
            {
                var destFileName =
                    Utility.Path.GetRegularPath(Path.Combine(streamingPath,
                        fileName.Substring(outputDirectory.Length)));
                File.Copy(fileName, destFileName);
            }
            
            Debug.Log("资源打包并导入成功");
        }

        public void OnPostprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
            string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName,
            bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName,
            string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions, string workingPath,
            bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath,
            bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
        }
    }
}