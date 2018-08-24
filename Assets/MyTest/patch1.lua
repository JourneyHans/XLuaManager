-- 补丁1
xlua.private_accessible(CS.TargetScript)

xlua.hotfix(CS.TargetScript, "Show", function(self)
	print("Lua ------------> Show")
end)
