# Phase 1 Implementation Guide
## Foundation (3 Weeks) - Treasure Excavator

**Version**: 1.0
**Last Updated**: 2025-11-18
**Phase Duration**: 3 weeks (21 days)
**Goal**: Create playable prototype with core mechanics

---

## Overview

Phase 1 establishes the foundation of Treasure Excavator:
- ✅ Core vehicle movement with physics
- ✅ Touch/tilt input controls
- ✅ Camera following system
- ✅ Basic treasure collection
- ✅ Audio management
- ✅ Game state management
- ✅ Prototype level (blockout)

**End Result**: A playable prototype where you can drive a vehicle, collect treasures, pass through gates, and deposit for points.

---

## Prerequisites

Before starting Phase 1:
- [x] Unity 2022.3 LTS installed
- [x] URP project created (`TreasureExcavator`)
- [x] Input System package installed
- [x] TextMeshPro imported
- [x] All Phase 1 scripts copied to project
- [ ] Input Actions asset created (follow `INPUT_SYSTEM_SETUP.md`)

---

## Week 1: Core Systems (Days 1-7)

### Day 1-2: Project Setup & Input System

#### Step 1: Configure Project Settings
1. **Player Settings** (iOS):
   - Edit → Project Settings → Player → iOS
   - Bundle Identifier: `com.yourcompany.treasureexcavator`
   - Minimum iOS Version: 14.0
   - Target SDK: Latest
   - Architecture: ARM64
   - Accelerometer Frequency: 60Hz (for tilt controls)

2. **Physics Settings**:
   - Edit → Project Settings → Physics
   - Create new layer: "Vehicle" (Layer 6)
   - Create new layer: "Treasure" (Layer 7)
   - Create new layer: "Ground" (Layer 8)
   - Create new layer: "Gate" (Layer 9)
   - Create new layer: "DepositZone" (Layer 10)

3. **Tags**:
   - Create tag: "Vehicle"
   - Create tag: "Treasure"
   - Create tag: "Ground"
   - Create tag: "Gate"
   - Create tag: "DepositZone"

#### Step 2: Set Up Input System
Follow `INPUT_SYSTEM_SETUP.md` to create Input Actions asset.

**Verify**:
- [ ] `PlayerInputActions.inputactions` exists in `Assets/Settings/InputActions/`
- [ ] `PlayerInputActions.cs` generated
- [ ] Compiles without errors

---

### Day 3-4: Create Manager Singletons

#### Step 1: Create Empty GameObjects for Managers
1. Create new scene: `Assets/Scenes/Level_01.unity`
2. Save scene
3. Create empty GameObject: "Managers"
4. Under "Managers", create:
   - Empty GameObject: "GameManager" → Add `GameManager.cs`
   - Empty GameObject: "InputManager" → Add `InputManager.cs`
   - Empty GameObject: "AudioManager" → Add `AudioManager.cs`

#### Step 2: Configure AudioManager
1. Select "AudioManager"
2. Add two `AudioSource` components:
   - AudioSource 1: Rename to "MusicSource"
     - Loop: ON
     - Play On Awake: OFF
   - AudioSource 2: Rename to "SFXSource"
     - Loop: OFF
     - Play On Awake: OFF
3. In `AudioManager` component:
   - Music Source: Drag "MusicSource" AudioSource
   - SFX Source: Drag "SFXSource" AudioSource
4. SFX Pool Size: 10
5. Master Volume: 1.0
6. Music Volume: 0.7
7. SFX Volume: 1.0

**Note**: Audio clips will be assigned in Phase 2 when assets are acquired.

#### Step 3: Configure InputManager
1. Select "InputManager"
2. In `InputManager` component:
   - Control Mode: Touch
   - Tilt Sensitivity: 2.0
   - Touch Sensitivity: 0.01
   - Touch Drag Threshold: 10

