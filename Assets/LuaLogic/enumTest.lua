local util = require ("framework/util")

util.hotfix_ex(CS.HotfixPanel, "EnumFixTestBtnCall", function(self)
    self:EnumFixTestBtnCall()		-- 调用一次原有方法

    -- lua端测试枚举
    print("Lua -- Enum Value: \t", CS.HotFixEnum.Test, "\tAnd type is ", type(CS.HotFixEnum.Test))
    print("Lua -- Enum Value To String: ", tostring(CS.HotFixEnum.Test), "\tAnd type is ", type(tostring(CS.HotFixEnum.Test)))
    print("Lua -- Enum CastFrom value: ", CS.HotFixEnum.__CastFrom(1), "\tAnd type is ", type(CS.HotFixEnum.__CastFrom(1)))
    print("Lua -- Enum CastFrom name: ", CS.HotFixEnum.__CastFrom("Test"), "\tAnd type is ", type(CS.HotFixEnum.__CastFrom("Test")))
end)
