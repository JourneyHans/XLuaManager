xlua.hotfix(CS.TargetScript, "Update", function(self)
	self.tick = self.tick + 1
	if self.tick % 50 == 0 then
		self.title.text = "Update in Lua, tick = " .. self.tick
	end
end)

print("Hello TargetHotFix")
