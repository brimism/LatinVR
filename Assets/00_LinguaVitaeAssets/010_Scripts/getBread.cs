using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getBread : MonoBehaviour
{
    public GameObject panis;
    public GameObject vase;
    public GameObject grapes;
    [Yarn.Unity.YarnCommand("getPanis")]
    public void GetPanis(string t)
    {
        if (t == "true") {
            panis.SetActive(true);
        }
        else
        {
            panis.SetActive(false);
        }
    }

    [Yarn.Unity.YarnCommand("getVase")]
    public void GetVase(string t)
    {
        if (t == "true")
        {
            vase.SetActive(true);
        }
        else
        {
            vase.SetActive(false);
        }
    }

    [Yarn.Unity.YarnCommand("getGrapes")]
    public void GetGrapes(string t)
    {
        if (t == "true")
        {
            grapes.SetActive(true);
        }
        else
        {
            grapes.SetActive(false);
        }
    }

}
