using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudioPlayer : MonoBehaviour
{
    [Yarn.Unity.YarnCommand("playSound")]
    public void PlaySound(string audioFile)
    {
        //Play audio file with this string name
        Debug.Log(audioFile);
    }
}
