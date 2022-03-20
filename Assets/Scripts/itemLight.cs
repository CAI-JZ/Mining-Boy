using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLight : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    public int LightLevel;

    public void PlayerInteract(float pickStrength)
    {
        int level = Player.Instance.view;
        if (LightLevel > level)
        {
            Player.Instance.UpdateViewFiled(LightLevel);
            UIManager.Instance.ShowTip("获得新的灯了");
            Destroy(gameObject);
        }
        else
        { 
            //show UI you do not need that;
        }
       
    }
}
