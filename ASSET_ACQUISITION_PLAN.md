# Asset Acquisition Plan
## Treasure Excavator - Complete Asset Sourcing Strategy

**Version**: 1.0
**Last Updated**: 2025-11-18
**Total Budget**: $900 minimum, $5,000 recommended

---

## 1. Asset Budget Breakdown

### Bootstrap Budget ($900)
```
Apple Developer Account: $99
3D Models: $200
Audio (SFX + Music): $100
Fonts: $0 (free commercial licenses)
UI Assets: $50 (or free from Figma)
Marketing (App Icon, Screenshots): $150
Contingency: $301
────────────────────────
TOTAL: $900
```

### Recommended Budget ($5,000)
```
Apple Developer Account: $99
Legal (LLC + Trademark): $500
3D Models: $400
Audio (SFX + Music): $200
Fonts: $50
UI Assets: $100
Marketing Assets: $500
Professional App Icon: $300
Soft Launch Marketing: $1,000
Testing Device (iPhone 11): $300 (if needed)
Firebase/Analytics: $0 (free tier)
Contingency: $1,551
────────────────────────
TOTAL: $5,000
```

---

## 2. 3D Model Assets

### Required Models

#### Vehicles (3 minimum, 4 ideal)
1. **Starter Bulldozer**
   - Style: Low-poly, cartoon/stylized
   - Polycount: 5,000-10,000 tris (mobile optimized)
   - Features: Movable blade, cabin, wheels
   - Cost: $30-50 or included in pack

2. **Dual-Scoop Loader**
   - Style: Matching bulldozer style
   - Polycount: 5,000-10,000 tris
   - Features: Dual scoop buckets, wider chassis
   - Cost: $30-50 or included in pack

3. **Nitro Hauler**
   - Style: Sporty, racing-inspired
   - Polycount: 5,000-10,000 tris
   - Features: Aerodynamic design, exhaust
   - Cost: $30-50 or included in pack

4. **Mega Vault Truck** (Post-launch)
   - Style: Large, armored
   - Polycount: 8,000-12,000 tris
   - Cost: $30-50

#### Environment Assets
- **Mining Cave Modular Kit**: $40-60
  - Walls, floors, ceiling tiles
  - Rock formations, stalagmites
  - Support beams, tracks
  - Modular pieces for 8 levels

- **Deposit Zone**: Free (can create with Unity primitives + shader)

#### Treasures (3 types)
- **Small Treasure**: Gold coin (simple model)
- **Medium Treasure**: Gem cluster
- **Large Treasure**: Treasure chest
- **Cost**: $20 (or included in treasure pack)

#### Multiplier Gates
- **Gate Arch Models**: Can create with Unity primitives
  - x2, x3, x5, x10 gates (same model, different materials)
- **Cost**: $0 (DIY) or $15 (purchase gate pack)

### Recommended Unity Asset Store Packs

#### Option 1: All-in-One Pack (Recommended for Bootstrap)
**"Simple Mining Pack" or similar**
- Cost: ~$100
- Includes: Vehicles, environment, props, treasures
- Pros: Cohesive art style, one purchase
- Cons: Less customization

#### Option 2: Individual Packs (Recommended for Quality)
1. **"Cartoon Vehicles Pack"** - $40
   - 10+ stylized vehicles (pick 3-4)
2. **"Modular Cave Environment"** - $60
   - Complete cave tileset
3. **"Treasure & Collectibles Pack"** - $25
   - Coins, gems, chests
4. **"Low Poly Particle Effects"** - $20
   - Collection trails, explosions, sparkles

**Total**: $145

#### Option 3: Free Assets (Bootstrap Budget)
**Sources**:
- **Unity Asset Store (Free)**: Search "free low poly", "free cartoon"
- **Sketchfab (CC0 License)**: https://sketchfab.com/3d-models?features=downloadable&sort_by=-likeCount
- **Poly Pizza**: https://poly.pizza (formerly Google Poly)

**Pros**: $0 cost
**Cons**: Inconsistent art style, need to modify/retexture

---

## 3. Audio Assets

### Required Audio

#### SFX (25-30 sounds)
- Treasure collection: 5 SFX
- Gate interactions: 4 SFX
- Deposit zone: 3 SFX
- Vehicle sounds: 6 SFX
- UI sounds: 8 SFX
- Ambient/contextual: 3 SFX

#### Music (3-4 tracks)
- Main menu theme
- Gameplay loop
- Victory jingle
- Ambient cave (optional)

### Recommended Sources

#### Option 1: Envato Elements (Best Value)
- **Cost**: $16.50/month (cancel after 1 month)
- **Process**:
  1. Subscribe
  2. Download 30 SFX + 3 music tracks
  3. Cancel before month 2
- **License**: Commercial use included
- **Total Cost**: $17

#### Option 2: Unity Asset Store Packs
- **"Casual Game SFX Pack"**: $25 (100+ sounds)
- **"Mobile UI Sounds"**: $15 (50 UI sounds)
- **"Upbeat Game Music Collection"**: $20 (5 tracks)
- **Total Cost**: $60

