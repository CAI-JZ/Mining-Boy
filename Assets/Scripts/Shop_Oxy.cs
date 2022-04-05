using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Oxy : MonoBehaviour, Inf_Interact
{
    [SerializeField]
    private float Cost;
    [SerializeField]
    CanvasGroup Tips;
    [SerializeField]
    Text ShowCost;

    private void Awake()
    {
        ShowCost.text = Cost.ToString();
    }

    public void PlayerInteract(float pickStrength)
    {
        float money = Player.Instance.Money;
        float oxy = Player.Instance._MayOxygen * 0.5f;
        float oxyValue = Player.Instance.CurrentOxygen / Player.Instance._MayOxygen;
        if (money >= Cost)
        {
            if (oxyValue < 1)
            {
                Player.Instance.AddOxygen(oxy, Cost);
            }
            else
            {
                UIManager.Instance.ShowTip("You don't need to do that");
            }
        }
        else
        {
            UIManager.Instance.ShowTip("You need more money...");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tips.alpha = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Tips.alpha = 0;
    }
}
