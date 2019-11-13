using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MouseClick_CMD : VRCommand
{
    public bool clicked = false;
    RaycastHit hit;
    public override void RunCommand(TestResult tr, Bone val_bone = default, bool val_bool = false, Eyes val_eyes = default, float val_float = 0, Hand val_hand = default, InputTrackingState val_input_tracking_state = InputTrackingState.None, Quaternion val_quaternion = default, Vector2 val_vector2 = default, Vector3 val_vector3 = default)
    {

        if(tr == TestResult.TRUE && !clicked)
        {
            print("clicked");
            
        }

        if(tr == TestResult.FALSE)
        {
            clicked = false;
        }
    }
}
