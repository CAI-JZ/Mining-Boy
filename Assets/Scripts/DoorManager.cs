using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    //private static List<GameObject> Doors = new List<GameObject> ();

    private void Awake()
    {
        int DoorNum = Random.Range(0, transform.childCount);
        print(transform.childCount+" "+DoorNum);
        transform.GetChild(DoorNum).gameObject.SetActive(true);        
    }

}

