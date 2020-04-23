using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioTag", menuName = "CustomTags/AudioTag")]
public class audio_TMPTag : CustomTMPTag
{

    public override string tag_name
    {
        get
        {
            return "audio";
        }
    }

    public override bool needs_closing_tag
    {
        get
        {
            return false;
        }
    }

    public override IEnumerator applyToText(TMPro.TextMeshPro text, int startIndex, int length, string param)
    {
        int i;
        /*
         * Old cuttoff for character name was determined by grabbing the character's name from the beginning of the text. 
         * This for some reason doesn't work for Oculus Quest due to [BUG 0005]
        for(i = 0; i < text.text.Length; i++)
        {
            if(text.text[i] == ':')
            {
                break;
            }
        }
        */

        // New cutoff requires that the sound files are named starting with the character's name and then either a - or a _
        for(i = 0; i < param.Length; i++)
        {
            if(param[i] == '-' || param[i] == '_')
            {
                break;
            }
        }
        string char_name = param.Substring(0, i);
        Debug.Log(char_name);
        GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundOnChar(char_name, param);
        yield return new WaitForSeconds(0.0f);
    }
}
