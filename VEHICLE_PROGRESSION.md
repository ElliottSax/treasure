# Vehicle Progression Specification
## Treasure Excavator - Complete Vehicle System Design

**Version**: 1.0
**Last Updated**: 2025-11-18
**Total Vehicles**: 4 (3 in MVP, 1 post-launch)

---

## 1. Design Philosophy

### Core Principles
- ✅ **Meaningful Differences**: Each vehicle has distinct gameplay advantages
- ✅ **No Power Creep**: Later vehicles are situationally better, not strictly superior
- ✅ **Player Choice**: Multiple viable strategies for any level
- ✅ **Skill Expression**: Vehicles reward different playstyles
- ✅ **Fair Monetization**: All vehicles unlockable via gameplay

### Balance Goals
- No single "best" vehicle for all situations
- Starter vehicle remains viable for entire game
- Unlock progression feels rewarding
- Special abilities are impactful but not mandatory

---

## 2. Vehicle Overview Matrix

| Vehicle | Speed | Capacity | Radius | Special Ability | Best For | Unlock |
|---------|-------|----------|--------|-----------------|----------|--------|
| **Starter Bulldozer** | 8 m/s | 10 | 1.5m | None | Tutorials, beginner levels | Default |
| **Dual-Scoop Loader** | 8 m/s | 15 | 2.5m | Auto-Collect | Scattered treasures, efficiency | Level 3 / 1,000 gold |
| **Nitro Hauler** | 8-16 m/s | 10 | 1.5m | Nitro Boost | Speed runs, gate chaining | Level 6 / 2,500 gold |
| **Mega Vault Truck** | 7 m/s | 25 | 1.5m | Gate Magnet | 3-star runs, max score | Level 8 (3⭐) / 5,000 gold |

---

## 3. Detailed Vehicle Specifications

### Vehicle 1: Starter Bulldozer

**Visual Description**:
- Classic yellow bulldozer with front blade
- Compact, sturdy design
- Exhaust smoke particle effect
- Headlights glow in dark areas

**Stats**:
```yaml
Speed: 8 m/s (base movement)
Acceleration: 5 m/s²
Turning Speed: 180°/second
Cargo Capacity: 10 treasures
Collection Radius: 1.5 meters
Special Ability: None
```

**Strengths**:
- Balanced stats (no weaknesses)
- Easy to control for beginners
- Good for learning game mechanics
- Free (no unlock cost)

**Weaknesses**:
- No special abilities
- Smaller capacity than Dual-Scoop
- Slower than Nitro Hauler

**Best Use Cases**:
- Levels 1-3 (tutorial and early game)
- Players learning the game
- Levels with linear paths and few treasures

**Physics Properties**:
```csharp
Mass: 1000 kg
Drag: 0.5
Angular Drag: 2.0
```

**Unlock**: Available from game start

---

### Vehicle 2: Dual-Scoop Loader

**Visual Description**:
- Two large scoop buckets on front
- Wider chassis than Bulldozer
- Teal/turquoise color scheme
- Glowing collection aura when near treasures

**Stats**:
```yaml
Speed: 8 m/s (same as Bulldozer)
Acceleration: 4.5 m/s² (slightly slower)
Turning Speed: 160°/second (slightly slower)
Cargo Capacity: 15 treasures (+50%)
Collection Radius: 2.5 meters (+67%)
Special Ability: "Auto-Collect" - Treasures fly to you from extended range
```

**Special Ability: Auto-Collect**:
- Treasures within 2.5m radius automatically fly to vehicle
- Visual: Magnetic pull effect with particle trail
- Audio: "Whoosh" sound as treasures are pulled in
- No cooldown (always active)

**Strengths**:
- **+50% cargo capacity** (10 → 15 treasures)
- **+67% collection radius** (1.5m → 2.5m)
- Less precise driving needed
- Great for levels with scattered treasures

**Weaknesses**:
- Slightly slower acceleration
- Slightly slower turning
- No speed advantage

