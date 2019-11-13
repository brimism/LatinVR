using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Hands : Hand_scr
{
    public GameObject player;
    public float playerTalkRadius;
    public ButtonInteraction activeButton;
    

    RaycastHit hit;
    bool isTalking = false;
    bool triggerReleased = true;
    GameObject activeCanvas;

    // Update is called once per frame
    void Update()
    {
        hand_obj.transform.localPosition = pos;
        hand_obj.transform.localRotation = rot;

        FireRay();

        if (isTalking)
        {
            if (CheckTag("Button"))
            {
                if (activeButton == null)
                {
                    ActivateButton();
                }
                else
                {
                    DeactivateButton();
                    ActivateButton();
                }
            }
            else
            {
                if (activeButton != null) //if not pointing at a button, turn off any active buttons
                {
                    DeactivateButton();
                }
            }

            if (triggerReleased && pressed)
            {
                if (CheckTag("Button")) //if its a button, click it
                {
                    ClickButton();
                    
                }
                else if (CheckTag("Teleporter")) //if its a tp then go there and end the conversation
                {
                    triggerReleased = false;
                    activeCanvas.SetActive(false);
                    isTalking = false;
                    Teleport();
                }
                
            }
        }
        else
        {
            if(triggerReleased && pressed) //fire once per trigger pull
            {
                triggerReleased = false; //toggle to perform on trigger press functionality (the fire once part)
                if (CheckTag("NPC"))
                {
                    TalkToNPC();
                }
                else if (CheckTag("Teleporter"))
                {
                    Teleport();
                }
            }
        }

        if (!pressed)
        {
            triggerReleased = true; // if the player released the trigger, update the variable (fire once functionality)
        }
    }

    

    void Teleport()
    {
        player.transform.position = new Vector3(hit.collider.transform.position.x, player.transform.position.y, hit.collider.transform.position.z); //keeps player height the same
    }

    void TalkToNPC()
    {
        if (playerTalkRadius >= Vector3.Distance(player.transform.position, hit.transform.position)) //if the npc is close enough
        {
            activeCanvas = hit.collider.transform.GetChild(0).gameObject; //grab their canvas
            activeCanvas.SetActive(true); //activate it
            isTalking = true;
        }
        else
        {
            print("not close enough");
        }
    }
    
    void FireRay()
    {
        if (Physics.Raycast(hand_obj.transform.position, hand_obj.transform.forward, out hit, 2000.0F)) //perform raycast
        {
            pointer_sphere.transform.position = hit.point; //if hit something move a target sphere
        }
        else
        {
            pointer_sphere.transform.position = new Vector3(0, 0, 10000); //otherwise put it somewhere else
        }
    }
    
    bool CheckTag(string objTag)
    {
        if (hit.collider != null) //if there was something the ray hit
            return objTag == hit.collider.gameObject.tag; //check the tag
        else
            return false;
    }

    void ClickButton()
    {
        activeButton.onClick(); //update button state
        hit.transform.gameObject.GetComponent<Button>().onClick.Invoke(); //invoke the button component
    }

    void ActivateButton()
    {

        activeButton = hit.transform.gameObject.GetComponent<ButtonInteraction>();
        activeButton.onHover();//update button state
    }

    void DeactivateButton()
    {
        activeButton.onRelease();//update button state
        activeButton = null; //stop pointing at this button
    }

}
