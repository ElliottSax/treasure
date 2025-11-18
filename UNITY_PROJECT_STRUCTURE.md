# Unity Project Structure Guide
## Treasure Excavator - Complete Folder Organization

**Version**: 1.0
**Last Updated**: 2025-11-18
**Unity Version**: 2022.3 LTS

---

## Complete Assets Folder Structure

Once you create the Unity project, organize it exactly like this:

```
TreasureExcavator/
├── Assets/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── LevelSelect.unity
│   │   ├── Level_01.unity
│   │   ├── Level_02.unity
│   │   ├── Level_03.unity
│   │   ├── Level_04.unity
│   │   ├── Level_05.unity
│   │   ├── Level_06.unity
│   │   ├── Level_07.unity
│   │   └── Level_08.unity
│   │
│   ├── Scripts/
│   │   ├── Managers/
│   │   │   ├── GameManager.cs
│   │   │   ├── AudioManager.cs
│   │   │   ├── InputManager.cs
│   │   │   ├── CameraController.cs
│   │   │   └── LevelManager.cs
│   │   ├── Vehicles/
│   │   │   └── VehicleController.cs
│   │   ├── Gameplay/
│   │   │   ├── Treasure.cs
│   │   │   ├── CargoManager.cs
│   │   │   └── MultiplierGate.cs
│   │   ├── UI/
│   │   │   ├── GameHUD.cs
│   │   │   ├── DepositZone.cs
│   │   │   ├── MainMenu.cs (create in Phase 2)
│   │   │   ├── LevelSelectUI.cs (create in Phase 2)
│   │   │   └── PauseMenu.cs (create in Phase 2)
│   │   ├── Data/
│   │   │   ├── LevelData.cs
│   │   │   └── VehicleData.cs
│   │   ├── Utilities/
│   │   │   ├── GameUtilities.cs
│   │   │   ├── ObjectPooler.cs
│   │   │   └── Singleton.cs
│   │   └── PlayerInputActions.cs (auto-generated)
│   │
│   ├── Prefabs/
│   │   ├── Vehicles/
│   │   │   ├── Bulldozer.prefab
│   │   │   ├── DualScoopLoader.prefab
│   │   │   ├── NitroHauler.prefab
│   │   │   └── MegaVaultTruck.prefab (post-launch)
│   │   ├── Treasures/
│   │   │   ├── TreasureSmall.prefab
│   │   │   ├── TreasureMedium.prefab
│   │   │   └── TreasureLarge.prefab
│   │   ├── Gates/
│   │   │   ├── GateBronze.prefab
│   │   │   ├── GateSilver.prefab
│   │   │   ├── GateGold.prefab
│   │   │   └── GatePlatinum.prefab
│   │   ├── Environment/
│   │   │   ├── Ground.prefab
│   │   │   ├── DepositZone.prefab
│   │   │   └── Obstacles/ (rocks, barriers)
│   │   ├── UI/
│   │   │   ├── GameHUD.prefab
│   │   │   ├── MainMenu.prefab
│   │   │   ├── LevelSelectUI.prefab
│   │   │   └── PauseMenu.prefab
│   │   └── Managers/
│   │       ├── GameManager.prefab
│   │       ├── AudioManager.prefab
│   │       ├── InputManager.prefab
│   │       └── LevelManager.prefab
│   │
│   ├── Materials/
│   │   ├── Vehicles/
│   │   │   ├── VehicleBodyRed.mat
│   │   │   ├── VehicleBodyBlue.mat
│   │   │   └── VehicleBodyYellow.mat
│   │   ├── Environment/
│   │   │   ├── Ground.mat
│   │   │   ├── Rock.mat
│   │   │   └── Cave.mat
│   │   ├── Effects/
│   │   │   ├── TreasureGold.mat
│   │   │   ├── GateBronze.mat
│   │   │   ├── GateSilver.mat
│   │   │   ├── GateGold.mat
│   │   │   ├── GatePlatinum.mat
│   │   │   └── DepositZoneGlow.mat
│   │   └── UI/
│   │       └── UIBackground.mat
│   │
│   ├── Textures/
│   │   ├── UI/
│   │   │   ├── Icons/
│   │   │   │   ├── icon_settings.png
│   │   │   │   ├── icon_pause.png
│   │   │   │   ├── icon_play.png
│   │   │   │   ├── icon_star.png
│   │   │   │   ├── icon_star_filled.png
│   │   │   │   ├── icon_gold.png
│   │   │   │   └── icon_gem.png
│   │   │   ├── Buttons/
│   │   │   │   ├── button_normal.png
│   │   │   │   ├── button_pressed.png
│   │   │   │   └── button_disabled.png
│   │   │   └── Panels/
│   │   │       └── panel_background.png
│   │   └── Environment/
│   │       ├── ground_texture.png
│   │       └── rock_texture.png
│   │
│   ├── Models/
│   │   ├── Vehicles/
│   │   │   ├── Bulldozer.fbx
│   │   │   ├── DualScoopLoader.fbx
│   │   │   ├── NitroHauler.fbx
│   │   │   └── MegaVaultTruck.fbx
│   │   ├── Environment/
│   │   │   ├── Cave_Wall_01.fbx
│   │   │   ├── Cave_Floor_01.fbx
│   │   │   ├── Rock_Small.fbx
│   │   │   ├── Rock_Medium.fbx
│   │   │   └── Rock_Large.fbx
│   │   └── Treasures/
│   │       ├── Coin.fbx
│   │       ├── Gem.fbx
│   │       └── Chest.fbx
│   │
│   ├── Audio/
│   │   ├── SFX/
│   │   │   ├── Gameplay/
│   │   │   │   ├── collect_small.wav
│   │   │   │   ├── collect_medium.wav
│   │   │   │   ├── collect_large.wav
│   │   │   │   ├── gate_bronze.wav
│   │   │   │   ├── gate_silver.wav
│   │   │   │   ├── gate_gold.wav
│   │   │   │   ├── gate_platinum.wav
│   │   │   │   └── deposit.wav
│   │   │   ├── UI/
│   │   │   │   ├── button_click.wav
│   │   │   │   ├── menu_open.wav
│   │   │   │   ├── level_complete.wav
│   │   │   │   └── star_earned.wav
│   │   │   └── Vehicle/
│   │   │       ├── engine_loop.wav
│   │   │       ├── engine_rev.wav
│   │   │       ├── nitro_boost.wav
│   │   │       └── collision.wav
│   │   └── Music/
│   │       ├── main_menu.ogg
│   │       ├── gameplay.ogg
│   │       └── victory.ogg
│   │
│   ├── Animations/
│   │   ├── UI/
│   │   │   ├── StarAppear.anim
│   │   │   └── ButtonPress.anim
│   │   └── Vehicles/
│   │       └── (vehicle animations if needed)
│   │
│   ├── Fonts/
│   │   ├── FredokaOne-Regular.ttf
│   │   └── Roboto-Regular.ttf
│   │
│   ├── Shaders/
│   │   └── (custom URP shaders if needed)
│   │
│   ├── Settings/
│   │   ├── URP/
│   │   │   ├── TreasureExcavator_URPAsset.asset
│   │   │   └── ForwardRenderer.asset
│   │   ├── InputActions/
│   │   │   └── PlayerInputActions.inputactions
│   │   ├── LevelData/
│   │   │   ├── Level01_Data.asset
│   │   │   ├── Level02_Data.asset
│   │   │   ├── Level03_Data.asset
│   │   │   ├── Level04_Data.asset
│   │   │   ├── Level05_Data.asset
│   │   │   ├── Level06_Data.asset
│   │   │   ├── Level07_Data.asset
│   │   │   └── Level08_Data.asset
│   │   └── VehicleData/
│   │       ├── Bulldozer_Data.asset
│   │       ├── DualScoop_Data.asset
│   │       ├── NitroHauler_Data.asset
│   │       └── MegaVault_Data.asset
│   │
│   ├── Resources/
│   │   └── (runtime-loaded assets)
│   │
│   └── StreamingAssets/
│       └── (platform-specific data)
│
├── Packages/
│   ├── manifest.json
│   └── packages-lock.json
│
├── ProjectSettings/
│   ├── ProjectSettings.asset
│   ├── Physics.asset
│   ├── InputManager.asset
│   └── ...
│
├── UserSettings/
│   └── (user-specific settings, gitignored)
│
└── .gitignore
```

