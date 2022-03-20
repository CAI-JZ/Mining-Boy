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
    private float MaxOxygen;
    private float OxygenCost;

    public float _MayOxygen => MaxOxygen;
         
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
    [SerializeField]
    private int ViewLevel;
    public int view => ViewLevel;


    [Header("¡¾Test¡¿")]
    [SerializeField]
    private int viewLevel;

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
        #if UNITY_EDITOR
        UpdateViewFiled(viewLevel);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UpdatePick(0);
        }
        #endif
    }


    // Add oxy cost when get deeper cave;
    public void UpdateOxyCost(float Layer)
    {
        OxygenCost = Mathf.Pow(Layer, 2);
    }

    public void MoneyUpdate(float Incoming)
    {
        Money = Money + Incoming; 
    }

    public void UpdatePick(float cost)
    {
            Picks[PickLevel].SetActive(false);
            PickLevel = PickLevel + 1;
            Picks[PickLevel].SetActive(true);
            PickStength = 3 + 3 * PickLevel;
            Money -= cost;
    }

    public bool CanUpgradePick()
    {
        bool can = PickLevel < Picks.Length - 1;
        return can;
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
                ViewLevel = lightLevel;
                break;
            case 2:
                View.range = 10;
                View.intensity = 2.5f;
                ViewLevel = lightLevel;
                break;
            case 3:
                View.range = 20;
                View.intensity = 1.7f;
                ViewLevel = lightLevel;
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
