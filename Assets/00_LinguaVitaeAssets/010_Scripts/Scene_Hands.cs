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
    bool triggerReleased = true;
    GameObject dialogueRunner;
    public ObjectSelect activeObject;
    public NPCSelect activeNPC;
    GameObject activeTeleporter = null;

    Yarn.Unity.DialogueRunner currentDialogue = null;

    // Update is called once per frame
    void Update()
    {
        hand_obj.transform.localPosition = pos;
        hand_obj.transform.localRotation = rot;

        FireRay();

        if (currentDialogue != null) // If you are currently talking, you will be unable to teleport or engage in another conversation.
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
                triggerReleased = false;
                if (CheckTag("Button")) //if its a button, click it
                {
                    ClickButton();
                    
                }
                /*
                else if (CheckTag("Teleporter")) //if its a tp then go there and end the conversation
                {
                    //activeCanvas.SetActive(false);
                    isTalking = false;
                    Teleport();
                }
                */

            }
            if(currentDialogue.isDialogueRunning == false) // Set currentDialogue to null if dialogue is over so player can teleport and talk again.
            {
                currentDialogue = null;
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
                else if (CheckTag("DialogueTrigger"))
                {
                    if (playerTalkRadius >= Vector3.Distance(player.transform.position, hit.transform.position))
                    { //if the item is close enough
                        hit.transform.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                        currentDialogue = hit.transform.gameObject.GetComponent<DialogueTrigger>().runnerToTrigger;
                    }
                }
            }
        }
        if (CheckTag("DialogueTrigger"))
        {
            if (activeObject == null)
            {
                ActivateObject();
            }
            else
            {
                DeactivateObject();
                ActivateObject();
            }
        }
        else
        {
            if (activeObject != null) //if not pointing at a button, turn off any active buttons
            {
                DeactivateObject();
            }
        }

        if (CheckTag("NPC"))
        {
            if (activeNPC == null)
            {
                ActivateNPC();
            }
            else
            {
                DeactivateNPC();
                ActivateNPC();
            }
        }
        else
        {
            if (activeNPC != null) //if not pointing at a button, turn off any active buttons
            {
                DeactivateNPC();
            }
        }

        if (CheckTag("Teleporter"))
        {
            hit.transform.gameObject.GetComponent<TeleporterSpin>().spin = true;
            activeTeleporter = hit.transform.gameObject;
        }
        else
        {
            if(activeTeleporter != null)
            {
                activeTeleporter.GetComponent<TeleporterSpin>().spin = false;
                activeTeleporter = null;
            }
        }


        if (!pressed)
        {
            triggerReleased = true; // if the player released the trigger, update the variable (fire once functionality)
        }
    }

    

    void Teleport()
    {
        player.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 3.5f, hit.collider.transform.position.z); //keeps player height the same
    }

    void TalkToNPC()
    {
        if (playerTalkRadius >= Vector3.Distance(player.transform.position, hit.transform.position)) //if the npc is close enough
        {
            dialogueRunner = hit.collider.transform.GetChild(1).gameObject; //grab their dialogueRunner
            dialogueRunner.GetComponent<Yarn.Unity.DialogueRunner>().StartDialogue(); //start dialogue
            currentDialogue = dialogueRunner.GetComponent<Yarn.Unity.DialogueRunner>(); //keep track of current dialogue to know when dialogue is over
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

    void ActivateObject()
    {
        activeObject = hit.transform.gameObject.GetComponent<ObjectSelect>();
        hit.collider.gameObject.GetComponent<ObjectSelect>().hovered = true;
    }

    void DeactivateObject()
    {
        activeObject.hovered = false;
        activeObject = null;
    }

    void ActivateNPC()
    {
        activeNPC = hit.transform.gameObject.GetComponent<NPCSelect>();
        hit.collider.gameObject.GetComponent<NPCSelect>().hovered = true;
    }

    void DeactivateNPC()
    {
        activeNPC.hovered = false;
        activeNPC = null;
    }

}
