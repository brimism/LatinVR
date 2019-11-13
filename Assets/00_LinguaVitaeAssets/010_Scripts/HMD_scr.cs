using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMD_scr : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion rot;

    public GameObject HMDcube;

    void Update()
    {
        HMDcube.transform.localPosition = pos;
        HMDcube.transform.localRotation = rot;
    }
}
