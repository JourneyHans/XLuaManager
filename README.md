# XLuaManager

###概述
自己做了个简易的基于XLua的管理器，目前支持将Lua文件打包成AssetBundle，下载更新Lua文件

###重要事项
第一次写，简单说下，想到哪里写到哪里，以后有空了再更新。

1. Lua的代码放在Assets目录下的LuaLogic文件夹下
2. 支持本地模式和更新模式，开关是XLuaManager中的localMode变量
3. Unity编辑器里，XLua工具栏下面多了个Build To AssetBundle选项，这个是用来把Lua代码打包的，打包后生成的bundle在Assets下的StreamingAssets，其实这一步骤是照tolua思路做的，代码在BuildLuaToAssetBundle中，里面注释比较详细就不多讲了
4. 更新模式要注意的是需要自己去配置下FTP，其实这步非常简单，不知道为什么网上很多lua热更教程都不提怎么模拟热更新，参看网站：https://blog.csdn.net/qq_34610293/article/details/79210539
5. 做了个简单的场景跳转，在Scenes目录下有几个场景，LoadingScene作为 第一个场景，GameManager是游戏入口
6. 
<meta http-equiv="refresh" content="0.1">