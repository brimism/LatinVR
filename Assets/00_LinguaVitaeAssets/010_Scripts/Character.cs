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
        if(GameObject.Find("GameManager").GetComponent<GameManager>().characters == null)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().characters = new Dictionary<string, Character>();
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().characters.Add(name, this);
    }

    // Update is called once per frame
    void Update()
    {
        
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
