using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSelect : MonoBehaviour
{
    public bool hovered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hovered)
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
