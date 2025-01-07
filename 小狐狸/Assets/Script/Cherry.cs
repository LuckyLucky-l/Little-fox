using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 挂载到樱桃上面，死亡时更新分数和销毁樱桃
/// </summary>
public class Cherry : MonoBehaviour
{
    public void Death()
    {
            CherryNumberMgr.Instance.ChangeCherry();
            CherryNumberMgr.Instance.SaveCherry();
            Destroy(gameObject);
    }
}
