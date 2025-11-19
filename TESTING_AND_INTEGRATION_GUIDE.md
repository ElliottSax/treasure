# Testing & Integration Guide - Treasure Excavator Phase 2

## Document Information
- **Version**: 1.0
- **Last Updated**: 2025-11-19
- **Purpose**: Comprehensive testing and integration instructions for Phase 2 systems

---

## Quick Start

### Step 1: Scene Setup (5 minutes)

1. **Create Managers GameObject**
   ```
   Create Empty GameObject: "GameManagers"
   Add all manager scripts:
   - GameManager
   - AudioManager
   - InputManager
   - CameraController (attach to Main Camera instead)
   - LevelManager
   - SaveManager (NEW)
   - SettingsManager (NEW)
   - ParticleEffectsManager (NEW)
   - AnalyticsManager (NEW)
   ```

2. **Create Systems GameObject**
   ```
   Create Empty GameObject: "GameSystems"
   Add all system scripts:
   - AchievementManager
   - PowerUpSystem
   - ComboSystem
   - ShopManager
   - DailyRewardsManager
   ```

3. **Add Debug Tools**
   ```
   Create Empty GameObject: "DebugTools"
   Add scripts:
   - DebugMenuSystem
   - SystemValidator
   ```

### Step 2: Run System Validation

1. Select the DebugTools GameObject
2. Find SystemValidator component
3. Click "Validate All Systems" button in Inspector
4. Check Console for validation report
5. Fix any errors or warnings

### Step 3: Test with Debug Menu

1. Run the game
2. Press **F1** (or three-finger tap on mobile)
3. Debug menu appears
4. Test each system:
   - Save/Load
   - Achievements
   - Power-Ups
   - Combos
   - Shop
   - Daily Rewards
   - Currency

---

## Integration Instructions

### Phase 1 Script Integration

The new Phase 2 systems require adding methods to your existing Phase 1 scripts. We've created extension files that show you exactly what to add.

#### 1. VehicleController Integration

**File**: `Scripts/Vehicles/VehicleController_Phase2Extensions.cs`

**What to Add to VehicleController.cs:**
```csharp
// Add these fields
[Header("Phase 2 - Power-Up Support")]
[SerializeField] private float baseSpeed = 10f;
[SerializeField] private float baseCollectionRadius = 1.5f;
private float speedMultiplier = 1f;
private bool isInvincible = false;
private bool shieldActive = false;

// Add these methods
public void SetSpeedMultiplier(float multiplier) { /* see extension file */ }
public float GetEffectiveSpeed() { /* see extension file */ }
public void SetCollectionRadius(float radius) { /* see extension file */ }
public void ResetCollectionRadius() { /* see extension file */ }
public void SetInvincible(bool invincible) { /* see extension file */ }
public bool IsInvincible() { /* see extension file */ }
public void EnableShield(bool enable) { /* see extension file */ }
public void TakeDamage(int damage) { /* see extension file */ }
```

**Integration Points:**
- Modify your `Move()` method to use `GetEffectiveSpeed()` instead of `speed`
- Check `IsInvincible()` in collision handlers

#### 2. GameManager Integration

**File**: `Scripts/Managers/GameManager_Phase2Extensions.cs`

**What to Add to GameManager.cs:**
```csharp
// Add these fields
[Header("Phase 2 - Multipliers & Timers")]
private float scoreMultiplier = 1f;
private float goldMultiplier = 1f;
private bool timerFrozen = false;

// Add these methods
public void SetScoreMultiplier(float multiplier) { /* see extension file */ }
public void SetGoldMultiplier(float multiplier) { /* see extension file */ }
public void FreezeTimer(bool freeze) { /* see extension file */ }
public float GetScoreProgress() { /* see extension file */ }
public void AddScoreWithMultipliers(int baseScore) { /* see extension file */ }
```

**Integration Points:**
- Replace `AddScore()` calls with `AddScoreWithMultipliers()` for power-up support
- Call `UpdateTimerWithFreeze()` in your Update() method

#### 3. CargoManager Integration

**File**: `Scripts/Gameplay/CargoManager_Phase2Extensions.cs`

**What to Add to CargoManager.cs:**
```csharp
// Add these methods
public void FillToCapacity() { /* see extension file */ }
public void LoseCargo(int amount) { /* see extension file */ }
public int GetCurrentCargo() { /* see extension file */ }
public float GetCargoFillPercentage() { /* see extension file */ }
```

**Integration Points:**
- In `AddTreasure()`, call `ComboSystem.Instance?.OnTreasureCollected()`
- In `AddTreasure()`, call `AchievementManager.Instance?.OnTreasureCollected(1)`
- In `ApplyMultiplier()`, call `AchievementManager.Instance?.OnGateUsed(1)`

