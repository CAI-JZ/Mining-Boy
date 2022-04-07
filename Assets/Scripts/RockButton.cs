using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour, Inf_Interact
{
    [SerializeField]
    private int Function;
    // 1 = START GAME;
    // 2 = END GAME;
    private bool isTouch;

    [SerializeField]
    private CanvasGroup ButtonName;

    public void PlayerInteract(float pickStrength)
    {
        if (isTouch)
        {
            switch (Function)
            {
                case 0: //start game;
                    print("ToL1");
                    GameManager.instance.GameStart();
                    break;
                case 1:
                    print("EndGme");
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ButtonName.alpha = 1;
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ButtonName.alpha = 0;
            isTouch = false;
        }
    }
}