#### Option 3: Free Resources
- **Freesound.org**: CC0 license sounds (verify each)
- **Incompetech.com**: Royalty-free music by Kevin MacLeod
- **OpenGameArt.org**: Community-submitted game audio
- **Total Cost**: $0

**Recommendation**: Option 1 (Envato) for MVP quality at low cost.

---

## 4. UI Assets

### Required UI Elements

#### Icons (44x44 pts minimum)
- Settings (gear)
- Pause (two bars)
- Play (triangle)
- Back (left arrow)
- Close (X)
- Gold coin
- Gem
- Star (filled and outline)
- Lock
- Calendar
- Video ad

#### UI Textures
- Button backgrounds (normal, pressed, disabled states)
- Panel backgrounds (semi-transparent)
- Progress bars (health, loading)
- Star rating (1-3 stars)

### Recommended Sources

#### Option 1: Figma Design (Free)
- **Cost**: $0
- **Process**:
  1. Design all UI in Figma (see UI_WIREFRAMES_PLAN.md)
  2. Export as PNG @2x and @3x
  3. Import to Unity
- **Pros**: Fully custom, free
- **Cons**: Requires design skills (2-3 days)

#### Option 2: Unity Asset Store UI Kit
- **"Mobile Game UI Kit"**: $30
- **"Casual Game UI Pack"**: $20
- **Pros**: Ready-made, professional
- **Cons**: Less unique (others may use same assets)

#### Option 3: Free UI Packs
- **Unity Asset Store**: Search "free UI" (100+ results)
- **Kenney.nl**: Free game assets including UI
- **Cost**: $0

**Recommendation**: Option 1 (Figma) for unique branding, or Option 2 for speed.

---

## 5. Fonts

### Required Fonts

#### Heading Font
- Style: Bold, rounded, friendly
- Example: Fredoka One, Baloo, Bowlby One
- License: Commercial use (SIL Open Font License)

#### Body Font
- Style: Clean sans-serif, readable
- Example: Roboto, Open Sans, Lato
- License: Commercial use

### Recommended Sources

#### Google Fonts (Free, Recommended)
- **Cost**: $0
- **Process**:
  1. Visit https://fonts.google.com
  2. Search for desired fonts
  3. Download .ttf files
  4. Import to Unity (Assets/Fonts/)
- **License**: All Google Fonts are free for commercial use

#### Premium Fonts (Optional)
- **MyFonts.com**: $30-50 per font
- **Only if budget allows and branding is critical**

**Recommendation**: Google Fonts (Fredoka One + Roboto).

---

## 6. Particle Effects (VFX)

### Required Effects

#### Gameplay VFX
- Treasure collection trail
- Gate pass explosion
- Deposit cascade (coins falling)
- Vehicle exhaust smoke
- Nitro flames (Nitro Hauler)
- Level complete confetti

### Recommended Sources

#### Unity Built-In Particle System (Free)
- **Cost**: $0
- **Process**: Create custom particles in Unity
- **Pros**: Fully customizable
- **Cons**: Time-consuming (1-2 days)

#### Unity Asset Store VFX Packs
- **"Cartoon FX Remaster"**: $40 (200+ effects)
- **"Epic Toon FX"**: $50 (high quality)
- **Pros**: Professional, ready-made
- **Cons**: Higher cost

#### Free Particle Packs
- **Unity Particle Pack** (free on Asset Store)
- **Cost**: $0

**Recommendation**: Unity Particle Pack (free) + custom tweaks.

---

## 7. App Icon & Marketing Assets

### App Icon (1024x1024 required)

#### Option 1: Professional Designer (Recommended)
- **Cost**: $100-300
- **Platform**: Fiverr, Upwork, 99designs
- **Deliverables**: 1024x1024 PNG, all iOS icon sizes
- **Timeline**: 3-7 days

#### Option 2: DIY in Figma/Photoshop
- **Cost**: $0
- **Tools**: Figma (free), Canva (free tier)
- **Pros**: Full control
- **Cons**: May look amateurish without design skills

#### Option 3: AI-Generated (Midjourney, DALL-E)
- **Cost**: $10-20/month subscription
- **Process**: Generate treasure/vehicle icon, refine in Figma
- **Pros**: Unique, fast
- **Cons**: May need touchups

**Recommendation**: Option 1 (professional) for best first impression.

### Screenshots (App Store Listing)

#### Required Sizes (iPhone)
- 6.5" Display: 1242 x 2688 px (iPhone 13 Pro Max)
- 5.5" Display: 1242 x 2208 px (iPhone 8 Plus)

#### Content Needed
- 3-5 gameplay screenshots showing:
  1. Vehicle collecting treasures
  2. Driving through multiplier gate
  3. Level complete screen (3 stars)
  4. Vehicle selection menu
  5. Diverse level environments

**Cost**:
- DIY: $0 (capture in Unity, add text overlays in Canva)
- Professional: $150-300 (designer creates promotional screenshots)

