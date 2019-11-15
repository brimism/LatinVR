using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Yarn.Unity.DialogueRunner runnerToTrigger;
    public string node;
    
    public void TriggerDialogue()
    {
        runnerToTrigger.StartDialogue(node);
    }
}
