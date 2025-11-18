# Treasure Excavator - Game Design Document

## Document Information
- **Version**: 1.0
- **Last Updated**: 2025-11-18
- **Status**: Phase 0 - Pre-Production
- **Author**: Development Team

---

## 1. Game Overview

### 1.1 High Concept
A physics-based mobile treasure collection game where players drive excavation vehicles to collect treasures, multiply their value through strategic gate usage, and maximize their score. The game delivers the actual gameplay shown in misleading mobile game ads - an "honest game" that delivers what it advertises.

### 1.2 Core Gameplay Loop
1. **Start**: Player spawns in excavation vehicle at level start point
2. **Collect**: Drive around 3D mining environment, collect treasure items (touch = auto-collect)
3. **Store**: Treasures load into vehicle "cargo hold" (visible UI counter + visual effect)
4. **Navigate**: Find and approach multiplier gates (x2, x3, x5, x10)
5. **Multiply**: Drive through gate with cargo → all cargo multiplies by gate value
6. **Deposit**: Drive to "Deposit Zone" (large glowing circle)
7. **Score**: Auto-deposit all cargo on entry → score increases
8. **Repeat**: Continue collecting and multiplying until win condition met
9. **Complete**: Reach target score → level complete screen with star rating

### 1.3 Target Platform
- **Primary**: iOS (iPhone 11 or newer)
- **Future**: iPad (Phase 2), Android (Phase 2)
- **Engine**: Unity 2022.3 LTS with URP

### 1.4 Target Audience
- **Primary**: Casual mobile gamers aged 25-45
- **Secondary**: Players frustrated by misleading mobile game ads
- **Tertiary**: Fans of vehicle simulation and resource management games
- **Session Length**: 3-10 minutes per level

### 1.5 Key Differentiators
- Delivers the actual gameplay shown in misleading ads
- Physics-based treasure interaction (weight, momentum)
- Satisfying visual feedback (multiplication animations, particle effects)
- No predatory monetization - ethical free-to-play model
- "Honest game" positioning - we deliver what we advertise

---

## 2. Core Mechanics

### 2.1 Treasure Collection System

#### Collection Phase
- Player drives vehicle near treasure item (within 1.5m by default)
- OnTriggerEnter detects treasure
- Treasure flies up to vehicle with particle trail
- Cargo counter updates: "Cargo: 5/10"
- Satisfying "ding" sound plays
- Vehicle capacity limited (varies by vehicle)

#### Treasure Types
| Type | Value | Visual | Rarity |
|------|-------|--------|--------|
| Small | 10 | Gold coin | Common |
| Medium | 50 | Gem cluster | Uncommon |
| Large | 200 | Treasure chest | Rare |

### 2.2 Multiplication Gates

#### Gate Types
| Gate | Multiplier | Visual | Unlock |
|------|------------|--------|--------|
| Bronze | x2 | Bronze arch | Level 1 |
| Silver | x3 | Silver arch | Level 2 |
| Gold | x5 | Gold arch | Level 5 |
| Platinum | x10 | Glowing platinum | Level 8 |

#### Gate Mechanics
- Gate only triggers if cargo > 0
- ALL cargo multiplied by gate value
- Visual: Treasure count animates rapidly
- Audio: Satisfying "ka-ching" sound
- Cargo counter updates immediately
- Can chain multiple gates for exponential growth

### 2.3 Deposit System

#### Deposit Zone
- Large glowing circle on ground
- Pulsing particle effects
- Auto-deposit all cargo on entry
- Visual: Treasure rains down from vehicle
- Score increases by total cargo value
- UI: Score counter animates with "+250!" popup
- Audio: "Cash register" sound
- Cargo resets to 0

### 2.4 Vehicle Physics

#### Movement
- Input: Touch/tilt controls for mobile
- Acceleration: Progressive (not instant)
- Turning: Realistic physics-based
- Terrain: Affects speed (uphill slower, downhill faster)

#### Physics Properties
- Mass: Affects momentum and turning
- Drag: Air resistance for realistic feel
- Gravity: Standard Unity gravity
- Collision: Bounces off walls and obstacles

---

## 3. Vehicles

### Vehicle 1: Starter Bulldozer (Free)
- **Speed**: 8 m/s
- **Cargo Capacity**: 10 treasures
- **Collection Radius**: 1.5m
- **Special Ability**: None
- **Best For**: Learning the game, Levels 1-3
- **Unlock**: Available from start

### Vehicle 2: Dual-Scoop Loader (1,000 gold)
- **Speed**: 8 m/s
- **Cargo Capacity**: 15 treasures (+50%)
- **Collection Radius**: 2.5m (+67%)
- **Special Ability**: "Auto-Collect" - Treasures automatically fly to you from further away
- **Best For**: Levels with scattered treasures, efficiency runs
- **Unlock**: Complete Level 3 OR pay 1,000 gold

