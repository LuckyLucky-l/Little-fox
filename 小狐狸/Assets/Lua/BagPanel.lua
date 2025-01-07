BasePanel:subClass("BagPanel")
BagPanel.Content=nil
--初始化
function BagPanel:Init()
    self.base.Init(self,"BagPanel")
    if self.isFindControl==false then--只找一次控件
        local SvBag=self.objPanel.transform:Find("SvBag")
        local Viewport=SvBag.transform:Find("Viewport")
        self.Content=Viewport.transform:Find("Content").transform
        self.isFindControl=true;
    end
    for i = 1, 2 do
        self:GetControls("Button"..i,"Button").onClick:AddListener(function ()
        self:HideMe()
        end)
    end
    self:GridInit()
end

function BagPanel:GridInit()
    if self.objPanel~=nil then
        --获得玩家数据
        local nowItem=PlayData.Item
        local index=0;
        for k, v in pairs(nowItem) do
            index=index+1
        end
        --根据玩家的数据创建装备格子
        local G= MyGrid:new()
        for i = 1, index do
            self.Content.sizeDelta=Vector2((i-1)%5*167+135,math.floor((i-1)/5)*167+135)
            --实例化格子
             G:Init("ui","MyGrid",(i-1)%5*167,math.floor((i-1)/5)*-167)
             G:InitInfo(nowItem["cherry"].id,nowItem["cherry"].num)
         end
    end
end

--显示
function BagPanel:ShowMe()
    self.base.ShowMe(self,"BagPanel")
end
--隐藏
function BagPanel:HideMe()
    self.base.HideMe(self)
end