**Best Use Cases**:
- Levels 3-5 (mid-game)
- Levels with scattered treasure placement
- Efficiency runs (fewer trips to deposit zone)
- Players who want forgiving collection mechanics

**Physics Properties**:
```csharp
Mass: 1200 kg (heavier)
Drag: 0.6 (more air resistance)
Angular Drag: 2.5 (slower turning)
```

**Unlock Options**:
1. Complete Level 3 (earn via gameplay)
2. Purchase for 1,000 gold (skip requirement)

**Strategic Value**:
- Reduces trips to deposit zone by 50%
- Allows collection without precise pathfinding
- Ideal for "clean sweep" strategies (collect everything)

---

### Vehicle 3: Nitro Hauler

**Visual Description**:
- Sleek, aerodynamic design
- Red/orange racing stripes
- Exhaust flames when Nitro active
- Blue nitro glow effect on boost

**Stats**:
```yaml
Speed: 8 m/s (base), 16 m/s (nitro active)
Acceleration: 6 m/s² (faster than others)
Turning Speed: 200°/second (fastest turning)
Cargo Capacity: 10 treasures (same as Bulldozer)
Collection Radius: 1.5 meters (same as Bulldozer)
Special Ability: "Nitro Boost" - 3-second speed burst
```

**Special Ability: Nitro Boost**:
- **Activation**: Tap nitro button (UI bottom-right)
- **Effect**: Speed doubles (8 m/s → 16 m/s) for 3 seconds
- **Cooldown**: 10 seconds
- **Visual**: Blue flame trail, motion blur, screen shake
- **Audio**: Engine roar, whoosh sound

**Strengths**:
- **Double speed during boost** (fastest vehicle)
- Fastest base acceleration
- Fastest turning speed
- Excellent for speedruns

**Weaknesses**:
- Standard cargo capacity (10)
- Standard collection radius (1.5m)
- Requires skillful boost timing

**Best Use Cases**:
- Levels 6-8 (late game)
- Speedrun challenges
- Chaining distant gates quickly
- Time-based objectives (future)
- Levels with long distances between zones

**Physics Properties**:
```csharp
Mass: 800 kg (lighter, faster)
Drag: 0.4 (low air resistance)
Angular Drag: 1.5 (nimble turning)
```

**Unlock Options**:
1. Complete Level 6 (earn via gameplay)
2. Purchase for 2,500 gold (skip requirement)

**Strategic Value**:
- Can chain multiple gates in single trip
- Reduces overall level completion time
- Allows riskier high-reward paths
- Skill-based: Timing boost for optimal results

**Nitro UI Element**:
```
[Bottom Right HUD]
┌─────────┐
│ NITRO   │
│ [████░] │ ← Cooldown bar
│ [BOOST] │ ← Button (disabled when cooling down)
└─────────┘
```

---

### Vehicle 4: Mega Vault Truck (Post-Launch v1.1)

**Visual Description**:
- Massive armored truck with vault door on back
- Gold-plated accents
- Glowing cargo bay
- Particle effects show treasure overflow

**Stats**:
```yaml
Speed: 7 m/s (slower than others)
Acceleration: 4 m/s² (slowest)
Turning Speed: 140°/second (slowest turning)
Cargo Capacity: 25 treasures (+150% vs Bulldozer)
Collection Radius: 1.5 meters (standard)
Special Ability: "Gate Magnet" - Shows optimal gate path
```

**Special Ability: Gate Magnet**:
- **Activation**: Passive (always active)
- **Effect**: Highlights optimal gate chain on HUD
- **Visual**: Glowing path line showing highest multiplier route
- **Audio**: Subtle "ping" when near optimal gate
- **Strategic Depth**: Teaches advanced pathing to players

**Strengths**:
- **+150% cargo capacity** (10 → 25 treasures)
- Path guidance for max score
- Best for 3-star challenges
- Shows hidden optimal strategies

**Weaknesses**:
- **Slowest speed** (7 m/s vs 8 m/s)
- Slowest acceleration
- Sluggish turning
- Requires patient playstyle

