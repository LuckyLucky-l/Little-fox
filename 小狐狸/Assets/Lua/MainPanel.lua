BasePanel:subClass("MainPanel")

--初始化
function MainPanel:Init()
    self.base.Init(self,"btnBagPanel")
    self:GetControls("Button","Button").onClick:AddListener(function ()
        self:ShowMe()
    end)
end
--显示
function MainPanel:ShowMe()
    BagPanel:ShowMe()
end