### Vehicle 3: Nitro Hauler (2,500 gold)
- **Speed**: 8 m/s (base), 16 m/s (nitro)
- **Cargo Capacity**: 10 treasures
- **Collection Radius**: 1.5m
- **Special Ability**: "Nitro Boost" - 3-second speed burst, 10-second cooldown
- **Best For**: Speedruns, time-based challenges, chaining distant gates
- **Unlock**: Complete Level 6 OR pay 2,500 gold

### Vehicle 4: Mega Vault Truck (5,000 gold) [Post-launch v1.1]
- **Speed**: 7 m/s
- **Cargo Capacity**: 25 treasures (+150%)
- **Collection Radius**: 1.5m
- **Special Ability**: "Gate Magnet" - Shows optimal path to highest-value gate chain
- **Best For**: 3-star runs, maximum score challenges
- **Unlock**: Complete Level 8 with 3 stars OR pay 5,000 gold

---

## 4. Level Design Specifications

### Level 1: First Haul (Tutorial Extension)

**Difficulty**: Tutorial
**Layout**: Linear path (spawn → treasures → gate → deposit)
**Size**: 150m x 100m (smaller)

**Treasures**:
- 5x Small (value: 10 each, total: 50)

**Gates**:
- 1x x2 gate (positioned after treasure cluster)

**Target Scores**:
- 1 Star: 100 points (base requirement)
- 2 Stars: 150 points (collect all, x2 gate once)
- 3 Stars: 200 points (collect all, x2 gate twice)

**Obstacles**: None (clear path)

**Teaching Goals**:
- Basic movement controls
- Treasure collection (touch to collect)
- Gate usage (drive through to multiply)
- Deposit zone interaction
- Cargo UI understanding

**Estimated Completion Time**: 2-3 minutes

---

### Level 2: Choose Wisely (Multiple Gates Intro)

**Difficulty**: Easy
**Layout**: Y-shaped fork (two paths merge at deposit)

**Treasures**:
- 8x Small (value: 10 each, total: 80)
- Evenly split between two paths

**Gates**:
- Left path: 1x x2 gate
- Right path: 1x x3 gate (slightly harder to reach)

**Target Scores**:
- 1 Star: 150 points
- 2 Stars: 225 points
- 3 Stars: 300 points

**Obstacles**: Small rock clusters forcing path choice

**Teaching Goals**:
- Strategic gate selection
- Risk vs reward (x3 gate harder to reach)
- Multiple-trip planning
- Path optimization

**Estimated Completion Time**: 3-5 minutes

---

### Level 3: Load Limits (Capacity Management)

**Difficulty**: Easy-Medium
**Layout**: Open arena with central deposit

**Treasures**:
- 15x Small (value: 10 each, total: 150)
- Clustered in 3 groups of 5

**Gates**:
- 2x x2 gates (different locations)
- 1x x3 gate (central, requires full cargo)

**Target Scores**:
- 1 Star: 250 points
- 2 Stars: 375 points
- 3 Stars: 500 points

**Obstacles**: Rock formations creating paths

**Teaching Goals**:
- Teach cargo capacity limits (Bulldozer holds 10)
- Encourage multiple trips
- Introduce gate chaining concept

**Unlock**: Dual-Scoop Loader becomes available

**Estimated Completion Time**: 4-6 minutes

---

### Level 4: Obstacle Course

**Difficulty**: Medium
**Layout**: Winding path with branching sections

**Treasures**:
- 12x Small (value: 10 each, total: 120)
- 2x Medium (value: 50 each, total: 100)
- Total available: 220

**Gates**:
- 2x x2 gates (easy access)
- 2x x3 gates (require navigation)

**Target Scores**:
- 1 Star: 350 points
- 2 Stars: 525 points
- 3 Stars: 700 points

**Obstacles**: Moving platforms, narrow bridges, ramps

**Teaching Goals**:
- Advanced vehicle control
- Medium treasure value recognition
- Obstacle navigation
- Time management

**Estimated Completion Time**: 5-7 minutes

---

### Level 5: Multiplication Mastery (Gate Chaining)

**Difficulty**: Medium
**Layout**: Circuit with multiple gate opportunities

**Treasures**:
- 10x Small (value: 10 each, total: 100)
- 3x Medium (value: 50 each, total: 150)
- Total available: 250

**Gates**:
- 3x x2 gates (in sequence)
- 2x x3 gates (alternate path)
- 1x x5 gate (requires precise path)

**Target Scores**:
- 1 Star: 500 points
- 2 Stars: 750 points
- 3 Stars: 1,000 points

**Obstacles**: Rotating barriers, timed gates

**Teaching Goals**:
- Gate chaining mastery (x2 → x2 → x3 = x12 total)
- High-value gate (x5) introduction
- Strategic cargo management
- Path optimization for maximum multiplier

