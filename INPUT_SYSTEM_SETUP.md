# Input System Setup Guide
## Unity New Input System Configuration for Treasure Excavator

**Version**: 1.0
**Last Updated**: 2025-11-18
**Unity Version**: 2022.3 LTS
**Input System Package**: 1.5.0+

---

## 1. Installing Input System Package

### Step 1: Open Package Manager
1. Unity → Window → Package Manager
2. Change dropdown to "Unity Registry"
3. Search for "Input System"
4. Click "Install"

### Step 2: Configure Active Input Handling
1. Edit → Project Settings → Player
2. Scroll to "Other Settings"
3. Find "Active Input Handling"
4. Select "Input System Package (New)" or "Both" (for compatibility)
5. Click "Apply"
6. Unity will prompt to restart → Click "Yes"

---

## 2. Creating Input Actions Asset

### Step 1: Create Input Actions Asset
1. In Project window, navigate to `Assets/Settings/InputActions/`
2. Right-click → Create → Input Actions
3. Name it: `PlayerInputActions`
4. Double-click to open Input Actions Editor

### Step 2: Create Action Maps

#### Action Map: "Player"
This is the main action map for gameplay controls.

**Actions to Create**:

1. **Touch** (Button)
   - Type: Button
   - Control Type: Any
   - Binding: `<Touchscreen>/primaryTouch/press`
   - Used for: Detecting touch start/end

2. **TouchPosition** (Value, Vector2)
   - Type: Value
   - Control Type: Vector2
   - Binding: `<Touchscreen>/primaryTouch/position`
   - Used for: Getting touch position on screen

3. **Move** (Value, Vector2)
   - Type: Value
   - Control Type: Vector2
   - Binding: `<Gamepad>/leftStick` (for testing in Editor)
   - Used for: Manual movement input (testing only)

### Step 3: Configure Bindings

For **Touch** action:
```
Action: Touch
Type: Button
Control Type: Any
Bindings:
  - Path: <Touchscreen>/primaryTouch/press
    Interactions: (none)
    Processors: (none)
```

For **TouchPosition** action:
```
Action: TouchPosition
Type: Value
Control Type: Vector2
Bindings:
  - Path: <Touchscreen>/primaryTouch/position
    Interactions: (none)
    Processors: (none)
```

For **Move** action (Editor testing):
```
Action: Move
Type: Value
Control Type: Vector2
Bindings:
  - Path: <Gamepad>/leftStick
    Interactions: (none)
    Processors: (none)
  - Composite: 2D Vector (Keyboard WASD)
      Up: <Keyboard>/w
      Down: <Keyboard>/s
      Left: <Keyboard>/a
      Right: <Keyboard>/d
```

### Step 4: Save and Generate C# Class
1. Click "Save Asset" in Input Actions Editor
2. Check "Generate C# Class" at top of Inspector
3. Set Class Name: `PlayerInputActions`
4. Set Namespace: `TreasureExcavator`
5. Set Class File Path: `Assets/Scripts/PlayerInputActions.cs`
6. Click "Apply"

---

## 3. Input Actions Configuration File

If you prefer to create the Input Actions asset manually, here's the JSON:

