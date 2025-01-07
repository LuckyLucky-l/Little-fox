Object:subClass("MyGrid")
MyGrid.objPanel=nil
MyGrid.Icon=nil
MyGrid.TexT=nil

--实例化格子面板
function MyGrid:Init(ABname,ResName,Xpos,Ypos)
    self.objPanel= ABMgr:LoadRes(ABname,ResName,typeof(GameObject))
    if self.objPanel~=nil then
        --设置父对象和位置
        self.objPanel.transform:SetParent(BagPanel.Content.transform,false)
        self.objPanel.transform.localPosition=Vector3(Xpos,Ypos,0)
        --寻找组件
            self.Icon=self.objPanel.transform:Find("Icon"):GetComponent("Image")
            self.TexT=self.objPanel.transform:Find("TexT"):GetComponent("Text")
    end
end

--初始化格子信息
function MyGrid:InitInfo(itemId,itemNum)
   --根据玩家的装备id加载具体的装备信息
    local dataInfo=ItemData[itemId]
    local dataStrs=string.split(dataInfo.icon,"_")
    --加载图集
    local SpriteAtlas=ABMgr:LoadRes("sprite",dataStrs[1])
    --替换图片,数量
    self.Icon.sprite=SpriteAtlas:GetSprite(dataStrs[2])
    self.TexT.text=itemNum
end
--删除格子
function MyGrid:DestoryG()
    
end
--[[
如何更新格子数量？
更新完数据后再调用一遍初始化格子信息？
]]