using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource _audio;
    AudioClip _clip;

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