```json
{
    "name": "PlayerInputActions",
    "maps": [
        {
            "name": "Player",
            "id": "player-01",
            "actions": [
                {
                    "name": "Touch",
                    "type": "Button",
                    "id": "touch-01",
                    "expectedControlType": "Button",
                    "processors": "",
                    "interactions": "",
                    "initialStateCheck": false
                },
                {
                    "name": "TouchPosition",
                    "type": "Value",
                    "id": "touch-pos-01",
                    "expectedControlType": "Vector2",
                    "processors": "",
                    "interactions": "",
                    "initialStateCheck": true
                },
                {
                    "name": "Move",
                    "type": "Value",
                    "id": "move-01",
                    "expectedControlType": "Vector2",
                    "processors": "",
                    "interactions": "",
                    "initialStateCheck": true
                }
            ],
            "bindings": [
                {
                    "name": "",
                    "id": "touch-binding-01",
                    "path": "<Touchscreen>/primaryTouch/press",
                    "interactions": "",
                    "processors": "",
                    "groups": "Touch",
                    "action": "Touch",
                    "isComposite": false,
                    "isPartOfComposite": false
                },
                {
                    "name": "",
                    "id": "touch-pos-binding-01",
                    "path": "<Touchscreen>/primaryTouch/position",
                    "interactions": "",
                    "processors": "",
                    "groups": "Touch",
                    "action": "TouchPosition",
                    "isComposite": false,
                    "isPartOfComposite": false
                },
                {
                    "name": "",
                    "id": "move-gamepad-binding",
                    "path": "<Gamepad>/leftStick",
                    "interactions": "",
                    "processors": "",
                    "groups": "Gamepad",
                    "action": "Move",
                    "isComposite": false,
                    "isPartOfComposite": false
                },
                {
                    "name": "WASD",
                    "id": "move-keyboard-binding",
                    "path": "2DVector",
                    "interactions": "",
                    "processors": "",
                    "groups": "",
                    "action": "Move",
                    "isComposite": true,
                    "isPartOfComposite": false
                },
                {
                    "name": "up",
                    "id": "wasd-up",
                    "path": "<Keyboard>/w",
                    "interactions": "",
                    "processors": "",
                    "groups": "Keyboard",
                    "action": "Move",
                    "isComposite": false,
                    "isPartOfComposite": true
                },
                {
                    "name": "down",
                    "id": "wasd-down",
                    "path": "<Keyboard>/s",
                    "interactions": "",
                    "processors": "",
                    "groups": "Keyboard",
                    "action": "Move",
                    "isComposite": false,
                    "isPartOfComposite": true
                },
                {
                    "name": "left",
                    "id": "wasd-left",
                    "path": "<Keyboard>/a",
                    "interactions": "",
                    "processors": "",
                    "groups": "Keyboard",
                    "action": "Move",
                    "isComposite": false,
                    "isPartOfComposite": true
                },
                {
                    "name": "right",
                    "id": "wasd-right",
                    "path": "<Keyboard>/d",
                    "interactions": "",
                    "processors": "",
                    "groups": "Keyboard",
                    "action": "Move",
                    "isComposite": false,
                    "isPartOfComposite": true
                }
            ]
        }
    ],
    "controlSchemes": [
        {
            "name": "Touch",
            "bindingGroup": "Touch",
            "devices": [
                {
                    "devicePath": "<Touchscreen>",
                    "isOptional": false,
                    "isOR": false
                }
            ]
        },
        {
            "name": "Keyboard",
            "bindingGroup": "Keyboard",
            "devices": [
                {
                    "devicePath": "<Keyboard>",
                    "isOptional": false,
                    "isOR": false
                }
            ]
        },
        {
            "name": "Gamepad",
            "bindingGroup": "Gamepad",
            "devices": [
                {
                    "devicePath": "<Gamepad>",
                    "isOptional": false,
                    "isOR": false
                }
            ]
        }
    ]
}
```

**To use this JSON**:
1. Create new `.inputactions` file in `Assets/Settings/InputActions/`
2. Open in text editor
3. Paste JSON above
4. Save and return to Unity
5. Unity will import it automatically

---

## 4. Using Input Actions in Code

The `InputManager.cs` script (already created) uses the Input Actions like this:

```csharp
using UnityEngine.InputSystem;

// Initialize
private PlayerInputActions inputActions;

private void Awake()
{
    inputActions = new PlayerInputActions();
}

private void OnEnable()
{
    inputActions.Enable();

    // Subscribe to events
    inputActions.Player.Touch.started += OnTouchStarted;
    inputActions.Player.Touch.canceled += OnTouchEnded;
}

private void OnDisable()
{
    inputActions.Disable();

    // Unsubscribe
    inputActions.Player.Touch.started -= OnTouchStarted;
    inputActions.Player.Touch.canceled -= OnTouchEnded;
}

// Read values
Vector2 touchPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
```

