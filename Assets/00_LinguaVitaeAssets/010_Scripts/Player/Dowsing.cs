using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dowsing : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.transform.LookAt(target.transform);
    }
}
