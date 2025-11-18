# Scripts Directory
## Treasure Excavator - Code Organization

**Last Updated**: 2025-11-18
**Total Scripts**: 16 C# files (Phase 1 Foundation)

---

## Overview

This directory contains all C# code for Treasure Excavator, organized by functionality.

---

## Directory Structure

```
Scripts/
├── Managers/         # Singleton managers (5 scripts)
├── Vehicles/         # Vehicle-specific logic (1 script)
├── Gameplay/         # Core gameplay systems (3 scripts)
├── UI/              # User interface controllers (2 scripts)
├── Data/            # ScriptableObject definitions (2 scripts)
├── Utilities/        # Helper classes (3 scripts)
└── README.md        # This file
```

---

## Managers/ (5 Scripts)

### Purpose
Singleton managers that persist across scenes and manage global game state.

### Scripts:

#### 1. **GameManager.cs** (380 lines)
- **Purpose**: Main game state machine and flow controller
- **Responsibilities**:
  - Game state management (Menu, Playing, Paused, Complete)
  - Score tracking and level targets
  - Gold currency management
  - Level completion detection
  - Save/load progress
- **Usage**: `GameManager.Instance.AddScore(100);`

#### 2. **AudioManager.cs** (250 lines)
- **Purpose**: Centralized audio playback system
- **Responsibilities**:
  - SFX pooling (10 concurrent sounds)
  - Music crossfade
  - Volume controls (Master, Music, SFX)
  - Settings persistence
- **Usage**: `AudioManager.Instance.PlayCollectSmall();`

#### 3. **InputManager.cs** (150 lines)
- **Purpose**: Handle touch and tilt controls for mobile
- **Responsibilities**:
  - Touch drag input
  - Tilt/accelerometer input
  - Control mode switching
  - Settings persistence
- **Usage**: `Vector2 input = InputManager.Instance.GetMoveInput();`

#### 4. **CameraController.cs** (120 lines)
- **Purpose**: Smooth camera follow system
- **Responsibilities**:
  - Follow target vehicle with offset
  - Smooth damping
  - Camera shake effects
  - Optional bounds limiting
- **Usage**: Attach to Main Camera, assign target vehicle

#### 5. **LevelManager.cs** (220 lines)
- **Purpose**: Level data management and spawning
- **Responsibilities**:
  - Load level configurations
  - Spawn treasures and gates
  - Manage level lifecycle
  - Clear levels
- **Usage**: `LevelManager.Instance.LoadLevel(1);`

---

## Vehicles/ (1 Script)

### Purpose
Vehicle-specific movement and physics logic.

### Scripts:

#### 1. **VehicleController.cs** (180 lines)
- **Purpose**: Physics-based vehicle movement
- **Responsibilities**:
  - Rigidbody-based movement
  - Touch/tilt input integration
  - Ground detection
  - Audio (engine pitch varies with speed)
  - Particle effects
- **Usage**: Attach to vehicle GameObject with Rigidbody

---

## Gameplay/ (3 Scripts)

### Purpose
Core gameplay mechanics (collection, gates, cargo).

### Scripts:

#### 1. **Treasure.cs** (160 lines)
- **Purpose**: Individual treasure item behavior
- **Responsibilities**:
  - Floating and rotating animation
  - Collection detection (trigger)
  - Collection animation (fly to vehicle)
  - Value-based scoring (10, 50, 200)
- **Usage**: Attach to treasure prefabs

#### 2. **CargoManager.cs** (170 lines)
- **Purpose**: Vehicle cargo storage system
- **Responsibilities**:
  - Capacity management (default 10)
  - Collection radius (SphereCollider trigger)
  - Deposit calculation
  - Multiplier application
  - UnityEvents for UI updates
- **Usage**: Attach to vehicle GameObject

#### 3. **MultiplierGate.cs** (180 lines)
- **Purpose**: Score multiplier gates
- **Responsibilities**:
  - x2, x3, x5, x10 multiplier types
  - Activate only with cargo
  - Single-use or reusable modes
  - Particle and audio effects
  - Camera shake trigger
