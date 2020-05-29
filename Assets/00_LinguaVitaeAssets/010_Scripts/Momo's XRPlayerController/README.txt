Momo's XR Player Controller Readme
----------------------------------
I haven't made proper documentation for this yet,
but here's a quick guide to setting up a scene for VR
----------------------------------
1. Drop in an XR Processor Prefab
2. Drop in an XR Player Controller with Avatar prefab (or without avatar if you dont want it. The avatar is customizable btw)
3. Drop the XR Processor object from the scene heirarchy into the XR Player Controller's XR Processor Slot.
4. Profit :D
----------------------------------
If you want to get specific button values, just grab them from the XR Player Controller's controlValues variable.
If you want to send a Haptic impulse, use the XR Player Controller's SendHaptics() function.