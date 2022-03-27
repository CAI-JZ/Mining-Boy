using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    private AdvancedText text;
    [SerializeField]
    GameObject DialogUI;
    [SerializeField]
    private string content;
    [SerializeField]
    GameObject Buttons;
    [SerializeField]
    GameObject Door;

    [Header("DialogFile")]
    public TextAsset TextFile;
    [SerializeField]
    private int index;
    private bool IsTalk;
    private bool IsOpenDoor;
    [SerializeField]
    private string FirstTalk;
    bool stopTalking = false;

    List<string> DialogList = new List<string>();

    private void Awake()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        Buttons = UI.transform.GetChild(3).gameObject;
        DialogUI = UI.transform.GetChild(2).gameObject;
        Button btnTalk = Buttons.transform.GetChild(0).GetComponent<Button>();
        Button btnOpenDoor = Buttons.transform.GetChild(1).GetComponent<Button>();
        Button btnLeave = Buttons.transform.GetChild(2).GetComponent<Button>();
        btnTalk.onClick.AddListener(OnBtnTalk);
        btnOpenDoor.onClick.AddListener(OnBtnOpenDoor);
        btnLeave.onClick.AddListener(OnBtnLeave);
        Buttons.transform.GetChild(1).gameObject.SetActive(true);

        text = DialogUI.transform.GetChild(0).GetComponent<AdvancedText>();
        GetTextFromFile(TextFile);
        index = 0;
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

    // 1 点击对话后，展示第一句话和可选项
    // 2 开门btn-调用开门方法； 交谈btn调用对话方法，正常对话； 离开直接关掉所有的内容。
    // 门开后再次对话，去掉开门btn

    public void PlayerInteract(float pickStrength)
    {
        if (!DialogUI.activeSelf)
        {
            DialogUI.SetActive(true);
            Buttons.SetActive(true);
            text.ShowTextByTyping(FirstTalk);
        }
        if (IsTalk)
        {

            if (index == DialogList.Count)
            {
                DialogUI.SetActive(false);
                IsTalk = false;
                index = 0;
                return;
            }
            text.ShowTextByTyping(DialogList[index]);
            index++;
        }
        if (IsOpenDoor)
        {
            if (!stopTalking)
            {
                    if (Player.Instance.Money - Door.GetComponent<DoorNextLevel>().Cost >= 0)
                    {
                        text.ShowTextByTyping("...<0.7>! <0.2>Door is Open...");
                        //DialogUI.SetActive(false);
                        Door.GetComponent<DoorNextLevel>().OpenDoor();
                        Buttons.transform.GetChild(1).gameObject.SetActive(false);
                        stopTalking = true;
                    }
                    else
                    {
                        text.ShowTextByTyping("You need more money...");
                        stopTalking = true;
                    }              
            }
            else
            {
                DialogUI.SetActive(false);
                IsOpenDoor = false;
                stopTalking = false;
                return;
            }
        }

    }


    private void OnBtnTalk()
    {
        text.ShowTextByTyping(DialogList[index]);
        index = 1;
        Buttons.SetActive(false);
        IsTalk = true;
    }


    private void OnBtnLeave()
    {
        DialogUI.SetActive(false);
        Buttons.SetActive(false);
    }

    private void OnBtnOpenDoor()
    {
        Buttons.SetActive(false);
        float cost = Door.GetComponent<DoorNextLevel>().Cost;
        string content = $"Do you want to open the door? You need to pay <color=#FBC531>{cost}</color>.<0.2>.<0.2>.<0.2> !";
        text.ShowTextByTyping(content);
        IsOpenDoor = true;
    }
}