- **Usage**: Attach to gate prefabs

---

## UI/ (2 Scripts)

### Purpose
User interface controllers and HUD.

### Scripts:

#### 1. **GameHUD.cs** (150 lines)
- **Purpose**: In-game heads-up display
- **Responsibilities**:
  - Score display (updates in real-time)
  - Cargo counter with progress bar
  - Star rating display (1-3 stars)
  - Target score reminder (10 seconds)
  - Pause button
- **Usage**: Attach to Canvas GameObject

#### 2. **DepositZone.cs** (120 lines)
- **Purpose**: Score deposit zone
- **Responsibilities**:
  - Cargo deposit detection
  - Score calculation
  - Particle burst effects
  - Pulsing glow animation
  - Audio feedback
- **Usage**: Attach to deposit zone GameObject

---

## Data/ (2 Scripts)

### Purpose
ScriptableObject data structures for configuration.

### Scripts:

#### 1. **LevelData.cs** (200 lines)
- **Purpose**: Level configuration data
- **Properties**:
  - Level number, name, description
  - Score requirements (1/2/3 stars)
  - Treasure counts (small/medium/large)
  - Gate counts (bronze/silver/gold/platinum)
  - Level size, scene name
  - Gold rewards
- **Usage**:
  ```csharp
  // Create asset: Right-click → Create → TreasureExcavator → Level Data
  LevelData level = levelManager.GetLevelData(1);
  int targetScore = level.OneStarScore;
  ```

#### 2. **VehicleData.cs** (230 lines)
- **Purpose**: Vehicle configuration data
- **Properties**:
  - Name, description, icon
  - Stats (speed, acceleration, capacity, radius)
  - Special ability type
  - Unlock requirements
  - Visual data (prefab, colors)
  - Audio data
- **Usage**:
  ```csharp
  // Create asset: Right-click → Create → TreasureExcavator → Vehicle Data
  VehicleData vehicle = vehicleDatabase.GetVehicle("Bulldozer");
  bool unlocked = vehicle.IsUnlocked();
  ```

---

## Utilities/ (3 Scripts)

### Purpose
Reusable helper classes and patterns.

### Scripts:

#### 1. **GameUtilities.cs** (120 lines)
- **Purpose**: Static utility functions
- **Methods**:
  - `CalculateStarRating()` - Score to stars conversion
  - `CalculateGoldReward()` - Reward calculation
  - `FormatNumber()` - 1000 → "1K"
  - `FormatTime()` - Seconds → "MM:SS"
  - `RandomPointInCircle()` - Spawn helpers
- **Usage**: `string formatted = GameUtilities.FormatNumber(1500); // "1.5K"`

#### 2. **ObjectPooler.cs** (140 lines)
- **Purpose**: Object pooling for performance
- **Responsibilities**:
  - Pre-instantiate objects
  - Reuse instead of Instantiate/Destroy
  - Reduce garbage collection
- **Usage**:
  ```csharp
  // Spawn from pool
  GameObject obj = ObjectPooler.Instance.SpawnFromPool("Treasure", pos, rot);

  // Return to pool
  ObjectPooler.Instance.ReturnToPool("Treasure", obj);
  ```

#### 3. **Singleton.cs** (80 lines)
- **Purpose**: Generic singleton pattern base class
- **Usage**:
  ```csharp
  // Inherit from Singleton<T>
  public class MyManager : Singleton<MyManager>
  {
      // Automatically becomes singleton
  }

  // Access
  MyManager.Instance.DoSomething();
  ```

---

## Code Style Guidelines

### Naming Conventions
- **Classes**: PascalCase (`GameManager`, `VehicleController`)
- **Methods**: PascalCase (`GetScore()`, `AddTreasure()`)
- **Private fields**: camelCase (`currentScore`, `isGrounded`)
- **Serialized fields**: camelCase with `[SerializeField]`
- **Public properties**: PascalCase (`CurrentScore`, `IsGrounded`)

