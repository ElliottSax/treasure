# UI Wireframes Planning Document
## Treasure Excavator - Mobile Game UI/UX Specification

**Version**: 1.0
**Last Updated**: 2025-11-18
**Target Platform**: iOS (iPhone 11+)
**Design Tool**: Figma (free)
**Estimated Design Time**: 2-3 days

---

## 1. Design Principles

### 1.1 Core UX Goals
- **Clarity**: All UI elements clearly visible and understandable
- **Simplicity**: Minimal clutter, focus on gameplay
- **Responsiveness**: Touch targets 44x44 pts minimum (Apple HIG)
- **Feedback**: Visual/audio confirmation for all interactions
- **Accessibility**: High contrast, readable text sizes

### 1.2 Visual Style
- **Art Style**: Colorful, casual, friendly
- **Color Palette**: Warm earth tones (mining theme)
  - Primary: Golden yellow (#FFD700)
  - Secondary: Brown/bronze (#8B4513)
  - Accent: Teal/turquoise (#40E0D0)
  - Background: Dark cave gray (#2F2F2F)
  - Text: White (#FFFFFF) with dark outline
- **Typography**:
  - Heading: Bold, rounded sans-serif (e.g., Fredoka One)
  - Body: Clean sans-serif (e.g., Roboto)
  - Sizes: Minimum 18pt for body text

### 1.3 Safe Zones (iPhone)
```
Top Safe Zone: 44-88 pts (notch area)
Bottom Safe Zone: 34 pts (home indicator)
Side Margins: 16 pts minimum
```

---

## 2. Screen Inventory

### Required Screens (MVP)
1. Splash Screen
2. Main Menu
3. Level Select
4. Vehicle Selection
5. Settings
6. Shop (Cosmetics)
7. In-Game HUD
8. Pause Menu
9. Level Complete
10. Daily Rewards
11. Tutorial Overlay

### Future Screens (Post-Launch)
- Leaderboards
- Achievements
- Profile/Stats
- Social Features

---

## 3. Screen Specifications

### 3.1 Splash Screen

**Purpose**: App launch, branding

**Duration**: 2 seconds (while loading assets)

**Elements**:
- Game logo (centered)
- Studio logo (bottom)
- Loading spinner (if needed)

**Layout**:
```
┌─────────────────────┐
│                     │
│                     │
│   [GAME LOGO]       │
│                     │
│                     │
│                     │
│                     │
│   [Studio Logo]     │
└─────────────────────┘
```

**Specs**:
- Background: Gradient (dark to light)
- Logo: 200x200 pts
- Animation: Fade in + scale

---

### 3.2 Main Menu

**Purpose**: Primary navigation hub

**Elements**:
- Game logo (top center)
- Play button (large, center)
- Vehicle selection button
- Settings button
- Shop button
- Daily rewards indicator (badge)

**Layout**:
```
┌─────────────────────┐
│    [GAME LOGO]      │
│                     │
│                     │
│   ┌─────────────┐   │
│   │    PLAY     │   │  (Primary CTA)
│   └─────────────┘   │
│                     │
│  [Vehicles] [Shop]  │
│                     │
│  [Settings]  [📅]   │
└─────────────────────┘
```

**Specs**:
- Play button: 280x80 pts, golden gradient
- Secondary buttons: 120x60 pts
- Settings icon: 44x44 pts (bottom left)
- Daily rewards: 44x44 pts with notification badge

**Interactions**:
- Tap Play → Level Select screen
- Tap Vehicles → Vehicle Selection screen
- Tap Shop → Shop screen
- Tap Settings → Settings screen
- Tap Daily Rewards → Daily Rewards popup

---

### 3.3 Level Select

**Purpose**: Choose level to play

**Elements**:
- Level grid (2 columns, scroll vertical)
- Level cards showing:
  - Level number
  - Level name
  - Star rating (0-3)
  - Lock icon (if locked)
- Back button

**Layout**:
```
┌─────────────────────┐
│ [←] LEVEL SELECT    │
│                     │
│  ┌───┐    ┌───┐    │
│  │ 1 │    │ 2 │    │  Level 1, Level 2
│  │⭐⭐⭐│    │⭐⭐ │    │  (with stars)
│  └───┘    └───┘    │
│                     │
│  ┌───┐    ┌───┐    │
│  │ 3 │    │ 4 │    │
│  │⭐  │    │🔒 │    │  Level 3, Level 4 locked
│  └───┘    └───┘    │
│                     │
│  (scroll)           │
└─────────────────────┘
```

**Specs**:
- Level card: 140x160 pts
- Grid spacing: 16 pts
- Star icons: 20x20 pts
- Lock overlay: Semi-transparent gray

**Interactions**:
- Tap unlocked level → Load level
- Tap locked level → "Complete Level X first" message
- Swipe up/down → Scroll

---

### 3.4 Vehicle Selection

**Purpose**: Choose and preview vehicles

**Elements**:
- Vehicle carousel (swipe left/right)
- Vehicle 3D preview
- Vehicle stats:
  - Speed
  - Capacity
  - Collection radius
  - Special ability
- Select button (if owned)
- Purchase button (if locked)
- Back button

**Layout**:
```
┌─────────────────────┐
│ [←] VEHICLES        │
│                     │
│    < [VEHICLE] >    │  (3D model preview)
│                     │
│  Dual-Scoop Loader  │  (name)
│                     │
│  Speed: ████░       │
│  Capacity: ██████   │
│  Radius: ████░      │
│                     │
│  "Auto-Collect"     │  (special ability)
│                     │
│  ┌─────────────┐    │
│  │  1,000 💰   │    │  (purchase button)
│  └─────────────┘    │
└─────────────────────┘
```

**Specs**:
- Vehicle preview: 300x200 pts
- Stat bars: 200x20 pts
- Purchase button: 200x60 pts
- Swipe gesture: Horizontal carousel

**Interactions**:
- Swipe left/right → Change vehicle
- Tap Select → Equip vehicle (if owned)
- Tap Purchase → Confirm purchase popup

---

### 3.5 In-Game HUD

**Purpose**: Display game state during play

**Elements**:
- **Top Left**: Current score
- **Top Right**: Star progress bar (to 3 stars)
- **Bottom Left**: Cargo counter (e.g., "5/10")
- **Bottom Right**: Pause button
- **Center (first 10 sec)**: Target score reminder

**Layout**:
```
┌─────────────────────┐
│ Score: 250  [⭐⭐⭐] │  (score + star progress)
│                     │
│                     │
│   Target: 500       │  (fades after 10s)
│                     │
│      (3D GAME       │
│       VIEWPORT)     │
│                     │
│                     │
│ Cargo: 5/10    [⏸] │  (cargo + pause)
└─────────────────────┘
```

**Specs**:
- Score text: 24pt bold
- Star progress: 120x20 pts bar
- Cargo counter: 80x40 pts, rounded bg
- Pause button: 44x44 pts
- Target reminder: 200x40 pts, semi-transparent

**Interactions**:
- Tap Pause → Pause Menu
- HUD auto-hides after 3 seconds of no interaction (optional)

---

### 3.6 Pause Menu

**Purpose**: Pause game, access options

**Elements**:
- Resume button
- Restart button
- Settings button
- Main Menu button
- Semi-transparent overlay

**Layout**:
```
┌─────────────────────┐
│     (BLURRED        │
│      GAMEPLAY)      │
│                     │
│   ┌─────────────┐   │
│   │   RESUME    │   │
│   └─────────────┘   │
│   ┌─────────────┐   │
│   │   RESTART   │   │
│   └─────────────┘   │
│   ┌─────────────┐   │
│   │  SETTINGS   │   │
│   └─────────────┘   │
│   ┌─────────────┐   │
│   │ MAIN MENU   │   │
│   └─────────────┘   │
└─────────────────────┘
```

**Specs**:
- Overlay: 60% opacity black
- Buttons: 240x60 pts
- Button spacing: 16 pts vertical

**Interactions**:
- Tap Resume → Resume gameplay
- Tap Restart → Restart level (confirm)
- Tap Settings → Open settings
- Tap Main Menu → Return to main (confirm)

---

### 3.7 Level Complete

**Purpose**: Show results, encourage replay

**Elements**:
- Star rating animation (1-3 stars)
- Final score
- Gold earned
- Buttons:
  - Next Level
  - Retry (for 3 stars)
  - Main Menu
- Optional: Watch Ad for +100 gold

**Layout**:
```
┌─────────────────────┐
│                     │
│    LEVEL COMPLETE!  │
│                     │
│      ⭐ ⭐ ⭐        │  (animated)
│                     │
│   Score: 1,250      │
│   Gold: +150 💰     │
│                     │
│  ┌─────────────┐    │
│  │ NEXT LEVEL  │    │
│  └─────────────┘    │
│  ┌─────────────┐    │
│  │   RETRY     │    │
│  └─────────────┘    │
│                     │
│  [📺 +100 gold]     │  (ad button)
└─────────────────────┘
```

**Specs**:
- Stars: 60x60 pts each, stagger animation (0.2s delay)
- Score text: 32pt bold
- Buttons: 240x60 pts
- Ad button: 180x50 pts, smaller/secondary style

**Interactions**:
- Tap Next Level → Load next level
- Tap Retry → Restart current level
- Tap Ad button → Show rewarded video

**Animations**:
- Stars pop in sequence (scale + rotation)
- Score counts up from 0
- Confetti particle effect (3 stars only)

---

### 3.8 Settings

**Purpose**: Configure app preferences

**Elements**:
- Master volume slider
- Music volume slider
- SFX volume slider
- Control scheme toggle (touch/tilt)
- Vibration toggle
- Links:
  - Privacy Policy
  - Terms of Service
  - Contact Support
- Version number

**Layout**:
```
┌─────────────────────┐
│ [←] SETTINGS        │
│                     │
│ Master Volume       │
│ [━━━━━━━━━░]       │
│                     │
│ Music               │
│ [━━━━━━━░░░]       │
│                     │
│ SFX                 │
│ [━━━━━━━━━━]       │
│                     │
│ Controls: [Touch ▼] │
│ Vibration: [ON/OFF] │
│                     │
│ Privacy Policy      │
│ Terms of Service    │
│ Contact Support     │
│                     │
│ v1.0.0              │
└─────────────────────┘
```

**Specs**:
- Sliders: 280x40 pts
- Toggles: 60x34 pts
- Links: 18pt, underlined
- Version text: 12pt, gray

**Interactions**:
- Drag sliders → Adjust volume (live preview)
- Tap toggle → Switch ON/OFF
- Tap links → Open in Safari

---

### 3.9 Shop (Cosmetics)

**Purpose**: Purchase cosmetic vehicle skins

**Elements**:
- Skin grid (2 columns)
- Skin preview thumbnail
- Skin name
- Price (gold or gems)
- "Owned" badge
- Back button

**Layout**:
```
┌─────────────────────┐
│ [←] SHOP            │
│                     │
│  ┌───┐    ┌───┐    │
│  │[🚜]│    │[🚜]│    │  (skin previews)
│  │Red │    │Blue│    │
│  │500💰│    │✓  │    │  (price / owned)
│  └───┘    └───┘    │
│                     │
│  ┌───┐    ┌───┐    │
│  │[🚜]│    │[🚜]│    │
│  │Gold│    │Camo│    │
│  │1000│    │200💎│    │
│  └───┘    └───┘    │
│                     │
│  (scroll)           │
└─────────────────────┘
```

**Specs**:
- Skin card: 140x160 pts
- Thumbnail: 120x80 pts
- Price badge: 80x30 pts

**Interactions**:
- Tap skin → Preview + confirm purchase
- Tap owned skin → Equip skin

---

### 3.10 Daily Rewards

**Purpose**: Encourage daily play

**Elements**:
- 7-day calendar
- Each day shows reward (gold amount)
- Current day highlighted
- Claim button (if reward available)

**Layout**:
```
┌─────────────────────┐
│  DAILY REWARDS      │
│                     │
│  Day 1   Day 2   ... │
│  [50💰]  [100💰]     │
│   ✓      ✓          │  (claimed)
│                     │
│  Day 3              │
│  [150💰]            │  (today - pulsing)
│                     │
│  ┌─────────────┐    │
│  │   CLAIM     │    │
│  └─────────────┘    │
│                     │
│  Come back tomorrow!│
└─────────────────────┘
```

**Specs**:
- Day card: 80x80 pts
- Claim button: 200x60 pts, pulsing animation

**Interactions**:
- Tap Claim → Award gold, mark day claimed
- Auto-open on first daily launch

---

### 3.11 Tutorial Overlay

**Purpose**: Teach controls during Level 1

**Elements**:
- Semi-transparent overlay
- Highlight circle (spotlights UI element)
- Text instruction bubble
- "Got it" button or tap to continue

**Layout**:
```
┌─────────────────────┐
│                     │
│   ┌─────────────┐   │
│   │Drive near   │   │  (instruction)
│   │treasures to │   │
│   │collect them │   │
│   └─────────────┘   │
│        ↓            │
│   [TREASURE]        │  (highlighted)
│                     │
│                     │
│   Tap to continue   │
└─────────────────────┘
```

**Specs**:
- Overlay: 80% opacity black
- Highlight circle: Animated pulse
- Text bubble: 240x80 pts
- Arrow: Animated bounce

**Interactions**:
- Tap anywhere → Next tutorial step
- Sequence:
  1. Move controls
  2. Collect treasure
  3. Drive through gate
  4. Deposit at zone
  5. Complete level

---

## 4. UI Component Library

### 4.1 Buttons

| Type | Size | Style |
|------|------|-------|
| Primary | 280x80 pts | Golden gradient, white text, drop shadow |
| Secondary | 200x60 pts | Brown outline, white fill, black text |
| Icon | 44x44 pts | Circular, semi-transparent bg |
| Small | 120x40 pts | Flat color, small text |

### 4.2 Text Styles

| Element | Font | Size | Weight | Color |
|---------|------|------|--------|-------|
| H1 Heading | Fredoka One | 36pt | Bold | White w/ outline |
| H2 Heading | Fredoka One | 28pt | Bold | White w/ outline |
| Body | Roboto | 18pt | Regular | White |
| Small | Roboto | 14pt | Regular | Gray |
| Button | Roboto | 22pt | Bold | White |

### 4.3 Icons

**Required Icons (44x44 pts minimum)**:
- Settings (gear)
- Pause (two bars)
- Play (triangle)
- Back (left arrow)
- Close (X)
- Gold coin (💰)
- Gem (💎)
- Star (⭐)
- Lock (🔒)
- Calendar (📅)
- Video (📺)

**Source**: Use free icon packs from Figma community or create custom

---

## 5. Responsive Design (iPhone Sizes)

### Supported Devices
- iPhone 11 (414x896 pts) - Primary
- iPhone 13 Pro (390x844 pts)
- iPhone 13 Pro Max (428x926 pts)
- iPhone SE (375x667 pts) - Minimum

### Design Strategy
- Design at **1x scale (pts)** for iPhone 11
- Export at **@2x and @3x** for retina displays
- Use **Auto Layout** in Figma for responsiveness
- Test on smallest device (iPhone SE)

---

## 6. Animation Specifications

### Button Press
- Scale: 1.0 → 0.95 → 1.0
- Duration: 0.1s ease-in-out

### Screen Transitions
- Fade: Cross-dissolve, 0.3s
- Slide: From right, 0.25s ease-out

### UI Popups
- Scale: 0.8 → 1.05 → 1.0 (bounce)
- Duration: 0.4s

### Star Rating
- Stagger: 0.2s delay per star
- Animation: Scale + rotate (720°)
- Sound: "Ding" per star

### Cargo Counter Update
- Pulse: Scale 1.0 → 1.2 → 1.0
- Color: Flash golden yellow
- Duration: 0.2s

---

## 7. Figma Workflow

### Step 1: Setup
1. Create new Figma file: "Treasure Excavator UI"
2. Set frame size: 414x896 (iPhone 11)
3. Install plugins:
   - Iconify (free icons)
   - Unsplash (placeholder images)
   - Stark (accessibility check)

### Step 2: Design System
1. Create color palette (swatches)
2. Define text styles
3. Create button components
4. Build icon library

### Step 3: Screen Design
1. Design each screen (reference layouts above)
2. Use components for consistency
3. Add placeholder 3D vehicle images
4. Create interactive prototype

### Step 4: Prototype
1. Link screens with transitions
2. Add button hover states
3. Simulate tutorial flow
4. Test on mobile (Figma Mirror app)

### Step 5: Export
1. Select all artboards
2. Export settings:
   - Format: PNG
   - Scale: @2x, @3x
   - Naming: screen_name@2x.png
3. Organize in folders:
   - /UI/Screens/
   - /UI/Icons/
   - /UI/Buttons/

---

## 8. Accessibility Checklist

- [ ] Color contrast ratio ≥ 4.5:1 (WCAG AA)
- [ ] Touch targets ≥ 44x44 pts
- [ ] Text size ≥ 18pt (readable)
- [ ] Icons have text labels
- [ ] Support Dynamic Type (iOS)
- [ ] VoiceOver labels for all UI elements
- [ ] Haptic feedback for important actions

---

## 9. Implementation Notes for Unity

### UI Toolkit: Unity UI (Canvas)
- Use **Canvas Scaler**: Scale with Screen Size
- Reference Resolution: 1242x2688 (iPhone 13 Pro Max @3x)
- Match: Width or Height = 0.5 (balanced)

### Asset Import
- Import PNGs at @2x and @3x resolutions
- Set Texture Type: Sprite (2D and UI)
- Max Size: 2048
- Compression: Automatic

### UI Hierarchy
```
Canvas (Screen Space - Camera)
├── MainMenu
│   ├── Logo
│   ├── PlayButton
│   └── ...
├── LevelSelect
├── GameHUD
└── ...
```

### Prefabs
- Create prefab for each screen
- Activate/deactivate screens (don't destroy)
- Use object pooling for repeated elements (level cards)

---

## 10. Deliverables

### Phase 0 Deliverables
- [x] Wireframes planning document (this file)
- [ ] Figma design file (all screens)
- [ ] Interactive prototype in Figma
- [ ] Exported PNG assets (@2x, @3x)
- [ ] UI specification document (measurements, colors, fonts)

### Timeline
- Day 1: Setup Figma, design system, 5 screens
- Day 2: Remaining screens, prototype
- Day 3: Export, documentation, review

---

## 11. Review and Approval

### Before Phase 1 Implementation
- [ ] All screens designed in Figma
- [ ] Prototype tested on mobile device
- [ ] Accessibility checks passed
- [ ] Approved by product owner
- [ ] Assets exported and organized
- [ ] Ready for Unity implementation

---

*End of UI Wireframes Planning Document*

---

## Next Steps

1. Open Figma (https://figma.com)
2. Create account (free)
3. Start with Main Menu design
4. Reference this document for all specifications
5. Share Figma link with team for feedback
6. Iterate based on playtesting feedback

**Figma Community Resources:**
- Search "mobile game UI kit" for templates
- Check "casual game UI" for inspiration
- Use "iOS UI kit" for standard iOS components
