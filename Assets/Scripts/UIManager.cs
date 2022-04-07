using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup Title;
    [SerializeField]
    private CanvasGroup CharacterUI;
    [SerializeField]
    private GameObject Tips;
    [SerializeField]
    GameObject UpgradePick;

    bool TipsIsVisiable;
    Canvas canvas;

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
    }

    public void GameStart()
    {
        Title.alpha = 0;
        Title.gameObject.SetActive(false);
        CharacterUI.gameObject.SetActive(true);
        CharacterUI.alpha = 1;
    }

    public void ShowTip(string tip)
    {
        Tips.gameObject.SetActive(true);
        Tips.transform.GetChild(0).gameObject.GetComponent<Text>().text = tip;
        StartCoroutine(TipsShowCold(0.2f));
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

    public void ShowUpdatePick()
    {
        UpgradePick.gameObject.SetActive(true);
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

 
}
