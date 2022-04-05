using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character : MonoBehaviour
{
    [SerializeField]
    private GameObject Oxygen;
    [SerializeField]
    private Text Money;

    private Slider OxyValue;

    private void Start()
    {
        OxyValue = Oxygen.GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        Money.text = string.Format("Money: {0}", Player.Instance.Money);
        Oxygen.GetComponent<RectTransform>().sizeDelta = new Vector2(Player.Instance._MayOxygen, 13);
        OxyValue.value = Player.Instance.CurrentOxygen / Player.Instance._MayOxygen;
    }
}
