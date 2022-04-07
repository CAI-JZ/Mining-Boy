using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    public void OnbtnRetry()
    {
        GameManager.instance.RetryGame();
        Time.timeScale = 1;
    }

    public void OnbtnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
