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

    [Header("¡¾Object¡¿")]
    // Pick Data
    [SerializeField]
    private GameObject[] Picks;
    [SerializeField]
    private Light View;


    private float PickStength = 3;
    public int PickLevel { get; private set; }
    public float Strength => PickStength;



    [Header("¡¾Test¡¿")]
    [SerializeField]
    private int Viel;


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

    private void Update()
    {
        UpdateViewFiled(Viel);
       
    }






    // Add oxy cost when get deeper cave;
    public void UpdateOxyCost(float Layer)
    {
        OxygenCost = Mathf.Pow(Layer, 2);
    }
    public void MoneyUpdate(float Incoming)
    {
        print(Incoming);
        Money = Money + Incoming;
        //print(Money);
    }

    void PickUpdate()
    {
        if (PickLevel < Picks.Length-1)
        {
            Picks[PickLevel].SetActive(false);
            PickLevel = PickLevel + 1;
            Picks[PickLevel].SetActive(true);
            print(PickLevel);
            PickStength += 5;
        }
        else
        {
            print("Can not update anymore");
        }
    } 


    public void updateOxygen(float newOxy)
    {
        MaxOxygen = newOxy;
        CurrentOxygen = MaxOxygen;
    }

    public void UpdateViewFiled(int lightLevel)
    {
        switch (lightLevel)
        {
            case 1:
                View.range = 5;
                View.intensity = 5;
                break;
            case 2:
                View.range = 10;
                View.intensity = 2.5f;
                break;
            case 3:
                View.range = 20;
                View.intensity = 1.7f;
                break;
        }
    }


    public void UseOxygen()
    {
        float oxy = CurrentOxygen - OxygenCost;
        if (oxy > 0)
        {
            CurrentOxygen = oxy;
        }
        else
        { 
            //game over;
        }
         
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
}
