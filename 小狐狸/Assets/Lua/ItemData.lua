local txt=ABMgr:LoadRes("json","ItemData")
--[{"id":"1","name":"樱桃","icon":"icon_cherry-4","type ":"1","tips":"不可思议的樱桃，吃了会有神奇的事情发生"}]
local ItemList=json.decode(txt.text)

--["1"={"id":"1","name":"樱桃","icon":"icon_cherry-4","type ":"1","tips":"不可思议的樱桃，吃了会有神奇的事情发生"}]
ItemData={}
for _, value in pairs(ItemList) do
    ItemData[value.id]=value
end
