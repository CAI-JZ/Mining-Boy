using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameState
    private int LevelIndex;
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
        LevelIndex = 0;
        OxygenCost = 0;
    }

    public void NextLevel()
    {
        LevelIndex = SceneManager.GetActiveScene().buildIndex+1;
        SceneManager.LoadScene(LevelIndex);
        OxygenCost = LevelIndex * 5;
        print("OxygenCost: " + OxygenCost);
    }

}
