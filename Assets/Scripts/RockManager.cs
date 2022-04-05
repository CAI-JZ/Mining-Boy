using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockManager : MonoBehaviour, Inf_Interact
{
    //RockInfo
    private float Value;
    private float CurrentDurability;
    private float MaxDurability;
    private int RockLevel;

    //RockChoose

    [Header("Rock Data")]
    [SerializeField]
    private Rock[] Rocks;
    private Rock CurrentRock;
    [SerializeField]
    private GameObject CoinPrefab;
    [SerializeField]
    private Transform CoinPos;

    [Header("-> Rock Set")]
    [SerializeField]
    private bool UseRandom = true;
    [SerializeField]
    private Rock UseRock;
    
    [Header("-> UI")]
    [SerializeField]
    private Slider RockDur;
    [SerializeField]
    private CanvasGroup DurBar;

    private void Awake()
    {
        if (UseRandom)
        {
            int level = GameManager.instance.LevelIndex;

            int SpawnInt = Random.Range(0, 20);
            
            if (SpawnInt % (level + 2) > level)// Set if rock SPAWN;
            {
                gameObject.SetActive(false);
            }
            else
            {
                //Get random Index
                int rockCount = 0;
                foreach (Rock r in Rocks)
                {
                    if (r.AppearanceLevel <= level)
                    {
                        rockCount++;
                    }
                }
                int Index = Random.Range(0, rockCount);
                SetRockInfo(Rocks[Index]);
            }
        }
        else
        {
            SetRockInfo(UseRock);
        }

    }

    private void SetRockInfo(Rock rock)
    {
        CurrentRock = rock;
        GetComponent<SpriteRenderer>().sprite = CurrentRock.ArtWork;
        Value = CurrentRock.Value;
        MaxDurability = CurrentRock.MaxDurability;
        CurrentDurability = MaxDurability;
        RockLevel = CurrentRock.RockLevel;
    }


    private void FixedUpdate()
    {
        RockDur.value = CurrentDurability / MaxDurability;
    }
   
    public void PlayerInteract(float pickStrength)
    {
        if (CurrentDurability == MaxDurability)
        {
            DurBar.alpha = 1;
        }
        if (RockLevel <= Player.Instance.PickLevel)
        { 
            CurrentDurability -= pickStrength;
            if (CurrentDurability <= 0)
            {
                if (Value > 0)
                { 
                    FinishPick();
                    GameObject coin = GameObject.Instantiate(CoinPrefab, CoinPos.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }

    private void FinishPick()
    {
        if (Player.Instance != null)
        {
            Player.Instance.MoneyUpdate(Value);
        }
    }

}
