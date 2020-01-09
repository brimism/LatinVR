using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    float time = 0;
    float startPosY = 0;
    float amplitude = 0.3f;

    public bool visibleOnStart;

    void Start()
    {
        if(!visibleOnStart)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        startPosY = transform.GetChild(0).position.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, startPosY + Mathf.Sin(time) * amplitude, transform.GetChild(0).position.z ); // Makes Exclamation mark bob
        }
    }

    [Yarn.Unity.YarnCommand("disappear")]
    public void Disappear() // Gets rid of exclamation mark
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    [Yarn.Unity.YarnCommand("appear")]
    public void Appear() // Makes exclamation mark appear
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
