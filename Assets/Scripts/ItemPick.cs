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
            if (PickLevel > Player.Instance.PickLevel)
            {
                StartCoroutine(PlayerNewPick());
            }
            else
            {
                UIManager.Instance.ShowTip("Got a new pick");
            }
            IsOpen = true;
        }

    }

    IEnumerator PlayerNewPick()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Player.Instance.GetNewPick(PickLevel);
        UIManager.Instance.ShowTip("New Pick is equiped...");
        _light.SetActive(false);

    }
}
