using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Singleton
    public static Player Instance { get; private set; }
    private Player() { }

    // player basic data
    public float CurrentOxygen;
    [SerializeField] 
    public float MaxOxygen;
    private float OxygenCost;
         
    public float Money { get; private set; }

    [SerializeField] 
    private int ViewRange;
    private GameObject player;

    // Pick Data
    [SerializeField]
    private GameObject[] Picks;
    private float PickStength = 3;
    public int PickLevel { get; private set; }

    public float Strength => PickStength;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Money = 0;
        
    }

    // Add oxy cost when get deeper cave;
    public void UpdateOxyCost(float Layer)
    {
        OxygenCost = Mathf.Pow(Layer, 2);
    }


    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonDown(1))
        {
            PickUpdate();
        }
        
    }

    void PickUpdate()
    {
        if (PickLevel < Picks.Length-1)
        {
            Picks[PickLevel].SetActive(false);
            PickLevel = PickLevel + 1;
            Picks[PickLevel].SetActive(true);
            print(PickLevel);
            PickStength += 10;
        }
        else
        {
            print("Can not update anymore");
        }
    }

   

    public void MoneyUpdate(float Incoming)
    {
        print(Incoming);
        Money = Money + Incoming;
        //print(Money);
    }

    public void UpdateTools(int ToolType, float Cost)
    {
        if (Money >= Cost)
        {
            Money -= Cost;
            switch (ToolType)
            {
                case 1:
                    print("buy a New Tool");
                    PickUpdate();
                    break;
                case 2:
                    print("buy a new OxygenValue");
                    break;
                case 3:
                    print("Buy a new glass");
                    break;
                case 4:
                    print("Buy a new Luck");
                    break;
            }
        }
    }

    public void UseOxygen()
    {
        CurrentOxygen -= OxygenCost;  
    }

    public void AddOxygen(float Oxygen, float cost)
    {
        float Oxy = CurrentOxygen + Oxygen;
        if (Oxy < MaxOxygen)
        {
            CurrentOxygen = Oxy;
        }
        else 
        {
            CurrentOxygen = MaxOxygen;
        }
        Money -= cost;
    }

    void OxygenEmpty()
    {
        if (CurrentOxygen <= 0)
        { 
            //PlayerDie
        }
    }
}
