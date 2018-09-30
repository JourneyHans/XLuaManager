local util = require ("util")

util.hotfix_ex(CS.Hotfix_ex, "TargetMethod", function(self)
	self:TargetMethod()		-- 调用一次原有方法

	-- lua增加的逻辑
	print("lua 第一条打印")
end)
