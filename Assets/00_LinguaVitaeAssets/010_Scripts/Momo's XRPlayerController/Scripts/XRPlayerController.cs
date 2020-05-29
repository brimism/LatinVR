using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRPlayerController : MonoBehaviour
{
    public bool controlsMove = true;
    public bool controlsRotate = true;
    public bool moveRelativeToFacing;
    public CharacterController controller;
    public XRProcessor XRProcessor;
    public Camera mainCamera;
    public float gravity = -9.81f;
    public float moveSpeed = 2;
    public float stickDeadzone = 0.3f;
    public float termVel = 0f;
    Vector3 fProj;
    Vector3 rProj;
    float velocityY;
    bool rotateFlicked;
    public float rotationAngle = 30;
    float currentSelectedRotation;
    public float gripSensitivity = 0.3f;
    public float triggerSensitivity = 0.3f;
    [HideInInspector]
    public ControlValues controlValues;

    public struct ControlValues
    {
        public Vector3 headPos;
        public Vector3 rightHandPos;
        public Vector3 leftHandPos;
        public Quaternion headRot;
        public Quaternion rightHandRot;
        public Quaternion leftHandRot;
        public Vector2 leftHandJoystick;
        public Vector2 rightHandJoystick;
        public float leftHandTrigger;
        public float rightHandTrigger;
        public float leftHandGrip;
        public float rightHandGrip;
        public bool leftHandButtonX;
        public bool leftHandButtonY;
        public bool rightHandButtonA;
        public bool rightHandButtonB;
        public bool menuButton;
        public bool leftHandJoystickPressed;
        public bool rightHandJoystickPressed;
    }

    void Start() {
        velocityY = 0;
        rotateFlicked = false;
        currentSelectedRotation = 0;
    }

    void Update()
    {
        PollProcessor();
        // Calculate forward and right vectors
        if(moveRelativeToFacing) {
            fProj = Vector3.Normalize(Vector3.Scale(mainCamera.transform.forward, new Vector3(1,0,1)));
        }
        else {
            fProj = Quaternion.Euler(0, currentSelectedRotation, 0) * new Vector3(0,0,1);
        }
        rProj = Vector3.Normalize(Vector3.Cross(new Vector3(0,1,0),fProj));

        Debug.DrawRay(mainCamera.transform.position, fProj * 20,Color.red);
        Debug.DrawRay(mainCamera.transform.position, rProj * 20,Color.blue);
        if(controlsRotate)
            ProcessRotate();
        if(controlsMove)
            ProcessMove();
    }
    void ProcessRotate() {
        // Preprocess input data
        Vector2 rawStickData = controlValues.rightHandJoystick;
        // Create stick deadzone
        if(Mathf.Abs(rawStickData.x) <= stickDeadzone) {
            rawStickData.x = 0;
        }
        if(!rotateFlicked && rawStickData.x != 0) {
            int direction = (int)Mathf.Sign(rawStickData.x); //-1 = left. 1 = right.
            mainCamera.transform.parent.RotateAround(mainCamera.transform.position,Vector3.up,rotationAngle * direction);
            currentSelectedRotation += rotationAngle * direction;
            rotateFlicked = true;
        }
        if(rawStickData.magnitude < stickDeadzone) {
            rotateFlicked = false;
        }
        
    }

    void ProcessMove() {
        // Preprocess input data
        Vector2 rawStickData = controlValues.leftHandJoystick;
        // Create stick dead zone
        if(Mathf.Abs(rawStickData.x) <= stickDeadzone) {
            rawStickData.x = 0;
        }
        if(Mathf.Abs(rawStickData.y) <= stickDeadzone) {
            rawStickData.y = 0;
        }
        Vector3 moveData = moveSpeed * Vector3.Normalize(fProj * rawStickData.y + rProj * rawStickData.x);
        velocityY += gravity * Time.deltaTime;
        if(velocityY < termVel && termVel != 0)
        {
            velocityY = termVel;
        }
        if(controller.isGrounded) {
            velocityY = -0.1f;
        }
        moveData.y = velocityY;
        controller.Move(moveData * Time.deltaTime);
    }

    public void SendHaptics(bool leftHand, float amplitude, float duration)
    {
        UnityEngine.XR.HapticCapabilities capabilities;
        foreach (var device in XRProcessor.devices)
        {
            if(device.Key == UnityEngine.XR.XRNode.LeftHand && device.Value.Count != 0 && leftHand)
            {
                // Send haptics to left hand
                if(device.Value[0].TryGetHapticCapabilities(out capabilities))
                {
                    if(capabilities.supportsImpulse)
                    {
                        device.Value[0].SendHapticImpulse(0, amplitude, duration);
                    }
                }
                break;
            }
            else if (device.Key == UnityEngine.XR.XRNode.RightHand && device.Value.Count != 0 && !leftHand)
            {
                // Send haptics to right hand
                if (device.Value[0].TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        device.Value[0].SendHapticImpulse(0, amplitude, duration);
                    }
                }
                break;
            }
        }
    }
    
    void PollProcessor()
    {
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.Head, Feature.DEVICE_POSITION_VECTOR3, out controlValues.headPos);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.Head, Feature.DEVICE_ROTATION_QUATERNION, out controlValues.headRot);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.PRIMARY_BUTTON_BOOL, out controlValues.leftHandButtonX);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.SECONDARY_BUTTON_BOOL, out controlValues.leftHandButtonY);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.GRIP_FLOAT, out controlValues.leftHandGrip);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.PRIMARY_2D_AXIS_VECTOR2, out controlValues.leftHandJoystick);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.PRIMARY_2D_AXIS_CLICK_BOOL, out controlValues.leftHandJoystickPressed);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.DEVICE_POSITION_VECTOR3, out controlValues.leftHandPos);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.DEVICE_ROTATION_QUATERNION, out controlValues.leftHandRot);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.TRIGGER_FLOAT, out controlValues.leftHandTrigger);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.LeftHand, Feature.MENU_BUTTON_BOOL, out controlValues.menuButton);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.PRIMARY_BUTTON_BOOL, out controlValues.rightHandButtonA);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.SECONDARY_BUTTON_BOOL, out controlValues.rightHandButtonB);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.GRIP_FLOAT, out controlValues.rightHandGrip);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.PRIMARY_2D_AXIS_VECTOR2, out controlValues.rightHandJoystick);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.PRIMARY_2D_AXIS_CLICK_BOOL, out controlValues.rightHandJoystickPressed);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.DEVICE_POSITION_VECTOR3, out controlValues.rightHandPos);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.DEVICE_ROTATION_QUATERNION, out controlValues.rightHandRot);
        XRProcessor.PollFeature(UnityEngine.XR.XRNode.RightHand, Feature.TRIGGER_FLOAT, out controlValues.rightHandTrigger);
    }
}
