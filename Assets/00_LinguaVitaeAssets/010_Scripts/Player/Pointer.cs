using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, this.gameObject.transform.position);
        lr.SetPosition(1, this.gameObject.transform.position+this.transform.forward*100);
    }
}
