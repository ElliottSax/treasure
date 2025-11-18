# Tutorial Script - Treasure Excavator
## Step-by-Step Player Onboarding

**Version**: 1.0
**Last Updated**: 2025-11-18
**Target Duration**: 90 seconds
**Implementation**: Level 1 with overlay prompts

---

## 1. Tutorial Design Principles

### Goals
- ✅ Teach core mechanics in under 2 minutes
- ✅ Make player feel successful immediately
- ✅ Don't overwhelm with information
- ✅ Show, don't tell (visual > text)
- ✅ Let player experiment safely

### Approach
- **Contextual Learning**: Teach mechanics when needed
- **Progressive Disclosure**: One concept at a time
- **Forced Success**: Make first actions almost impossible to fail
- **Positive Reinforcement**: Celebrate every action
- **Skippable**: Allow experienced players to skip

---

## 2. Tutorial Flow Overview

```
Start → Movement → Collection → Gate Usage → Deposit → Win → Play More
  ↓        ↓          ↓            ↓           ↓       ↓
 5s      10s        15s          20s         30s     90s
```

**Total Steps**: 6 main teaching moments
**Total Duration**: 60-90 seconds (player-paced)

---

## 3. Pre-Tutorial Setup (Level 1 Configuration)

### Level Modifications for Tutorial
- **Spawn Position**: Clear view of first treasure (15m away)
- **Treasure Placement**: Linear path, impossible to miss
- **Gate Position**: Directly between treasure and deposit zone
- **Obstacles**: None (clear path)
- **Camera**: Slightly zoomed out for better view

### UI Setup
- All HUD elements visible
- Tutorial overlay layer (semi-transparent)
- Highlight circles for interactive elements
- Text bubbles with arrows

---

## 4. Tutorial Script (Step-by-Step)

---

### STEP 1: Welcome & Context
**Trigger**: Level loads
**Duration**: 5 seconds
**Player Action**: None (can skip with tap)

**UI Overlay**:
```
┌─────────────────────────────────┐
│                                 │
│   🏆 WELCOME TO TREASURE        │
│       EXCAVATOR!                │
│                                 │
│   Collect treasures, multiply   │
│   your haul, and score big!     │
│                                 │
│   [Tap to start tutorial]       │
│                                 │
└─────────────────────────────────┘
```

**Voiceover (Optional)**: "Welcome, treasure hunter! Let's learn how to strike it rich!"

**Analytics Event**: `tutorial_started`

---

### STEP 2: Movement Controls
**Trigger**: Player taps to continue
**Duration**: 10 seconds
**Player Action**: Move vehicle 10 meters forward

**UI Overlay**:
```
┌─────────────────────────────────┐
│   Touch and drag to drive       │
│        your vehicle              │
│                                 │
│   [Hand icon showing swipe]     │
│            ↓                    │
│      [Vehicle icon]             │
│                                 │
│   Try driving forward!          │
└─────────────────────────────────┘
```

**Implementation**:
- Highlight: Vehicle with pulsing circle
- Input: Touch controls enabled
- Success Condition: Vehicle moves >10m from spawn
- Fail-Safe: Arrow pointing forward if idle >5 seconds

**On Success**:
- ✅ Checkmark animation
- 🎵 Success chime
- Auto-advance to Step 3

**Analytics Event**: `tutorial_movement_complete`

---

### STEP 3: Treasure Collection
**Trigger**: Movement complete
**Duration**: 15 seconds
**Player Action**: Collect 3 treasures

**UI Overlay**:
```
┌─────────────────────────────────┐
│   Drive near treasures to       │
│   collect them automatically!   │
│                                 │
│        [Arrow pointing down]    │
│          💎💎💎                 │
│     (Treasures highlighted)     │
│                                 │
│   Cargo: 0/10 ← Watch this!    │
└─────────────────────────────────┘
```

**Implementation**:
- Highlight: First 3 treasures with glowing circles
- Treasure Position: Linear path, 5m apart
- Camera: Zoom to show treasures clearly
- Success Condition: Collect all 3 treasures

