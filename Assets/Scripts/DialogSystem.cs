using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    private AdvancedText text;
    [SerializeField]
    private string content;

    [Header("DialogFile")]
    public TextAsset TextFile;
    [SerializeField]
    private int index;

    List<string> DialogList = new List<string>();

    private void Awake()
    {
        GetTextFromFile(TextFile);
        index = 0;
    }

    private void OnEnable()
    {
        //text.ShowTextByTyping(DialogList[index]);
        //index++;
    }

    void GetTextFromFile(TextAsset file)
    {
        DialogList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            DialogList.Add(line);
        }
    }

    public void PlayerInteract(float pickStrength)
    {
        if (!text.transform.parent.gameObject.activeSelf)
        {
            text.transform.parent.gameObject.SetActive(true);
        }
        if (index == DialogList.Count)
        {
            text.transform.parent.gameObject.SetActive(false);
            index = 0;
            return;
        }
        text.ShowTextByTyping(DialogList[index]);
        index++;
    }
}
