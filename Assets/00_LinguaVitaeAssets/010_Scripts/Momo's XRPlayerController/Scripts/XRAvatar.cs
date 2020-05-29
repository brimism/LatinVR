using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRAvatar : MonoBehaviour
{
    public XRPlayerController inputs;

    public Transform head;
    public Transform rightHand;
    public Transform leftHand;

    void Update()
    {
        head.localPosition = inputs.controlValues.headPos;
        head.localRotation = inputs.controlValues.headRot;
        rightHand.localPosition = inputs.controlValues.rightHandPos;
        rightHand.localRotation = inputs.controlValues.rightHandRot;
        leftHand.localPosition = inputs.controlValues.leftHandPos;
        leftHand.localRotation = inputs.controlValues.leftHandRot;
    }
}
