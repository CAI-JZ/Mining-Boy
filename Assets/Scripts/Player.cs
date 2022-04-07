using System;
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

    [Header("【Object】")]
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

    [Header("【Test】")]
    [SerializeField]
    private int viewLevel;

    public event Action<GameObject> PickUpdate;


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
        OxygenCost = Mathf.Pow(Layer, 2)/2;
    }

    public void MoneyUpdate(float Incoming)
    {
        Money = Money + Incoming;
    }

    public void GetNewPick(int pickLevel)
    {
        Picks[PickLevel].SetActive(false);
        Picks[pickLevel].SetActive(true);
        PickLevel = pickLevel;
        PickStength = 3 + 3 * PickLevel;
        PickUpdate?.Invoke(Picks[PickLevel]);
    }

    public void UpdatePick(float cost)
    {
        Picks[PickLevel].SetActive(false);
        PickLevel = PickLevel + 1;
        Picks[PickLevel].SetActive(true);
        PickStength = 3 + 3 * PickLevel;
        Money -= cost;
        PickUpdate?.Invoke(Picks[PickLevel]);
    }

    public bool CanUpgradePick()
    {
        bool can = PickLevel < Picks.Length - 1;
        return can;
    }

    public void updateOxygen(float newOxy)
    {
        MaxOxygen = newOxy;
        StartCoroutine(CurOxyNumberUpdate(MaxOxygen));
    }

    IEnumerator CurOxyNumberUpdate(float targetOxy)
    {
        while (CurrentOxygen < targetOxy)
        {
            CurrentOxygen += Time.deltaTime * 500;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    public void UpdateViewFiled(int lightLevel)
    {
        switch (lightLevel)
        {
            case 1:
                View.range = 4;
                View.intensity = 5;
                ViewLevel = lightLevel;
                break;
            case 2:
                View.range = 5.5f;
                View.intensity = 5f;
                ViewLevel = lightLevel;
                break;
            case 3:
                View.range = 12;
                View.intensity = 3f;
                ViewLevel = lightLevel;
                break;
        }
    }

    void ChangeViewLightColor(int Level)
    {
        switch (Level)
        {
            case 1:
                //暖色黄光
                return;
            case 2:
                //暖色黄光
                return;
            case 3:
                //暖色黄光
                return;
            case 4:
                //暖色黄光
                return;
            case 5:
                //暖色黄光
                return;
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
            GameManager.instance.GameOver();
        }
         
    }

    public void AddOxygen(float Oxygen, float cost)
    {
        float Oxy = CurrentOxygen + Oxygen;
        if (Oxy < MaxOxygen)
        {
            StartCoroutine(CurOxyNumberUpdate(Oxy));
        }
        else 
        {
            StartCoroutine(CurOxyNumberUpdate(MaxOxygen));
        }
        Money -= cost;
    }

    public void PlayerReset()
    {
        MaxOxygen = 100;
        CurrentOxygen = MaxOxygen;
        UpdateViewFiled(0);
        GetNewPick(0);
        Money = 0;
    }
}
