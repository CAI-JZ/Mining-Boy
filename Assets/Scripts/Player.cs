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
    public float Money { get; private set; }

    [SerializeField]
    private int ViewRange;

    // Pick Data
    private int PickNum;
    private float PickStength = 3;
    public GameObject[] Picks;
    public GameObject CheckPoint;
    private bool IsHit;

  
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
        
        Picks[0].SetActive(true);

        Money = 0;
    }

    float UseOxygen(float Layer)
    {
        CurrentOxygen -= GameManager.instance.OxygenCost;
        return CurrentOxygen;
    }


    // Update is called once per frame
    void Update()
    {
        IsHit = Input.GetKeyDown(KeyCode.Mouse0);
    
        if (Input.GetMouseButtonDown(1))
        {
            PickUpdate();
        }

        Pick(IsHit);
    }

    void PickUpdate()
    {
        if (PickNum < Picks.Length - 1)
        {
            Picks[PickNum].SetActive(false);
            PickNum = PickNum + 1;
            Picks[PickNum].SetActive(true);
            PickStength += 10;
        }
    }

    void Pick(bool Click)
    {
        if (CurrentOxygen > 0)
        {
            if (Click)
            {
                RaycastHit2D Hitinfo = Physics2D.Raycast(CheckPoint.transform.position, CheckPoint.transform.right, 1f);
                Debug.DrawRay(CheckPoint.transform.position, CheckPoint.transform.right * 2, Color.red, 10f);

                if (Hitinfo.collider != null)
                {
                    print(Hitinfo.collider.tag);
                    Inf_PickRock Rock = Hitinfo.collider.GetComponent<Inf_PickRock>();
                    if (Rock != null)
                    {
                        //print(Hitinfo.collider.tag);
                        Rock.UsePick(PickStength);
                        CurrentOxygen = CurrentOxygen - GameManager.instance.OxygenCost;
                        print(CurrentOxygen);
                    }
                }
            }
        }

    }

    public void MoneyUpdate(float Incoming)
    {
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
}
