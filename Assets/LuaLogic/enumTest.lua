local util = require ("framework/util")

util.hotfix_ex(CS.HotfixPanel, "EnumFixTestBtnCall", function(self)
    self:EnumFixTestBtnCall()		-- 调用一次原有方法

    -- lua端测试枚举
    print("Lua -- Enum Value: \t", CS.HotfixPanel.HotFixEnum.Test)
    print("Lua -- Enum Value To String: ", tostring(CS.HotfixPanel.HotFixEnum.Test))
    print("Lua -- Enum CastFrom value: ", CS.HotfixPanel.HotFixEnum.__CastFrom(1))
    print("Lua -- Enum CastFrom name: ", CS.HotfixPanel.HotFixEnum.__CastFrom("Test"))
end)