**Recommendation**: DIY for MVP, upgrade for v1.1.

---

## 8. Legal Assets

### Privacy Policy & Terms of Service

#### Option 1: Template (Free)
- **Cost**: $0
- **Source**: Use templates in this repository (PRIVACY_POLICY.md, TERMS_OF_SERVICE.md)
- **Process**: Fill in placeholders, review carefully
- **Pros**: Free, immediate
- **Cons**: Generic, no legal review

#### Option 2: Legal Service (Recommended)
- **Cost**: $200-500
- **Platform**: LegalZoom, Rocket Lawyer
- **Process**: Answer questionnaire, get custom documents
- **Pros**: Legally reviewed, tailored to your game
- **Cons**: Higher cost

**Recommendation**: Use templates for MVP, upgrade to legal service after soft launch revenue.

### Trademark (Optional but Recommended)

- **Cost**: $250-350 (USPTO filing fee + legal service)
- **Process**: Search trademark availability, file application
- **Timeline**: 6-12 months for approval

---

## 9. Asset Acquisition Timeline

### Phase 0 (Pre-Production) - Week -4 to 0
- [ ] Purchase 3D model packs (Week -4)
- [ ] Download audio assets (Week -3)
- [ ] Design/purchase UI kit (Week -2)
- [ ] Download fonts (Week -2)
- [ ] Commission app icon (Week -1, ready by Phase 6)

### Phase 1-2 (Foundation & Core Mechanics) - Week 1-8
- [ ] Import 3D models to Unity
- [ ] Import audio assets
- [ ] Set up UI prefabs
- [ ] Create particle effects

### Phase 6 (Launch Preparation) - Week 23-26
- [ ] Finalize app icon
- [ ] Create App Store screenshots
- [ ] Finalize Privacy Policy & TOS

---

## 10. Asset Licenses Checklist

### Before Using Any Asset:
- [ ] Verify license allows commercial use
- [ ] Check if attribution required (credit in game/app store)
- [ ] Confirm license allows distribution (App Store)
- [ ] Save license documentation (PDF/screenshot)

### Common License Types:
- **CC0 (Public Domain)**: Use freely, no attribution
- **CC-BY (Attribution)**: Use freely, credit creator
- **Commercial License**: Purchased, allows commercial use
- **Royalty-Free**: One-time purchase, unlimited use
- **Asset Store EULA**: Unity Asset Store standard license

---

## 11. Asset Storage & Organization

### Cloud Backup (Recommended)
- **Google Drive / Dropbox**: Store all purchased assets + licenses
- **Folder Structure**:
  ```
  TreasureExcavator_Assets/
  ├── 3D_Models/
  │   ├── [Asset Pack Name]/
  │   └── Licenses/
  ├── Audio/
  │   ├── SFX/
  │   ├── Music/
  │   └── Licenses/
  ├── UI/
  ├── Fonts/
  └── Licenses/
  ```

### Version Control (Git LFS)
- Large files (models, audio, textures) stored in Git LFS
- Licenses and documentation committed to Git (not LFS)

---

## 12. Asset Acquisition Checklist

### Before Phase 1 Starts:
- [ ] 3D models purchased/downloaded
- [ ] Audio assets acquired
- [ ] UI assets designed/purchased
- [ ] Fonts downloaded
- [ ] Particle effects sourced
- [ ] All licenses verified and saved
- [ ] Assets organized in cloud backup
- [ ] Assets imported to Unity (test import)

### Ready for Phase 1:
- [ ] Vehicle models imported and visible in Unity
- [ ] Treasure models imported
- [ ] Environment tiles ready
- [ ] Audio files imported to Unity
- [ ] UI icons and textures ready
- [ ] Fonts imported (TextMeshPro assets created)

---

## 13. Budget Optimization Tips

### How to Save Money:
1. **Use Free Assets for Prototyping**: Test gameplay with free placeholders, buy premium assets only when validated
2. **Asset Store Sales**: Unity Asset Store has sales (50-90% off) during:
   - Black Friday (November)
   - Summer Sale (July)
   - Spring Sale (March)
3. **Bundle Deals**: Buy multi-packs instead of individual assets
4. **Envato 1-Month Strategy**: Subscribe, download everything needed, cancel
5. **Reuse Assets**: One vehicle model can have multiple skins (reduces model count)

### How to Maximize Budget:
1. **Invest in App Icon**: First impression matters (worth $200-300)
2. **Professional Audio**: Cheap audio sounds cheap (Envato $17 is best value)
3. **Legal Review**: Spend $200-500 on Privacy Policy after soft launch revenue

---

## 14. Post-Launch Asset Plan

### v1.1 Updates (New Assets Needed)
- 1 new vehicle model ($30-50)
- 5 new level environment variations ($50 or reuse existing)
- 5 new cosmetic skins (textures only, $0-20)
- Additional SFX for new features ($20)

### Budget for Updates: $100-150

---

*End of Asset Acquisition Plan*

**Status**: Ready for Asset Purchasing
**Recommended First Purchase**: 3D Model Pack ($100-150)
