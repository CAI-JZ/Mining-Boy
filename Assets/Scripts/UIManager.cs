using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup Title;
    [SerializeField]
    private CanvasGroup CharacterUI;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GameStart()
    {
        Title.alpha = 0;
        Title.gameObject.SetActive(false);
        CharacterUI.gameObject.SetActive(true);
        CharacterUI.alpha = 1;
    }
}
