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
        if(Input.GetKeyDown("space")){
            PlayAnimation("Talking");
            Debug.Log("space pressed");
        }
        if(Input.GetKeyDown("1")){ //this one's wonky
            PlayAnimation("headshake");
            Debug.Log("1 pressed");
        }
    }
    public void PlayAnimation(string animationName){
        npcAnimator.Rebind();
        npcAnimator.Play(animationName);
    }
}
