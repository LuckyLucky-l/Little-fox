Object:subClass("BasePanel")
BasePanel.objPanel=nil
--一个表装控件
BasePanel.Controls={}--{SvBag={ScrollRect=组件}}
BasePanel.isFindControl=false
--创建面板
function BasePanel:Init(resName)
    if self.objPanel==nil then
    --从AB包加载面板
    self.objPanel=ABMgr:LoadRes("ui",resName,typeof(GameObject));
        self.objPanel.transform:SetParent(Canvas,false)
    --寻找组件
        local AllControls=self.objPanel:GetComponentsInChildren(typeof(UIBehaviour));--获得所有的组件
        for i = 1, AllControls.Length-1 do
            local ControlsName=AllControls[i].name--获得单独的组件名
            --筛选组件
            if string.find(ControlsName,"btn")~=nil or
                string.find(ControlsName,"But")
                then
                --{SvBag={ScrollRect=组件}}
                local ControlsType=AllControls[i]:GetType().Name;

                if self.Controls[ControlsName]~=nil then
                    self.Controls[ControlsName][ControlsType]=AllControls[i]
                else
                    self.Controls[ControlsName]={[ControlsType]=AllControls[i]}
                end
            end
        end
    end
end
function BasePanel:GetControls(C_name,T_name)--控件名，控件类型
    if self.Controls[C_name]~=nil then
        local name=self.Controls[C_name]
        if name[T_name]~=nil then
            return name[T_name]
        end
    end
end
function BasePanel:ShowMe(Gname)
    self:Init(Gname)
    self.objPanel.gameObject:SetActive(true)
end
function BasePanel:HideMe()
        self.objPanel.gameObject:SetActive(false)
end