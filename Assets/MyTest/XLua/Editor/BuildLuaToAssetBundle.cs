using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

namespace XLua
{
    public static class BuildLuaToAssetBundle
    {
        private static string bytesPath = "Assets/XLua/Out";   // 输出的 *.bytes文件所在目录，这个目录需要放在Assets下，不然没法设置bundleName以及打包操作
        private static string luaPath = "/../LuaLogic";     // lua源文件
        private static string bundlePath = "/../StreamingAssets/LuaHotfix";  // 打包后放的位置

//        [MenuItem("XLua/Clean Lua AssetBundle", false, 20)]
//        public static void CleanAssetBundle()
//        {
//            string bundleOutPath = Path.Combine(Application.streamingAssetsPath, "bundle");
//            if (!Directory.Exists(bundleOutPath))
//            {
//                Directory.CreateDirectory(bundleOutPath);
//            }
//            else
//            {
//                Directory.Delete(bundleOutPath, true);
//            }
//
//            ClearBytesFile();
//        }

        [MenuItem("XLua/Build To AssetBundle", false, 21)]
        public static void BuildToAssetBundle()
        {
            // 把lua源代码转为bytes后缀并拷贝到输出文件夹
            CopyLuaFileToBytes();

            // 先清理掉设置的AB name
            ClearAssetBundlesName();

            // 将输出的 *.bytes 文件打包
            Pack(bytesPath);

            // 开始打包
            BuildAssetBundles();

            // 打包完毕后清理掉生成的bytes文件
            ClearBytesFile();

            Debug.Log("XLua 打包完成");
        }

        // 把lua源代码转为bytes后缀并拷贝到输出文件夹
        static void CopyLuaFileToBytes()
        {
            // 清理这个目录下的 *.bytes文件，方便重新生成
            CheckAndCreateDirectory(bytesPath);

            // 读取luaPath下的lua源文件
            // 把 *.lua文件都转化为 *.bytes文件
            // 然后拷贝到outPath下
            string[] files = Directory.GetFiles(Application.dataPath + luaPath, "*.lua", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                string bytes_file_name = Path.GetFileName(files[i]);
                FileUtil.CopyFileOrDirectory(files[i], bytesPath + "/" + bytes_file_name + ".bytes");
            }

            AssetDatabase.Refresh();
        }

        // 先清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
        static void ClearAssetBundlesName()
        {
            int length = AssetDatabase.GetAllAssetBundleNames().Length;
//            Debug.Log("Clear start, now asset bundle names length is: " + length);

            string[] oldAssetBundleNames = new string[length];
            for (int i = 0; i < length; i++)
            {
                // 只清除有关Lua代码的AB
                if (AssetDatabase.GetAllAssetBundleNames()[i].Contains(".lua.bytes"))
                {
                    oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
                }
            }

            for (int i = 0; i < oldAssetBundleNames.Length; i++)
            {
                AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[i], true);
            }
//            Debug.Log("Clear completed, now asset bundle names length is: " + AssetDatabase.GetAllAssetBundleNames().Length);
//            Debug.Log("--------------------------------");
        }

        static void Pack(string source)
        {
            DirectoryInfo folder = new DirectoryInfo(source);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    Pack(files[i].FullName);
                }
                else
                {
                    if (!files[i].Name.EndsWith(".meta"))
                    {
                        SetBundleName(files[i].FullName);
                    }
                }
            }
        }

        // 设置要打包的文件
        static void SetBundleName(string source)
        {
//            Debug.Log("Pack source: " + source);
            string _source = source.Replace("\\", "/");
            string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);

//            Debug.Log(_assetPath);

            // 自动获取依赖项并给其资源设置AssetBundleName
            string[] dependencies = AssetDatabase.GetDependencies(_assetPath);
            foreach (var dependency in dependencies)
            {
//                Debug.Log("Dependency: " + dependency);
                if (!dependency.EndsWith(".bytes"))      // 只打包bytes
                {
                    continue;
                }

                AssetImporter assetImporter = AssetImporter.GetAtPath(dependency);
                string pathTem = dependency.Substring("Assets".Length + 1).Replace("Out/", "");
//                Debug.Log("pathTem: " + pathTem);
                string assetName = pathTem.Substring(pathTem.IndexOf("/", StringComparison.Ordinal) + 1);
//                Debug.Log("assetname: " + assetName.ToLower());
//                Debug.Log("assetImporter is null?: " + (assetImporter == null));
                assetImporter.assetBundleName = assetName.ToLower();
//                Debug.Log("assetBundleName: " + assetImporter.assetBundleName);
//                Debug.Log("--------------------------------");
            }
        }

        // 开始打包
        static void BuildAssetBundles()
        {
            string bundleOutPath = Application.dataPath + bundlePath;
            CheckAndCreateDirectory(bundleOutPath);

            // 开始打包
            BuildPipeline.BuildAssetBundles(bundleOutPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.Refresh();
        }

        // 打包完毕后清理掉生成的bytes文件
        static void ClearBytesFile()
        {
            if (Directory.Exists(bytesPath))
            {
                Directory.Delete(bytesPath, true);
            }
            AssetDatabase.Refresh();
        }

        static void CheckAndCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            AssetDatabase.Refresh();
        }
    }
}