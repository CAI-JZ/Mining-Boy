using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Rock_Oxygen : MonoBehaviour, Inf_Interact
{
    [Header("-> UI")]
    [SerializeField]
    private Slider RockDur;
    [SerializeField]
    private CanvasGroup DurBar;

    [SerializeField]
    float PickProcess;
    [SerializeField]
    float TotalValue;

    bool isPick = false;

    private void FixedUpdate()
    {
        RockDur.value = PickProcess / TotalValue;
        StartCoroutine(ProcessReduce());


    }
    private void Update()
    {
        if (isPick)
        {
            IsPicking(1);
        }
    }

    public void PlayerInteract(float pickStrength)
    {
        if (PickProcess <= 0)
        {
            DurBar.alpha = 1;
        }
        StartCoroutine(ProcessUpdate(4));
        isPick = true;
        if (PickProcess >= TotalValue)
        {
            FinishPick();
            Destroy(gameObject);
        }
    }

    void IsPicking(float timer)
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime *100;
            if (timer <= 0)
            {
                timer = 0;
                isPick = false;
                return;
            }
        }
    }

    IEnumerator ProcessUpdate(float target)
    {
        float value= PickProcess + target;
        while (PickProcess < value)
        {
            PickProcess += Time.deltaTime * 50;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator ProcessReduce()
    {
        if (PickProcess > 0 && !isPick)
        { 
            PickProcess -= Time.deltaTime * 10;
            if (PickProcess <= 0)
            {
                PickProcess = 0;
                DurBar.alpha = 0;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
       
    }

    void FinishPick()
    {
        if (Player.Instance != null)
        { 
            float oxy = Player.Instance._MayOxygen;
            Player.Instance.AddOxygen(oxy,0);
        }
    }
}
