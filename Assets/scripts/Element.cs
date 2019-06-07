using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public Boolean found = false;
    public GameObject[] creates;
    public GameObject[] combinesWith;
    public Text this_name;

    int created = 0;

    private void Start()
    {
        //if there is nothing left to create
        if (created == creates.Length) {
            this.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void increase() {
        created++;
        //if there is nothing left to create
        if (created == creates.Length) {
            this.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}