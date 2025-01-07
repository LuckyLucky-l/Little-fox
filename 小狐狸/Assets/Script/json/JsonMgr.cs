using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
/// <summary>
/// json管理类
/// </summary>
public enum JsonType
{
    JsonUtility,
    LitJson
}
public  class JsonMgr 
{
    private static JsonMgr instance=new JsonMgr();
    public static JsonMgr Instance=>instance;
    //存储数据
    public void SaveJson<T>(T obj,string fileName,JsonType jsonType=JsonType.JsonUtility)
    {
        string path=Application.persistentDataPath+"/"+fileName+".json";
        string jsons="";
        switch (jsonType)
        {
            case JsonType.JsonUtility:
             jsons=JsonUtility.ToJson(obj);
            break;
            case JsonType.LitJson:
            jsons=JsonMapper.ToJson(obj);
            break;
        }
        File.WriteAllText(path,jsons);
    }
    //读取数据
    public T LoadJson<T>(string fileName,JsonType jsonType=JsonType.JsonUtility){
        string path=Application.persistentDataPath+"/"+fileName+".json";
        T obj=default(T);
        if (File.Exists(path))
        {
          string jsons=File.ReadAllText(path);
          switch (jsonType)//根据不同取数据
            {
            case JsonType.JsonUtility:
                   obj=JsonUtility.FromJson<T>(jsons);
            break;
            case JsonType.LitJson:
                   obj=JsonMapper.ToObject<T>(jsons);
            break; 
             }
        return obj;
        }else{
            Debug.Log("文件不存在");
        }
        return default;
    }
    //删除Json文件
    public void DeletJson(string fileName){
        string path=Application.persistentDataPath+"/"+fileName+".json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
