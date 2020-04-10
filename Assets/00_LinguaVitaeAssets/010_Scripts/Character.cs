using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AudioSource _audio;
    AudioClip _clip;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().characters.Add(name, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string audioFile)
    {
        //Play audio file with this string name
        Debug.Log(audioFile);
        _clip = (AudioClip)Resources.Load("Sounds/" + audioFile);
        _audio.Stop();
        _audio.PlayOneShot(_clip);
    }

    public void StopSound()
    {
        _audio.Stop();
    }
}
