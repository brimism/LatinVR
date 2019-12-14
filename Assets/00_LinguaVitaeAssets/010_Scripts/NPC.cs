using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator npcAnimator;
    public AudioSource _audio;
    AudioClip _clip;
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

    [Yarn.Unity.YarnCommand("playAnimation")]
    public void PlayAnimation(string animationName)
    {
        npcAnimator.Rebind();
        npcAnimator.Play(animationName);
    }

    [Yarn.Unity.YarnCommand("playSound")]
    public void PlaySound(string audioFile)
    {
        //Play audio file with this string name
        Debug.Log(audioFile);
        _clip = (AudioClip)Resources.Load("Sounds/" + audioFile);
        _audio.Stop();
        _audio.PlayOneShot(_clip);
    }

    [Yarn.Unity.YarnCommand("stopSound")]
    public void StopSound()
    {
        _audio.Stop();
    }
}