---

## Folder-by-Folder Guide

### 1. Scenes/
**Purpose**: Store all Unity scene files

**Organization**:
- **MainMenu.unity**: First scene, main menu UI
- **LevelSelect.unity**: Level selection UI (optional, can be part of MainMenu)
- **Level_XX.unity**: Gameplay scenes for each of 8 levels

**Naming Convention**: Use numbered format (Level_01, Level_02) for easy sorting

**Build Settings**:
- Add all scenes to Build Settings in order:
  1. MainMenu (index 0)
  2. Level_01 (index 1)
  3. Level_02 (index 2)
  4. ... etc.

---

### 2. Scripts/
**Purpose**: All C# code (already created in this repository)

**Subfolders**:
- **Managers/**: Singleton managers (GameManager, AudioManager, etc.)
- **Vehicles/**: Vehicle-specific scripts
- **Gameplay/**: Core gameplay systems (Treasure, Gates, Cargo)
- **UI/**: UI controllers and menus
- **Data/**: ScriptableObject definitions
- **Utilities/**: Reusable helper classes

**Assembly Definitions** (Optional but recommended):
Create `TreasureExcavator.asmdef` in Scripts/ root for faster compile times

---

### 3. Prefabs/
**Purpose**: Reusable game objects

**Best Practices**:
- Every frequently-used object should be a prefab
- Use Prefab Variants for variations (e.g., different colored vehicles)
- Always work on prefabs, not scene instances
- Organize by type (Vehicles, Treasures, Gates, etc.)

**Prefab Workflow**:
1. Create object in scene
2. Configure fully
3. Drag to Prefabs/ folder
4. Delete scene instance
5. Add prefab to scene from folder

---

### 4. Materials/
**Purpose**: URP materials for rendering

**Material Setup**:
- Shader: Universal Render Pipeline/Lit (default)
- For glowing effects: URP/Lit with Emission enabled
- For UI: UI/Default

**Organization by Type**:
- Vehicles: Different colors/skins
- Environment: Ground, rocks, cave walls
- Effects: Gates, deposit zone, treasures
- UI: Buttons, panels

---

### 5. Textures/
**Purpose**: Image files (PNG, JPG)

**Import Settings**:
- UI textures: Texture Type = Sprite (2D and UI)
- Max Size: 2048 (for mobile)
- Compression: Automatic
- Generate Mip Maps: OFF (for UI)

**Alpha Channel**:
- PNG for transparency
- JPG for opaque textures (smaller file size)

---

### 6. Models/
**Purpose**: 3D models (FBX, OBJ)

**Import Settings**:
- Scale Factor: 1.0 (or adjust based on Asset Store pack)
- Generate Colliders: OFF (add manually for control)
- Materials: Use External Materials (legacy)
- Normals: Import
- Tangents: Calculate Mikktspace

**Optimization**:
- Target poly count: 5,000-10,000 tris per vehicle
- LOD (Level of Detail): Optional for Phase 1, add in Phase 4

---

### 7. Audio/
**Purpose**: Sound effects and music

**Import Settings for SFX** (.wav):
- Load Type: Decompress On Load (short sounds <1s)
- Compression Format: PCM (no compression for quality)
- Sample Rate: 44100 Hz
- Quality: 100%

**Import Settings for Music** (.ogg):
- Load Type: Streaming
- Compression Format: Vorbis
- Quality: 50-70% (acceptable for looping music)

**Naming Convention**:
- Lowercase with underscores: `collect_small.wav`
- Descriptive names: `gate_bronze_pass.wav`

---

### 8. Fonts/
**Purpose**: TrueType fonts for UI

**Recommended Fonts**:
- **Heading**: Fredoka One (free from Google Fonts)
- **Body**: Roboto (free from Google Fonts)

**TextMeshPro Setup**:
1. Import font file
2. Window → TextMeshPro → Font Asset Creator
3. Select font
4. Click "Generate Font Atlas"
5. Save as `Fredoka_SDF.asset`

---

### 9. Settings/
**Purpose**: Project configuration assets

**URP/**:
- **URPAsset**: Main rendering pipeline settings
- **ForwardRenderer**: Rendering features configuration

**InputActions/**:
- **PlayerInputActions.inputactions**: Input System configuration

**LevelData/**:
- Create `LevelData` ScriptableObjects for each level
- Right-click → Create → TreasureExcavator → Level Data

**VehicleData/**:
- Create `VehicleData` ScriptableObjects for each vehicle
- Right-click → Create → TreasureExcavator → Vehicle Data

---

### 10. Resources/
**Purpose**: Runtime-loaded assets

**When to Use**:
- Use sparingly (increases build size)
- Prefer AssetBundles or Addressables for large assets
- Good for: Small config files, debug tools

**Loading**:
```csharp
GameObject prefab = Resources.Load<GameObject>("Prefabs/MyPrefab");
```

---

## Layer Setup

**Edit → Project Settings → Tags and Layers**

### Layers (0-31):
- 0: Default
- 1: TransparentFX (built-in)
- 2: Ignore Raycast (built-in)
- 3: (unused)
- 4: Water (built-in)
- 5: UI (built-in)
- **6: Vehicle** (custom)
- **7: Treasure** (custom)
- **8: Ground** (custom)
- **9: Gate** (custom)
- **10: DepositZone** (custom)
- 11-31: (reserved for future use)

### Tags:
- Vehicle
- Treasure
- Ground
- Gate
- DepositZone
- Player (default)
- Untagged (default)

---

## Physics Layer Collision Matrix

**Edit → Project Settings → Physics**

Check/uncheck layer interactions:

|              | Vehicle | Treasure | Ground | Gate | Deposit |
|--------------|---------|----------|--------|------|---------|
| **Vehicle**  | ❌      | ✅       | ✅     | ✅   | ✅      |
| **Treasure** | ✅      | ❌       | ✅     | ❌   | ❌      |
| **Ground**   | ✅      | ✅       | ✅     | ✅   | ✅      |
| **Gate**     | ✅      | ❌       | ✅     | ❌   | ❌      |
| **Deposit**  | ✅      | ❌       | ✅     | ❌   | ❌      |

---

## Build Settings

### iOS Platform Settings

**File → Build Settings → iOS**

**Add Scenes** (in order):
1. MainMenu
2. Level_01 through Level_08

**Player Settings → iOS**:
- Bundle Identifier: `com.yourcompany.treasureexcavator`
- Version: 0.1.0
- Build: 1
- Minimum iOS Version: 14.0
- Target SDK: Device SDK
- Architecture: ARM64

---

## Recommended Folder Creation Order

1. **Week 1** (Phase 1 Start):
   - Scripts/ (copy from this repo)
   - Scenes/ (create Level_01)
   - Prefabs/ (create prototypes)
   - Materials/ (basic colors)

2. **Week 2** (Asset Acquisition):
   - Models/ (import purchased packs)
   - Audio/ (import audio files)
   - Textures/ (import textures)

3. **Week 3** (Configuration):
   - Settings/LevelData/ (create 8 level configs)
   - Settings/VehicleData/ (create 4 vehicle configs)
   - Fonts/ (download Google Fonts)

4. **Phase 2+** (Expansion):
   - Animations/ (UI animations)
   - Shaders/ (custom effects)
   - Resources/ (runtime assets)

---

## .gitignore Reminder

Already created in root, but ensure these are ignored:

```
# Unity Generated
Library/
Temp/
Obj/
Build/
Builds/
Logs/
UserSettings/

# Large Files (use Git LFS instead)
*.fbx
*.png
*.wav
*.ogg
*.mp3
```

---

## Checklist for Project Setup

- [ ] Create Unity project with URP template
- [ ] Copy all Scripts/ from this repo to Assets/Scripts/
- [ ] Create all Scenes/ folders and files
- [ ] Create all Prefabs/ subfolders
- [ ] Configure Layers and Tags
- [ ] Set up Physics collision matrix
- [ ] Import Input System package
- [ ] Import TextMeshPro package
- [ ] Configure iOS build settings
- [ ] Test project compiles without errors

---

*End of Unity Project Structure Guide*

**Next Step**: Follow `PHASE_1_IMPLEMENTATION_GUIDE.md` to populate this structure with game content.
