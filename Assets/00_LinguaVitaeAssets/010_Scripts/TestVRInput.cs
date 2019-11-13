using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TestVRInput : MonoBehaviour
{
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    public GameObject cube;
    Vector3 pos;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
    }

    float leftHandTriggerForce;
    float rightHandTriggerForce;

    // Update is called once per frame
    void Update()
    {
        
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count >= 1)
        {
            if(leftHandDevices[0].TryGetFeatureValue(CommonUsages.trigger, out leftHandTriggerForce))
            {
                Debug.Log("Left Hand pushing with Force: " + leftHandTriggerForce.ToString());
            }
        }

        if (rightHandDevices.Count >= 1)
        {
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.trigger, out rightHandTriggerForce))
            {
                Debug.Log("Right Hand pushing with Force: " + rightHandTriggerForce.ToString());
            }
            if(rightHandDevices[0].TryGetFeatureValue(CommonUsages.devicePosition,out pos))
                cube.transform.position = pos;
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.deviceRotation, out rot))
                cube.transform.rotation = rot;

        }



    }
    
}
