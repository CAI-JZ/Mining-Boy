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
    private int RockCount = 0;
    
    [SerializeField]
    private Slider RockDur;
    [SerializeField]
    private CanvasGroup DurBar;

    private void Awake()
    {
       
        int level = GameManager.instance.LevelIndex;
       

        int SpawnInt = Random.Range(0, 20);
        if (SpawnInt % (level+2) > level)
        {
            gameObject.SetActive(false);
        }
        else
        {
            //Get random Index
            foreach (Rock r in Rocks)
            {
                if (r.AppearanceLevel <= level)
                {
                    RockCount++;
                }
            }
            int Index = Random.Range(0, RockCount);
            SetRockInfo(Index);
        }
       
    }

    private void SetRockInfo(int index)
    {
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
        if (CurrentDurability <= 0)
        {
            FinishPick();
            gameObject.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        RockDur.value = CurrentDurability / MaxDurability;
    }

    private void FinishPick()
    {
        string sf = CurrentRock.SpiecalFeature;
        if (Player.Instance != null)
        {
            Player.Instance.MoneyUpdate(Value);

            switch (sf)
            {
                case "ADD_OXYGEN":
                    print("Player current max oxygen " + Player.Instance.MaxOxygen +" []" + "Player current oxygen" + Player.Instance.CurrentOxygen);
                    Player.Instance.AddOxygen(CurrentRock.FeatureValue);
                    print("Player max oxygen" + Player.Instance.MaxOxygen);
                    break;
                default:
                    print("No Skill");
                    break;

            }
        }
    }
    
    

}