#### 4. CameraController - Already Complete!

The CameraController should already work with Phase 2 systems. Just ensure it's a singleton:

```csharp
public class CameraController : Singleton<CameraController>
{
    // Your existing code...
}
```

---

## Testing Checklist

### ✅ Save System Tests

- [ ] Game saves when calling `SaveManager.Instance.SaveGame()`
- [ ] Save file appears at: `Application.persistentDataPath/gamedata.sav`
- [ ] Save file is encrypted (not readable as plain text)
- [ ] Backup files created (up to 3)
- [ ] Game loads correctly after restart
- [ ] Deleting save data creates fresh save
- [ ] Save data persists between sessions

**Test Script:**
```csharp
// In Console or Debug Menu
SaveManager.Instance.AddGold(1000);
SaveManager.Instance.SaveGame();
// Restart game
Debug.Log(SaveManager.Instance.GetGold()); // Should print 1000
```

### ✅ Settings System Tests

- [ ] Volume sliders work in real-time
- [ ] Graphics quality changes are visible
- [ ] Control scheme switching works
- [ ] Haptic feedback triggers on events
- [ ] Settings persist after restart
- [ ] Auto-detect sets appropriate quality
- [ ] Battery mode reduces frame rate

**Test Script:**
```csharp
// Test volume
SettingsManager.Instance.SetMasterVolume(0.5f);
// Should hear quieter audio

// Test graphics
SettingsManager.Instance.SetGraphicsQuality(0); // Low
// Should see quality reduction

// Test haptic
SettingsManager.Instance.TriggerHaptic(SettingsManager.HapticType.Success);
// Should feel vibration on mobile
```

### ✅ Achievement System Tests

- [ ] Achievements unlock when conditions met
- [ ] Progress tracking increments correctly
- [ ] Gold rewards granted on unlock
- [ ] Achievement UI shows progress
- [ ] Unlocked achievements persist
- [ ] Events fire on unlock
- [ ] Completion percentage calculates correctly

**Test Script:**
```csharp
// Simulate collecting 100 treasures
AchievementManager.Instance.OnTreasureCollected(100);

// Check if achievement unlocked
var unlocked = AchievementManager.Instance.GetUnlockedAchievements();
Debug.Log($"Unlocked: {unlocked.Count} achievements");

// Check completion
float completion = AchievementManager.Instance.GetCompletionPercentage();
Debug.Log($"Completion: {completion}%");
```

### ✅ Power-Up System Tests

- [ ] Power-ups spawn at specified positions
- [ ] Power-ups float and rotate
- [ ] Collection triggers effects
- [ ] Duration timer counts down
- [ ] Effects apply correctly:
  - Speed Boost: Vehicle moves faster
  - Magnet: Collection radius increases
  - Double Points: Score doubled
  - Invincibility: No damage from obstacles
  - Shield: Blocks one hit
- [ ] Multiple power-ups can stack
- [ ] Power-ups expire after duration
- [ ] Events fire on collect/expire

**Test Script:**
```csharp
// Activate speed boost
PowerUpSystem.Instance.ActivatePowerUp(PowerUpType.SpeedBoost, 15f);
// Vehicle should move 50% faster for 15 seconds

// Check active power-ups
bool isActive = PowerUpSystem.Instance.IsPowerUpActive(PowerUpType.SpeedBoost);
float remaining = PowerUpSystem.Instance.GetPowerUpTimeRemaining(PowerUpType.SpeedBoost);
```

### ✅ Combo System Tests

- [ ] Combo increments on treasure collection
- [ ] Combo timer resets (3 seconds)
- [ ] Combo breaks after timeout
- [ ] Score multiplier applies correctly
- [ ] Combo color changes with level
- [ ] Milestones trigger events (5x, 10x, etc.)
- [ ] Highest combo tracked

**Test Script:**
```csharp
// Build combo
for (int i = 0; i < 10; i++)
{
    ComboSystem.Instance.OnTreasureCollected();
    // Wait less than 3 seconds between calls
}

int combo = ComboSystem.Instance.GetCurrentCombo(); // Should be 10
float multiplier = ComboSystem.Instance.GetCurrentMultiplier(); // Should be 1.8x
```

### ✅ Shop System Tests

- [ ] Shop displays all items
- [ ] Purchase deducts correct currency
- [ ] Insufficient funds shows error
- [ ] Vehicles unlock after purchase
- [ ] Power-up bundles add to inventory
- [ ] Already-owned check works
- [ ] IAP flow ready (simulated in test)

