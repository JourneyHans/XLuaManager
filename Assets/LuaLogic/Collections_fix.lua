local util = require("framework/util")

xlua.hotfix(CS.HotfixPanel, "CollectionFixBtnCall", function(self)
    print("------------ List测试 ------------")
    for i, v in pairs(self.intList) do
        print("Lua -- element: " .. v)
    end
    local newElement = 5
    print("增加一个元素：" .. newElement)
    self.intList:Add(newElement)
    for i, v in pairs(self.intList) do
        print("Lua -- element: " .. v)
    end

    print("------------ Dictionary测试 ------------")
    for k, v in pairs(self.dic_1) do
        print("Lua -- element: " .. k .. " - " .. v)
    end
    print("增加一组元素：e - 5")
    self.dic_1:set_Item("e", 5)
    for k, v in pairs(self.dic_1) do
        print("Lua -- element: " .. k .. " - " .. v)
    end
end)