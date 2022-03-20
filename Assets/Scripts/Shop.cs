using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour,Inf_Interact
{
    private float OxygenCost;
    private float PickCost;
    private float ViewCost;
    private float item01Cost;

    public void PlayerInteract(float pickStrength)
    {
        bool canUpgrade = Player.Instance.CanUpgradePick();
        print(canUpgrade);
        if (canUpgrade)
        {
            UIManager.Instance.ShowUpdatePick();
        }
        else
        {
            UIManager.Instance.ShowTip("The PICK is on Max Level");
        }
    }
}
