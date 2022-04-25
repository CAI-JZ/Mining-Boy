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
    [SerializeField]
    private float OxygenCost;

    public float _MayOxygen => MaxOxygen;

    public float Money { get; private set; }

    [Header("¡¾Object¡¿")]
    // Pick Data
    //[SerializeField]
    //private GameObject[] Picks;
    [SerializeField]
    private float PickStength = 3;
    [SerializeField]
    public int PickLevel; //{ get; private set; }
    public float Strength => PickStength;
    [SerializeField]
    private int ViewLevel;
    public int view => ViewLevel;

    [Header("¡¾Test¡¿")]
    [SerializeField]
    private int viewLevel;

    public event Action<int> PickUpdate;
    public event Action<int> ViewUpdate;

    AudioSource gameover;

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
        gameover = GetComponent<AudioSource>();
        Money = 0;
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
        PickStength = 3 + 3 * pickLevel;
        PickUpdate?.Invoke(pickLevel);
        print("ÒÑÉý¼¶¸ä×Ó" + pickLevel);
        PickLevel = pickLevel;
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
        ViewUpdate?.Invoke(lightLevel);
        ViewLevel = lightLevel;
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
            gameover.Play();
            StartCoroutine(GameOver());
        }        
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        UIManager.Instance.GameOver();
        GameManager.instance.GameOver();
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
        ViewLevel = 0;
        PickLevel = 0;
        Money = 0;
    }
}
