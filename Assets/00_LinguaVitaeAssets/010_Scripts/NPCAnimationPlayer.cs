using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationPlayer : MonoBehaviour
{
    [Yarn.Unity.YarnCommand("playAnimation")]

    public Animator npcAnimator;
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.name == "Shopkeeper")
        {
            if (Input.GetKeyDown("space"))
            {
                PlayAnimation("Talking");
                Debug.Log("space pressed");
            }
            if (Input.GetKeyDown("1"))
            { //this one's wonky
                PlayAnimation("headshake");
                Debug.Log("1 pressed");
            }
            if (Input.GetKeyDown("2"))
            {
                PlayAnimation("Standing Yell");
                Debug.Log("2 pressed");
            }
        } else if(this.gameObject.name == "Centurion")
        {
            if (Input.GetKeyDown("space"))
            {
                PlayAnimation("Talk");
                Debug.Log("space pressed");
            }
            if (Input.GetKeyDown("1"))
            {
                PlayAnimation("Clap");
                Debug.Log("1 pressed");
            }
            if (Input.GetKeyDown("2"))
            {
                PlayAnimation("HeadShake");
                Debug.Log("2 pressed");
            }
        }
        
    }
    [Yarn.Unity.YarnCommand("playAnimation")]
    public void PlayAnimation(string animationName){
        npcAnimator.Rebind();
        npcAnimator.Play(animationName);
    }
}
