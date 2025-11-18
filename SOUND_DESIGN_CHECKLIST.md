# Sound Design Checklist
## Treasure Excavator - Complete Audio Asset List

**Version**: 1.0
**Last Updated**: 2025-11-18
**Total SFX Needed**: 25-30
**Total Music Tracks**: 3-4

---

## 1. Audio Philosophy

### Design Goals
- **Satisfying Feedback**: Every player action has audio response
- **Casual-Friendly**: Upbeat, non-stressful soundscape
- **Mobile-Optimized**: Compressed for small file sizes (<10MB total)
- **Loopable**: All music and ambient sounds loop seamlessly

### Technical Specifications
- **Format**: .wav (source), .ogg (Unity import)
- **Sample Rate**: 44.1 kHz
- **Bit Depth**: 16-bit
- **Compression**: Vorbis (Unity's OGG compression)

---

## 2. Sound Effects (SFX) Checklist

### Core Gameplay SFX

#### Treasure Collection (5 SFX)
- [ ] **Collect Small Treasure** - Bright "ding" chime (0.3s)
  - Pitch: Medium-high
  - Volume: Medium
  - Example: Coin pickup sound

- [ ] **Collect Medium Treasure** - Deeper "ding" + sparkle (0.5s)
  - Pitch: Medium
  - Volume: Medium-high
  - Example: Gem collect sound

- [ ] **Collect Large Treasure** - Rich "ka-ching" + echo (1s)
  - Pitch: Medium-low
  - Volume: High
  - Example: Treasure chest open sound

- [ ] **Auto-Collect Whoosh** (Dual-Scoop only) - Magnetic pull (0.4s)
  - Pitch: Rising sweep
  - Volume: Medium
  - Example: Vacuum/magnet sound

- [ ] **Cargo Full Warning** - Soft "bonk" (rejection sound) (0.2s)
  - Pitch: Low
  - Volume: Low
  - Example: Error/full sound

#### Gate Interactions (4 SFX)
- [ ] **Pass Through x2 Gate** - Bronze "whoosh" + chime (0.6s)
  - Pitch: Medium
  - Volume: High
  - Example: Magic spell cast

- [ ] **Pass Through x3 Gate** - Silver "whoosh" + brighter chime (0.7s)
  - Pitch: Medium-high
  - Volume: High
  - Example: Power-up sound

- [ ] **Pass Through x5 Gate** - Gold "whoosh" + triumphant chime (0.8s)
  - Pitch: High
  - Volume: Very high
  - Example: Achievement unlock

- [ ] **Pass Through x10 Gate** - Epic explosion + echo (1.2s)
  - Pitch: Wide spectrum
  - Volume: Maximum
  - Example: Epic win sound

#### Deposit Zone (3 SFX)
- [ ] **Enter Deposit Zone** - "Cash register" cha-ching (0.5s)
  - Pitch: Medium
  - Volume: High
  - Example: Classic cash register

- [ ] **Treasure Cascade** - Coins pouring (1.5s, can interrupt)
  - Pitch: Varied (random pitch)
  - Volume: Medium
  - Example: Waterfall of coins

- [ ] **Score Count-Up** - Rapid tick-tick-tick (loops until done)
  - Pitch: High
  - Volume: Low-medium
  - Example: Slot machine ticking

#### Vehicle Sounds (5 SFX + 1 loop)
- [ ] **Engine Idle Loop** - Rumble (seamless loop)
  - Pitch: Low
  - Volume: Low (ambient)
  - Example: Diesel engine idle

- [ ] **Acceleration Rev** - Engine revving up (1s)
  - Pitch: Rising
  - Volume: Medium
  - Example: Car accelerating

- [ ] **Brake/Stop** - Screech (0.4s)
  - Pitch: High
  - Volume: Medium
  - Example: Tire screech

- [ ] **Nitro Boost Activation** (Nitro Hauler only) - Rocket ignite (0.8s)
  - Pitch: Explosive
  - Volume: Very high
  - Example: Jet engine ignition

- [ ] **Nitro Boost Loop** (Nitro Hauler only) - Jet engine (loop during boost)
  - Pitch: High, pulsing
  - Volume: High
  - Example: Rocket thrust

- [ ] **Collision/Bump** - Metallic clang (0.3s)
  - Pitch: Medium
  - Volume: Medium
  - Example: Metal impact

### UI Sounds (8 SFX)
- [ ] **Button Tap** - Soft click (0.1s)
  - Pitch: Medium
  - Volume: Low
  - Example: UI click

- [ ] **Button Hover/Highlight** - Subtle tick (0.05s, optional)
  - Pitch: High
  - Volume: Very low

- [ ] **Menu Open/Close** - Swish (0.3s)
  - Pitch: Medium
  - Volume: Medium
  - Example: Paper swipe

- [ ] **Star Earned** - Magical twinkle (0.6s)
  - Pitch: High, rising
  - Volume: High
  - Example: Star collect sound

- [ ] **Level Complete Fanfare** - Triumphant jingle (2-3s)
  - Pitch: Celebratory
  - Volume: High
  - Example: Victory theme

- [ ] **Gold Earned Notification** - Coin jingle (0.5s)
  - Pitch: Bright
  - Volume: Medium
  - Example: Currency gain sound

- [ ] **Purchase Confirm** - Cash register ding (0.4s)
  - Pitch: Satisfying
  - Volume: Medium
  - Example: Successful purchase

- [ ] **Error/Cannot Afford** - Buzzer (0.3s)
  - Pitch: Low
  - Volume: Medium
  - Example: Error beep

### Ambient/Contextual (3 SFX)
- [ ] **Level Start Countdown** - Beep-beep-beep-GO! (3s total)
  - Pitch: Beeps medium, GO! high
  - Volume: Medium
  - Example: Race countdown

- [ ] **Tutorial Prompt Appear** - Soft "pop" (0.2s)
  - Pitch: Medium-high
  - Volume: Low
  - Example: Notification pop

- [ ] **Pause** - Time stop "whoosh" (0.4s)
  - Pitch: Descending
  - Volume: Medium
  - Example: Time freeze sound

### Special Effects (Optional, 3 SFX)
- [ ] **Confetti (3-Star Celebration)** - Party popper (1s)
  - Pitch: Bright
  - Volume: Medium
  - Example: Celebration burst

- [ ] **Daily Reward Claim** - Gift unwrap (0.8s)
  - Pitch: Cheerful
  - Volume: Medium
  - Example: Present open

- [ ] **Vehicle Unlock** - Heroic reveal (2s)
  - Pitch: Epic
  - Volume: High
  - Example: Achievement fanfare

---

## 3. Music Tracks Checklist

### Track 1: Main Menu Theme
- [ ] **Duration**: 60-90 seconds (seamless loop)
- **Mood**: Upbeat, adventurous, welcoming
- **Tempo**: 120-140 BPM
- **Instruments**: Orchestral + electronic blend
- **Reference**: Indiana Jones theme meets casual mobile game music
- **File Size Target**: <2MB

### Track 2: Gameplay Loop
- [ ] **Duration**: 2-3 minutes (seamless loop)
- **Mood**: Energetic, focused, non-stressful
- **Tempo**: 130-150 BPM
- **Instruments**: Light percussion, synth, bass
- **Dynamic Layers**: Intensity increases near goals (adaptive music, optional)
- **Reference**: Mario Kart background music
- **File Size Target**: <3MB

### Track 3: Level Complete Victory Jingle
- [ ] **Duration**: 5-10 seconds (one-shot)
- **Mood**: Triumphant, celebratory
- **Tempo**: Free (fanfare-style)
- **Instruments**: Brass, strings, chimes
- **Reference**: Classic game victory fanfare
- **File Size Target**: <500KB

### Track 4: Ambient Mining Cave (Optional)
- [ ] **Duration**: 3-5 minutes (seamless loop)
- **Mood**: Atmospheric, subtle, background
- **Tempo**: Slow (60-80 BPM)
- **Instruments**: Echoing drips, wind, distant rumble
- **Use Case**: Alternative to gameplay music (settings option)
- **File Size Target**: <2MB

---

## 4. Audio Source Options

### Option 1: Envato Elements (Recommended)
**Cost**: $16.50/month (cancel after 1 month)
**License**: Commercial use allowed
**Process**:
1. Subscribe to Envato Elements
2. Search for each SFX by keyword (e.g., "coin collect", "treasure chest")
3. Download 25-30 SFX + 3 music tracks
4. Cancel subscription after download

**Pros**:
- High quality
- Huge library
- Commercial license included

**Cons**:
- Requires subscription (but can cancel)

### Option 2: Unity Asset Store
**Cost**: $20-40 per SFX pack, $10-20 per music pack
**License**: Included with purchase
**Recommended Packs**:
- "Casual Game SFX Pack" (~$25, 100+ sounds)
- "Mobile UI Sounds" (~$15, 50+ UI sounds)
- "Upbeat Game Music Pack" (~$20, 5 tracks)

**Pros**:
- One-time purchase
- Easy Unity integration
- Asset Store sales/discounts

**Cons**:
- Less variety than Envato
- Higher upfront cost

### Option 3: Free Resources (Budget Option)
**Cost**: $0
**Sources**:
- Freesound.org (CC0 license sounds)
- Incompetech.com (royalty-free music)
- OpenGameArt.org (game SFX)

**Pros**:
- Free
- Large community library

**Cons**:
- Quality varies
- Need to verify licenses carefully
- May not find perfect matches

**Recommendation for Bootstrap**: Option 3 (free) for MVP, upgrade to Option 1/2 after soft launch revenue.

---

## 5. Audio Implementation (Unity)

### Audio Mixer Setup
```
Master
├── Music (volume control)
├── SFX (volume control)
│   ├── Gameplay SFX
│   ├── UI SFX
│   └── Vehicle SFX
└── Ambient (volume control)
```

### AudioManager.cs Structure
```csharp
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SFX Clips")]
    public AudioClip collectSmall;
    public AudioClip collectMedium;
    // ... (all SFX)

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        // Play one-shot SFX
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        // Crossfade music
    }
}
```

### Audio Settings (PlayerPrefs)
```csharp
float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
```

---

## 6. Audio Budget

### File Size Targets
- Total SFX: <5MB (25-30 files @ ~100-200KB each)
- Total Music: <5MB (3-4 tracks @ ~1-2MB each)
- **Total Audio Budget**: <10MB (within 150MB app size target)

### Compression Strategy
- SFX: OGG Vorbis, quality 70%
- Music: OGG Vorbis, quality 50% (looping music tolerates more compression)
- Short sounds (<1s): Can use WAV uncompressed (minimal size impact)

---

## 7. Audio Testing Checklist

### Before Phase 3 Completion:
- [ ] All SFX trigger correctly in gameplay
- [ ] No audio clipping or distortion
- [ ] Volume levels balanced (no jarring loud sounds)
- [ ] Music loops seamlessly (no pops/clicks)
- [ ] Settings sliders work correctly
- [ ] Mute toggle works
- [ ] Audio persists across scenes correctly
- [ ] No audio lag on older devices (iPhone 11 test)
- [ ] Adaptive music layers work (if implemented)
- [ ] All AudioClips assigned in Unity (no missing references)

---

## 8. Accessibility Features

### Audio Options (Settings Menu)
- [ ] Master volume slider (0-100%)
- [ ] Music volume slider (0-100%)
- [ ] SFX volume slider (0-100%)
- [ ] Mute all toggle
- [ ] Vibration toggle (haptic feedback)

### Hearing Impaired Considerations
- [ ] Visual feedback for all audio cues (e.g., particle effects on collect)
- [ ] Subtitle option for voiceover (if added post-launch)
- [ ] Screen shake option for major events

---

## 9. Phase 0 Action Items

### Immediate Tasks:
1. [ ] Choose audio source (Envato/Asset Store/Free)
2. [ ] Create audio asset acquisition budget ($50-100)
3. [ ] Download/purchase SFX pack
4. [ ] Download/purchase music tracks
5. [ ] Import into Unity project
6. [ ] Organize in Assets/Audio/ folder structure:
   ```
   Assets/Audio/
   ├── SFX/
   │   ├── Gameplay/
   │   ├── UI/
   │   ├── Vehicle/
   │   └── Ambient/
   └── Music/
       ├── MainMenu.ogg
       ├── Gameplay.ogg
       └── Victory.ogg
   ```

### Before Phase 1:
- [ ] All audio assets sourced and downloaded
- [ ] Audio budget confirmed ($0-100)
- [ ] Licenses verified (commercial use)
- [ ] Assets organized in project folder

---

## 10. Post-Launch Audio Enhancements

### v1.1 Features:
- [ ] Adaptive music (intensity changes based on gameplay)
- [ ] Vehicle-specific engine sounds (unique per vehicle)
- [ ] Environmental audio (cave ambiance, wind, drips)
- [ ] Voice acting for tutorial (optional)
- [ ] Additional music tracks for variety

### v1.2 Features:
- [ ] Seasonal music themes (holiday events)
- [ ] User-uploaded music support (play from library)
- [ ] Dynamic audio mixing (ducks music during important SFX)

---

*End of Sound Design Checklist*

**Status**: Ready for Asset Acquisition
**Budget**: $0-100 (bootstrap) or $50-100 (recommended)
