using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOxygen : MonoBehaviour,Inf_Interact
{
    [SerializeField]
    private int OxygenLevel;
    private bool IsOpen = false;
    private Animator _Animator;
    GameObject _light;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _light = transform.GetChild(0).gameObject;
    }

    public void PlayerInteract(float pickStrength)
    {
        float newOxy = OxygenLevel * 50 + 100;
        if (!IsOpen)
        { 
            if (newOxy > Player.Instance._MayOxygen)
            {
                _Animator.SetTrigger("Open");
                StartCoroutine(PlayerAddOxy(newOxy));
                IsOpen = true;
            }
        }
        
    }

    IEnumerator PlayerAddOxy(float newOxy)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Player.Instance.updateOxygen(newOxy);
        _light.SetActive(false);
    }

  
}
