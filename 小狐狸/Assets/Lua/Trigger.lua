print("Trigger正确加载")

Object:subClass("TriggerDetector")
TriggerDetector.ItemName={}
-- 触发器进入事件
function TriggerDetector.OnTriggerEnter(other)
    -- 输出触发器被哪个对象进入
    -- 进入触发器处理物品拾取
    local strs= string.split(other," ")
    TriggerDetector.PickupItem(strs[1])
end
-- 处理物品拾取
function TriggerDetector.PickupItem(other)
    --记录物品的名字,第一次初始化，只有表中已经拥有才能改变去改变数量
    PlayData:Init(other)
    --记录物品的名字
    --如果物品存在则改变数量 
        -- 如果物品存在则改变数量
        if TriggerDetector.ItemName[other] == nil then
            TriggerDetector.ItemName[other]={id=other}  -- 初始化物品--{cherry{id=cherry}}
        else
            PlayData:updateNumber(other)  -- 更新物品数量
            print("Item exists, updated number: " .. other)
        end
end
