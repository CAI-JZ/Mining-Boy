using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameState
    public int LevelIndex;
    public float OxygenCost;
    [SerializeField]
    private GameObject UI;

    public event Action Retry;

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

    public void GameStart()
    {
        UIManager.Instance.GameStart();
        NextLevel();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(6);
        Retry?.Invoke();
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Player.Instance.PlayerReset();
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        LevelIndex = SceneManager.GetActiveScene().buildIndex+1;
        SceneManager.LoadScene(LevelIndex);
        Player.Instance.UpdateOxyCost(LevelIndex);
        print("level: " + LevelIndex);
        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
