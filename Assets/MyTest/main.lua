-- lua主文件，在这里加载其他的lua脚本
print("main.lua--------------> xLua Start!")

-- 打补丁
local list = require("PatchList")
for _, fileName in ipairs(list) do
	print(fileName)
	require(fileName)
end
