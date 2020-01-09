using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem(string item_name)
    { 
        // Makes item disappear
        transform.Find(item_name).gameObject.SetActive(false);
    }
    public void ReturnItem(string item_name)
    {
        // Makes item appear
        transform.Find(item_name).gameObject.SetActive(true);
    }
}
