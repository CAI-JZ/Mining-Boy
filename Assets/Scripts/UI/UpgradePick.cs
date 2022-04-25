using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePick : MonoBehaviour
{
    float CurrentMoney;
    float CurrentCost;
    int ShowLevel;

    [SerializeField]
    GameObject[] Artwork;
    [SerializeField]
    Text Cost;

    
    private void OnEnable()
    {
        //����Ĭ����ʾ
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);

        //��ȡ��ҵ�����
        CurrentMoney = Player.Instance.Money;
        ShowLevel = Player.Instance.PickLevel;
        //չʾͼƬ�ͽ��
        Artwork[ShowLevel].SetActive(true);
        switch (ShowLevel)
        {
            case 0:
                CurrentCost = 100;
                Cost.text = CurrentCost.ToString();
                break;
            case 1:
                CurrentCost = 300;
                Cost.text = CurrentCost.ToString();
                break;
            case 2:
                CurrentCost = 500;
                Cost.text = CurrentCost.ToString();
                break;
            case 3:
                CurrentCost = 1000;
                Cost.text = CurrentCost.ToString();
                break;
        }
    }


    public void OnBtnYes()
    {
        if (CurrentMoney > CurrentCost)
        {
            //Player.Instance.UpdatePick(CurrentCost);
            CloseTips();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

    }

    public void OnBtnNotNow()
    {
        CloseTips();
    }

    public void OnBtnLeave()
    {
        CloseTips();
    }

    private void CloseTips()
    {
        Artwork[ShowLevel].SetActive(false);
        gameObject.SetActive(false);
    }
}
