using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    float time = 0;
    float startPosY = 0;
    float amplitude = 0.3f;

    void Start()
    {
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

    [Yarn.Unity.YarnCommand("obliterate")]
    public void Obliterate() // Gets rid of exclamation mark
    {
        //TODO: Change function name. Sorry i was in a rough mood.
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