### Comments
- XML documentation for public methods:
  ```csharp
  /// <summary>
  /// Add score to the current total.
  /// </summary>
  /// <param name="amount">Amount to add</param>
  public void AddScore(int amount)
  ```

### Regions (Optional)
Use regions to organize large scripts:
```csharp
#region Score Management
// Score-related methods here
#endregion

#region Save/Load
// Persistence methods here
#endregion
```

---

## Dependencies

### Unity Packages Required
- **Universal RP** (built-in with template)
- **Input System** (1.5.0+)
- **TextMeshPro** (3.0+)
- **Cinemachine** (2.9.0+, optional)

### External Dependencies
None for Phase 1 Foundation.

Phase 2+ will add:
- Firebase SDK (Analytics, Crashlytics)
- Unity Ads SDK
- Unity IAP

---

## Build Order / Compilation Dependencies

```
Utilities → Data → Managers → Vehicles/Gameplay → UI
```

**Explanation**:
- Utilities can be used by all other scripts
- Data (ScriptableObjects) are referenced by Managers
- Managers are singletons used by gameplay scripts
- UI scripts use Managers for data display

**No circular dependencies!**

---

## Testing

### Unit Tests (Future)
Place in `Assets/Tests/` when implementing in Phase 3.

### Manual Testing
Each script has debug visualization:
- **Gizmos**: Select object in Scene view to see debug info
- **Debug.Log**: Check Console for state changes
- **OnDrawGizmosSelected()**: Visual debugging for positions, radii, etc.

---

## Performance Considerations

### Mobile Optimization
- **Update() calls**: Minimized, only when needed
- **GetComponent()**: Cached in Awake/Start
- **Physics**: Uses layers for collision filtering
- **Object pooling**: Use ObjectPooler for frequent spawns

### Memory Management
- **Singleton pattern**: Prevents duplicate managers
- **DontDestroyOnLoad**: Managers persist (no reload cost)
- **ScriptableObjects**: Data stored in assets, not instances

---

## Common Patterns

### 1. Singleton Access
```csharp
if (GameManager.Instance != null)
{
    GameManager.Instance.AddScore(10);
}
```

### 2. UnityEvents
```csharp
// In script
public UnityEvent<int> onScoreChanged;

// Invoke
onScoreChanged?.Invoke(newScore);

// Subscribe
gameManager.onScoreChanged.AddListener(UpdateScoreUI);
```

### 3. ScriptableObject Data
```csharp
[SerializeField] private LevelData levelData;

void Start()
{
    int target = levelData.OneStarScore;
}
```

---

## Future Additions (Phase 2+)

### Planned Scripts
- **TutorialManager.cs** - Tutorial system
- **FirebaseManager.cs** - Analytics integration
- **AdsManager.cs** - Unity Ads integration
- **IAPManager.cs** - In-app purchases
- **MainMenuUI.cs** - Main menu controller
- **LevelSelectUI.cs** - Level selection UI
- **PauseMenuUI.cs** - Pause menu controller
- **VehicleSelector.cs** - Vehicle selection UI
- **ShopUI.cs** - Cosmetics shop
- **SettingsUI.cs** - Settings menu

---

## Troubleshooting

### Common Errors

**"Instance is null"**
- Ensure manager GameObject exists in scene
- Check Awake() method runs before access
- Verify DontDestroyOnLoad is set

**"Missing component"**
- Check required components in Inspector
- Add required components via [RequireComponent]
- Verify references are assigned

**"Trigger not working"**
- Ensure collider has "Is Trigger" checked
- Verify layers in Physics collision matrix
- Check tags are assigned correctly

---

## Contact & Support

For questions about the code:
1. Read inline XML documentation
2. Check this README
3. Review PHASE_1_IMPLEMENTATION_GUIDE.md
4. Consult Unity documentation

---

**Last Updated**: 2025-11-18
**Phase**: 1 - Foundation Complete
**Status**: Production-Ready
