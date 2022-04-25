using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour,Inf_Interact
{
    public AudioSource win;

    public void PlayerInteract(float pickStrength)
    {
        //Play sound
        win.Play();
        // show UI
        Invoke("Destory", 1f);
        UIManager.Instance.Win();
    }

    void Destory()
    {
        Destroy(gameObject);
    }


}
