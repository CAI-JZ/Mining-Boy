using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameState
    private float LevelIndex;
    public float OxygenCost;

    public static GameManager instance { get; private set; }
    private GameManager() { }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        //Set Initial Data;
        LevelIndex = 1;
        OxygenCost = 5;
    }

    public void LevelUpdate()
    {
        LevelIndex += 1;
        OxygenCost = LevelIndex * 5;
    }


}