**Test Script:**
```csharp
// Add gold
SaveManager.Instance.AddGold(5000);

// Purchase vehicle
bool success = ShopManager.Instance.PurchaseItem("vehicle_nitro_hauler");
Debug.Log($"Purchase: {success}");

// Check if owned
bool owned = ShopManager.Instance.IsItemOwned("vehicle_nitro_hauler");
Debug.Log($"Owned: {owned}");
```

### ✅ Daily Reward System Tests

- [ ] Reward available on first login
- [ ] Reward can be claimed
- [ ] Streak increments on consecutive days
- [ ] Streak breaks if day missed
- [ ] Reward schedule shows 7 days
- [ ] Milestones trigger events
- [ ] Time until next reward displays correctly

**Test Script:**
```csharp
// Check if can claim
bool canClaim = DailyRewardsManager.Instance.CanClaimToday();
Debug.Log($"Can claim: {canClaim}");

// Claim reward
bool claimed = DailyRewardsManager.Instance.ClaimDailyReward();
Debug.Log($"Claimed: {claimed}");

// Check streak
int streak = DailyRewardsManager.Instance.GetCurrentStreak();
Debug.Log($"Streak: {streak} days");
```

### ✅ Analytics System Tests

- [ ] Events log to console (debug mode)
- [ ] Event parameters captured correctly
- [ ] No performance impact
- [ ] Can enable/disable tracking
- [ ] Firebase integration ready (commented code)

**Test Script:**
```csharp
// Send test event
AnalyticsManager.Instance.TrackLevelComplete(1, 3, 500, 45f);
// Check console for log

// Test custom event
AnalyticsManager.Instance.TrackEvent("test_event", new Dictionary<string, object> {
    { "param1", "value1" },
    { "param2", 42 }
});
```

### ✅ Particle Effects Tests

- [ ] Effects play at correct positions
- [ ] Pooling reuses particle systems
- [ ] Effects auto-return to pool
- [ ] Color override works
- [ ] Attached effects follow transform
- [ ] No performance issues with many effects

**Test Script:**
```csharp
// Play explosion effect
ParticleEffectsManager.Instance.PlayExplosionEffect(transform.position);

// Play with color
ParticleEffectsManager.Instance.PlayEffect("TreasureCollect", position, Color.gold);

// Check pool stats
Debug.Log(ParticleEffectsManager.Instance.GetPoolStats());
```

### ✅ Obstacle System Tests

- [ ] Static obstacles block movement
- [ ] Moving obstacles patrol correctly
- [ ] Rotating obstacles spin
- [ ] Explosive obstacles explode on hit
- [ ] Breakable obstacles destroy and award points
- [ ] Bouncy obstacles apply force
- [ ] Cargo loss percentage correct
- [ ] Combo breaks on hit
- [ ] Invincibility blocks damage

**Test Script:**
```csharp
// Test with invincibility
VehicleController.Instance.SetInvincible(true);
// Hit obstacle - should take no damage

// Test with shield
VehicleController.Instance.EnableShield(true);
// Hit obstacle - shield breaks but no damage

// Test cargo loss
CargoManager.Instance.AddTreasure(treasure); // Add 10 treasures
// Hit obstacle
// Should lose 25% (2-3 treasures)
```

---

## Common Issues & Solutions

### Issue: "Instance is null"

**Cause**: Manager GameObject not in scene or Awake() not called

**Solution**:
1. Ensure Managers GameObject exists in scene
2. Check that scripts are enabled
3. Verify DontDestroyOnLoad is working
4. Check for duplicate singletons

### Issue: Save file not persisting

**Cause**: Path not writable or encryption error

**Solution**:
1. Check `Application.persistentDataPath` is valid
2. Verify disk space available
3. Check console for encryption errors
4. Try disabling encryption temporarily

### Issue: Power-ups not working

**Cause**: VehicleController missing Phase 2 methods

**Solution**:
1. Add methods from `VehicleController_Phase2Extensions.cs`
2. Ensure `GetEffectiveSpeed()` is used in movement
3. Check power-up prefabs are assigned

### Issue: Achievements not unlocking

**Cause**: Events not being called

**Solution**:
1. Add achievement calls to gameplay code:
```csharp
// In CargoManager.AddTreasure()
AchievementManager.Instance?.OnTreasureCollected(1);

// In MultiplierGate.ActivateGate()
AchievementManager.Instance?.OnGateUsed(1);
```

### Issue: Combos breaking too fast

**Cause**: Collection rate slower than 3-second window

**Solution**:
1. Increase combo window in ComboSystem:
```csharp
[SerializeField] private float comboTimeWindow = 5f; // Increase from 3f
```

### Issue: No analytics events

**Cause**: Analytics disabled or not initialized

