using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    public bool DoorOpen;
    [SerializeField]
    public float Cost;
    [SerializeField]
    AudioSource openDoor;

    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        DoorOpen = false;
    }

    public void OpenDoor()
    {
        if (!DoorOpen)
        {
            float money = Player.Instance.Money - Cost;
            if (money >= 0)
            {
                DoorOpen = true;
                openDoor.Play();
                Player.Instance.MoneyUpdate(-Cost);
                //UIManager.Instance.ShowTip("Door is open");
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else { print("Door 的默认值是true"); }
    }


    public void PlayerInteract(float pickStrength)
    {
        if (DoorOpen)
        {
            //ShowUI to chose - tbc;
            GameManager.instance.NextLevel();
        }
    }
}
