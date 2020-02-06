using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour
{
    private enum VisibleState {NONE, LEFT, RIGHT};
    public GameObject dictionary;
    public GameObject RightHandDictionarySlot;
    public GameObject LeftHandDictionarySlot;
    private GameManager gameManager;

    private VisibleState visible = VisibleState.NONE;
    // Start is called before the first frame update
    void Start()
    {
        dictionary.SetActive(false);
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.LH_Grip && visible != VisibleState.LEFT) {
            // Left grip pressed and dictionary invisible -> Put dictionary in left hand.
            dictionary.transform.parent = LeftHandDictionarySlot.transform;
            dictionary.transform.localPosition = Vector3.zero;
            dictionary.transform.localEulerAngles = Vector3.zero;
            dictionary.SetActive(true);
            visible = VisibleState.LEFT;
        }
        else if(gameManager.LH_Grip && visible == VisibleState.LEFT) {
            // Left grip pressed and dictionary in left hand -> Make dictionary invisible
            visible = VisibleState.NONE;
        }
        
        if(gameManager.RH_Grip && visible != VisibleState.RIGHT) {
            // Right grip pressed and dictionary invisible -> Put dictionary in rightt hand.
            dictionary.transform.parent = RightHandDictionarySlot.transform;
            dictionary.transform.localPosition = Vector3.zero;
            dictionary.transform.localEulerAngles = Vector3.zero;
            dictionary.SetActive(true);
            visible = VisibleState.RIGHT;
        }
        else if(gameManager.RH_Grip && visible == VisibleState.RIGHT) {
            // Right grip pressed and dictionary in right hand -> Make dictionary invisible
            visible = VisibleState.NONE;
        }
        
        if(visible == VisibleState.NONE && dictionary.activeSelf){
            // If dictionary is visible but should not be, make it invisible.
            dictionary.transform.parent = this.transform;
            dictionary.transform.localPosition = Vector3.zero;
            dictionary.transform.localEulerAngles = Vector3.zero;
            dictionary.SetActive(false);
        }
    }
}