**Visual Feedback Per Treasure**:
- ✨ Particle trail to vehicle
- 🔊 "Ding!" sound
- 📈 Cargo counter animates: "1/10", "2/10", "3/10"

**On Success**:
- "Great job! You collected 3 treasures!" message
- Auto-advance to Step 4

**Analytics Event**: `tutorial_collection_complete`

---

### STEP 4: Multiplier Gate
**Trigger**: 3 treasures collected
**Duration**: 20 seconds
**Player Action**: Drive through x2 gate

**UI Overlay**:
```
┌─────────────────────────────────┐
│   Drive through the gate to     │
│   MULTIPLY your treasures!      │
│                                 │
│        [Arrow pointing]         │
│           ↓                     │
│      [x2 GATE]                  │
│     (Glowing arch)              │
│                                 │
│   3 treasures x2 = 6!           │
└─────────────────────────────────┘
```

**Implementation**:
- Highlight: x2 Gate with pulsing golden circle
- Gate Position: 10m ahead, impossible to miss
- Path: Funneled (rocks block alternate routes)
- Success Condition: Drive through gate

**On Gate Trigger**:
- 🎆 Explosion of golden particles
- 🔊 "Ka-ching!" sound
- 📈 Cargo counter animates: "3/10" → "6/10" (count-up animation)
- Screen shake (subtle)
- "MULTIPLIED x2!" floating text

**On Success**:
- "Amazing! Your treasures doubled!" message
- Auto-advance to Step 5

**Analytics Event**: `tutorial_gate_complete`

---

### STEP 5: Deposit Zone
**Trigger**: Gate passed
**Duration**: 30 seconds
**Player Action**: Drive into deposit zone

**UI Overlay**:
```
┌─────────────────────────────────┐
│   Now deposit your treasures    │
│   to score points!              │
│                                 │
│        [Arrow pointing]         │
│           ↓                     │
│    [DEPOSIT ZONE]               │
│   (Large glowing circle)        │
│                                 │
│   Your score: 0 → 60            │
└─────────────────────────────────┘
```

**Implementation**:
- Highlight: Deposit zone with bright pulsing circle
- Zone Position: 15m ahead, large target (10m radius)
- Success Condition: Enter deposit zone

**On Zone Entry**:
- 🎆 Treasure "rains down" from vehicle (particle effect)
- 🔊 "Cash register" sound
- 📈 Score counter animates: "0" → "60" (count-up)
- 💰 "+60 points!" floating text (golden, large)
- Cargo counter resets: "6/10" → "0/10"

**On Success**:
- "Excellent! You scored your first points!" message
- Auto-advance to Step 6

**Analytics Event**: `tutorial_deposit_complete`

---

### STEP 6: Complete the Level
**Trigger**: First deposit made
**Duration**: Until target reached
**Player Action**: Reach 100 points to win

**UI Overlay**:
```
┌─────────────────────────────────┐
│   Keep collecting, multiplying, │
│   and depositing to reach       │
│   the target score!             │
│                                 │
│   Target: 100 points            │
│   Current: 60 points            │
│                                 │
│   You've got this! 🎯           │
└─────────────────────────────────┘
```

**Implementation**:
- Overlay fades after 3 seconds
- Tutorial system disengaged (full control to player)
- Remaining treasures: 2 more (value: 10 each)
- Additional x2 gate available
- Math: 2 treasures x2 = 40 points → Total = 100 ✅

**On Target Reached**:
- Level Complete screen appears
- Star rating: Guaranteed 1 star minimum
- Tutorial complete!

**Analytics Event**: `tutorial_complete`, `level_1_complete`

---

### STEP 7: Post-Tutorial Encouragement
**Trigger**: Level Complete screen shown
**Duration**: Until player continues
**Player Action**: Tap "Next Level" or "Main Menu"

