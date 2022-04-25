using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLight : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    public int LightLevel;
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
        int level = Player.Instance.view;
        if (!IsOpen)
        { 
            if (LightLevel > level)
            {
                _Animator.SetTrigger("Open");
                OpenChest.Play();
                StartCoroutine(PlayerNewLight());
                IsOpen = true;
            }
            else
            {
                UIManager.Instance.ShowTip("You have better light",0.2f);
            }
        }
       
    }

    IEnumerator PlayerNewLight()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Player.Instance.UpdateViewFiled(LightLevel);
        UIManager.Instance.ShowTip("New light is equiped...",0.2f);
        _light.SetActive(false);

    }

}
