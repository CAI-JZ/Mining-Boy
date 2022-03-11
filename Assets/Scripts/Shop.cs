using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private float OxygenCost;
    private float PickCost;
    private float ViewCost;
    private float item01Cost;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        { 
            //show UI
        }
    }

    void UpadatePick()
    { 
    
    }

    void UpdateViewFiled()
    { 
    
    }

    void UpdateOxygenValue()
    { 
    
    }

   

    void BuyOxygen()
    {
        if (OxygenCost <= Player.Instance.Money)
        {
            Player.Instance.AddOxygen(40, OxygenCost);
        }
        CostUpdate(OxygenCost,1.5f);
    }

    void CostUpdate(float cost, float parameter)
    {
        cost *= parameter;
    }
}
