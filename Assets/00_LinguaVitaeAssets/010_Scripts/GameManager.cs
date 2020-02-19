using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

[RequireComponent(typeof(DictionaryController))]
[RequireComponent(typeof(NextDialogue_CMD))]
[RequireComponent(typeof(NextDialogue2_CMD))]
[RequireComponent(typeof(LHGrip_CMD))]
[RequireComponent(typeof(RHGrip_CMD))]
public class GameManager : YarnObserver
{
    public bool rightHanded = true;
    public bool LH_Trigger = false;
    public bool RH_Trigger = false;
    public bool LH_Grip = false;
    public bool RH_Grip = false;
    public ItemHolder playerItemHolder;
    public ShopController shopController;
    public Dictionary<string, Character> characters;

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
    }

    public void StopAllSounds()
    {
        foreach (var c in characters)
        {
            c.Value.StopSound();
        }
    }


    [Yarn.Unity.YarnCommand("endDialogue")]
    public void EndOfDialogue()
    {
        StopAllSounds();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (characters == null)
        {
            characters = new Dictionary<string, Character>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