**Estimated Completion Time**: 6-8 minutes

---

### Level 6: Speed Challenge

**Difficulty**: Medium-Hard
**Layout**: Large open area with distant zones

**Treasures**:
- 15x Small (value: 10 each, total: 150)
- 4x Medium (value: 50 each, total: 200)
- Total available: 350

**Gates**:
- 4x x2 gates (spread out)
- 2x x3 gates (corner locations)
- 1x x5 gate (central, high visibility)

**Target Scores**:
- 1 Star: 750 points
- 2 Stars: 1,125 points
- 3 Stars: 1,500 points

**Obstacles**: Long distances, jumps, speed ramps

**Teaching Goals**:
- Vehicle speed optimization (ideal for Nitro Hauler)
- Long-distance planning
- Jump mechanics
- Time pressure (optional timer for 3-star)

**Unlock**: Nitro Hauler becomes available

**Estimated Completion Time**: 7-9 minutes

---

### Level 7: Treasure Valley

**Difficulty**: Hard
**Layout**: Multi-level vertical environment

**Treasures**:
- 12x Small (value: 10 each, total: 120)
- 5x Medium (value: 50 each, total: 250)
- 1x Large (value: 200, total: 200)
- Total available: 570

**Gates**:
- 3x x2 gates (lower level)
- 3x x3 gates (mid level)
- 2x x5 gates (upper level, challenging)

**Target Scores**:
- 1 Star: 1,000 points
- 2 Stars: 1,500 points
- 3 Stars: 2,000 points

**Obstacles**: Vertical ramps, falling platforms, gravity zones

**Teaching Goals**:
- Large treasure introduction
- Vertical navigation
- Multi-level strategy
- Risk assessment (high-value vs safe routes)

**Estimated Completion Time**: 8-10 minutes

---

### Level 8: The Big Score (Final Challenge)

**Difficulty**: Hard
**Layout**: Large multi-area complex combining all mechanics

**Treasures**:
- 15x Small (value: 10 each, total: 150)
- 5x Medium (value: 50 each, total: 250)
- 1x Large (value: 200, total: 200)
- Total available: 600

**Gates**:
- 4x x2 gates (scattered)
- 3x x3 gates (mid-tier placement)
- 2x x5 gates (challenging access)
- 1x x10 gate (hidden, requires exploration)

**Target Scores**:
- 1 Star: 1,500 points
- 2 Stars: 2,250 points
- 3 Stars: 3,000 points (requires x10 gate)

**Obstacles**: All previous obstacle types combined

**Teaching Goals**:
- Master all game mechanics
- Exploration and discovery (x10 gate)
- Perfect execution challenge
- Replayability for optimal score

**Unlock**: Mega Vault Truck (post-launch), all cosmetics

**Estimated Completion Time**: 10-12 minutes

---

## 5. Win/Fail Conditions

### Win Conditions
- Reach target score for level (displayed at start)
- 1 Star: Meet minimum target score
- 2 Stars: Reach 1.5x target score
- 3 Stars: Reach 2x target score

### Fail Conditions
- **None** (casual-friendly design)
- Player can retry unlimited times
- Vehicle respawns at checkpoint if falls off map (keeps current cargo)
- Optional "Hard Mode" in post-launch: 3-hit vehicle destruction

---

## 6. Progression System

### Level Unlocking
- Linear progression: Complete Level N to unlock Level N+1
- All levels replayable for 3-star completion
- Each level has unique challenge

### Currency System

#### Gold (Soft Currency)
**Earning**:
- Level completion: 50-200 gold (based on stars)
- Daily login bonus: 50 gold
- Watching rewarded video: 100 gold

**Spending**:
- Vehicle unlocks (1,000 - 5,000 gold)
- Cosmetic skins (500 - 2,000 gold)
- Continue/retry (optional, 50 gold)

#### Gems (Hard Currency) [Optional]
**Earning**:
- IAP only ($0.99 = 100 gems)
- Special events

**Spending**:
- Premium skins (200-500 gems)
- Gold exchange (100 gems = 500 gold)

### Vehicle Unlocking
| Vehicle | Unlock Method |
|---------|---------------|
| Starter Bulldozer | Default |
| Dual-Scoop Loader | Complete Level 3 OR 1,000 gold |
| Nitro Hauler | Complete Level 6 OR 2,500 gold |
| Mega Vault Truck | Complete Level 8 (3 stars) OR 5,000 gold |

---

## 7. UI/UX Design

### Main Menu
- Play button (large, center)
- Vehicle selection
- Settings (audio, controls)
- Shop (cosmetics)
- Daily rewards

### In-Game HUD
- **Top Left**: Current score
- **Top Right**: Star progress bar
- **Bottom Left**: Cargo counter (5/10)
- **Bottom Right**: Pause button
- **Center**: Target score reminder (first 10 seconds)

