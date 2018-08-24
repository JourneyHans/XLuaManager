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
        private static string outPath = "Assets/XLua/Out";      // 输出的 *.bytes文件所在目录
        private static string luaPath = "";

        [MenuItem("XLua/Build To AssetBundle", false, 4)]
        public static void BuildToAssetBundle()
        {
            // TODO:过时的方法
            BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies |
                                              BuildAssetBundleOptions.CompleteAssets |
                                              BuildAssetBundleOptions.DeterministicAssetBundle;

            // 清理这个目录下的 *.bytes文件，方便重新生成
            // TODO:似乎可以删除整个文件夹
            string[] files = Directory.GetFiles("Assets/XLua/Out", "*.lua.bytes");
            for (int i = 0; i < files.Length; i++)
            {
                FileUtil.DeleteFileOrDirectory(files[i]);
            }

            // 把 *.lua文件都转化为 *.bytes文件
//            files = Directory.GetFiles()
        }
    }
}