**Verify**:
- [ ] All managers have `DontDestroyOnLoad` set (check in code)
- [ ] No errors in Console
- [ ] Play mode: Managers persist across scene loads

---

### Day 5-7: Vehicle & Camera Setup

#### Step 1: Create Prototype Vehicle
Since we don't have 3D models yet, create a placeholder:

1. Create **Cube** (GameObject → 3D Object → Cube)
2. Rename to "VehiclePrototype"
3. Transform:
   - Position: (0, 1, 0)
   - Scale: (2, 1, 3) (car-shaped)
4. Add **Rigidbody** component:
   - Mass: 1000
   - Drag: 0.5
   - Angular Drag: 2.0
   - Interpolate: Interpolate
   - Collision Detection: Continuous
5. Add **VehicleController.cs** script
6. Add **CargoManager.cs** script
7. Set Tag: "Vehicle"
8. Set Layer: "Vehicle"

#### Step 2: Configure VehicleController
Select "VehiclePrototype":
- Speed: 8
- Acceleration: 5
- Turning Speed: 180
- Drag: 0.5
- Angular Drag: 2.0
- Ground Layer: "Ground" (Layer Mask)
- Ground Check Distance: 0.5

#### Step 3: Create Ground Check Point
1. Under "VehiclePrototype", create Empty GameObject: "GroundCheckPoint"
2. Position: (0, -0.6, 0) (below vehicle)
3. Drag to VehicleController → Ground Check Point field

#### Step 4: Configure CargoManager
Select "VehiclePrototype":
- Max Capacity: 10
- Collection Radius: 1.5

#### Step 5: Create Ground Plane
1. Create **Plane** (GameObject → 3D Object → Plane)
2. Rename to "Ground"
3. Transform:
   - Position: (0, 0, 0)
   - Scale: (10, 1, 10) (100x100m area)
4. Set Tag: "Ground"
5. Set Layer: "Ground"
6. Add **Box Collider** (if not present)

#### Step 6: Create Camera System
1. Select Main Camera
2. Add **CameraController.cs** script
3. Configure:
   - Target: Drag "VehiclePrototype"
   - Offset: (0, 8, -10) (above and behind vehicle)
   - Follow Speed: 5
   - Rotation Speed: 3
   - Look At Target: ON

**Verify**:
- [ ] Play mode: Vehicle exists in scene
- [ ] Vehicle has physics (Rigidbody)
- [ ] Ground check ray visible in Scene view (select vehicle)
- [ ] Camera follows vehicle smoothly

---

## Week 2: Gameplay Systems (Days 8-14)

### Day 8-9: Treasure Collection

#### Step 1: Create Treasure Prototype
1. Create **Sphere** (GameObject → 3D Object → Sphere)
2. Rename to "TreasureSmall"
3. Transform:
   - Position: (5, 1, 0)
   - Scale: (0.5, 0.5, 0.5)
4. Add **Treasure.cs** script
5. Configure:
   - Type: Small
   - Value: 10
   - Float Height: 0.3
   - Float Speed: 2
   - Rotation Speed: 50
6. Add **Sphere Collider**:
   - Is Trigger: ON
   - Radius: 0.5
7. Set Tag: "Treasure"
8. Set Layer: "Treasure"

#### Step 2: Create Treasure Material (Optional)
1. Create Material: `Assets/Materials/TreasureGold.mat`
2. Shader: URP/Lit
3. Base Color: Golden yellow
4. Metallic: 0.8
5. Smoothness: 0.9
6. Assign to TreasureSmall

#### Step 3: Create Treasure Prefab
1. Drag "TreasureSmall" from Hierarchy to `Assets/Prefabs/Treasures/`
2. Delete from scene
3. Drag prefab back to scene 3-5 times, spread around ground

**Verify**:
- [ ] Play mode: Treasures float and rotate
- [ ] Drive near treasure: Auto-collects
- [ ] Console shows: "Treasure collected! Cargo: X/10"
- [ ] Cargo counter increases

