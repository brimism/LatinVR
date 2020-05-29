using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public enum Feature
{
    BATTERY_LEVEL_FLOAT,
    CENTER_EYE_ACCELERATION_VECTOR3,
    CENTER_EYE_ANGULAR_ACCELERATION_VECTOR3,
    CENTER_EYE_ANGULAR_VELOCITY_VECTOR3,
    CENTER_EYE_POSITION_VECTOR3,
    CENTER_EYE_ROTATION_QUATERNION,
    CENTER_EYE_VELOCITY_VECTOR3,
    COLOR_CAMERA_ACCELERATION_VECTOR3,
    COLOR_CAMERA_ANGULAR_ACCELERATION_VECTOR3,
    COLOR_CAMERA_ANGULAR_VELOCITY_VECTOR3,
    COLOR_CAMERA_POSITION_VECTOR3,
    COLOR_CAMERA_ROTATION_QUATERNION,
    COLOR_CAMERA_VELOCITY_VECTOR3,
    DEVICE_ACCELERATION_VECTOR3,
    DEVICE_ANGULAR_ACCELERATION_VECTOR3,
    DEVICE_ANGULAR_VELOCITY_VECTOR3,
    DEVICE_POSITION_VECTOR3,
    DEVICE_ROTATION_QUATERNION,
    DEVICE_VELOCITY_VECTOR3,
    D_PAD_VECTOR2,
    EYES_DATA_EYES,
    GRIP_FLOAT,
    GRIP_BUTTON_BOOL,
    HAND_DATA_HAND,
    INDEX_FINGER_FLOAT,
    INDEX_TOUCH_FLOAT,
    IS_TRACKED_BOOL,
    LEFT_EYE_ACCELERATION_VECTOR3,
    LEFT_EYE_ANGULAR_ACCELERATION_VECTOR3,
    LEFT_EYE_ANGULAR_VELOCITY_VECTOR3,
    LEFT_EYE_POSITION_VECTOR3,
    LEFT_EYE_ROTATION_QUATERNION,
    LEFT_EYE_VELOCITY_VECTOR3,
    MENU_BUTTON_BOOL,
    MIDDLE_FINGER_FLOAT,
    PINKY_FINGER_FLOAT,
    PRIMARY_2D_AXIS_VECTOR2,
    PRIMARY_2D_AXIS_CLICK_BOOL,
    PRIMARY_2D_AXIS_TOUCH_BOOL,
    PRIMARY_BUTTON_BOOL,
    PRIMARY_TOUCH_BOOL,
    RIGHT_EYE_ACCELERATION_VECTOR3,
    RIGHT_EYE_ANGULAR_ACCELERATION_VECTOR3,
    RIGHT_EYE_ANGULAR_VELOCITY_VECTOR3,
    RIGHT_EYE_POSITION_VECTOR3,
    RIGHT_EYE_ROTATION_QUATERNION,
    RIGHT_EYE_VELOCITY_VECTOR3,
    RING_FINGER_FLOAT,
    SECONDARY_2D_AXIS_VECTOR2,
    SECONDARY_BUTTON_BOOL,
    SECONDARY_TOUCH_BOOL,
    THUMBREST_BOOL,
    THUMBTOUCH_FLOAT,
    TRACKING_STATE_INPUTTRACKINGSTATE,
    TRIGGER_FLOAT,
    TRIGGER_BUTTON_BOOL
}

