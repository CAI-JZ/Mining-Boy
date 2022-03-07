using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{
    bool GetIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GetIn)
        {
            GameManager.instance.NextLevel();
        } 
    }

    private void Update()
    {
        GetIn = Input.GetKeyDown(KeyCode.F);
    }
}
