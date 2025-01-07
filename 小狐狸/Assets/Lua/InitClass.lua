--Unity自带
GameObject=CS.UnityEngine.GameObject
Transform=CS.UnityEngine.Transform
Canvas=GameObject.Find("Canvas").transform
Vector3=CS.UnityEngine.Vector3
Vector2=CS.UnityEngine.Vector2

--Unity UI
RectTransform=CS.UnityEngine.RectTransform
UIBehaviour=CS.UnityEngine.EventSystems.UIBehaviour
SpriteAtlas=CS.UnityEngine.U2D.SpriteAtlas
Image=CS.UnityEngine.U2D.Image
Button=CS.UnityEngine.U2D.Button
Text=CS.UnityEngine.UI.Text
ScrollRect=CS.UnityEngine.UI.ScrollRect

--自己写的AB包管理类
ABMgr=CS.ABMgr.GetInstance()
--Lua工具包
require("Object")--基类
json=require("JsonUtility")--Lua解析Json工具
require("SplitTools")--字符拆分