using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    public Animator npcAnimator;
    public bool hovered = false;

    void Update()
    {
        // Turns spotlight on and off if NPC is selected
        if (hovered)
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void PlayAnimation(string animationName)
    {
        npcAnimator.Rebind();
        npcAnimator.Play(animationName);
    }

}
