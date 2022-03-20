using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOxygen : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    private int OxygenLevel;

    public void PlayerInteract(float pickStrength)
    {
        float newOxy = OxygenLevel * 50 + 100;
        if (newOxy > Player.Instance._MayOxygen)
        {
            Player.Instance.updateOxygen(newOxy);
            Destroy(gameObject);
        }
        
    }

  
}
