using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterSpin : MonoBehaviour
{
    public bool spin;
    // Start is called before the first frame update
    void Start()
    {
        spin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spin && !transform.GetChild(3).gameObject.activeSelf)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if(!spin && transform.GetChild(3).gameObject.activeSelf)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
