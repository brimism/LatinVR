using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool selected;
    public float rotSpeed = 10;
    public float scaleSpeed = 10;
    public float arrowRot = 0f;
    public float middleRingRot = 360f;
    public float resetStart = 0f;
    public float arrowScale = 1.723f;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(selected) {
            resetStart = 0f;
            arrowRot += rotSpeed * Time.deltaTime;
            middleRingRot -= rotSpeed * Time.deltaTime;
            arrowScale = Mathf.Lerp(0.7f,1.723f,Mathf.Abs(Mathf.Sin(Time.fixedTime * scaleSpeed)));
        }
        if(!selected && resetStart == 0f) {
            resetStart = Time.fixedTime;
        }
        if(!selected && transform.GetChild(2).eulerAngles != new Vector3(0,0,0)) {
            arrowRot = Mathf.Clamp(arrowRot - (Time.fixedTime - resetStart) * rotSpeed, 0f, 360f);
            middleRingRot = Mathf.Clamp(middleRingRot + (Time.fixedTime - resetStart) * rotSpeed, 0f, 360f);
            arrowScale = Mathf.Clamp(arrowScale + (Time.fixedTime - resetStart) * scaleSpeed, 0f, 1.723f);
        }

        if(arrowRot >= 360f) {
            arrowRot -= 360f;
        }
        if(middleRingRot <= 0f) {
            middleRingRot += 360f;
        }

        transform.GetChild(2).eulerAngles = new Vector3(transform.GetChild(2).eulerAngles.x, arrowRot, transform.GetChild(2).eulerAngles.z);
        transform.GetChild(1).eulerAngles = new Vector3(transform.GetChild(1).eulerAngles.x, middleRingRot, transform.GetChild(1).eulerAngles.z);
        transform.GetChild(0).eulerAngles = new Vector3(transform.GetChild(0).eulerAngles.x, arrowRot, transform.GetChild(0).eulerAngles.z);
        transform.GetChild(2).localScale = new Vector3(arrowScale, arrowScale, arrowScale);
    }
}
