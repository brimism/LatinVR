using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortraitTag", menuName = "CustomTags/PortraitTag")]
public class portrait_TMPTag : CustomTMPTag
{

    public override string tag_name
    {
        get
        {
            return "portrait";
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
        for (i = 0; i < param.Length; i++)
        {
            if (param[i] == '-' || param[i] == '_')
            {
                break;
            }
        }
        string char_name = param.Substring(0, i);
        Debug.Log(char_name);
        GameObject.Find("GameManager").GetComponent<GameManager>().SwapCharPortrait(char_name, param);
        yield return new WaitForSeconds(0.0f);
    }
}
