--[[
        id=1 樱桃
]]
PlayData={}
PlayData.Item={}--{cherry={id=1,ChangeNumber=1}}
PlayData.CherryNum=1;

--初始化玩家数据
function PlayData:Init(TypeName)
    if not self.Item[TypeName] then
        self.Item[TypeName]={}
        if TypeName=="cherry" then
            self.Item[TypeName].id="1"
            self.Item[TypeName].num=self.CherryNum   
        end
    end

end

--更新数量
function PlayData:updateNumber(TypeName)
    if TypeName=="cherry" then
        self.CherryNum=self.CherryNum+1
        PlayData.Item["cherry"].num=self.CherryNum
        for k, v in pairs(self.Item) do
            print(k,v.id,v.num)
        end 
    end
end