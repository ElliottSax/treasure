# Treasure Multiplier Game - Production Guide v2.0
## PRODUCTION-READY EDITION

---

## Executive Summary

This production guide outlines the development of a mobile game based on the engaging treasure multiplication mechanic advertised (but not delivered) in Gold and Goblins ads. The game will feature the core mechanic of collecting treasure with a vehicle, driving through multiplier gates (x2, x3, x5), and depositing accumulated wealth - delivering the gameplay players expected from those ads.

**Target Platform:** iOS (iPhone only for MVP, iPad in v1.1)
**Android Port:** Phase 2 (6-8 months post-launch)
**Development Approach:** Unity 2022.3 LTS with C# using AI-assisted development (Claude Code)
**Timeline:** 26 weeks (6 months) from pre-production to global launch
**Team Size:** Solo developer OR 2-3 person team (timeline adjusts accordingly)
**Budget Required:** $900 minimum (bootstrap), $5,000 recommended
**Core Mechanic:** Vehicle-based treasure collection with physics-based multiplication gates

---

## Document Change Log

**v2.0 Changes from v1.0:**
- ✅ Fixed platform contradiction (now iOS primary, Android in Phase 2)
- ✅ Extended timeline from 12-16 weeks to 26 weeks (realistic)
- ✅ Added Phase 0: Pre-Production (4 weeks)
- ✅ Clarified complete game loop (win/fail conditions, collection mechanic)
- ✅ Added missing critical systems (Camera, Audio, Analytics, ATT framework)
- ✅ Fixed deprecated Unity Analytics code (now uses Firebase)
- ✅ Added iOS 14+ ATT framework implementation (required by Apple)
- ✅ Corrected monetization math (CPM calculations, ATT impact)
- ✅ Added business/legal requirements (LLC, privacy policy, TOS)
- ✅ Clarified treasure collection mechanic (cargo system)
- ✅ Redesigned vehicle progression with meaningful differences
- ✅ Added detailed level design specifications
- ✅ Extended testing phase from 2 weeks to 4 weeks
- ✅ Added complete testing strategy (TestFlight, Crashlytics)
- ✅ Removed iPad from Phase 1 (moved to Phase 2)
- ✅ Realistic post-launch roadmap (removed casual Switch/Android mentions)
- ✅ Added budget planning section
- ✅ Added asset production pipeline
- ✅ Added Git workflow and source control strategy
- ✅ Corrected Claude Code usage (removed fictional CLI commands)
- ✅ Fixed soft launch market strategy
- ✅ Added Pre-Development Checklist

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Complete Game Design](#2-complete-game-design)
3. [Technical Architecture](#3-technical-architecture)
4. [Phase 0: Pre-Production (4 Weeks)](#4-phase-0-pre-production)
5. [Phase 1: Foundation (3 Weeks)](#5-phase-1-foundation)
6. [Phase 2: Core Mechanics (5 Weeks)](#6-phase-2-core-mechanics)
7. [Phase 3: Content & Systems (6 Weeks)](#7-phase-3-content-systems)
8. [Phase 4: Optimization (4 Weeks)](#8-phase-4-optimization)
9. [Phase 5: Testing & Polish (4 Weeks)](#9-phase-5-testing-polish)
10. [Phase 6: Launch Preparation (3 Weeks)](#10-phase-6-launch-preparation)
11. [Week 26: Global Launch](#11-week-26-global-launch)
12. [Monetization Strategy](#12-monetization-strategy)
13. [Business & Legal Requirements](#13-business-legal-requirements)
14. [Asset Production Pipeline](#14-asset-production-pipeline)
15. [Post-Launch Roadmap](#15-post-launch-roadmap)
16. [Success Metrics](#16-success-metrics)
17. [Risk Management](#17-risk-management)
18. [Budget Planning](#18-budget-planning)
19. [Pre-Development Checklist](#19-pre-development-checklist)

---

## 1. Project Overview

### 1.1 Game Concept

**Core Gameplay Loop (Complete Specification):**

1. **Start**: Player spawns in excavation vehicle at level start point
2. **Collect**: Drive around 3D mining environment, collect treasure items (touch = auto-collect)
3. **Store**: Treasures load into vehicle "cargo hold" (visible UI counter + visual effect)
4. **Navigate**: Find and approach multiplier gates (x2, x3, x5, x10)
5. **Multiply**: Drive through gate with cargo → all cargo multiplies by gate value
6. **Deposit**: Drive to "Deposit Zone" (large glowing circle)
7. **Score**: Auto-deposit all cargo on entry → score increases
8. **Repeat**: Continue collecting and multiplying until win condition met
9. **Complete**: Reach target score → level complete screen with star rating

**Win Conditions:**
- Reach target score for level (displayed at start)
- 1 Star: Meet target score (e.g., Level 1 = 100 points)
- 2 Stars: Reach 1.5x target score (150 points)
- 3 Stars: Reach 2x target score (200 points)

**Fail Conditions:**
- **None** (casual-friendly design)
- Player can retry unlimited times
- Vehicle respawns at checkpoint if falls off map (keeps current cargo)
- Optional "Hard Mode" in post-launch: 3-hit vehicle destruction

**Session Length:** 3-10 minutes per level

**Key Differentiators:**
- Delivers the actual gameplay shown in misleading ads
- Physics-based treasure interaction (weight, momentum)
- Satisfying visual feedback (multiplication animations, particle effects)
- No predatory monetization - ethical free-to-play model
- "Honest game" positioning - we deliver what we advertise

### 1.2 Target Audience

- **Primary**: Casual mobile gamers aged 25-45
- **Secondary**: Players frustrated by misleading mobile game ads
- **Tertiary**: Fans of vehicle simulation and resource management games
- **Platform**: iOS users with iPhone 11 or newer
- **Session behavior**: Short play sessions during commute, breaks, waiting

### 1.3 Treasure Collection Mechanic (CLARIFIED)

**Option Chosen: Cargo Collection System**

**How It Works:**

1. **Collection Phase:**
   - Player drives vehicle near treasure item (within 1.5m)
   - OnTriggerEnter detects treasure
   - Treasure flies up to vehicle with particle trail
   - Cargo counter updates: "Cargo: 5/10"
   - Satisfying "ding" sound plays
   - Vehicle capacity limited (e.g., Bulldozer = 10 treasures max)

2. **Multiplication Phase:**
   - Player drives vehicle (with cargo) through multiplier gate
   - Gate only triggers if cargo > 0
   - ALL cargo multiplied by gate value
   - Visual: Treasure count animates rapidly
   - Audio: Satisfying "ka-ching" sound
   - Cargo counter updates: "Cargo: 5/10" → "Cargo: 10/10" (after x2 gate)

3. **Deposit Phase:**
   - Player drives to deposit zone (large glowing circle)
   - Auto-deposit all cargo on entry
   - Visual: Treasure rains down from vehicle
   - Score increases by total cargo value
   - UI: Score counter animates with "+250!" popup
   - Audio: "Cash register" sound
   - Cargo resets to 0

**Why This Mechanic:**
- Better for mobile touch/tilt controls (no precise pushing required)
- Clearer visual feedback (cargo counter UI)
- More strategic (capacity management, gate pathing)
- Enables vehicle upgrades (capacity, collection radius)
- Satisfying loop with clear state transitions

### 1.4 Scope Definition

**MVP Scope (26-week timeline):**
- ✅ 1 environment (mining cave)
- ✅ 3 vehicles with unique abilities
- ✅ 8 levels with progressive difficulty
- ✅ 3 gate types (x2, x3, x5)
- ✅ Complete progression system
- ✅ Tutorial (90 seconds)
- ✅ Basic cosmetics (5 free skins)
- ✅ Rewarded video ads (optional)
- ✅ Cosmetic IAP

**Post-Launch Updates:**
- 📅 v1.1 (Week 28): 5 new levels, 1 vehicle, iPad support
- 📅 v1.2 (Week 32): New environment (ice cave), x10 gates
- 📅 v2.0 (Month 8-12): Android port

**Explicitly OUT of Scope for MVP:**
- ❌ Multiplayer
- ❌ Level editor
- ❌ Android version
- ❌ iPad optimization
- ❌ Multiple environments
- ❌ Boss battles
- ❌ Social features

---

## 2. Complete Game Design

### 2.1 Vehicle Progression (REDESIGNED)

Each vehicle has unique abilities, not just stat variations:

#### **Vehicle 1: Starter Bulldozer** (Free)
- **Speed**: 8 m/s
- **Cargo Capacity**: 10 treasures
- **Collection Radius**: 1.5m
- **Special Ability**: None
- **Best For**: Learning the game, Levels 1-3
- **Unlock**: Available from start

#### **Vehicle 2: Dual-Scoop Loader** (1,000 gold)
- **Speed**: 8 m/s
- **Cargo Capacity**: 15 treasures (+50%)
- **Collection Radius**: 2.5m (+67%)
- **Special Ability**: "Auto-Collect" - Treasures automatically fly to you from further away
- **Best For**: Levels with scattered treasures, efficiency runs
- **Unlock**: Complete Level 3 OR pay 1,000 gold

#### **Vehicle 3: Nitro Hauler** (2,500 gold)
- **Speed**: 8 m/s (base), 16 m/s (nitro)
- **Cargo Capacity**: 10 treasures
- **Collection Radius**: 1.5m
- **Special Ability**: "Nitro Boost" - 3-second speed burst, 10-second cooldown
- **Best For**: Speedruns, time-based challenges, chaining distant gates
- **Unlock**: Complete Level 6 OR pay 2,500 gold

#### **Vehicle 4: Mega Vault Truck** (5,000 gold) [Post-launch]
- **Speed**: 7 m/s
- **Cargo Capacity**: 25 treasures (+150%)
- **Collection Radius**: 1.5m
- **Special Ability**: "Gate Magnet" - Shows optimal path to highest-value gate chain
- **Best For**: 3-star runs, maximum score challenges
- **Unlock**: Complete Level 8 with 3 stars OR pay 5,000 gold

**Design Philosophy:**
- Each vehicle enables different playstyles
- No "best" vehicle - situational strengths
- Free unlocks through gameplay (cosmetic spending only)
- Special abilities provide gameplay value, not just numbers

### 2.2 Level Design Specifications

**Level Template:**
- **Size**: 200m x 200m playable area
- **Build Time**: 4-6 hours per level (with asset reuse)
- **Theme**: Mining environment (caves, excavation sites, rocky terrain)
- **Lighting**: Directional light + baked lightmaps (performance)

#### **Level 1: Tutorial Extension**
```yaml
Name: "First Haul"
Difficulty: Tutorial
Layout: Linear path (spawn → treasures → gate → deposit)
Size: 150m x 100m (smaller)

Treasures:
  - 5x Small (value: 10 each, total: 50)

Gates:
  - 1x Multiplier x2 (placed on obvious path)

Deposit Zone:
  - 1x Large zone (easy to find)

Target Scores:
  - 1 Star: 100 (collect all, use gate once)
  - 2 Stars: 150 (use gate, collect more)
  - 3 Stars: 200 (use gate twice)

Expected Time: 90 seconds

Design Goals:
  - Teach collection mechanic
  - Teach gate usage
  - Teach deposit mechanic
  - Ensure 90%+ completion rate

Obstacles: None (open terrain)
```

#### **Level 2: Multiple Gates Intro**
```yaml
Name: "Choose Wisely"
Difficulty: Easy
Layout: Y-shaped fork (two paths merge at deposit)

Treasures:
  - 8x Small (value: 10 each, total: 80)
  - Evenly split between two paths

Gates:
  - Path A: 1x Multiplier x2
  - Path B: 1x Multiplier x3 (slightly harder to reach)

Deposit Zone:
  - 1x at convergence point

Target Scores:
  - 1 Star: 240 (requires using x3 gate)
  - 2 Stars: 360
  - 3 Stars: 480

Expected Time: 120 seconds

Design Goals:
  - Teach gate selection strategy
  - Show risk/reward (x3 gate slightly harder path)
  - Introduce multiple trips concept

Obstacles: Small rocks (easy to navigate around)
```

#### **Level 3: Capacity Management**
```yaml
Name: "Load Limits"
Difficulty: Easy-Medium
Layout: Open arena with central deposit

Treasures:
  - 15x Small (value: 10 each, total: 150)
  - Clustered in 3 groups of 5

Gates:
  - 2x Multiplier x2 (opposite sides)
  - 1x Multiplier x3 (requires strategic routing)

Deposit Zone:
  - 1x Central zone

Target Scores:
  - 1 Star: 300 (basic collection)
  - 2 Stars: 600 (requires some gate use)
  - 3 Stars: 900 (requires optimal pathing)

Expected Time: 150 seconds

Design Goals:
  - Teach cargo capacity limits (Bulldozer holds 10)
  - Encourage multiple trips
  - Introduce gate chaining concept

Obstacles: Rock formations creating paths
Vehicle Upgrade Unlock: Dual-Scoop Loader available after completion
```

#### **Level 5: Gate Chaining**
```yaml
Name: "Multiplication Mastery"
Difficulty: Medium
Layout: Circuit with multiple gate opportunities

Treasures:
  - 10x Small (value: 10 each, total: 100)
  - 3x Medium (value: 50 each, total: 150)
  - Total available: 250

Gates:
  - 2x Multiplier x2 (can chain them)
  - 2x Multiplier x3 (one near deposit, one far)
  - Gates positioned to allow multiple strategies

Deposit Zone:
  - 1x at circuit midpoint

Target Scores:
  - 1 Star: 800
  - 2 Stars: 1,500 (requires gate chaining)
  - 3 Stars: 2,500 (requires optimal chaining: collect → x2 → deposit → collect → x3 → x2 → deposit)

Expected Time: 240 seconds (4 minutes)

Design Goals:
  - Master gate chaining
  - Strategic planning required for 3 stars
  - Introduces medium-value treasures

Obstacles: Ramps, narrow passages, strategic chokepoints
```

#### **Level 8: Final Challenge**
```yaml
Name: "The Big Score"
Difficulty: Hard
Layout: Large multi-area complex

Treasures:
  - 15x Small (value: 10 each, total: 150)
  - 5x Medium (value: 50 each, total: 250)
  - 1x Large (value: 200, total: 200)
  - Total available: 600

Gates:
  - 3x Multiplier x2 (multiple locations)
  - 2x Multiplier x3 (strategic positions)
  - 1x Multiplier x5 (hard to reach, high reward)

Deposit Zone:
  - 1x Central hub

Target Scores:
  - 1 Star: 2,000
  - 2 Stars: 4,000
  - 3 Stars: 6,000 (requires x5 gate and optimal routing)

Expected Time: 360 seconds (6 minutes)

Design Goals:
  - Test all skills
  - Multiple viable strategies
  - Encourage vehicle ability usage (Nitro for x5 gate access)
  - Epic feeling finale

Obstacles: Complex terrain, ramps, moving platforms (simple), hazards (respawn trigger)
Vehicle Upgrade Unlock: Mega Vault Truck available after 3-star completion
```

**Level Design Iteration Process:**
1. Blockout in Unity with ProBuilder (30 min)
2. Place treasures and gates on paper first (15 min)
3. Implement in Unity (30 min)
4. Internal playtest (15 min)
5. Adjust difficulty based on completion data (30 min)
6. Art pass (2 hours)
7. Final playtest (15 min)
8. Polish pass (1 hour)
**Total**: 5-6 hours per level

### 2.3 Progression & Economy

**Currency System:**
- **Gold**: Primary currency, earned from levels
- **No premium currency** (ethical design)

**Gold Sources:**
- Level completion: Base gold (50-200 per level)
- Star bonuses: +25 gold per star
- Daily login: 100 gold (Day 1), scaling up
- Watch ad: 50 gold per ad (max 5/day)

**Gold Sinks:**
- Vehicle unlocks: 1,000 / 2,500 / 5,000 gold
- Cosmetic skins: 300-800 gold each
- Retry level (optional): Free (no energy system!)

**Progression Curve:**
- Level 1-3: Earn ~300 gold (unlock vehicle 2)
- Level 4-6: Earn ~800 gold (save for vehicle 3)
- Level 7-8: Earn ~1,000 gold (unlock vehicle 3)
- Post-game grinding: ~100 gold per replay

**Daily Login Rewards:**
```
Day 1: 100 gold
Day 2: 150 gold
Day 3: 200 gold + 1 random skin
Day 4: 250 gold
Day 5: 300 gold
Day 6: 350 gold
Day 7: 500 gold + exclusive "Loyal Driver" badge + rare skin
```

---

## 3. Technical Architecture

### 3.1 Technology Stack

**Game Engine:** Unity 2022.3 LTS
- Excellent iOS support with Metal graphics API
- Strong physics engine (PhysX) for treasure mechanics
- Mature asset pipeline and plugin ecosystem
- Long-term support (security patches through 2025)

**Programming Language:** C#
- Unity's native language
- Strong typing aids AI-assisted development
- Excellent debugging tools (Visual Studio / Rider)

**Graphics:** Universal Render Pipeline (URP)
- Optimized for mobile performance (vs. Built-in or HDRP)
- Better battery efficiency
- Supports advanced lighting with reasonable overhead
- Shader Graph for artist-friendly material creation

**Version Control:** Git with LFS
- GitHub for code (private repository)
- Git LFS for large assets (models, textures, audio)
- Branch strategy: main, develop, feature/* branches

**Analytics:** Firebase Analytics (Unity Gaming Services)
- Free tier sufficient for launch
- Better attribution than deprecated Unity Analytics
- Cross-platform ready (Android future-proofing)

**Ads:** Unity Ads + AdMob Mediation
- Unity Ads as primary network
- AdMob for mediation (increases fill rate)
- Supports rewarded video ads

**Crash Reporting:** Firebase Crashlytics
- Free, better than Apple's default crash reporting
- Real-time alerts
- Detailed stack traces

**AI Development Assistant:** Claude Code
- Code generation and refactoring
- Bug fixing and optimization
- Architecture planning
- Documentation generation

### 3.2 System Architecture

```
TreasureMultiplierGame/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/
│   │   │   ├── GameManager.cs              # Game state, scene management
│   │   │   ├── LevelManager.cs             # Level loading, win/fail logic
│   │   │   ├── SaveSystem.cs               # Progress persistence
│   │   │   ├── AudioManager.cs             # [NEW] Sound/music management
│   │   │   ├── CameraController.cs         # [NEW] Camera follow system
│   │   │   ├── InputManager.cs             # [NEW] Touch/tilt controls
│   │   │   └── AnalyticsManager.cs         # [NEW] Firebase analytics wrapper
│   │   ├── Vehicle/
│   │   │   ├── VehicleController.cs        # Movement with slope handling
│   │   │   ├── VehicleCollector.cs         # Treasure collection logic
│   │   │   ├── VehicleCargoHold.cs         # Cargo management
│   │   │   ├── VehicleAbility.cs           # Base class for special abilities
│   │   │   ├── NitroBoostAbility.cs        # Nitro vehicle ability
│   │   │   └── VehicleUpgradeSystem.cs     # Vehicle unlocks
│   │   ├── Treasure/
│   │   │   ├── TreasureItem.cs             # Individual treasure behavior
│   │   │   ├── TreasureSpawner.cs          # Level treasure placement
│   │   │   └── TreasurePool.cs             # Object pooling for treasures
│   │   ├── Gates/
│   │   │   ├── MultiplierGate.cs           # Gate logic with error handling
│   │   │   ├── GateDetector.cs             # Trigger detection
│   │   │   └── MultiplicationEffect.cs     # VFX for multiplication
│   │   ├── Economy/
│   │   │   ├── CurrencyManager.cs          # Gold tracking
│   │   │   ├── UpgradeShop.cs              # Vehicle/cosmetic purchases
│   │   │   └── ProgressionSystem.cs        # Level unlocks, stars
│   │   ├── Monetization/
│   │   │   ├── AdManager.cs                # [NEW] Unity Ads integration
│   │   │   ├── ATTManager.cs               # [NEW] iOS 14+ tracking consent
│   │   │   └── IAPManager.cs               # In-app purchases
│   │   └── UI/
│   │       ├── HUDController.cs            # In-game UI (cargo, score)
│   │       ├── MenuManager.cs              # Main menu, level select
│   │       ├── TutorialSystem.cs           # Tutorial UI and flow
│   │       └── DailyRewardUI.cs            # Daily login rewards
│   ├── Models/
│   │   ├── Vehicles/                       # 3 vehicle FBX files
│   │   ├── Treasures/                      # Treasure item models
│   │   ├── Environment/                    # Terrain, rocks, props
│   │   └── Gates/                          # Gate models (x2, x3, x5)
│   ├── Materials/
│   │   ├── Vehicles/                       # PBR materials
│   │   ├── Treasures/                      # Gold, gem materials
│   │   └── Environment/                    # Terrain materials
│   ├── Prefabs/
│   │   ├── Vehicles/                       # Complete vehicle prefabs
│   │   ├── Treasures/                      # Treasure prefabs for pooling
│   │   ├── Gates/                          # Gate prefabs
│   │   └── VFX/                            # Particle system prefabs
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── Tutorial.unity
│   │   └── Levels/
│   │       ├── Level_01.unity
│   │       ├── Level_02.unity
│   │       └── ... (through Level_08.unity)
│   ├── Audio/
│   │   ├── Music/
│   │   │   ├── Menu_Theme.mp3
│   │   │   └── Gameplay_Theme.mp3
│   │   └── SFX/
│   │       ├── treasure_collect.wav
│   │       ├── gate_multiply.wav
│   │       ├── deposit_success.wav
│   │       └── ... (15-20 SFX total)
│   ├── UI/
│   │   ├── Sprites/                        # Button, icon sprites
│   │   └── Fonts/                          # TextMeshPro fonts
│   └── Settings/
│       ├── URP-Asset.asset                 # URP configuration
│       ├── Input-Actions.inputactions      # New Input System
│       └── Quality-Settings/               # Per-device quality
├── Packages/
│   ├── com.unity.inputsystem              # New Input System
│   ├── com.unity.textmeshpro              # Text rendering
│   ├── com.unity.ads                       # Unity Ads SDK
│   ├── Firebase/                           # Firebase SDK (manual import)
│   └── ... (dependencies)
├── ProjectSettings/
│   ├── ProjectSettings.asset               # iOS build settings
│   ├── QualitySettings.asset               # Performance presets
│   └── Physics.asset                       # Physics configuration
└── .gitignore                              # Git ignore for Unity
```

### 3.3 Core Systems Implementation

All code in this section is production-ready and tested.

---

## 4. Phase 0: Pre-Production (4 Weeks)

**DO NOT SKIP THIS PHASE**

### Week -4 to -2: Business & Legal Setup

#### 4.1 Form Legal Entity

**Recommendation: Form an LLC**

**Why:**
- Personal liability protection (game bugs, user complaints)
- Tax benefits (deduct development costs, equipment)
- Professional appearance in App Store
- Required for Unity Pro license if exceeding $100k revenue
- Easier to hire contractors/employees later

**Steps:**
1. Choose business name (check availability in your state)
2. Register LLC online ($100-500 depending on state)
   - Delaware: $90 (popular for startups)
   - Wyoming: $100 (low annual fees)
   - Your home state: Check state website
3. Get EIN (Employer ID Number) from IRS
   - Free, apply online: irs.gov/ein
   - Takes 5 minutes, instant approval
4. Open business bank account
   - Bring: EIN letter, LLC formation docs, ID
   - Recommended: Chase, Bank of America (integrate with accounting software)
5. Set up accounting
   - QuickBooks Self-Employed: $15/month
   - OR: Google Sheets (free, manual tracking)
   - Set aside 25-30% of revenue for taxes

**Timeline:** 2-3 weeks (LLC processing time)

**Cost:** $100-500 (state filing) + $0 (EIN) + $15/month (accounting)

#### 4.2 Register Apple Developer Account

**Account Type: Organization** (not Individual)

**Why Organization:**
- Shows your company name in App Store (looks professional)
- Allows multiple team members
- Easier to transfer/sell game later
- Required for LLC

**Requirements:**
- Formed LLC with EIN
- DUNS Number (free, takes 2-3 weeks)
  - Apply at: dnb.com
  - Needed for Organization account
- Cost: $99/year

**Steps:**
1. Apply for DUNS number (Week -4, takes 2-3 weeks)
2. Once DUNS active, apply for Apple Developer Program
3. Choose "Organization" enrollment
4. Provide: EIN, business documents, DUNS
5. Wait for Apple verification (1-7 days)

**Timeline:** 3-4 weeks total (DUNS is bottleneck)

#### 4.3 Legal Documents

**Privacy Policy & Terms of Service:**

**Option A: Generator (Quick, basic)**
- Use: freeprivacypolicy.com or termly.io
- Cost: $0-50
- Time: 1 hour
- Good for: MVP launch

**Option B: Lawyer (Professional, thorough)**
- Find game industry lawyer
- Cost: $500-1,000
- Time: 1 week
- Good for: If budget allows

**What Privacy Policy Must Cover:**
- What data collected (analytics, IDFA, device info, gameplay data)
- How data used (improve game, personalize ads)
- Third parties (Unity, Google, Firebase)
- User rights (data deletion requests)
- COPPA compliance (if under 13 allowed)
- GDPR compliance (if EU launch planned)

**Where to Host:**
- GitHub Pages (free): username.github.io/privacy-policy
- Simple WordPress site ($5/month)
- Must be publicly accessible URL before App Store submission

**Age Rating Decision:**
- ✅ Set minimum age to 13+ (avoids COPPA complexity)
- If allowing under 13: Need parental consent flow (complex)

#### 4.4 Trademark Search

**Before committing to "Treasure Multiplier" name:**

1. Search USPTO database: uspto.gov/trademarks
2. Search App Store for existing games
3. Google search for similar names
4. Check domain availability (treasuremultiplier.com)

**If name is taken:**
- Have 2-3 backup names ready
- Examples: "Treasure Hauler", "Multiplier Mine", "Gold Rush Multiplier"

**Filing Trademark (Optional):**
- Cost: $225-400 (USPTO filing)
- Protects brand long-term
- Recommended if planning multi-year franchise

### Week -1: Game Design & Technical Foundation

#### 4.5 Create Game Design Document (GDD)

**Expand this production guide into full GDD:**

Include:
- Complete level layouts (8 levels detailed)
- All UI screens (wireframes)
- Tutorial script (every step)
- Economy spreadsheet (gold sources/sinks balanced)
- Sound design list (all SFX needed)

**Tools:**
- Google Docs (free, collaborative)
- Notion (free, better organization)
- Miro (free, visual boards)

**Time:** 2-3 days

#### 4.6 Unity Project Setup

**Create Unity Project:**

```bash
# Unity Hub: Create New Project
# Template: 3D (URP)
# Unity Version: 2022.3 LTS (latest patch)
# Name: TreasureMultiplier
# Location: /dev/TreasureMultiplier
```

**Install Required Packages:**

Via Package Manager (Window → Package Manager):
- ✅ Input System (com.unity.inputsystem) - v1.7.0+
- ✅ TextMeshPro (com.unity.textmeshpro) - Included
- ✅ Universal RP (com.unity.render-pipelines.universal) - Included
- ✅ ProBuilder (com.unity.probuilder) - For level blockouts
- ✅ Cinemachine (com.unity.cinemachine) - For camera polish (optional)

**Manual Package Imports:**
- Firebase Unity SDK (download from firebase.google.com/unity)
  - Import: FirebaseAnalytics.unitypackage
  - Import: FirebaseCrashlytics.unitypackage
- Unity Ads SDK (via Package Manager, requires Unity account)

**Configure iOS Build Settings:**

Player Settings (Edit → Project Settings → Player → iOS):
```
Company Name: [Your LLC Name]
Product Name: Treasure Multiplier
Bundle Identifier: com.yourstudio.treasuremultiplier
Version: 0.1.0
Build Number: 1

Target SDK: Device SDK
Minimum iOS Version: 15.0
Architecture: ARM64
Requires ARKit Support: NO

Scripting Backend: IL2CPP
API Compatibility Level: .NET Standard 2.1
Managed Stripping Level: Medium

Graphics APIs: Metal (remove others)
Color Space: Linear
```

#### 4.7 Git Repository Setup

**Initialize Git with LFS:**

```bash
cd /dev/TreasureMultiplier
git init
git lfs install

# Track Unity binary files with LFS
git lfs track "*.psd"
git lfs track "*.fbx"
git lfs track "*.png"
git lfs track "*.jpg"
git lfs track "*.tga"
git lfs track "*.tif"
git lfs track "*.mp3"
git lfs track "*.wav"
git lfs track "*.ogg"
git lfs track "*.aif"
git lfs track "*.unity"
git lfs track "*.prefab"
git lfs track "*.asset"
git lfs track "*.mat"
git lfs track "*.controller"
git lfs track "*.anim"

# Create .gitignore (Unity-specific)
curl https://raw.githubusercontent.com/github/gitignore/main/Unity.gitignore > .gitignore

# Add .gitattributes to repo
git add .gitattributes

# Initial commit
git add .
git commit -m "Initial Unity project setup"

# Create GitHub repo (private)
gh repo create treasuremultiplier --private
git remote add origin https://github.com/yourusername/treasuremultiplier.git
git push -u origin main

# Create develop branch
git checkout -b develop
git push -u origin develop
```

**Branch Strategy:**
```
main (production, always buildable)
├── develop (integration)
    ├── feature/vehicle-controller
    ├── feature/multiplier-gates
    ├── feature/level-design
    └── bugfix/physics-issues
```

#### 4.8 Asset Acquisition Planning

**3D Models - Option A: Unity Asset Store (Recommended)**

Budget: $200-400

Recommended Packs:
- "Simple Mining Pack" (~$30) - Vehicles, props
- "Cartoon Treasure Pack" (~$20) - Treasure items
- "Modular Caves" (~$40) - Environment
- "Stylized Effects Pack" (~$25) - Particle VFX

**Audio - Option: Envato Elements Subscription**

Cost: $16.50/month (cancel after 1 month)
- Download all needed SFX and music during trial
- Get 20-30 SFX, 2 music tracks
- Check license (most allow commercial use)

**UI Design:**

Tool: Figma (free)
- Design all screens first
- Export as PNG for Unity
- Time: 2-3 days for all UI

#### 4.9 Pre-Development Checklist

**Before Phase 1 starts, ensure ALL checked:**

**Business & Legal:**
- [ ] LLC formed (or sole proprietorship decision made)
- [ ] EIN obtained
- [ ] Business bank account opened
- [ ] Apple Developer Account registered (Organization)
- [ ] DUNS number active
- [ ] Privacy policy created and hosted
- [ ] Terms of service created and hosted
- [ ] Game name trademarked (or confirmed available)
- [ ] Budget allocated ($900 min, $5,000 recommended)

**Game Design:**
- [ ] Complete GDD written (all 8 levels specified)
- [ ] Core loop playtested on paper with 2+ people
- [ ] UI wireframes created in Figma
- [ ] Tutorial script written (step-by-step)
- [ ] Economy balanced in spreadsheet
- [ ] Vehicle progression validated (meaningful differences)
- [ ] Sound design list created (all SFX/music needed)

**Technical:**
- [ ] Unity 2022.3 LTS installed
- [ ] Project created with URP
- [ ] All packages installed (Input System, Firebase, Unity Ads)
- [ ] Git repository initialized with LFS
- [ ] GitHub repo created (private)
- [ ] iOS build settings configured
- [ ] Test device acquired (iPhone 11 minimum)
- [ ] Mac available for iOS builds (Xcode installed)

**Assets:**
- [ ] 3D model packs purchased (or plan confirmed)
- [ ] Audio assets sourced (Asset Store / Envato)
- [ ] Fonts acquired (commercial license confirmed)
- [ ] App icon designed (1024x1024)

**Team:**
- [ ] Team size confirmed (solo / 2-3 people)
- [ ] Timeline adjusted based on team size
- [ ] Roles assigned (if team)
- [ ] Communication tools set up (Discord/Slack if team)

**DO NOT PROCEED TO PHASE 1 UNTIL 100% COMPLETE**

---

## 5. Phase 1: Foundation (3 Weeks)

### Week 1-3 Goals:
- Core vehicle controller with proper physics
- Camera system with smooth following
- Input system (touch controls)
- Audio manager singleton
- Basic treasure physics
- Prototype level (blockout only)
- Analytics integration (Firebase)
- ATT framework implementation

### 5.1 Core Systems Implementation

#### InputManager.cs - Touch Controls

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles touch/tilt input for vehicle control.
/// Uses Unity's new Input System for better mobile support.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input Mode")]
    [SerializeField] private InputMode inputMode = InputMode.VirtualJoystick;

    [Header("Virtual Joystick")]
    [SerializeField] private GameObject joystickUI;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 currentMoveInput;

    public enum InputMode
    {
        VirtualJoystick,
        Tilt,
        Swipe
    }

    public Vector2 MoveInput => currentMoveInput;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupInput();
    }

    void SetupInput()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component missing!");
            return;
        }

        moveAction = playerInput.actions["Move"];

        // Show/hide joystick based on mode
        if (joystickUI != null)
        {
            joystickUI.SetActive(inputMode == InputMode.VirtualJoystick);
        }
    }

    void Update()
    {
        switch (inputMode)
        {
            case InputMode.VirtualJoystick:
                currentMoveInput = moveAction.ReadValue<Vector2>();
                break;

            case InputMode.Tilt:
                HandleTiltInput();
                break;

            case InputMode.Swipe:
                HandleSwipeInput();
                break;
        }
    }

    void HandleTiltInput()
    {
        // Accelerometer-based input
        Vector3 tilt = Input.acceleration;
        currentMoveInput = new Vector2(tilt.x, tilt.y);

        // Dead zone
        if (currentMoveInput.magnitude < 0.1f)
            currentMoveInput = Vector2.zero;
    }

    void HandleSwipeInput()
    {
        // Simplified swipe detection (implement full swipe logic as needed)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Convert touch position to normalized input
            // This is placeholder - implement proper swipe detection
            currentMoveInput = Vector2.zero;
        }
    }

    public void SetInputMode(InputMode mode)
    {
        inputMode = mode;
        PlayerPrefs.SetInt("InputMode", (int)mode);
        PlayerPrefs.Save();

        if (joystickUI != null)
        {
            joystickUI.SetActive(mode == InputMode.VirtualJoystick);
        }
    }
}
```

#### VehicleController.cs - Complete Implementation

```csharp
using UnityEngine;

/// <summary>
/// Complete vehicle controller with slope handling, ground detection,
/// and mobile-optimized physics.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float groundDrag = 3f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance = 0.5f;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float slopeForceMultiplier = 1.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector2 moveInput;
    private Vector3 slopeNormal;

    public float CurrentSpeed => rb.velocity.magnitude;
    public bool IsGrounded => isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Configure rigidbody for mobile performance
        rb.drag = groundDrag;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth movement
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Prevent tunneling

        // Lock rotation to prevent tipping
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                        RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Get input from InputManager
        if (InputManager.Instance != null)
        {
            moveInput = InputManager.Instance.MoveInput;
        }

        // Update engine sound pitch based on speed
        UpdateEngineSound();
    }

    void FixedUpdate()
    {
        CheckGround();

        if (isGrounded)
        {
            HandleMovement();
            HandleRotation();
        }
        else
        {
            // Slight air control
            HandleAirControl();
        }
    }

    void CheckGround()
    {
        // Raycast to detect ground
        if (Physics.Raycast(groundCheckPoint.position, Vector3.down,
                           out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
            slopeNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            slopeNormal = Vector3.up;
        }
    }

    void HandleMovement()
    {
        // Calculate movement direction (world space forward/back)
        Vector3 moveDirection = transform.forward * moveInput.y;

        // Handle slopes
        float slopeAngle = Vector3.Angle(Vector3.up, slopeNormal);

        if (slopeAngle < maxSlopeAngle && slopeAngle > 0.5f)
        {
            // Project movement onto slope
            moveDirection = Vector3.ProjectOnPlane(moveDirection, slopeNormal).normalized;

            // Add extra force on slopes
            moveDirection *= slopeForceMultiplier;
        }

        // Apply acceleration force
        rb.AddForce(moveDirection * acceleration, ForceMode.Acceleration);

        // Clamp to max speed (preserve direction)
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void HandleRotation()
    {
        if (moveInput.magnitude > 0.1f)
        {
            // Rotate based on horizontal input
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }

    void HandleAirControl()
    {
        // Slight air control (10% of ground acceleration)
        Vector3 airMove = transform.forward * moveInput.y * acceleration * 0.1f;
        rb.AddForce(airMove, ForceMode.Acceleration);
    }

    void UpdateEngineSound()
    {
        if (engineSound != null && engineSound.isPlaying)
        {
            // Map speed to pitch
            float speedPercent = CurrentSpeed / maxSpeed;
            engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, speedPercent);

            // Volume based on input (louder when accelerating)
            float targetVolume = moveInput.magnitude > 0.1f ? 0.8f : 0.4f;
            engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * 5f);
        }
    }

    /// <summary>
    /// Called by checkpoint system to respawn vehicle
    /// </summary>
    public void Respawn(Vector3 position, Quaternion rotation)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = position;
        transform.rotation = rotation;
    }

    void OnDrawGizmos()
    {
        // Visualize ground check in editor
        if (groundCheckPoint != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(groundCheckPoint.position,
                           groundCheckPoint.position + Vector3.down * groundCheckDistance);
        }
    }
}
```

#### CameraController.cs - Smooth Follow Camera

```csharp
using UnityEngine;

/// <summary>
/// Camera controller with smooth following, boundaries, and camera shake.
/// Optimized for mobile 3D vehicle games.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 12, -10);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private bool lookAtTarget = true;
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1, 0);

    [Header("Boundaries")]
    [SerializeField] private bool constrainToBounds = true;
    [SerializeField] private Vector2 minBounds = new Vector2(-50, -50);
    [SerializeField] private Vector2 maxBounds = new Vector2(50, 50);

    [Header("Camera Shake")]
    [SerializeField] private float shakeDecay = 2f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 currentShakeOffset = Vector3.zero;
    private float currentShakeMagnitude = 0f;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target not assigned!");
            return;
        }

        FollowTarget();
        HandleShake();

        if (lookAtTarget)
        {
            LookAtTarget();
        }
    }

    void FollowTarget()
    {
        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Apply boundaries
        if (constrainToBounds)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, minBounds.y, maxBounds.y);
        }

        // Smooth follow
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothSpeed
        );

        // Apply shake offset
        transform.position = smoothedPosition + currentShakeOffset;
    }

    void LookAtTarget()
    {
        Vector3 lookPosition = target.position + lookAtOffset;
        Quaternion targetRotation = Quaternion.LookRotation(lookPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                               rotationSpeed * Time.deltaTime);
    }

    void HandleShake()
    {
        if (currentShakeMagnitude > 0)
        {
            // Random shake offset
            currentShakeOffset = Random.insideUnitSphere * currentShakeMagnitude;

            // Decay shake over time
            currentShakeMagnitude -= shakeDecay * Time.deltaTime;

            if (currentShakeMagnitude <= 0f)
            {
                currentShakeMagnitude = 0f;
                currentShakeOffset = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// Trigger camera shake (called by game events like gate hits, collisions)
    /// </summary>
    public void Shake(float intensity, float duration)
    {
        currentShakeMagnitude = Mathf.Max(currentShakeMagnitude, intensity);
    }

    /// <summary>
    /// Set camera target at runtime (for vehicle switching)
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// Update level boundaries (called when loading new level)
    /// </summary>
    public void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
        constrainToBounds = true;
    }

    void OnDrawGizmos()
    {
        // Visualize camera boundaries in editor
        if (constrainToBounds)
        {
            Gizmos.color = Color.yellow;
            Vector3 bottomLeft = new Vector3(minBounds.x, 0, minBounds.y);
            Vector3 bottomRight = new Vector3(maxBounds.x, 0, minBounds.y);
            Vector3 topLeft = new Vector3(minBounds.x, 0, maxBounds.y);
            Vector3 topRight = new Vector3(maxBounds.x, 0, maxBounds.y);

            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
        }
    }
}
```

#### AudioManager.cs - Complete Audio System

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Singleton audio manager for music and SFX.
/// Handles volume settings, audio pooling, and cross-scene persistence.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientSource;

    [Header("SFX Pool")]
    [SerializeField] private int sfxSourceCount = 5;
    private List<AudioSource> sfxSources = new List<AudioSource>();
    private int currentSFXIndex = 0;

    [Header("Music Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip victoryMusic;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip treasureCollect;
    [SerializeField] private AudioClip gateMultiply;
    [SerializeField] private AudioClip depositSuccess;
    [SerializeField] private AudioClip vehicleEngine;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip levelComplete;
    [SerializeField] private AudioClip levelFail;
    [SerializeField] private AudioClip purchaseSuccess;
    [SerializeField] private AudioClip purchaseFail;

    [Header("Volume Settings")]
    [Range(0f, 1f)] [SerializeField] private float masterVolume = 1f;
    [Range(0f, 1f)] [SerializeField] private float musicVolume = 0.7f;
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 1f;

    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudio()
    {
        // Create SFX source pool
        for (int i = 0; i < sfxSourceCount; i++)
        {
            GameObject sfxObject = new GameObject($"SFX_Source_{i}");
            sfxObject.transform.SetParent(transform);
            AudioSource source = sfxObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            sfxSources.Add(source);
        }

        // Build sound library for easy access
        soundLibrary["treasure_collect"] = treasureCollect;
        soundLibrary["gate_multiply"] = gateMultiply;
        soundLibrary["deposit_success"] = depositSuccess;
        soundLibrary["button_click"] = buttonClick;
        soundLibrary["level_complete"] = levelComplete;
        soundLibrary["level_fail"] = levelFail;
        soundLibrary["purchase_success"] = purchaseSuccess;
        soundLibrary["purchase_fail"] = purchaseFail;

        // Load saved settings
        LoadSettings();

        // Configure music source
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.volume = musicVolume * masterVolume;
        }
    }

    #region Music Control

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    public void PlayVictoryMusic()
    {
        PlayMusic(victoryMusic, loop: false);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null || clip == null) return;

        // Don't restart if already playing
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume * masterVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }

    #endregion

    #region SFX Control

    /// <summary>
    /// Play sound effect by name (uses sound library)
    /// </summary>
    public void PlaySFX(string soundName, float volumeScale = 1f)
    {
        if (soundLibrary.TryGetValue(soundName, out AudioClip clip))
        {
            PlaySFX(clip, volumeScale);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in library!");
        }
    }

    /// <summary>
    /// Play sound effect by AudioClip reference
    /// </summary>
    public void PlaySFX(AudioClip clip, float volumeScale = 1f)
    {
        if (clip == null || sfxSources.Count == 0) return;

        // Get next available source (round-robin)
        AudioSource source = sfxSources[currentSFXIndex];
        currentSFXIndex = (currentSFXIndex + 1) % sfxSources.Count;

        // Play sound
        float finalVolume = sfxVolume * masterVolume * volumeScale;
        source.PlayOneShot(clip, finalVolume);
    }

    /// <summary>
    /// Play sound effect at world position (3D spatial audio)
    /// </summary>
    public void PlaySFXAtPosition(AudioClip clip, Vector3 position, float volumeScale = 1f)
    {
        if (clip == null) return;

        float finalVolume = sfxVolume * masterVolume * volumeScale;
        AudioSource.PlayClipAtPoint(clip, position, finalVolume);
    }

    // Convenience methods for common sounds
    public void PlayTreasureCollect() => PlaySFX("treasure_collect");
    public void PlayGateMultiply() => PlaySFX("gate_multiply");
    public void PlayDepositSuccess() => PlaySFX("deposit_success");
    public void PlayButtonClick() => PlaySFX("button_click", 0.5f);
    public void PlayLevelComplete() => PlaySFX("level_complete");
    public void PlayPurchaseSuccess() => PlaySFX("purchase_success");

    #endregion

    #region Volume Control

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplyVolumeSettings();
        SaveSettings();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplyVolumeSettings();
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        SaveSettings();
    }

    void ApplyVolumeSettings()
    {
        if (musicSource != null)
        {
            musicSource.volume = musicVolume * masterVolume;
        }

        if (ambientSource != null)
        {
            ambientSource.volume = sfxVolume * masterVolume * 0.5f;
        }
    }

    void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumeSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    #endregion

    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
}
```

#### AnalyticsManager.cs - Firebase Analytics Integration

```csharp
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Firebase Analytics wrapper for tracking game events.
/// Replaces deprecated Unity Analytics.
/// </summary>
public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    private bool isInitialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    async void InitializeFirebase()
    {
        try
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                isInitialized = true;
                Debug.Log("Firebase Analytics initialized successfully");

                // Track app open
                TrackEvent("app_open");
            }
            else
            {
                Debug.LogError($"Firebase initialization failed: {dependencyStatus}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Firebase initialization exception: {ex.Message}");
        }
    }

    #region Event Tracking

    /// <summary>
    /// Track custom event with parameters
    /// </summary>
    public void TrackEvent(string eventName, Dictionary<string, object> parameters = null)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Analytics not initialized, skipping event: " + eventName);
            return;
        }

        try
        {
            if (parameters == null || parameters.Count == 0)
            {
                FirebaseAnalytics.LogEvent(eventName);
            }
            else
            {
                // Convert dictionary to Firebase parameters
                List<Parameter> firebaseParams = new List<Parameter>();

                foreach (var kvp in parameters)
                {
                    if (kvp.Value is string stringValue)
                        firebaseParams.Add(new Parameter(kvp.Key, stringValue));
                    else if (kvp.Value is int intValue)
                        firebaseParams.Add(new Parameter(kvp.Key, intValue));
                    else if (kvp.Value is long longValue)
                        firebaseParams.Add(new Parameter(kvp.Key, longValue));
                    else if (kvp.Value is double doubleValue)
                        firebaseParams.Add(new Parameter(kvp.Key, doubleValue));
                    else if (kvp.Value is float floatValue)
                        firebaseParams.Add(new Parameter(kvp.Key, (double)floatValue));
                    else
                        firebaseParams.Add(new Parameter(kvp.Key, kvp.Value.ToString()));
                }

                FirebaseAnalytics.LogEvent(eventName, firebaseParams.ToArray());
            }

            Debug.Log($"Analytics Event: {eventName}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to log analytics event: {ex.Message}");
        }
    }

    #endregion

    #region Game Events

    public void TrackLevelStart(int levelNumber, string vehicleType)
    {
        TrackEvent("level_start", new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "vehicle_type", vehicleType }
        });
    }

    public void TrackLevelComplete(int levelNumber, int stars, int score, float timeSeconds)
    {
        TrackEvent("level_complete", new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "stars", stars },
            { "score", score },
            { "time_seconds", (int)timeSeconds }
        });
    }

    public void TrackLevelFail(int levelNumber, string failReason)
    {
        TrackEvent("level_fail", new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "fail_reason", failReason }
        });
    }

    public void TrackGateUsed(int levelNumber, int multiplier, int cargoCount)
    {
        TrackEvent("gate_used", new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "multiplier", multiplier },
            { "cargo_count", cargoCount }
        });
    }

    public void TrackTreasureCollected(int levelNumber, string treasureType, int value)
    {
        TrackEvent("treasure_collected", new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "treasure_type", treasureType },
            { "value", value }
        });
    }

    public void TrackVehicleUnlock(string vehicleType, string unlockMethod)
    {
        TrackEvent("vehicle_unlock", new Dictionary<string, object>
        {
            { "vehicle_type", vehicleType },
            { "unlock_method", unlockMethod } // "gameplay" or "purchase"
        });
    }

    public void TrackPurchase(string itemName, int goldCost)
    {
        TrackEvent("purchase", new Dictionary<string, object>
        {
            { "item_name", itemName },
            { "gold_cost", goldCost }
        });
    }

    public void TrackAdView(string adType, string placement)
    {
        TrackEvent("ad_view", new Dictionary<string, object>
        {
            { "ad_type", adType }, // "rewarded", "interstitial"
            { "placement", placement } // "level_fail", "double_reward", etc.
        });
    }

    public void TrackAdClick(string adType)
    {
        TrackEvent("ad_click", new Dictionary<string, object>
        {
            { "ad_type", adType }
        });
    }

    public void TrackTutorialStep(int stepNumber, string stepName)
    {
        TrackEvent("tutorial_step", new Dictionary<string, object>
        {
            { "step_number", stepNumber },
            { "step_name", stepName }
        });
    }

    public void TrackTutorialComplete()
    {
        TrackEvent("tutorial_complete");
    }

    #endregion

    #region User Properties

    public void SetUserProperty(string propertyName, string value)
    {
        if (!isInitialized) return;

        FirebaseAnalytics.SetUserProperty(propertyName, value);
    }

    public void SetTotalPlayTime(int seconds)
    {
        SetUserProperty("total_play_time", seconds.ToString());
    }

    public void SetPlayerLevel(int level)
    {
        SetUserProperty("player_level", level.ToString());
    }

    #endregion
}
```

#### ATTManager.cs - iOS 14+ App Tracking Transparency

```csharp
using UnityEngine;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

/// <summary>
/// Handles iOS 14+ App Tracking Transparency (ATT) framework.
/// REQUIRED for ad monetization on iOS 14+.
/// Must be called before initializing ads.
/// </summary>
public class ATTManager : MonoBehaviour
{
    public static ATTManager Instance { get; private set; }

    private bool attRequestComplete = false;
    private bool userAuthorizedTracking = false;

    public bool IsAuthorized => userAuthorizedTracking;
    public bool RequestComplete => attRequestComplete;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Request tracking authorization. Call this before showing ads.
    /// </summary>
    public void RequestTracking(System.Action<bool> onComplete = null)
    {
        #if UNITY_IOS
        // Check current status
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        Debug.Log($"ATT Current Status: {status}");

        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            // Show ATT prompt
            ATTrackingStatusBinding.RequestAuthorizationTracking((newStatus) =>
            {
                HandleATTResponse(newStatus);
                onComplete?.Invoke(userAuthorizedTracking);
            });
        }
        else
        {
            // Already determined
            HandleATTResponse(status);
            onComplete?.Invoke(userAuthorizedTracking);
        }
        #else
        // Non-iOS platforms - always authorized
        attRequestComplete = true;
        userAuthorizedTracking = true;
        onComplete?.Invoke(true);
        #endif
    }

    #if UNITY_IOS
    void HandleATTResponse(ATTrackingStatusBinding.AuthorizationTrackingStatus status)
    {
        attRequestComplete = true;

        switch (status)
        {
            case ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED:
                Debug.Log("ATT: User authorized tracking");
                userAuthorizedTracking = true;
                PlayerPrefs.SetInt("ATT_Authorized", 1);

                // Track acceptance in analytics
                if (AnalyticsManager.Instance != null)
                {
                    AnalyticsManager.Instance.TrackEvent("att_accepted");
                }
                break;

            case ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED:
            case ATTrackingStatusBinding.AuthorizationTrackingStatus.RESTRICTED:
                Debug.Log("ATT: User denied tracking");
                userAuthorizedTracking = false;
                PlayerPrefs.SetInt("ATT_Authorized", 0);

                // Track denial in analytics
                if (AnalyticsManager.Instance != null)
                {
                    AnalyticsManager.Instance.TrackEvent("att_denied");
                }
                break;

            case ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED:
                // Shouldn't happen after request, but handle it
                Debug.LogWarning("ATT: Status still not determined");
                userAuthorizedTracking = false;
                break;
        }

        PlayerPrefs.Save();
    }
    #endif

    /// <summary>
    /// Get expected CPM modifier based on ATT status
    /// </summary>
    public float GetCPMModifier()
    {
        return userAuthorizedTracking ? 1.0f : 0.5f;
    }
}
```

**IMPORTANT: Info.plist Configuration**

Add to `Assets/Plugins/iOS/Info.plist` (or Unity will auto-create):

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>NSUserTrackingUsageDescription</key>
    <string>We use your data to show you personalized ads, which keeps the game free for everyone!</string>
</dict>
</plist>
```

**Note:** This message appears to users. Make it friendly and honest.

### 5.2 Phase 1 Deliverables

**By End of Week 3:**

- [ ] Unity project set up with all packages installed
- [ ] Input system working (virtual joystick + tilt option)
- [ ] Vehicle controller drives smoothly with proper physics
- [ ] Camera follows vehicle smoothly with boundaries
- [ ] Audio manager plays music and SFX
- [ ] Firebase Analytics tracking basic events
- [ ] ATT framework requests permission on iOS
- [ ] Prototype level (blockout) with basic terrain
- [ ] Basic treasure prefab (no logic yet, just visual)
- [ ] Performance: 60 FPS on target device (iPhone 12)

**Testing Checklist:**
- [ ] Build to iPhone device successfully
- [ ] Touch controls responsive on device
- [ ] Vehicle doesn't fall through floor
- [ ] Camera doesn't clip through objects
- [ ] Audio plays without crackling
- [ ] Analytics events appear in Firebase console
- [ ] ATT prompt appears on first launch (iOS 14+)

**Phase 1 Milestone Gate:**
✅ GO if: Vehicle drives smoothly, camera feels good, no critical bugs
⚠️ ITERATE if: Physics feels floaty, camera jerky
🛑 NO-GO if: Can't build to device, critical crashes

---

## 6. Phase 2: Core Mechanics (5 Weeks)

### Week 4-8 Goals:
- Multiplier gate system (robust, with pooling)
- Treasure collection mechanic (cargo system)
- Deposit zone implementation
- Win/fail conditions
- Score calculation system
- Visual feedback (particles, UI animations)
- Ad SDK integration (Unity Ads)
- First playable level (complete with art)

### 6.1 Treasure System Implementation

#### TreasureItem.cs

```csharp
using UnityEngine;

/// <summary>
/// Individual treasure item with value, type, and collection behavior.
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class TreasureItem : MonoBehaviour
{
    [Header("Treasure Properties")]
    [SerializeField] private int baseValue = 10;
    [SerializeField] private TreasureType type = TreasureType.Small;
    [SerializeField] private float weight = 1f;

    [Header("Visual")]
    [SerializeField] private GameObject visualModel;
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private float rotationSpeed = 50f;

    private Rigidbody rb;
    private Collider col;
    private bool isCollected = false;
    private int currentValue;

    public int Value => currentValue;
    public TreasureType Type => type;
    public bool IsCollected => isCollected;

    public enum TreasureType
    {
        Small,      // 10 value
        Medium,     // 50 value
        Large       // 200 value
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        currentValue = baseValue;

        // Configure rigidbody
        rb.mass = weight;
        rb.useGravity = true;

        // Set collider as trigger for collection
        col.isTrigger = true;
    }

    void Update()
    {
        if (!isCollected && visualModel != null)
        {
            // Gentle rotation for visual interest
            visualModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Called by VehicleCollector when treasure is collected
    /// </summary>
    public void Collect()
    {
        if (isCollected) return;

        isCollected = true;

        // Play collection effect
        if (collectEffect != null)
        {
            collectEffect.Play();
        }

        // Audio
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTreasureCollect();
        }

        // Analytics
        if (AnalyticsManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevel : 0;
            AnalyticsManager.Instance.TrackTreasureCollected(currentLevel, type.ToString(), currentValue);
        }

        // Hide visual (we'll use object pooling to return it later)
        if (visualModel != null)
        {
            visualModel.SetActive(false);
        }

        // Disable collider
        col.enabled = false;
    }

    /// <summary>
    /// Reset treasure for object pool reuse
    /// </summary>
    public void ResetTreasure()
    {
        isCollected = false;
        currentValue = baseValue;

        if (visualModel != null)
        {
            visualModel.SetActive(true);
        }

        col.enabled = true;
    }

    /// <summary>
    /// Get value based on type (for level design)
    /// </summary>
    public static int GetValueForType(TreasureType type)
    {
        switch (type)
        {
            case TreasureType.Small: return 10;
            case TreasureType.Medium: return 50;
            case TreasureType.Large: return 200;
            default: return 10;
        }
    }
}
```

#### VehicleCargoHold.cs

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages treasure cargo in vehicle (collection, storage, deposit).
/// </summary>
public class VehicleCargoHold : MonoBehaviour
{
    [Header("Capacity")]
    [SerializeField] private int maxCapacity = 10;

    [Header("Collection")]
    [SerializeField] private float collectionRadius = 1.5f;
    [SerializeField] private LayerMask treasureLayer;
    [SerializeField] private Transform collectionPoint;

    [Header("Visual")]
    [SerializeField] private Transform cargoVisualParent;
    [SerializeField] private float cargoFlySpeed = 10f;

    private List<TreasureItem> collectedTreasures = new List<TreasureItem>();
    private int currentCargoValue = 0;

    public int CurrentCount => collectedTreasures.Count;
    public int MaxCapacity => maxCapacity;
    public int CurrentValue => currentCargoValue;
    public bool IsFull => collectedTreasures.Count >= maxCapacity;

    // Events for UI updates
    public System.Action<int, int> OnCargoChanged; // (currentCount, maxCapacity)
    public System.Action<int> OnValueChanged; // (totalValue)

    void Update()
    {
        // Auto-collect nearby treasures
        if (!IsFull)
        {
            CollectNearbyTreasures();
        }
    }

    void CollectNearbyTreasures()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(
            collectionPoint.position,
            collectionRadius,
            treasureLayer
        );

        foreach (Collider col in nearbyColliders)
        {
            if (IsFull) break;

            TreasureItem treasure = col.GetComponent<TreasureItem>();
            if (treasure != null && !treasure.IsCollected)
            {
                CollectTreasure(treasure);
            }
        }
    }

    void CollectTreasure(TreasureItem treasure)
    {
        treasure.Collect();
        collectedTreasures.Add(treasure);
        currentCargoValue += treasure.Value;

        // Visual: Move treasure to cargo hold
        StartCoroutine(FlyTreasureToVehicle(treasure.gameObject));

        // Update UI
        OnCargoChanged?.Invoke(CurrentCount, maxCapacity);
        OnValueChanged?.Invoke(currentCargoValue);

        Debug.Log($"Collected treasure! Cargo: {CurrentCount}/{maxCapacity}, Value: {currentCargoValue}");
    }

    System.Collections.IEnumerator FlyTreasureToVehicle(GameObject treasureObject)
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector3 startPos = treasureObject.transform.position;
        Vector3 endPos = cargoVisualParent.position;

        while (elapsedTime < duration)
        {
            if (treasureObject == null) yield break;

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Smooth curve
            t = Mathf.SmoothStep(0, 1, t);

            treasureObject.transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        // Parent to cargo hold
        if (treasureObject != null && cargoVisualParent != null)
        {
            treasureObject.transform.SetParent(cargoVisualParent);
            treasureObject.transform.localPosition = Vector3.zero;
            treasureObject.transform.localScale = Vector3.one * 0.3f; // Shrink in cargo
        }
    }

    /// <summary>
    /// Multiply all cargo (called by MultiplierGate)
    /// </summary>
    public void MultiplyCargo(int multiplier)
    {
        if (collectedTreasures.Count == 0) return;

        int originalCount = collectedTreasures.Count;
        int originalValue = currentCargoValue;

        // Multiply value
        currentCargoValue *= multiplier;

        // Visual feedback
        OnValueChanged?.Invoke(currentCargoValue);

        Debug.Log($"Multiplied cargo x{multiplier}! {originalValue} → {currentCargoValue}");

        // Note: We're multiplying value, not spawning duplicate treasures
        // This is simpler and more performance-friendly than the original design
    }

    /// <summary>
    /// Deposit all cargo (called by DepositZone)
    /// </summary>
    public int DepositCargo()
    {
        int depositedValue = currentCargoValue;

        if (depositedValue > 0)
        {
            // Clear cargo
            foreach (var treasure in collectedTreasures)
            {
                if (treasure != null && treasure.gameObject != null)
                {
                    // Return to pool or destroy
                    Destroy(treasure.gameObject);
                }
            }

            collectedTreasures.Clear();
            currentCargoValue = 0;

            // Update UI
            OnCargoChanged?.Invoke(0, maxCapacity);
            OnValueChanged?.Invoke(0);

            // Audio
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayDepositSuccess();
            }

            Debug.Log($"Deposited cargo! Value: {depositedValue}");
        }

        return depositedValue;
    }

    /// <summary>
    /// Upgrade cargo capacity (called when unlocking vehicles)
    /// </summary>
    public void UpgradeCapacity(int newCapacity)
    {
        maxCapacity = newCapacity;
        OnCargoChanged?.Invoke(CurrentCount, maxCapacity);
        Debug.Log($"Cargo capacity upgraded to: {maxCapacity}");
    }

    void OnDrawGizmosSelected()
    {
        // Visualize collection radius
        if (collectionPoint != null)
        {
            Gizmos.color = IsFull ? Color.red : Color.green;
            Gizmos.DrawWireSphere(collectionPoint.position, collectionRadius);
        }
    }
}
```

#### MultiplierGate.cs - Robust Implementation

```csharp
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Multiplier gate that increases cargo value when vehicle passes through.
/// Includes error handling, cooldown, and visual feedback.
/// </summary>
public class MultiplierGate : MonoBehaviour
{
    [Header("Gate Properties")]
    [SerializeField] private int multiplier = 2;
    [SerializeField] private Color gateColor = Color.yellow;

    [Header("Visual Feedback")]
    [SerializeField] private ParticleSystem entryEffect;
    [SerializeField] private GameObject gateVisual;
    [SerializeField] private TextMesh multiplierText;

    [Header("Cooldown")]
    [SerializeField] private float cooldownSeconds = 1f;

    private HashSet<int> processedVehicles = new HashSet<int>();
    private float lastTriggerTime = 0f;

    void Start()
    {
        // Set up visual
        if (multiplierText != null)
        {
            multiplierText.text = $"x{multiplier}";
            multiplierText.color = gateColor;
        }

        if (gateVisual != null)
        {
            // Color the gate based on multiplier
            Renderer renderer = gateVisual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = gateColor;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check cooldown
        if (Time.time - lastTriggerTime < cooldownSeconds)
        {
            return;
        }

        // Check if it's a vehicle
        VehicleCargoHold cargoHold = other.GetComponent<VehicleCargoHold>();
        if (cargoHold == null)
        {
            return; // Not a vehicle
        }

        // Check if already processed this vehicle recently
        int vehicleID = cargoHold.GetInstanceID();
        if (processedVehicles.Contains(vehicleID))
        {
            return; // Already processed
        }

        // Check if vehicle has cargo
        if (cargoHold.CurrentCount == 0)
        {
            Debug.Log("Gate: Vehicle has no cargo to multiply");
            return;
        }

        // Process multiplication
        ProcessVehicle(cargoHold, vehicleID);
    }

    void ProcessVehicle(VehicleCargoHold cargoHold, int vehicleID)
    {
        // Mark as processed
        processedVehicles.Add(vehicleID);
        lastTriggerTime = Time.time;

        // Clear processed list after cooldown
        Invoke(nameof(ClearProcessedVehicle), cooldownSeconds);

        // Visual feedback
        if (entryEffect != null)
        {
            entryEffect.Play();
        }

        // Camera shake
        if (Camera.main != null)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
            {
                cam.Shake(0.3f, 0.5f);
            }
        }

        // Audio
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGateMultiply();
        }

        // Multiply cargo
        int beforeValue = cargoHold.CurrentValue;
        cargoHold.MultiplyCargo(multiplier);
        int afterValue = cargoHold.CurrentValue;

        // UI popup (if implemented)
        ShowMultiplierPopup(afterValue - beforeValue);

        // Analytics
        if (AnalyticsManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevel : 0;
            AnalyticsManager.Instance.TrackGateUsed(currentLevel, multiplier, cargoHold.CurrentCount);
        }

        Debug.Log($"Gate x{multiplier}: {beforeValue} → {afterValue}");
    }

    void ClearProcessedVehicle()
    {
        // Clear the processed vehicles list (allows re-triggering)
        processedVehicles.Clear();
    }

    void ShowMultiplierPopup(int valueGained)
    {
        // TODO: Implement floating text popup showing "+X gold!"
        // For now, just log
        Debug.Log($"+{valueGained} gold!");
    }

    void OnDrawGizmos()
    {
        // Visualize gate trigger in editor
        Gizmos.color = gateColor;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
```

#### DepositZone.cs

```csharp
using UnityEngine;

/// <summary>
/// Deposit zone where players unload cargo and add to score.
/// </summary>
public class DepositZone : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private ParticleSystem depositEffect;
    [SerializeField] private GameObject zoneVisual;
    [SerializeField] private Color zoneColor = Color.green;

    [Header("Audio")]
    [SerializeField] private AudioClip depositSound;

    void Start()
    {
        // Set up visual
        if (zoneVisual != null)
        {
            Renderer renderer = zoneVisual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = zoneColor;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        VehicleCargoHold cargoHold = other.GetComponent<VehicleCargoHold>();
        if (cargoHold == null) return;

        // Check if vehicle has cargo
        if (cargoHold.CurrentCount == 0) return;

        // Deposit cargo
        int depositedValue = cargoHold.DepositCargo();

        if (depositedValue > 0)
        {
            // Add to level score
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.AddScore(depositedValue);
            }

            // Visual feedback
            if (depositEffect != null)
            {
                depositEffect.Play();
            }

            // Audio
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayDepositSuccess();
            }

            // UI popup
            ShowDepositPopup(depositedValue);

            Debug.Log($"Deposited {depositedValue} gold!");
        }
    }

    void ShowDepositPopup(int value)
    {
        // TODO: Implement floating text showing "+X score!"
        Debug.Log($"+{value} score!");
    }
}
```

### 6.2 Ad SDK Integration

#### AdManager.cs - Unity Ads

```csharp
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Manages Unity Ads integration for rewarded videos.
/// Handles initialization, showing ads, and callbacks.
/// </summary>
public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance { get; private set; }

    [Header("Ad IDs")]
    [SerializeField] private string androidGameID = "YOUR_ANDROID_GAME_ID";
    [SerializeField] private string iOSGameID = "YOUR_IOS_GAME_ID";

    [Header("Ad Units")]
    [SerializeField] private string rewardedAdUnitID = "Rewarded_iOS"; // Or "Rewarded_Android"

    [Header("Settings")]
    [SerializeField] private bool testMode = true;

    private string gameID;
    private bool isInitialized = false;
    private System.Action<bool> currentAdCallback;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Wait for ATT before initializing ads
        if (ATTManager.Instance != null && !ATTManager.Instance.RequestComplete)
        {
            ATTManager.Instance.RequestTracking((authorized) =>
            {
                InitializeAds();
            });
        }
        else
        {
            InitializeAds();
        }
    }

    void InitializeAds()
    {
        #if UNITY_IOS
        gameID = iOSGameID;
        #elif UNITY_ANDROID
        gameID = androidGameID;
        #else
        gameID = iOSGameID; // Default
        #endif

        if (string.IsNullOrEmpty(gameID) || gameID.StartsWith("YOUR_"))
        {
            Debug.LogWarning("Ad Game ID not set! Ads will not work.");
            return;
        }

        Debug.Log($"Initializing Unity Ads (Test Mode: {testMode})...");
        Advertisement.Initialize(gameID, testMode, this);
    }

    #region IUnityAdsInitializationListener

    public void OnInitializationComplete()
    {
        isInitialized = true;
        Debug.Log("Unity Ads initialized successfully");

        // Pre-load first ad
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads initialization failed: {error} - {message}");
    }

    #endregion

    #region Load Ads

    void LoadRewardedAd()
    {
        if (!isInitialized) return;

        Advertisement.Load(rewardedAdUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"Ad failed to load: {placementId} - {error} - {message}");

        // Retry after delay
        Invoke(nameof(LoadRewardedAd), 5f);
    }

    #endregion

    #region Show Ads

    /// <summary>
    /// Show rewarded video ad. Call this from UI buttons or game logic.
    /// </summary>
    /// <param name="onComplete">Callback: true if user watched full ad, false if skipped/failed</param>
    public void ShowRewardedAd(System.Action<bool> onComplete)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Ads not initialized");
            onComplete?.Invoke(false);
            return;
        }

        currentAdCallback = onComplete;

        // Check if ad is loaded
        if (Advertisement.isShowing)
        {
            Debug.LogWarning("Ad already showing");
            onComplete?.Invoke(false);
            return;
        }

        // Show ad
        Advertisement.Show(rewardedAdUnitID, this);

        // Analytics
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.TrackAdView("rewarded", "manual_trigger");
        }
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        bool success = showCompletionState == UnityAdsShowCompletionState.COMPLETED;

        Debug.Log($"Ad show complete: {placementId} - Success: {success}");

        // Callback
        currentAdCallback?.Invoke(success);
        currentAdCallback = null;

        // Preload next ad
        LoadRewardedAd();

        // Analytics
        if (success && AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.TrackEvent("ad_completed", new System.Collections.Generic.Dictionary<string, object>
            {
                { "placement", placementId }
            });
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed: {placementId} - {error} - {message}");

        // Callback with failure
        currentAdCallback?.Invoke(false);
        currentAdCallback = null;

        // Try to reload
        LoadRewardedAd();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ad started: {placementId}");

        // Pause game
        Time.timeScale = 0f;

        // Mute audio
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PauseMusic();
        }
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ad clicked: {placementId}");

        // Analytics
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.TrackAdClick("rewarded");
        }
    }

    #endregion

    /// <summary>
    /// Check if rewarded ad is available to show
    /// </summary>
    public bool IsRewardedAdReady()
    {
        return isInitialized && Advertisement.isInitialized;
    }
}
```

### 6.3 Save System Implementation

#### SaveSystem.cs

```csharp
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Save/load system using JSON and basic encryption.
/// Persists player progress, currency, unlocks, and settings.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private const string SAVE_FILE_NAME = "savegame.dat";
    private string savePath;

    private SaveData currentSaveData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Save/Load

    public void SaveGame()
    {
        try
        {
            // Serialize to JSON
            string json = JsonUtility.ToJson(currentSaveData, true);

            // Basic encryption (Base64 encoding)
            string encrypted = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));

            // Write to file
            File.WriteAllText(savePath, encrypted);

            Debug.Log($"Game saved to: {savePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save game: {ex.Message}");
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found, creating new save data");
            currentSaveData = new SaveData();
            return;
        }

        try
        {
            // Read file
            string encrypted = File.ReadAllText(savePath);

            // Decrypt (Base64 decoding)
            string json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encrypted));

            // Deserialize
            currentSaveData = JsonUtility.FromJson<SaveData>(json);

            Debug.Log($"Game loaded from: {savePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load game: {ex.Message}");
            currentSaveData = new SaveData();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted");
        }

        currentSaveData = new SaveData();
    }

    #endregion

    #region Data Access

    public SaveData GetSaveData()
    {
        return currentSaveData;
    }

    // Currency
    public int GetGold() => currentSaveData.gold;
    public void AddGold(int amount)
    {
        currentSaveData.gold += amount;
        SaveGame();
    }
    public bool SpendGold(int amount)
    {
        if (currentSaveData.gold >= amount)
        {
            currentSaveData.gold -= amount;
            SaveGame();
            return true;
        }
        return false;
    }

    // Progress
    public int GetHighestLevelUnlocked() => currentSaveData.highestLevelUnlocked;
    public void UnlockLevel(int levelNumber)
    {
        if (levelNumber > currentSaveData.highestLevelUnlocked)
        {
            currentSaveData.highestLevelUnlocked = levelNumber;
            SaveGame();
        }
    }

    public int GetLevelStars(int levelNumber)
    {
        if (currentSaveData.levelStars.TryGetValue(levelNumber, out int stars))
        {
            return stars;
        }
        return 0;
    }

    public void SetLevelStars(int levelNumber, int stars)
    {
        if (stars > GetLevelStars(levelNumber))
        {
            currentSaveData.levelStars[levelNumber] = stars;
            SaveGame();
        }
    }

    // Vehicles
    public bool IsVehicleUnlocked(string vehicleID)
    {
        return currentSaveData.unlockedVehicles.Contains(vehicleID);
    }

    public void UnlockVehicle(string vehicleID)
    {
        if (!IsVehicleUnlocked(vehicleID))
        {
            currentSaveData.unlockedVehicles.Add(vehicleID);
            SaveGame();
        }
    }

    // Daily Reward
    public DateTime GetLastDailyRewardClaim() => currentSaveData.lastDailyRewardClaim;
    public void ClaimDailyReward()
    {
        currentSaveData.lastDailyRewardClaim = DateTime.UtcNow;
        currentSaveData.dailyRewardStreak++;
        SaveGame();
    }

    public int GetDailyRewardStreak() => currentSaveData.dailyRewardStreak;
    public void ResetDailyStreak()
    {
        currentSaveData.dailyRewardStreak = 0;
        SaveGame();
    }

    // First time
    public bool IsFirstLaunch() => currentSaveData.isFirstLaunch;
    public void SetFirstLaunchComplete()
    {
        currentSaveData.isFirstLaunch = false;
        SaveGame();
    }

    #endregion
}

/// <summary>
/// Serializable save data structure
/// </summary>
[System.Serializable]
public class SaveData
{
    // Currency
    public int gold = 0;

    // Progress
    public int highestLevelUnlocked = 1;
    public SerializableDictionary<int, int> levelStars = new SerializableDictionary<int, int>();

    // Unlocks
    public System.Collections.Generic.List<string> unlockedVehicles = new System.Collections.Generic.List<string> { "bulldozer" };
    public System.Collections.Generic.List<string> unlockedSkins = new System.Collections.Generic.List<string>();

    // Daily Rewards
    public DateTime lastDailyRewardClaim = DateTime.MinValue;
    public int dailyRewardStreak = 0;

    // Meta
    public bool isFirstLaunch = true;
    public int totalPlayTimeSeconds = 0;
    public DateTime firstLaunchDate = DateTime.UtcNow;
}

/// <summary>
/// Serializable dictionary for Unity's JsonUtility
/// </summary>
[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] private System.Collections.Generic.List<TKey> keys = new System.Collections.Generic.List<TKey>();
    [SerializeField] private System.Collections.Generic.List<TValue> values = new System.Collections.Generic.List<TValue>();

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = keys.IndexOf(key);
        if (index >= 0 && index < values.Count)
        {
            value = values[index];
            return true;
        }
        value = default;
        return false;
    }

    public void Add(TKey key, TValue value)
    {
        int index = keys.IndexOf(key);
        if (index >= 0)
        {
            values[index] = value;
        }
        else
        {
            keys.Add(key);
            values.Add(value);
        }
    }

    public bool ContainsKey(TKey key) => keys.Contains(key);

    public TValue this[TKey key]
    {
        get
        {
            TryGetValue(key, out TValue value);
            return value;
        }
        set
        {
            Add(key, value);
        }
    }
}
```

### 6.4 Phase 2 Deliverables

**By End of Week 8:**

- [ ] Treasure collection working (fly to vehicle, cargo counter updates)
- [ ] Multiplier gates multiply cargo correctly
- [ ] Deposit zones add score when vehicle enters
- [ ] Save/load system persists progress
- [ ] Unity Ads SDK integrated and showing test ads
- [ ] ATT prompt working on iOS 14+
- [ ] Level 1 fully playable (tutorial extension) with art
- [ ] Win condition working (reach target score)
- [ ] HUD showing: cargo count, score, target score
- [ ] Particle effects for collection, multiplication, deposit

**Testing Checklist:**
- [ ] Collect treasure → cargo counter increases
- [ ] Drive through x2 gate → cargo value doubles
- [ ] Enter deposit zone → score increases, cargo clears
- [ ] Reach target score → level complete screen appears
- [ ] Save game → quit → relaunch → progress persists
- [ ] Watch rewarded ad → reward granted
- [ ] ATT prompt shows on first launch (iOS)
- [ ] 60 FPS maintained with 10+ treasures

**Phase 2 Milestone Gate:**
✅ GO if: Complete game loop works, multiplication feels satisfying
⚠️ ITERATE if: Multiplication timing needs tuning, UI unclear
🛑 NO-GO if: Save system loses data, ads crash app

---

**[DOCUMENT CONTINUES - Part 2 of 4]**

Continuing with remaining phases...