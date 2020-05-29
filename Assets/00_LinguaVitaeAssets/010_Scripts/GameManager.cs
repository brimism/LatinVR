using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

[RequireComponent(typeof(DictionaryController))]
public class GameManager : YarnObserver
{
    public bool rightHanded = true;
    [HideInInspector]
    public bool LH_Trigger = false;
    [HideInInspector]
    public bool RH_Trigger = false;
    [HideInInspector]
    public bool LH_Grip = false;
    [HideInInspector]
    public bool RH_Grip = false;
    public ItemHolder playerItemHolder;
    public ShopController shopController;
    public Dictionary<string, Character> characters;
    public DialogueRunner[] dialogueRunners;
    public XRPlayerController playerController;

    public GameObject DebugPanel;

    public override void Observe(string var_name, Yarn.Value value)
    {
        // Set food quest item
        if(var_name == "$food_quest" && value.AsNumber != 0)
        {
            if (value.AsNumber == 1)
            {
                playerItemHolder.HoldItem("bread");
                shopController.ReturnItem("grapes");
                shopController.ReturnItem("jar");
                shopController.TakeItem("bread");
            }
            else if (value.AsNumber == 2)
            {
                playerItemHolder.HoldItem("grapes");
                shopController.ReturnItem("bread");
                shopController.ReturnItem("jar");
                shopController.TakeItem("grapes");
            }
            else if (value.AsNumber == 3)
            {
                playerItemHolder.HoldItem("jar");
                shopController.ReturnItem("grapes");
                shopController.ReturnItem("bread");
                shopController.TakeItem("jar");
            }
            else if(value.AsNumber == -1)
            {
                //Quest complete
                SceneManager.LoadScene("Finish");
            }

        }
    }

    public void PlaySoundOnChar(string character_name, string sound_file)
    {
        // Plays sound on the corresponding character
        StopAllSounds();
        characters[character_name].PlaySound(character_name + '/' + sound_file);
        DebugPanel.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Character: " + character_name + "; Sound: " + sound_file;
    }

    public void SwapCharPortrait(string character_name, string portrait_file)
    {
        Debug.Log("Swapping portrait");
        foreach(var dialogueRunner in dialogueRunners)
        {
            if(dialogueRunner.isDialogueRunning)
            {
                dialogueRunner.transform.parent.gameObject.GetComponent<NPC>().SwapSprite(character_name + '/' + portrait_file);
                break;
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (var c in characters)
        {
            c.Value.StopSound();
        }
        DebugPanel.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "No Sound";
    }


    [Yarn.Unity.YarnCommand("endDialogue")]
    public void EndOfDialogue()
    {
        StopAllSounds();
        SwapCharPortrait("None", "None");
    }

    // Start is called before the first frame update
    void Awake()
    {
        characters = new Dictionary<string, Character>();
    }

    // Update is called once per frame
    void Update()
    {
        LH_Grip = playerController.controlValues.leftHandGrip > playerController.gripSensitivity;
        RH_Grip = playerController.controlValues.rightHandGrip > playerController.gripSensitivity;
        LH_Trigger = playerController.controlValues.leftHandTrigger > playerController.triggerSensitivity;
        RH_Trigger = playerController.controlValues.rightHandTrigger > playerController.triggerSensitivity;
    }
}
