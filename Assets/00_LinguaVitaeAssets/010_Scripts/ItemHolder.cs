using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    GameObject heldItem;
    void Start()
    {
        // Set all children to be inactive
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        heldItem = null;
    }

    public void HoldItem(string itemName)
    {
        // Deactivate previously held item
        if (heldItem != null)
        {
            heldItem.SetActive(false);
        }

        // Activate new item
        heldItem = transform.Find(itemName).gameObject;
        heldItem.SetActive(true);
    }

    public void ClearItem()
    {
        // Removes held item
        if(heldItem != null)
        {
            heldItem.SetActive(false);
            heldItem = null;
        }
    }

    public GameObject GetItem()
    {
        return heldItem;
    }
}