**Best Use Cases**:
- 3-star completion runs
- Maximum score challenges
- Leaderboard competition (post-launch)
- Players who prioritize strategy over speed

**Physics Properties**:
```csharp
Mass: 1500 kg (heaviest)
Drag: 0.7 (high air resistance)
Angular Drag: 3.0 (very slow turning)
```

**Unlock Options**:
1. Complete Level 8 with 3 stars (earn via mastery)
2. Purchase for 5,000 gold (very expensive)

**Strategic Value**:
- Enables single-trip completions on most levels
- Teaches optimal gate chaining strategies
- Rewards careful planning over speed
- Unlocking it signals player mastery

**Gate Magnet Visualization**:
```
[In-Game Overlay]
Current Cargo: 15
Optimal Path: x2 → x3 → Deposit = 90 points
└→ [Glowing line shows path]
```

---

## 4. Progression Curve

### Unlock Timeline

```
Level 1 ━━━━━━━━━━━━━━━━━━━━━━━━━→ Level 8
  │                    │              │
  │                    │              │
Bulldozer       Dual-Scoop      Nitro Hauler
(default)      (Level 3)        (Level 6)
                                       │
                                  Mega Vault
                                 (Level 8, 3⭐)
```

### Gold Earning vs Vehicle Costs

| Milestone | Cumulative Gold | Vehicle Available | Cost | Net Gold |
|-----------|-----------------|-------------------|------|----------|
| Start | 0 | Bulldozer | 0 | 0 |
| Complete Level 3 | ~400 | Dual-Scoop | 1,000 | -600 (or unlock via level) |
| Complete Level 6 | ~900 | Nitro Hauler | 2,500 | -1,600 (or unlock via level) |
| Complete Level 8 (3⭐) | ~2,600 | Mega Vault | 5,000 | -2,400 (or unlock via stars) |

**Key Insight**: Players can unlock all vehicles via gameplay OR purchase early with gold from ads/IAP.

---

## 5. Vehicle Comparison by Playstyle

### Beginner-Friendly
1. **Starter Bulldozer** - Balanced, no complexity
2. **Dual-Scoop Loader** - Forgiving collection radius

### Speedrunner
1. **Nitro Hauler** - Fastest, skill-based
2. **Starter Bulldozer** - Nimble, predictable

### Completionist (3-Star Hunter)
1. **Mega Vault Truck** - Max capacity, path guidance
2. **Dual-Scoop Loader** - Efficient collection

### Casual Player
1. **Dual-Scoop Loader** - Less stressful, larger capacity
2. **Starter Bulldozer** - Simple, reliable

---

## 6. Level-Specific Vehicle Recommendations

| Level | Recommended Vehicle | Reason |
|-------|---------------------|--------|
| 1 | Bulldozer | Tutorial, learn basics |
| 2 | Bulldozer | Introduce gate choice |
| 3 | Bulldozer → Dual-Scoop | Unlock point, test new vehicle |
| 4 | Dual-Scoop | Scattered treasures benefit from radius |
| 5 | Dual-Scoop or Nitro | Gate chaining (Nitro) or capacity (Dual-Scoop) |
| 6 | Nitro Hauler | Speed challenge, distant zones |
| 7 | Dual-Scoop or Mega Vault | Vertical layout, lots of treasures |
| 8 | Mega Vault | Final challenge, max score required |

**Note**: All levels completable with any vehicle—recommendations are for optimal strategy, not requirements.

---

## 7. Cosmetic Customization

### Skins (Apply to Any Vehicle)
All vehicles support cosmetic skins:
- Red Racer (500 gold)
- Blue Steel (500 gold)
- Golden Glint (1,000 gold or complete all levels 3⭐)
- Camouflage (200 gems, IAP)
- Neon Glow (200 gems, IAP)
- Diamond Plated (500 gems, premium)
- Forest Green (7-day login reward)
- Sunset Orange (watch 10 ads)
- Carbon Fiber (replay achievement)
- Chrome Finish (300 gems, IAP)

