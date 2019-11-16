using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudioPlayer : MonoBehaviour
{
    [Yarn.Unity.YarnCommand("playSound")]
    AudioSource _audio;
    AudioClip _clip;
    void Start(){
        _audio = this.GetComponent<AudioSource>();
    }
    // void Update(){
    //     if(Input.GetKeyDown("space")){
    //         PlaySound("Hi Quintus");
    //         Debug.Log("space pressed");
    //     }
    // }
    public void PlaySound(string audioFile)
    {
        //Play audio file with this string name
        Debug.Log(audioFile);
        _clip = (AudioClip) Resources.Load("Sounds/"+audioFile);
        _audio.Stop();
        _audio.PlayOneShot(_clip);
    }
}
