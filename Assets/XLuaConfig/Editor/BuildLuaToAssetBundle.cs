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
        static string bytesPath = "Assets/XLua/Out";    // 输出的 *.bytes文件所在目录，这个目录需要放在Assets下，不然没法设置bundleName以及打包操作
        static string luaPath = "/LuaLogic";            // lua源文件
        static string bundlePath = "/StreamingAssets/LuaHotfix";  // 打包后放的位置
        
        static List<AssetBundleBuild> luaBundleMaps = new List<AssetBundleBuild>();     // 打包map
        static List<string> paths = new List<string>();
        static List<string> files = new List<string>();

        [MenuItem("XLua/Build To AssetBundle", false, 21)]
        public static void BuildToAssetBundle()
        {
            luaBundleMaps.Clear();
            // 处理lua文件
            HandleLuaFile();

            // 开始打包
            BuildAssetBundles();

            // 生成文件列表
            BuildFileIndex();

            // 打包完毕后清理掉多余的文件
            ClearUselessFile();

            Debug.Log("XLua 打包完成");
            EditorUtility.ClearProgressBar();
        }

        // 处理lua文件
        static void HandleLuaFile()
        {
            EditorUtility.DisplayProgressBar("Copy lua file to bytes", "copy file... 0%", 0);

            // 清理这个目录下的 *.bytes文件，方便重新生成
            CheckAndCreateDirectory(bytesPath);

            // 读取luaPath下的lua源文件
            // 把 *.lua文件都转化为 *.bytes文件
            // 然后拷贝到bytesPath下
            CopyLuaToBytes();

            // 设置打包项
            string[] dirs = Directory.GetDirectories(bytesPath, "*", SearchOption.AllDirectories);
            for (int i = 0; i < dirs.Length; i++)
            {
                string name = dirs[i].Replace(bytesPath + "\\", string.Empty);
                name = name.Replace("\\", "_").Replace("/", "_");
                name = "lua/lua_" + name.ToLower() + ".unity3d";

                string path = dirs[i].Replace("\\", "/");
                AddToBuildMap(name, "*.bytes", path);
            }

            // 添加到打包列表中
            AddToBuildMap("lua/lua.unity3d", "*.bytes", bytesPath + "/");

            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
        }

        // 把*.lua拷贝为*.bytes，用于打包
        static void CopyLuaToBytes()
        {
            string luaFileFullPath = Application.dataPath + luaPath;
            string[] files = Directory.GetFiles(luaFileFullPath, "*.lua", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                float progress = (float)i / files.Length;
                EditorUtility.DisplayProgressBar("Copy lua file to bytes", "copy file..." + Mathf.Round(progress * 100) + "%", progress);

                string luaFilePath = files[i].Remove(0, luaFileFullPath.Length);
                string dest = bytesPath + "/" + luaFilePath + ".bytes";
                string dir = Path.GetDirectoryName(dest);
                Directory.CreateDirectory(dir);
                File.Copy(files[i], dest, true);
            }
        }

        /// 添加到打包列表中
        static void AddToBuildMap(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern);

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace("\\", "/");
//                Debug.Log("\t [AddToBuildMap] file:" + files[i]);
            }

            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = bundleName;
            build.assetNames = files;
            luaBundleMaps.Add(build);
        }

        // 开始打包
        static void BuildAssetBundles()
        {
            EditorUtility.DisplayProgressBar("Build Asset Bundles", "Prepare build... 0%", 0);
            string bundleOutPath = Application.dataPath + bundlePath;
            EditorUtility.DisplayProgressBar("Build Asset Bundles", "Prepare build... 10%", 0.1f);
            CheckAndCreateDirectory(bundleOutPath);
            EditorUtility.DisplayProgressBar("Build Asset Bundles", "Prepare build... 50%", 0.5f);

            // 开始打包
            BuildPipeline.BuildAssetBundles(bundleOutPath, luaBundleMaps.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            EditorUtility.DisplayProgressBar("Build Asset Bundles", "Build Complete", 1);

            AssetDatabase.Refresh();
        }

        // 打包完毕后清理掉生成的bytes文件
        static void ClearUselessFile()
        {
            if (Directory.Exists(bytesPath))
            {
                Directory.Delete(bytesPath, true);
            }
            AssetDatabase.Refresh();
        }

        // 生成文件列表，用于对比下载
        static void BuildFileIndex()
        {
            string resPath = Application.dataPath + bundlePath;
            string newFilePath = resPath + "/files.txt";
            if (File.Exists(newFilePath)) File.Delete(newFilePath);

            paths.Clear();
            files.Clear();
            Recursive(resPath);

            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

                string md5 = Util.md5file(file);
                string value = file.Replace(resPath + "/", string.Empty);
                sw.WriteLine(value + "|" + md5);
            }

            sw.Close();
            fs.Close();
        }

        // 遍历目录及子目录
        static void Recursive(string path)
        {
            string[] names = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                string ext = Path.GetExtension(filename);
                if (ext.Equals(".meta")) continue;
                files.Add(filename.Replace('\\', '/'));
            }
            foreach (string dir in dirs)
            {
                paths.Add(dir.Replace('\\', '/'));
                Recursive(dir);
            }
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