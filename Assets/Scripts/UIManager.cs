using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup CharacterUI;
    [SerializeField]
    private GameObject Tips;
    [SerializeField]
    GameObject UIWin;
    [SerializeField]
    GameObject UIPause;

    bool TipsIsVisiable;
    Canvas canvas;
    AudioSource audio;

    public static UIManager Instance { get; private set; }
    private UIManager() { }
    
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

        canvas = GetComponent<Canvas>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Level_0")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        
    }

    public void GameOver()
    {
        CharacterUI.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        if (!CharacterUI.gameObject.activeSelf)
        {
            CharacterUI.gameObject.SetActive(true);
            CharacterUI.alpha = 1;
        }
    }

    public void Win()
    {
        UIWin.SetActive(true);
        //Time.timeScale = 0;
    }

    public void Pause()
    {
        UIPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        UIPause.SetActive(false);
        Time.timeScale = 1;
        audio.Play();
    }
    
    public void ShowTip(string tip, float time)
    {
        Tips.gameObject.SetActive(true);
        Tips.transform.GetChild(0).gameObject.GetComponent<Text>().text = tip;
        StartCoroutine(TipsShowCold(time));
    }

    IEnumerator TipsShowCold( float coldTime)
    {
        yield return new WaitForSecondsRealtime(coldTime);
        TipsIsVisiable = true;
    }

    private void TipHid()
    {
        Tips.gameObject.SetActive(false);
        TipsIsVisiable = false;
    }


    private void FixedUpdate()
    {
        if (TipsIsVisiable && Input.anyKeyDown)
        {
            TipHid();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = UnityEngine.Camera.main;
    }


    public void onbtnExit()
    {
        audio.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void onbtnMenu()
    {
        if (UIWin.activeSelf)
        { 
            UIWin.SetActive(false);
        }
        if (UIPause.activeSelf)
        {
            UIPause.SetActive(false);
        }
        CharacterUI.gameObject.SetActive(false);
        GameManager.instance.RetryGame();
        audio.Play();
        Time.timeScale = 1;
    }

}
