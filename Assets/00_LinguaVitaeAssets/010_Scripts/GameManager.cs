using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;

public class GameManager : YarnObserver
{
    public ItemHolder playerItemHolder;
    public Dictionary<string, Character> characters;

    public override void Observe(string var_name, Yarn.Value value)
    {
        // Set food quest item
        if(var_name == "$food_quest" && value.AsNumber != 0)
        {
            if (value.AsNumber == 1)
            {
                playerItemHolder.HoldItem("bread");
            }
            else if (value.AsNumber == 2)
            {
                playerItemHolder.HoldItem("grapes");
            }
            else if (value.AsNumber == 3)
            {
                playerItemHolder.HoldItem("jar");
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
