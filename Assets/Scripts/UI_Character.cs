using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character : MonoBehaviour
{
    [SerializeField]
    private Slider OxygenValue;
    [SerializeField]
    private Text Money;

    private void Update()
    {
        Money.text = string.Format("Money: {0}", Player.Instance.Money);
        OxygenValue.value = Player.Instance.CurrentOxygen / Player.Instance.MaxOxygen;
    }

}
