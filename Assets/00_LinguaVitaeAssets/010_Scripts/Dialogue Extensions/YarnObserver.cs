using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class YarnObserver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Observe(string var_name, Yarn.Value value); // Extend this based on what class you are creating

}
