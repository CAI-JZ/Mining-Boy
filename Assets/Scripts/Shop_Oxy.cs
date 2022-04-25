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

    AudioSource useCoin;

    private void Awake()
    {
        ShowCost.text = Cost.ToString();
        useCoin = GetComponent<AudioSource>();
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
                useCoin.Play();
                Player.Instance.AddOxygen(oxy, Cost);
                UIManager.Instance.ShowTip("Oxygen has been added! ", 0.1f);
            }
            else
            {
                UIManager.Instance.ShowTip("You don't need to do that",0.2f);
            }
        }
        else
        {
            UIManager.Instance.ShowTip("You need more money...",0.2f);
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
