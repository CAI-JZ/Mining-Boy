using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockManager : MonoBehaviour, Inf_PickRock
{

    private float Value;
    private float CurrentDurability;
    private float MaxDurability;
    [SerializeField]
    private Rock[] Rocks;
    private Rock CurrentRock;

    [SerializeField]
    private Slider RockDur;
    [SerializeField]
    private CanvasGroup DurBar;

    private void Awake()
    {  
        int index = Random.Range(0, Rocks.Length);
        CurrentRock = Rocks[index];

        GetComponent<SpriteRenderer>().sprite = CurrentRock.ArtWork;
        Value = CurrentRock.Value;
        MaxDurability = CurrentRock.MaxDurability;
        CurrentDurability = MaxDurability;
    }



    //lose durability
    public void UsePick(float pickStrength)
    {
        if (CurrentDurability == MaxDurability)
        {
            DurBar.alpha = 1;
        }
        CurrentDurability -= pickStrength;
        if (CurrentDurability < 0)
        {
            Destroy(gameObject);
        }
    }


    //Durability to 0
    protected void DurabilityZero()
    {
        
        
    }

    private void FixedUpdate()
    {
        RockDur.value = CurrentDurability / MaxDurability;
    }

    private void OnDestroy()
    {
        string sf = CurrentRock.SpiecalFeature;
        Player.Instance.MoneyUpdate(Value);

        switch (sf)
        {
            case "ADD_OXYGEN":
                Player.Instance.CurrentOxygen += 30;
                break;
            default:
                print("No Skill");
                break;

        }
    }

}