---

## 5. Testing Input in Unity Editor

### Method 1: Simulate Touch with Mouse
1. Open `Input Debugger`: Window → Analysis → Input Debugger
2. Click "Options" → Enable "Simulate Touch with Mouse"
3. Now mouse clicks will be treated as touches

### Method 2: Use Keyboard/Gamepad
The "Move" action has keyboard bindings (WASD) for testing.

### Method 3: Remote Device Testing
1. Window → Analysis → Input Debugger
2. Click "Remote Devices"
3. Connect your iOS device
4. Enable "Remote Input" on device
5. Test with real touch input

---

## 6. Mobile Testing Setup

### iOS Specific
No additional setup needed - touch input works automatically on iOS.

### Enable Accelerometer (for Tilt Controls)
1. Edit → Project Settings → Player → iOS
2. Other Settings → Accelerometer Frequency: 60Hz
3. This enables `Input.acceleration` for tilt controls

---

## 7. Common Issues & Solutions

### Issue: Input Actions not generating C# class
**Solution**:
- Ensure "Generate C# Class" is checked
- Click "Apply" button
- If still not working, manually create the class (Unity will auto-generate on reimport)

### Issue: Touch not working on device
**Solution**:
- Verify Input System package is installed
- Check Project Settings → Player → Active Input Handling is set to "Input System Package (New)"
- Ensure `<Touchscreen>` device path is correct in bindings

### Issue: "PlayerInputActions" class not found
**Solution**:
- Regenerate C# class from Input Actions asset
- Ensure namespace matches: `using TreasureExcavator;`
- Reimport the Input Actions asset

### Issue: Input lag on mobile
**Solution**:
- Set Input System update mode to "Dynamic Update" or "Fixed Update"
- Edit → Project Settings → Input System Package → Update Mode

---

## 8. Advanced Configuration

### Processors
You can add processors to inputs for fine-tuning:
- **Invert**: Flip input direction
- **Scale**: Multiply input by a value
- **Normalize**: Ensure input is normalized (0-1 or -1 to 1)
- **Clamp**: Limit input range

Example (in Input Actions Editor):
1. Select binding
2. Click "+" next to Processors
3. Add "Scale" processor
4. Set Scale value to 0.5 (reduce sensitivity)

### Interactions
Add interactions for advanced input detection:
- **Hold**: Require input held for X seconds
- **Tap**: Detect quick tap/release
- **SlowTap**: Detect longer tap
- **MultiTap**: Detect double-tap, triple-tap

---

## 9. Debugging Input

### Enable Input Debugging
```csharp
#if UNITY_EDITOR
    UnityEngine.InputSystem.InputSystem.settings.updateMode =
        UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
#endif
```

### Log Input Values
```csharp
void Update()
{
    Vector2 touchPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
    Debug.Log($"Touch Position: {touchPos}");
}
```

### Use Input Debugger
Window → Analysis → Input Debugger
- Shows all connected devices
- Displays real-time input values
- Helps identify input issues

---

## 10. Checklist

Before moving to Phase 1 implementation:

- [ ] Input System package installed (v1.5.0+)
- [ ] Active Input Handling set to "Input System Package (New)"
- [ ] `PlayerInputActions.inputactions` asset created
- [ ] C# class generated (`PlayerInputActions.cs`)
- [ ] Touch and TouchPosition actions configured
- [ ] Move action configured (for testing)
- [ ] Input Actions asset saved
- [ ] Tested in Unity Editor with mouse simulation
- [ ] (Optional) Tested on physical device

---

**Next Step**: Follow `PHASE_1_IMPLEMENTATION_GUIDE.md` to integrate these Input Actions with the game systems.

---

*End of Input System Setup Guide*
