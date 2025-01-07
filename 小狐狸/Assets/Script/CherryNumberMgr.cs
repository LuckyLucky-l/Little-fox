using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// json存储奖励数据
/// </summary>
public class CherryInfo{
    public int cherry;
}
public class CherryNumberMgr
{
    private static CherryNumberMgr instance = new CherryNumberMgr();
    public static CherryNumberMgr Instance => instance;

    public CherryInfo cf = new CherryInfo();

    public void ChangeCherry()
    {
        cf.cherry++;
        SaveCherry();
    }

    // 读取数据
    public CherryInfo LoadCherry()
    {
        return JsonMgr.Instance.LoadJson<CherryInfo>("Cherry",JsonType.LitJson);
    }

    // 存储数据
    public void SaveCherry()
    {
        JsonMgr.Instance.SaveJson(cf, "Cherry",JsonType.LitJson);
    }
    //重置数据
    public void DestroyCherry(){
        cf.cherry=0;
        SaveCherry();
    }
}
