using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    bool DoorOpen;
    bool IsTouch;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" )
        {
            IsTouch = true;
            print("touch");
        }
    }

    public void PlayerInteract(float pickStrength)
    {
        print("click");
        if (IsTouch && DoorOpen)
        {
            //ShowUI to chose - tbc;
            GameManager.instance.NextLevel();
        }  
    }
}
