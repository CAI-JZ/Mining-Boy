using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour, Inf_PickRock
{
    [SerializeField]
    private int Function;
    // 1 = START GAME;
    // 2 = END GAME;

    [SerializeField]
    private CanvasGroup ButtonName;

    
    public void UsePick(float pickStrength)
    {
        switch (Function)
        {
            case 0: //start game;
                print("ToL1");
                GameManager.instance.NextLevel();
                break;
            case 1:
                print("EndGme");
                Application.Quit();
                break;
        }
    }

    private void OnDestroy()
    {
        //.instance.NextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ButtonName.alpha = 1; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ButtonName.alpha = 0;
        }
    }
}
