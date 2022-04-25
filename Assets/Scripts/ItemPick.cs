using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour, Inf_Interact
{
    [SerializeField]
    int PickLevel;
    private bool IsOpen = false;
    private Animator _Animator;
    [SerializeField]
    GameObject _light;
    [SerializeField]
    AudioSource OpenChest;


    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _light = transform.GetChild(0).gameObject;
    }

    public void PlayerInteract(float pickStrength)
    {

        if (!IsOpen)
        {
            _Animator.SetTrigger("Open");
            OpenChest.Play();
            int pick = Player.Instance.PickLevel;
            print(pick);
            if (PickLevel > pick)
            {
                StartCoroutine(PlayerNewPick());
            }
            else
            {
                UIManager.Instance.ShowTip("Got a new pick",0.2f);
            }
            IsOpen = true;
        }

    }

    IEnumerator PlayerNewPick()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Player.Instance.GetNewPick(PickLevel);
        UIManager.Instance.ShowTip("New Pick is equiped...",0.2f);
        _light.SetActive(false);

    }
}