**Visual Changes Only**: Skins do not affect stats or abilities.

---

## 8. Unity Implementation Notes

### Vehicle Prefab Structure
```
Vehicle_Bulldozer/
├── Model (3D mesh)
├── Collider (Box/Mesh)
├── Rigidbody (physics)
├── VehicleController.cs (movement script)
├── CargoManager.cs (treasure storage)
├── AbilityController.cs (special abilities)
├── Particles/
│   ├── EngineSmoke
│   ├── CollectionAura
│   └── NitroFlames (Nitro Hauler only)
└── Audio/
    ├── EngineLoop
    ├── NitroBoost
    └── TreasureCollect
```

### Key Scripts

**VehicleController.cs**:
```csharp
public class VehicleController : MonoBehaviour
{
    public float speed = 8f;
    public float acceleration = 5f;
    public float turningSpeed = 180f;

    private Rigidbody rb;

    void Move(Vector2 input)
    {
        // Touch/tilt input handling
    }
}
```

**CargoManager.cs**:
```csharp
public class CargoManager : MonoBehaviour
{
    public int maxCapacity = 10;
    public float collectionRadius = 1.5f;
    private int currentCargo = 0;

    void CollectTreasure(Treasure treasure)
    {
        if (currentCargo < maxCapacity)
        {
            currentCargo++;
            // Update UI, play effects
        }
    }
}
```

**AbilityController.cs**:
```csharp
public abstract class VehicleAbility
{
    public abstract void Activate();
    public abstract bool IsReady();
}

public class NitroBoost : VehicleAbility
{
    private float cooldown = 10f;
    private float duration = 3f;

    public override void Activate()
    {
        // Double speed for duration
    }
}
```

---

## 9. Balancing Validation Checklist

### Testing Goals
- [ ] All vehicles can complete all levels
- [ ] No vehicle is >30% faster on average than others
- [ ] Special abilities feel impactful (player preference ≥50%)
- [ ] Unlock timing feels rewarding (survey playtesters)
- [ ] Nitro Hauler skill ceiling is high but accessible
- [ ] Mega Vault doesn't trivialize challenge (3⭐ still hard)

### Playtesting Questions
1. Which vehicle is your favorite? Why?
2. Do any vehicles feel useless or overpowered?
3. Was the unlock timing satisfying?
4. Did you purchase vehicles with gold or wait for unlocks?
5. Do special abilities feel worth using?

---

## 10. Future Vehicle Ideas (Post-v1.1)

### Vehicle 5: Drill Excavator
- **Ability**: Burrow through obstacles (create shortcuts)
- **Trade-off**: Slower speed, standard capacity

### Vehicle 6: Hover Loader
- **Ability**: Fly over gaps/obstacles
- **Trade-off**: Can't use x10 gates (balance)

### Vehicle 7: Magnetic Crane
- **Ability**: Pull distant treasures without moving
- **Trade-off**: Can't move while pulling

**Note**: Save for post-launch based on player feedback.

---

## 11. Accessibility Considerations

### Control Modes (All Vehicles)
- Touch controls (swipe to steer)
- Tilt controls (accelerometer)
- Button controls (on-screen joystick)

### Visual Clarity
- Each vehicle has distinct silhouette
- Color-blind friendly palette
- Collection radius shown with circle overlay (settings toggle)

### Difficulty Settings (Future)
- Easy Mode: +50% collection radius for all vehicles
- Hard Mode: -50% cargo capacity for all vehicles

---

## 12. Approval Checklist

Before Phase 1 Implementation:
- [x] All 4 vehicles fully specified
- [x] Stats balanced and meaningful
- [ ] Abilities designed with clear trade-offs
- [ ] Unlock progression tested on paper
- [ ] Unity implementation plan clear
- [ ] Playtesting plan prepared

---

*End of Vehicle Progression Specification*

**Status**: Ready for Phase 1 Implementation
**Next Steps**: Create 3D models or source from Asset Store
