using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    bool DoorOpen;


    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        DoorOpen = false;
    }

    public void OpenDoor()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        DoorOpen = true;
        UIManager.Instance.ShowTip("Door is open");
    }


    public void PlayerInteract(float pickStrength)
    {
        if (DoorOpen)
        {
            //ShowUI to chose - tbc;
            GameManager.instance.NextLevel();
        }
        else
        { 
            //Show Is OpenUI
        }
    }
}