---

### Day 10-11: Multiplier Gates

#### Step 1: Create Gate Prototype
1. Create Empty GameObject: "GateBronze"
2. Under it, create **Cube** (child): "GateArch"
3. Transform GateArch:
   - Position: (0, 2, 0)
   - Scale: (4, 4, 0.5) (archway shape)
4. Add **Box Collider** to GateBronze (not child):
   - Center: (0, 2, 0)
   - Size: (4, 4, 0.5)
   - Is Trigger: ON
5. Add **MultiplierGate.cs** to GateBronze
6. Configure:
   - Multiplier: 2
   - Gate Type: Bronze
   - Single Use: OFF
7. Set Tag: "Gate"
8. Set Layer: "Gate"

#### Step 2: Create Gate Material
1. Create Material: `Assets/Materials/GateBronze.mat`
2. Base Color: Bronze/brown (#CD7F32)
3. Assign to GateArch

#### Step 3: Duplicate for Other Gates
1. Duplicate GateBronze → "GateSilver"
   - Gate Type: Silver
   - Material: Silver color (#C0C0C0)
2. Duplicate GateBronze → "GateGold"
   - Gate Type: Gold
   - Material: Gold color (#FFD700)

#### Step 4: Create Gate Prefabs
Drag each gate to `Assets/Prefabs/Gates/`

#### Step 5: Place in Scene
1. Position gates around the level:
   - GateBronze at (10, 0, 0)
   - GateSilver at (0, 0, 10)
   - GateGold at (-10, 0, 0)

**Verify**:
- [ ] Play mode: Collect treasure, drive through gate
- [ ] Console: "Gate activated! x2 multiplier applied"
- [ ] Cargo value multiplies correctly

---

### Day 12-14: Deposit Zone & Scoring

#### Step 1: Create Deposit Zone
1. Create **Cylinder** (GameObject → 3D Object → Cylinder)
2. Rename to "DepositZone"
3. Transform:
   - Position: (0, 0, 15)
   - Scale: (10, 0.1, 10) (large circle)
4. Add **DepositZone.cs** script
5. Add **Box Collider**:
   - Center: (0, 1, 0)
   - Size: (10, 2, 10)
   - Is Trigger: ON
6. Set Tag: "DepositZone"
7. Set Layer: "DepositZone"

#### Step 2: Create Deposit Material
1. Create Material: `Assets/Materials/DepositZoneGlow.mat`
2. Shader: URP/Lit
3. Base Color: Bright yellow (#FFFF00)
4. Emission: ON, yellow glow
5. Assign to DepositZone

#### Step 3: Add Light (Optional)
1. Under DepositZone, create **Point Light**: "DepositLight"
2. Transform: Position (0, 2, 0)
3. Color: Yellow
4. Intensity: 2
5. Range: 15
6. Assign to DepositZone script → Glow Light

**Verify**:
- [ ] Play mode: Collect treasures, go through gate, enter deposit zone
- [ ] Console: "Deposited cargo for X points!"
- [ ] Score increases in GameManager
- [ ] Cargo resets to 0

---

## Week 3: UI & Polish (Days 15-21)

### Day 15-17: Create In-Game HUD

#### Step 1: Create Canvas
1. GameObject → UI → Canvas
2. Rename to "GameHUD"
3. Canvas settings:
   - Render Mode: Screen Space - Overlay
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1242 x 2688 (iPhone 13 Pro Max)
   - Match: 0.5 (balanced)

#### Step 2: Create Score Display
1. Under GameHUD, create **Panel**: "ScorePanel"
2. RectTransform:
   - Anchor: Top-Left
   - Pivot: (0, 1)
   - Position: (20, -20, 0)
   - Width: 300, Height: 100
3. Background: Semi-transparent black
4. Create Text (TMP): "ScoreText"
   - Text: "Score: 0"
   - Font Size: 36
   - Color: White

#### Step 3: Create Cargo Display
1. Under GameHUD, create **Panel**: "CargoPanel"
2. RectTransform:
   - Anchor: Bottom-Left
   - Pivot: (0, 0)
   - Position: (20, 20, 0)
   - Width: 250, Height: 80
3. Create Text (TMP): "CargoText"
   - Text: "Cargo: 0/10"
   - Font Size: 32

#### Step 4: Create Pause Button
1. Under GameHUD, create **Button** (TMP): "PauseButton"
2. RectTransform:
   - Anchor: Top-Right
   - Pivot: (1, 1)
   - Position: (-20, -20, 0)
   - Width: 80, Height: 80
3. Text: "||" (pause symbol)

#### Step 5: Add GameHUD Script
1. Select GameHUD Canvas
2. Add **GameHUD.cs** script
3. Assign references:
   - Score Text: Drag ScoreText
   - Cargo Text: Drag CargoText
   - Pause Button: Drag PauseButton

**Verify**:
- [ ] Play mode: HUD displays correctly
- [ ] Score updates when depositing cargo
- [ ] Cargo updates when collecting treasures
- [ ] Pause button visible (functionality in Phase 2)

---

### Day 18-19: Level Configuration

#### Step 1: Configure GameManager for Level 1
1. Select "GameManager" object
2. In Inspector:
   - Current State: Playing
   - Target Score: 100
   - Two Star Score: 150
   - Three Star Score: 200
   - Current Level: 1

#### Step 2: Test Full Gameplay Loop
**Test Scenario**:
1. Play mode
2. Drive vehicle around (WASD or mouse in Editor)
3. Collect 3-5 treasures
4. Drive through x2 gate
5. Enter deposit zone
6. Verify: Score increases, cargo resets
7. Repeat until score >= 100
8. Console should show: "Level 1 Complete!"

**Expected Results**:
- ✅ Vehicle controls smoothly
- ✅ Camera follows vehicle
- ✅ Treasures collected on contact
- ✅ Gates multiply cargo value
- ✅ Deposit zone adds score
- ✅ HUD updates in real-time
- ✅ Level completes at target score

---

### Day 20-21: Build & Test

#### Step 1: Create Build Settings
1. File → Build Settings
2. Add Open Scenes: Drag "Level_01" to list
3. Platform: iOS
4. Switch Platform (if not already)

#### Step 2: Build to Xcode (macOS only)
1. Click "Build"
2. Choose output folder: `Builds/iOS/`
3. Wait for build (5-10 minutes first time)
4. Open generated Xcode project
5. Select your iOS device or simulator
6. Click Run

#### Step 3: Test on Device
1. Install on iPhone/iPad
2. Test touch controls:
   - Tap and drag to steer
3. Test tilt controls:
   - In InputManager, set Control Mode: Tilt
   - Rebuild and test

**Known Issues**:
- Touch controls may feel sensitive → Adjust Touch Sensitivity in InputManager
- Tilt controls too sensitive → Adjust Tilt Sensitivity
- Vehicle too fast/slow → Adjust Speed in VehicleController

---

## Phase 1 Deliverables Checklist

### Core Systems ✅
- [x] VehicleController.cs (movement & physics)
- [x] InputManager.cs (touch & tilt controls)
- [x] CameraController.cs (smooth follow)
- [x] AudioManager.cs (SFX & music management)
- [x] GameManager.cs (state & score)

### Gameplay Systems ✅
- [x] Treasure.cs (collectible items)
- [x] CargoManager.cs (vehicle storage)
- [x] MultiplierGate.cs (x2, x3, x5 gates)
- [x] DepositZone.cs (score submission)

### UI Systems ✅
- [x] GameHUD.cs (score, cargo display)

### Scene Setup ✅
- [ ] Level_01.unity with prototype objects
- [ ] Ground plane
- [ ] Vehicle prototype
- [ ] 3-5 treasure prototypes
- [ ] 2-3 gate prototypes
- [ ] Deposit zone
- [ ] HUD canvas

### Testing ✅
- [ ] Playable in Unity Editor
- [ ] Builds to iOS successfully
- [ ] Touch controls work on device
- [ ] Core gameplay loop functional

---

## Common Issues & Solutions

### Issue: Vehicle falls through ground
**Solution**:
- Ensure Ground has Collider
- Set Ground layer correctly
- Check Rigidbody Collision Detection: Continuous

### Issue: Treasures not collecting
**Solution**:
- Verify CargoManager has SphereCollider (trigger)
- Check Treasure has Collider (trigger)
- Ensure layers/tags set correctly
- Check Collection Radius in CargoManager

### Issue: Gates not activating
**Solution**:
- Ensure Gate has BoxCollider (trigger)
- Verify Vehicle has tag "Vehicle"
- Check CargoManager is attached to vehicle
- Must have cargo > 0 to activate

### Issue: Input not responding
**Solution**:
- Verify InputManager.Instance is not null
- Check PlayerInputActions.cs generated
- Enable Input Debugger: Window → Analysis → Input Debugger
- Test with keyboard first (WASD)

### Issue: Camera not following
**Solution**:
- Assign Target in CameraController
- Check Follow Speed > 0
- Ensure LateUpdate() is being called

---

## Performance Optimization (Optional)

### Mobile Performance Tips
1. **Reduce Draw Calls**:
   - Combine meshes where possible
   - Use sprite atlases for UI

2. **Optimize Physics**:
   - Fixed Timestep: 0.02 (50 FPS)
   - Max Allowed Timestep: 0.1

3. **Reduce Particle Count**:
   - Limit particle systems to 100-200 particles max

4. **Texture Compression**:
   - Use ASTC compression for iOS
   - Max texture size: 2048x2048

---

## Next Steps: Phase 2

Once Phase 1 is complete:
1. **Acquire Assets**: Purchase 3D models, audio (see `ASSET_ACQUISITION_PLAN.md`)
2. **Replace Prototypes**: Swap cubes/spheres with real models
3. **Add Audio**: Import SFX and music, assign to AudioManager
4. **Create More Levels**: Design levels 2-8 using blockouts
5. **Implement Tutorial**: Follow `TUTORIAL_SCRIPT.md`
6. **Add VFX**: Particle effects for collection, gates, deposit

---

## Debug Commands (For Testing)

Add these to GameManager for quick testing:

```csharp
void Update()
{
    #if UNITY_EDITOR
    // Press '1' to add 100 score
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        AddScore(100);
    }

    // Press '2' to complete level
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        SetGameState(GameState.LevelComplete);
    }

    // Press '3' to add 1000 gold
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        AddGold(1000);
    }
    #endif
}
```

---

## Phase 1 Success Criteria

**You've completed Phase 1 when**:
- ✅ You can drive a vehicle around a level
- ✅ You can collect treasures (cargo increases)
- ✅ You can drive through gates (cargo multiplies)
- ✅ You can deposit cargo (score increases)
- ✅ Level completes when target score reached
- ✅ Game builds and runs on iOS device
- ✅ All core systems work without errors

**Estimated Playtime**: 30-60 seconds per level attempt

---

## Congratulations! 🎉

You've built the foundation of Treasure Excavator! The core gameplay loop is working:

**Collect → Multiply → Deposit → Score!**

You're now ready to proceed to **Phase 2: Core Mechanics** where you'll:
- Add vehicle abilities (Nitro Boost, Auto-Collect)
- Create 8 complete levels
- Implement the tutorial system
- Add Firebase analytics
- Polish gameplay feel

---

*End of Phase 1 Implementation Guide*

**Next Guide**: `PHASE_2_IMPLEMENTATION_GUIDE.md` (coming soon)