**Level Complete Overlay**:
```
┌─────────────────────────────────┐
│   🎉 TUTORIAL COMPLETE!          │
│                                 │
│      ⭐ (1 star earned)          │
│                                 │
│   Score: 100                    │
│   Gold Earned: +50 💰           │
│                                 │
│   You're ready to play!         │
│                                 │
│   [NEXT LEVEL] [MAIN MENU]      │
└─────────────────────────────────┘
```

**Encouragement Message**:
- "You're a natural treasure hunter!"
- "Ready for more challenging levels?"
- "Each level has new gates and treasures!"

**Analytics Event**: `tutorial_completed_full`

---

## 5. Tutorial System Implementation (Unity)

### TutorialManager.cs Structure
```csharp
public class TutorialManager : MonoBehaviour
{
    public enum TutorialStep
    {
        Welcome,
        Movement,
        Collection,
        Gate,
        Deposit,
        Complete
    }

    private TutorialStep currentStep;

    // Methods:
    // - StartTutorial()
    // - AdvanceStep()
    // - HighlightElement(GameObject target)
    // - ShowOverlay(string message)
    // - CheckStepCompletion()
}
```

### Key Components
1. **OverlayUI**: Semi-transparent panel with text bubble
2. **HighlightCircle**: Pulsing circle shader around target
3. **ArrowPointer**: Animated arrow pointing to next objective
4. **ProgressTracker**: Monitors player actions for step completion

### Persistence
- Save `tutorial_completed` flag to PlayerPrefs
- Check on Level 1 load: If true, skip tutorial
- "Skip Tutorial" button (bottom right, small)

---

## 6. Tutorial Variations (Accessibility)