public class XRProcessor : MonoBehaviour
{
    public bool displayErrors = true;
    public Dictionary<XRNode, List<InputDevice>> devices = new Dictionary<XRNode, List<InputDevice>>();

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            devices.Add((XRNode)i, new List<InputDevice>());
        }
    }

    private void Update()
    {
        foreach (XRNode node in System.Enum.GetValues(typeof(XRNode)))
        {
            InputDevices.GetDevicesAtXRNode(node, devices[node]);
        }
    }

    public void PollFeature(XRNode node, Feature feature, out bool out_bool)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.GRIP_BUTTON_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.gripButton, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.IS_TRACKED_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.isTracked, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.MENU_BUTTON_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.menuButton, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.PRIMARY_2D_AXIS_CLICK_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.PRIMARY_2D_AXIS_TOUCH_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.PRIMARY_BUTTON_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.primaryButton, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.PRIMARY_TOUCH_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.primaryTouch, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.SECONDARY_BUTTON_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.secondaryButton, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.SECONDARY_TOUCH_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.secondaryTouch, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.THUMBREST_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.thumbrest, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                case Feature.TRIGGER_BUTTON_BOOL:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.triggerButton, out out_bool))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_bool = false;
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type bool.");
                    out_bool = false;
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_bool = false;
        }
    }
    public void PollFeature(XRNode node, Feature feature, out Eyes out_eyes)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.EYES_DATA_EYES:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.eyesData, out out_eyes))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_eyes = new Eyes();
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type Eyes.");
                    out_eyes = new Eyes();
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_eyes = new Eyes();
        }
    }
    public void PollFeature(XRNode node, Feature feature, out float out_float)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.BATTERY_LEVEL_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.batteryLevel, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.GRIP_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.grip, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.INDEX_FINGER_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.indexFinger, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.INDEX_TOUCH_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.indexTouch, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.MIDDLE_FINGER_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.middleFinger, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.PINKY_FINGER_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.pinkyFinger, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.RING_FINGER_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.ringFinger, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.THUMBTOUCH_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.thumbTouch, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                case Feature.TRIGGER_FLOAT:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.trigger, out out_float))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_float = 0;
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type float.");
                    out_float = 0;
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_float = 0;
        }
    }
    public void PollFeature(XRNode node, Feature feature, out Hand out_hand)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.HAND_DATA_HAND:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.handData, out out_hand))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_hand = new Hand();
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type Hand");
                    out_hand = new Hand();
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_hand = new Hand();
        }
    }
    public void PollFeature(XRNode node, Feature feature, out InputTrackingState out_input_tracking_state)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.TRACKING_STATE_INPUTTRACKINGSTATE:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.trackingState, out out_input_tracking_state))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_input_tracking_state = InputTrackingState.None;
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type InputTrackingState");
                    out_input_tracking_state = InputTrackingState.None;
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_input_tracking_state = InputTrackingState.None;
        }
    }
    public void PollFeature(XRNode node, Feature feature, out Quaternion out_quaternion)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.CENTER_EYE_ROTATION_QUATERNION:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyeRotation, out out_quaternion))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_quaternion = new Quaternion();
                    }
                    break;
                case Feature.COLOR_CAMERA_ROTATION_QUATERNION:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraRotation, out out_quaternion))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_quaternion = new Quaternion();
                    }
                    break;
                case Feature.DEVICE_ROTATION_QUATERNION:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.deviceRotation, out out_quaternion))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_quaternion = new Quaternion();
                    }
                    break;
                case Feature.LEFT_EYE_ROTATION_QUATERNION:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyeRotation, out out_quaternion))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_quaternion = new Quaternion();
                    }
                    break;
                case Feature.RIGHT_EYE_ROTATION_QUATERNION:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyeRotation, out out_quaternion))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_quaternion = new Quaternion();
                    }
                    break;

                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type Quaternion");
                    out_quaternion = new Quaternion();
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_quaternion = new Quaternion();
        }
    }
    public void PollFeature(XRNode node, Feature feature, out Vector2 out_vector2)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.D_PAD_VECTOR2:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.dPad, out out_vector2))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector2 = new Vector2();
                    }
                    break;
                case Feature.PRIMARY_2D_AXIS_VECTOR2:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.primary2DAxis, out out_vector2))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector2 = new Vector2();
                    }
                    break;
                case Feature.SECONDARY_2D_AXIS_VECTOR2:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.secondary2DAxis, out out_vector2))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector2 = new Vector2();
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type Vector2.");
                    out_vector2 = new Vector2();
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_vector2 = new Vector2();
        }
    }
    public void PollFeature(XRNode node, Feature feature, out Vector3 out_vector3)
    {
        if (devices[node].Count == 1)
        {
            switch (feature)
            {
                case Feature.CENTER_EYE_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyeAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.CENTER_EYE_ANGULAR_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyeAngularAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.CENTER_EYE_ANGULAR_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyeAngularVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.CENTER_EYE_POSITION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyePosition, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.CENTER_EYE_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.centerEyeVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.COLOR_CAMERA_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.COLOR_CAMERA_ANGULAR_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraAngularAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.COLOR_CAMERA_ANGULAR_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraAngularVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.COLOR_CAMERA_POSITION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraPosition, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.COLOR_CAMERA_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.colorCameraVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.DEVICE_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.deviceAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                    }
                    break;
                case Feature.DEVICE_ANGULAR_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.DEVICE_ANGULAR_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.DEVICE_POSITION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.devicePosition, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.DEVICE_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.deviceVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.LEFT_EYE_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyeAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.LEFT_EYE_ANGULAR_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyeAngularAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.LEFT_EYE_ANGULAR_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyeAngularVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.LEFT_EYE_POSITION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyePosition, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.LEFT_EYE_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.leftEyeVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.RIGHT_EYE_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyeAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.RIGHT_EYE_ANGULAR_ACCELERATION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyeAngularAcceleration, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.RIGHT_EYE_ANGULAR_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyeAngularVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.RIGHT_EYE_POSITION_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyePosition, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                case Feature.RIGHT_EYE_VELOCITY_VECTOR3:
                    if (!devices[node][0].TryGetFeatureValue(CommonUsages.rightEyeVelocity, out out_vector3))
                    {
                        ErrorMessage(devices[node][0].name + " does not have feature " + feature.ToString());
                        out_vector3 = new Vector3();
                    }
                    break;
                default:
                    ErrorMessage(feature.ToString() + " does not return a value of type Vector3");
                    out_vector3 = new Vector3();
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid number of devices of XRNode type " + node.ToString() + ": " + devices[node].Count + " devices detected.");
            out_vector3 = new Vector3();
        }
    }

    void ErrorMessage(string s)
    {
        if(displayErrors)
        {
            Debug.Log(s);
        }
    }

}