**Solution**:
1. Check `enableAnalytics` is true in AnalyticsManager
2. Verify `enableDebugLogging` is true for testing
3. Check console for analytics initialization

---

## Performance Testing

### Frame Rate Test

**Target**: 60 FPS on mid-range devices

```csharp
// Add to Update() for monitoring
void Update()
{
    float fps = 1f / Time.deltaTime;
    if (fps < 55f)
    {
        Debug.LogWarning($"Low FPS: {fps:F1}");
    }
}
```

### Memory Test

**Target**: < 200MB on mobile

```csharp
// Check memory usage
long memory = System.GC.GetTotalMemory(false);
Debug.Log($"Memory: {memory / 1024 / 1024}MB");
```

### Object Pool Test

**Test**: Spawn 100 particles rapidly

```csharp
for (int i = 0; i < 100; i++)
{
    ParticleEffectsManager.Instance.PlayExplosionEffect(
        Random.insideUnitSphere * 10f
    );
}

// Check no GC allocations
// Profile in Unity Profiler
```

---

## Mobile Testing

### Device Test Matrix

| Device | OS | Resolution | RAM | Expected Quality |
|--------|----|-----------| ----|------------------|
| iPhone 11 | iOS 15+ | 828x1792 | 4GB | High |
| iPhone SE | iOS 14+ | 750x1334 | 2GB | Medium |
| iPad Air | iPadOS 15+ | 1640x2360 | 4GB | High |

### Touch Testing

- [ ] Single tap collection works
- [ ] Drag steering works
- [ ] Tilt control works (if enabled)
- [ ] Three-finger tap opens debug menu
- [ ] Touch feedback responsive

### Haptic Testing

- [ ] Success haptic on treasure collect
- [ ] Heavy haptic on obstacle hit
- [ ] Medium haptic on gate activation
- [ ] Settings toggle works

---

## Integration Timeline

### Day 1: Core Integration (4 hours)
- ✅ Add extension methods to Phase 1 scripts
- ✅ Set up manager GameObjects
- ✅ Run system validation
- ✅ Fix any errors

### Day 2: Feature Testing (4 hours)
- ✅ Test save/load system
- ✅ Test achievements
- ✅ Test power-ups
- ✅ Test combo system

### Day 3: Advanced Testing (4 hours)
- ✅ Test shop system
- ✅ Test daily rewards
- ✅ Test obstacles
- ✅ Test analytics

### Day 4: Polish & Optimization (4 hours)
- ✅ Performance profiling
- ✅ Mobile device testing
- ✅ Bug fixes
- ✅ Final validation

---

## Debug Menu Commands

Press **F1** (or three-finger tap) to open debug menu, then use these shortcuts:

| Button | Action |
|--------|--------|
| Save Game Now | Manually trigger save |
| Add 1,000 Gold | Test currency system |
| Unlock All Levels | Skip progression |
| Activate Speed Boost | Test power-up |
| Add 10x Combo | Test combo system |
| Claim Daily Reward | Test rewards |
| Test Analytics | Send test event |

---

## Automated Testing (Optional)

### Unity Test Framework

Create test assemblies in `Assets/Tests/`:

```csharp
[Test]
public void SaveManager_Saves_And_Loads_Correctly()
{
    SaveManager.Instance.AddGold(1000);
    SaveManager.Instance.SaveGame();

    SaveManager.Instance.LoadGame();
    int gold = SaveManager.Instance.GetGold();

    Assert.AreEqual(1000, gold);
}
```

### Play Mode Tests

```csharp
[UnityTest]
public IEnumerator PowerUpSystem_Activates_SpeedBoost()
{
    PowerUpSystem.Instance.ActivatePowerUp(PowerUpType.SpeedBoost, 5f);

    yield return new WaitForSeconds(1f);

    bool isActive = PowerUpSystem.Instance.IsPowerUpActive(PowerUpType.SpeedBoost);
    Assert.IsTrue(isActive);
}
```

---

## Final Validation

Before shipping:

- [ ] All systems validated with no errors
- [ ] Performance targets met (60 FPS)
- [ ] Memory usage acceptable (< 200MB)
- [ ] Save data persists correctly
- [ ] No console errors in 10-minute play session
- [ ] Mobile devices tested
- [ ] Analytics tracking verified
- [ ] IAP flow ready (even if not enabled)

---

## Support

If you encounter issues:

1. Check this guide's "Common Issues" section
2. Run SystemValidator and check report
3. Review console logs for errors
4. Check extension files for missing methods
5. Verify all managers exist in scene

---

**Last Updated**: 2025-11-19
**Status**: Ready for Integration Testing
**Estimated Integration Time**: 16 hours (4 days @ 4 hours/day)