### For Experienced Players
- "Skip Tutorial" button visible from Step 1
- Dismiss overlay with tap (don't block input)
- Auto-skip if player completes action before prompt

### For Struggling Players
- Extended timeouts (no rush)
- More obvious visual cues (bigger arrows, brighter highlights)
- Optional "Need Help?" button → Show hint

### Localization Support
- All text strings externalized
- Visual-first design (less text dependency)
- Icons and arrows universal

---

## 7. Tutorial Skip Flow

**Option 1: Skip Button (Recommended)**
- Small button in top-right: "Skip Tutorial"
- Tap → Confirm popup: "Skip tutorial? You can replay Level 1 anytime."
- If confirmed: Jump to normal Level 1 gameplay

**Option 2: Auto-Detect Experience**
- If player completes 3 steps without prompts, ask: "You seem experienced! Skip tutorial?"

---

## 8. Success Metrics

### Tutorial Completion Rate
- **Target**: >85% of players complete tutorial
- **Measure**: % of players who see Step 1 and reach Step 7

### Drop-Off Points (Watch For)
- Step 2 (Movement): Controls confusing?
- Step 4 (Gate): Concept unclear?
- Step 6 (Completion): Too hard to reach target?

### Time to Complete
- **Target**: 60-90 seconds average
- **Measure**: Time from Step 1 to Level Complete

### Retention After Tutorial
- **Target**: 60% of players who complete tutorial play Level 2
- **Measure**: % who start Level 2 after tutorial

---

## 9. Testing Checklist

Before Phase 1 completion:
- [ ] Tutorial triggers on first launch
- [ ] Tutorial skippable at any point
- [ ] All 6 steps advance correctly
- [ ] Visual highlights appear/disappear correctly
- [ ] Audio cues play at right moments
- [ ] Overlays don't block critical UI
- [ ] Tutorial doesn't re-trigger on Level 1 replay
- [ ] "Skip Tutorial" button works
- [ ] Tutorial completion saves to PlayerPrefs
- [ ] Analytics events fire correctly
- [ ] Localization strings load (if applicable)
- [ ] Works on smallest supported device (iPhone SE)

---

## 10. Playtesting Questions

Ask playtesters:
1. Did you understand the core gameplay loop after the tutorial?
2. Were any steps confusing or unclear?
3. Was the tutorial too long, too short, or just right?
4. Did you feel frustrated at any point?
5. Would you skip the tutorial if replaying?

**Iterate based on feedback!**

---

## 11. Post-Tutorial Onboarding

### Additional Help Resources
- **Pause Menu**: "How to Play" button (re-watch tutorial)
- **Level Select**: Tooltips for locked levels
- **Vehicle Select**: Stat explanations on first visit

### Gradual Complexity
- Level 2: Introduce 2nd gate type (x3)
- Level 3: Teach capacity limits
- Level 5: Introduce x5 gates
- Level 8: Introduce x10 gate (discovery, not tutorial)

---

## 12. Common Tutorial Mistakes to Avoid

❌ **Don't**: Overload with text
✅ **Do**: Use visual cues and arrows

❌ **Don't**: Force long unskippable cutscenes
✅ **Do**: Make every step tappable to continue

❌ **Don't**: Punish players for "failing" tutorial
✅ **Do**: Make tutorial impossible to fail

❌ **Don't**: Repeat obvious information
✅ **Do**: Assume some game literacy (mobile controls)

❌ **Don't**: Hide "Skip Tutorial" option
✅ **Do**: Prominently display skip for experienced players

---

## 13. Tutorial Localization

### Supported Languages (Phase 1)
- English (primary)

### Post-Launch (v1.1+)
- Spanish
- French
- German
- Japanese
- Chinese (Simplified)
- Portuguese

### String IDs
```
tutorial_welcome_title
tutorial_welcome_body
tutorial_movement_prompt
tutorial_collection_prompt
tutorial_gate_prompt
tutorial_deposit_prompt
tutorial_complete_title
tutorial_skip_confirm
```

---

## 14. Dialogue/Voiceover Script (Optional)

If budget allows, add voiceover:

**Step 1**: "Welcome, treasure hunter! Let's learn the ropes."
**Step 2**: "Use your finger to steer the vehicle. Give it a try!"
**Step 3**: "Drive close to treasures to scoop them up. Easy!"
**Step 4**: "Now for the fun part—drive through that gate to multiply!"
**Step 5**: "Excellent! Now deposit your haul in the glowing zone."
**Step 6**: "You're a natural! Keep going to reach the target score."
**Step 7**: "Tutorial complete! You're ready for bigger challenges!"

**Voice Actor**: Friendly, encouraging, gender-neutral tone

---

## 15. Implementation Priority

### Phase 1 (Must-Have)
- ✅ Steps 1-6 (core tutorial)
- ✅ Overlay UI
- ✅ Highlight system
- ✅ Skip button
- ✅ Completion tracking

### Phase 2 (Nice-to-Have)
- 🔲 Voiceover
- 🔲 Animated tutorial mascot (e.g., miner character)
- 🔲 Replay tutorial option in settings
- 🔲 Advanced tips (post-tutorial)

---

*End of Tutorial Script*

---

## Developer Notes

### Unity Implementation Timeline
- **Week 1**: Basic overlay UI system
- **Week 2**: Highlight and arrow components
- **Week 3**: Step sequencing logic
- **Week 4**: Polish, audio, testing

### Dependencies
- UI system (Canvas, TextMeshPro)
- Analytics integration (Firebase)
- PlayerPrefs for save system

### Testing Device
- Test on iPhone SE (smallest screen) to ensure overlays fit

---

## Appendix: Tutorial Analytics Events

```json
{
  "tutorial_started": { "timestamp": "2025-01-15T10:30:00Z" },
  "tutorial_movement_complete": { "duration_seconds": 8 },
  "tutorial_collection_complete": { "duration_seconds": 12 },
  "tutorial_gate_complete": { "duration_seconds": 15 },
  "tutorial_deposit_complete": { "duration_seconds": 20 },
  "tutorial_complete": { "total_duration_seconds": 85 },
  "tutorial_skipped": { "step_skipped_at": "Movement" }
}
```

Track these events to optimize tutorial flow over time.

---

**Status**: Ready for Phase 1 Implementation
**Approval**: Pending
