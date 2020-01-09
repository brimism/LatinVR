using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NextDialogue_CMD : VRCommand
{
    public TMPDialogueUI ui;
    bool pressed = false;
    public override void RunCommand(TestResult tr, Bone val_bone = default, bool val_bool = false, Eyes val_eyes = default, float val_float = 0, Hand val_hand = default, InputTrackingState val_input_tracking_state = InputTrackingState.None, Quaternion val_quaternion = default, Vector2 val_vector2 = default, Vector3 val_vector3 = default)
    {
        
        if (tr == TestResult.TRUE && !pressed)
        {
            Debug.Log("VR Input 1 detected");
            ui.VRInputDetected = true;
            pressed = true;
        }
        else
        {
            ui.VRInputDetected = false;
        }
        if(tr == TestResult.FALSE)
        {
            pressed = false;
        }
    }

}
