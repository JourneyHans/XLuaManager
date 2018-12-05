local util = require("framework/util")

util.hotfix_ex(CS.HotfixPanel, "ExtendFixBtnCall", function(self)
	print("先调用一次C#的方法")
	self:ExtendFixBtnCall()
	print("接下来是Lua侧调用扩展方法的方式：")
	local test_s = "x"
	--test_s:StringExtendMethodTest()	-- 不行
	--test_s.StringExtendMethodTest()	-- 不行
	-- 注意看下面这种调用方式，想一想静态类调用静态方法是不是也是类似的样子？
	CS.TestMethodExtendClass.StringExtendMethodTest(test_s)
end)