### Level Complete Screen
- Star rating (1-3)
- Score achieved
- Gold earned
- Buttons: Next Level, Retry, Main Menu

---

## 8. Audio Design

### Sound Effects (SFX)
- **Collection**: "Ding" sound (satisfying chime)
- **Multiplication**: "Ka-ching" (cash register)
- **Deposit**: "Cascade" sound (treasure pouring)
- **Gate Pass**: "Whoosh" with echo
- **Vehicle Movement**: Engine hum (looping)
- **UI Click**: Soft tap sound
- **Level Complete**: Triumphant fanfare

### Music
- **Main Menu**: Upbeat, adventurous theme
- **Gameplay**: Dynamic loop that intensifies near goals
- **Level Complete**: Victory jingle

### Audio Settings
- Master volume slider
- Music volume slider
- SFX volume slider
- Mute toggle

---

## 9. Monetization

### Free-to-Play Model
- All gameplay content free
- No energy system
- No forced ads

### Rewarded Video Ads
- Optional, player-initiated
- Rewards: 100 gold per view
- Placement: Level complete screen
- Frequency: Unlimited (with cooldown)

### In-App Purchases
- **Starter Pack**: $0.99 (500 gold)
- **Gold Pile**: $4.99 (3,000 gold)
- **Premium Skin Bundle**: $2.99 (5 exclusive skins)
- **Remove Ads**: $4.99 (cosmetic only, removes banners)

### Cosmetic Skins
- 5 free skins (earned through gameplay)
- 10 premium skins (IAP or high gold cost)
- Skins: Vehicle colors, patterns, effects

---

## 10. Technical Requirements

### Minimum Specs
- **iOS**: iPhone 11 or newer
- **iOS Version**: 14.0+
- **Storage**: 150MB
- **RAM**: 2GB

### Unity Packages Required
- Universal Render Pipeline (URP)
- Input System
- Firebase SDK (Analytics, Crashlytics)
- Unity Ads
- Unity IAP

### Performance Targets
- **Frame Rate**: 60 FPS
- **Load Time**: <3 seconds
- **Battery Impact**: Low (conservative rendering)

---

## 11. Scope & Timeline

### MVP Features (Phase 1-6: 26 weeks)
- ✅ 1 environment (mining cave)
- ✅ 3 vehicles with unique abilities
- ✅ 8 levels with progressive difficulty
- ✅ 3 gate types (x2, x3, x5)
- ✅ Complete progression system
- ✅ Tutorial (90 seconds)
- ✅ Basic cosmetics (5 free skins)
- ✅ Rewarded video ads
- ✅ Cosmetic IAP

### Post-Launch (v1.1+)
- Additional levels (5 per update)
- New vehicles
- iPad support
- Android port
- Seasonal events
- Leaderboards

---

## 12. Success Metrics

### KPIs to Track
- **Retention**: D1 (40%), D7 (20%), D30 (10%)
- **Session Length**: 8-12 minutes average
- **ARPDAU**: $0.05 (bootstrap), $0.15 (with marketing)
- **CPI**: <$0.50 organic
- **Star Completion**: 70% of players earn 1 star on Level 1

### Analytics Events
- Level start/complete
- Vehicle unlocked
- Gate type used
- Treasure collected (by type)
- IAP conversion
- Ad views

---

## 13. Post-Launch Content Roadmap

### v1.1 (Week 28)
- 5 new levels (Levels 9-13)
- 1 new vehicle (Mega Vault Truck)
- iPad support
- Bug fixes

### v1.2 (Week 32)
- Seasonal event system
- 5 new levels (Levels 14-18)
- 10 new cosmetic skins
- Leaderboards

### v1.3 (Week 36)
- Android port
- Cloud save (cross-platform)
- Daily challenges
- Achievement system

---

## 14. Risk Mitigation

### Technical Risks
- **Physics instability**: Extensive testing, conservative physics settings
- **Performance issues**: URP optimization, aggressive LOD
- **iOS submission rejection**: Follow guidelines strictly, test thoroughly

### Design Risks
- **Gameplay not fun**: Playtesting early, iterate on feedback
- **Progression too slow**: Balance testing, adjust gold rewards
- **Ads too aggressive**: Opt-in only, player-friendly placement

### Business Risks
- **Low retention**: A/B test tutorial, improve onboarding
- **Poor monetization**: Experiment with IAP pricing
- **Market saturation**: Focus on "honest game" differentiation

---

## Document Approval

### Sign-off Required Before Phase 1
- [ ] Game Designer: Reviewed and approved
- [ ] Lead Developer: Technical feasibility confirmed
- [ ] Product Owner: Scope and budget approved
- [ ] Art Director: Asset requirements clear

**Status**: Phase 0 - Awaiting Approval

---

*End of Game Design Document*
