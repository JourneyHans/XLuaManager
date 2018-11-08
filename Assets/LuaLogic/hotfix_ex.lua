local util = require ("framework/util")

util.hotfix_ex(CS.HotfixPanel, "HotfixExBtnCall", function(self)
	self:HotfixExBtnCall()		-- 调用一次原有方法

	-- lua增加的逻辑
	print("Second, lua call HotfixExBtnCall ")
end)
