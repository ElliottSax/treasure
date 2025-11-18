# Unity Setup Checklist
## Treasure Excavator - Complete Development Environment Setup

**Version**: 1.0
**Last Updated**: 2025-11-18
**Target Unity Version**: 2022.3 LTS
**Platform**: iOS (primary), Android (future)

---

## 1. Pre-Installation Requirements

### System Requirements

#### macOS (Required for iOS Builds)
- [ ] macOS Ventura (13.0) or later
- [ ] At least 20GB free disk space
- [ ] 8GB RAM minimum (16GB recommended)
- [ ] Xcode 14.0 or later installed
- [ ] Valid Apple Developer Account ($99/year)

#### Windows (Development OK, but cannot build for iOS)
- [ ] Windows 10/11 64-bit
- [ ] At least 20GB free disk space
- [ ] 8GB RAM minimum (16GB recommended)
- [ ] Note: Requires Mac for final iOS builds

---

## 2. Unity Installation

### Step 1: Install Unity Hub
- [ ] Download Unity Hub from https://unity.com/download
- [ ] Install Unity Hub
- [ ] Create Unity account (free) or sign in

### Step 2: Install Unity 2022.3 LTS
- [ ] Open Unity Hub
- [ ] Go to "Installs" tab
- [ ] Click "Install Editor"
- [ ] Select "2022.3.x LTS" (latest patch version)
- [ ] Choose modules to install:
  - [x] **iOS Build Support** (REQUIRED)
  - [x] **Android Build Support** (for future Phase 2)
  - [x] **Documentation**
  - [x] **Language Pack** (if needed)
- [ ] Click "Install" and wait (15-30 minutes)

### Step 3: Verify Installation
- [ ] Unity Hub shows "2022.3.x LTS" with green checkmark
- [ ] Launch Unity to test (create blank project)
- [ ] Close test project

---

## 3. Create New Project

### Project Setup
- [ ] Open Unity Hub
- [ ] Click "New Project"
- [ ] Select "2022.3.x LTS" as editor version
- [ ] Choose **"3D (URP)"** template (Universal Render Pipeline)
- [ ] Set Project Name: "TreasureExcavator"
- [ ] Set Location: (your preferred development folder)
- [ ] Click "Create Project"

### Initial Project Configuration
- [ ] Wait for Unity to initialize (2-5 minutes)
- [ ] Close welcome screens
- [ ] Save Scene: "Assets/Scenes/MainMenu.unity"

---

## 4. Project Settings Configuration

### Player Settings (iOS)

**Access**: Edit > Project Settings > Player > iOS tab

#### Company & Product Settings
- [ ] **Company Name**: [YOUR COMPANY NAME]
- [ ] **Product Name**: Treasure Excavator
- [ ] **Version**: 0.1.0
- [ ] **Bundle Identifier**: com.[yourcompany].treasureexcavator
  - Example: com.example.treasureexcavator
  - Must match Apple Developer account

#### Icon Settings
- [ ] **Default Icon**: (placeholder for now, will add in Phase 6)
- [ ] **Adaptive Icon**: Not needed for iOS

#### Resolution & Presentation
- [ ] **Default Orientation**: Portrait (or Landscape if preferred)
- [ ] **Allowed Orientations for Auto Rotation**:
  - [x] Portrait (recommended)
  - [ ] Landscape Left
  - [ ] Landscape Right
  - [ ] Portrait Upside Down (uncheck)
- [ ] **Status Bar**: Hidden (full-screen game)

#### Other Settings
- [ ] **Auto Graphics API**: Unchecked
- [ ] **Graphics APIs**: Metal (should be default)
- [ ] **Minimum iOS Version**: 14.0
- [ ] **Target SDK**: Latest
- [ ] **Architecture**: ARM64
- [ ] **Scripting Backend**: IL2CPP (required for iOS)
- [ ] **Target Device**: iPhone + iPad (iPhone only for Phase 1)

#### Identification
- [ ] **Signing Team ID**: (will add after Apple Developer setup)
- [ ] **Automatically Sign**: Checked (recommended)

---

### Quality Settings

**Access**: Edit > Project Settings > Quality

- [ ] **Pixel Light Count**: 2 (mobile optimization)
- [ ] **Texture Quality**: Full Res
- [ ] **Anisotropic Textures**: Per Texture
- [ ] **Anti Aliasing**: 2x Multi Sampling (balance quality/performance)
- [ ] **Soft Particles**: Enabled
- [ ] **V Sync Count**: Every V Blank (60 FPS target)

---

### Physics Settings

**Access**: Edit > Project Settings > Physics

- [ ] **Gravity**: (0, -9.81, 0) - default
- [ ] **Default Material**: (will create later)
- [ ] **Queries Hit Triggers**: Enabled (for treasure collection)
- [ ] **Layer Collision Matrix**: (will configure in Phase 1)

---

### Time Settings

**Access**: Edit > Project Settings > Time

- [ ] **Fixed Timestep**: 0.02 (50 FPS physics, standard)
- [ ] **Maximum Allowed Timestep**: 0.1
- [ ] **Time Scale**: 1 (normal speed)

---

### Input Settings

**Access**: Edit > Project Settings > Input System Package

- [ ] Install "Input System" package (see Package Manager section)
- [ ] Set Active Input Handling: "Input System Package (New)"
- [ ] Restart Unity when prompted

---

## 5. Package Manager Setup

**Access**: Window > Package Manager

### Required Packages (Install These)

#### Unity Packages (Unity Registry)
- [ ] **Universal RP** (URP) - Should be pre-installed with template
  - Version: 14.x (matches Unity 2022.3)

- [ ] **Input System**
  - Version: 1.5.0 or later
  - Install: Click "Install" in Package Manager

- [ ] **TextMeshPro**
  - Should be pre-installed
  - If not: Search and install
  - Import "TMP Essential Resources" when prompted

- [ ] **Cinemachine** (for camera control)
  - Version: 2.9.0 or later
  - Install: Search "Cinemachine" and click "Install"

#### Firebase SDK (External)
**Note**: Will install in Phase 2, but prepare now

- [ ] Download Firebase Unity SDK from https://firebase.google.com/download/unity
- [ ] Save to project folder: "Packages/Firebase/"
- [ ] Install later: FirebaseAnalytics.unitypackage, FirebaseCrashlytics.unitypackage

#### Unity Ads SDK
- [ ] **Advertisement** (Unity Monetization)
  - Search "Advertisement Legacy" in Package Manager
  - Install version 4.x

#### Unity IAP
- [ ] **In-App Purchasing**
  - Search "In-App Purchasing" in Package Manager
  - Install version 4.x

### Optional Packages (Nice to Have)
- [ ] **Recorder** (for recording gameplay videos)
- [ ] **ProBuilder** (for level prototyping)

---

## 6. Project Structure Setup

### Create Folder Structure

**Access**: Project window > Assets (right-click > Create > Folder)

```
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   ├── LevelSelect.unity
│   ├── Level_01.unity
│   └── ... (other levels)
├── Scripts/
│   ├── Managers/
│   ├── Vehicles/
│   ├── Gameplay/
│   ├── UI/
│   └── Utilities/
├── Prefabs/
│   ├── Vehicles/
│   ├── Treasures/
│   ├── Gates/
│   └── UI/
├── Materials/
│   ├── Vehicles/
│   ├── Environment/
│   └── Effects/
├── Textures/
│   ├── UI/
│   └── Environment/
├── Models/
│   ├── Vehicles/
│   ├── Environment/
│   └── Treasures/
├── Audio/
│   ├── SFX/
│   │   ├── Gameplay/
│   │   ├── UI/
│   │   └── Vehicle/
│   └── Music/
├── Animations/
├── Fonts/
├── Shaders/
├── Settings/
│   ├── URP/
│   └── InputActions/
└── Resources/
```

**Checklist**:
- [ ] All folders created
- [ ] Organized structure ready for assets

---

## 7. URP (Universal Render Pipeline) Setup

### URP Asset Configuration

**Access**: Assets > Settings > URP folder

- [ ] **URP Renderer**: Create > Rendering > URP Asset (Forward Renderer)
- [ ] Name it: "TreasureExcavator_URPAsset"
- [ ] Configure settings:
  - **Rendering Path**: Forward
  - **Depth Texture**: Enabled
  - **Opaque Texture**: Disabled (performance)
  - **HDR**: Disabled (mobile optimization)
  - **MSAA**: 2x (anti-aliasing)
  - **Render Scale**: 1.0

- [ ] Assign URP Asset:
  - Edit > Project Settings > Graphics
  - Set "Scriptable Render Pipeline Settings" to your URP asset

- [ ] Assign URP Asset to Quality Settings:
  - Edit > Project Settings > Quality
  - Set each quality level to use the URP asset

---

## 8. Git & Version Control Setup

### Initialize Git Repository

**Terminal Commands** (run in project root):
```bash
cd /path/to/TreasureExcavator
git init
git add .gitignore .gitattributes
git commit -m "Initial Unity project setup"
```

### Configure Git LFS

- [ ] Install Git LFS: https://git-lfs.github.com
- [ ] Run in terminal:
  ```bash
  git lfs install
  git lfs track "*.psd"
  git lfs track "*.fbx"
  git lfs track "*.png"
  git lfs track "*.wav"
  git lfs track "*.mp3"
  git add .gitattributes
  git commit -m "Configure Git LFS"
  ```

### Create GitHub Repository

- [ ] Go to https://github.com
- [ ] Create new repository: "treasure-excavator"
- [ ] Set to **Private**
- [ ] Do not initialize with README (already have local repo)
- [ ] Copy remote URL

### Connect Local to Remote

```bash
git remote add origin <YOUR_GITHUB_URL>
git branch -M main
git push -u origin main
```

---

## 9. iOS Build Settings (Xcode)

### Install Xcode (macOS Only)

- [ ] Download Xcode from Mac App Store
- [ ] Install (requires ~15GB space)
- [ ] Open Xcode once to accept license
- [ ] Install Command Line Tools:
  ```bash
  xcode-select --install
  ```

### Configure Xcode for Unity

- [ ] Open Xcode
- [ ] Preferences > Accounts
- [ ] Add Apple ID (same as Apple Developer account)
- [ ] Download certificates

### Test iOS Build from Unity

- [ ] Unity: File > Build Settings
- [ ] Select "iOS" platform
- [ ] Click "Switch Platform" (wait 5-10 minutes first time)
- [ ] Add open scenes to build
- [ ] Click "Build" (creates Xcode project, don't run yet)
- [ ] Open generated Xcode project
- [ ] Select your development device/simulator
- [ ] Click "Play" to test build

**Note**: Don't worry if build fails initially—this is just a test. Will fix in Phase 1.

---

## 10. Testing Device Setup

### iPhone Test Device (Physical)

- [ ] iPhone 11 or newer
- [ ] iOS 14.0 or later installed
- [ ] Plugged into Mac via USB
- [ ] "Trust This Computer" accepted on device
- [ ] Device added to Apple Developer account (Certificates, Identifiers & Profiles)

### Xcode Simulator (Backup Option)

- [ ] Xcode > Preferences > Components
- [ ] Download iOS 14.0+ simulator
- [ ] Use for quick testing (note: slower than real device)

---

## 11. Firebase Setup (Prepare for Phase 2)

### Firebase Project Creation

- [ ] Go to https://console.firebase.google.com
- [ ] Click "Add Project"
- [ ] Name: "Treasure Excavator"
- [ ] Disable Google Analytics for now (can add later)
- [ ] Click "Create Project"

### Add iOS App to Firebase

- [ ] In Firebase console, click "Add App" > iOS
- [ ] Enter Bundle ID: com.[yourcompany].treasureexcavator (same as Unity)
- [ ] Download "GoogleService-Info.plist"
- [ ] Save to project folder (will import in Phase 2)

**Note**: Don't import SDK yet—just prepare the project.

---

## 12. Apple Developer Account Setup

### Create Developer Account

- [ ] Go to https://developer.apple.com
- [ ] Enroll in Apple Developer Program ($99/year)
- [ ] Complete enrollment (can take 24-48 hours)
- [ ] Verify account is active

### Create App Identifier

- [ ] Log in to https://developer.apple.com/account
- [ ] Certificates, Identifiers & Profiles
- [ ] Identifiers > App IDs > "+" button
- [ ] Type: App
- [ ] Description: Treasure Excavator
- [ ] Bundle ID: com.[yourcompany].treasureexcavator (explicit)
- [ ] Capabilities: Check "In-App Purchase"
- [ ] Click "Continue" > "Register"

### Create Provisioning Profile

- [ ] Profiles > "+" button
- [ ] Type: iOS App Development
- [ ] Select your App ID
- [ ] Select your development certificate
- [ ] Select your test devices
- [ ] Name: "Treasure Excavator Development"
- [ ] Download and double-click to install

---

## 13. Analytics & Crash Reporting Prep

### Firebase Analytics

- [ ] Firebase console > Analytics
- [ ] Enable Analytics (if not done in setup)
- [ ] Note: Will implement events in Phase 2

### Crashlytics

- [ ] Firebase console > Crashlytics
- [ ] Click "Set up Crashlytics"
- [ ] Follow setup steps (will complete in Phase 2)

---

## 14. Third-Party SDKs Checklist

### Unity Ads
- [ ] Create Unity Ads account: https://dashboard.unity3d.com
- [ ] Create new project: "Treasure Excavator"
- [ ] Note Game ID (will use in Phase 3)
- [ ] Enable "Rewarded Video" ad placement

### Unity IAP
- [ ] Pre-installed with Package Manager (step 5)
- [ ] Will configure in Phase 3

---

## 15. Development Tools (Optional but Recommended)

### Code Editor

**Option 1: Visual Studio Code** (Recommended, Free)
- [ ] Download: https://code.visualstudio.com
- [ ] Install "C# for Visual Studio Code" extension
- [ ] Set as Unity external editor:
  - Unity: Edit > Preferences > External Tools
  - External Script Editor: Visual Studio Code

**Option 2: Visual Studio** (Free)
- [ ] Download: https://visualstudio.microsoft.com
- [ ] Install "Game development with Unity" workload

**Option 3: Rider** (Paid, Best for Unity)
- [ ] Download: https://www.jetbrains.com/rider/
- [ ] Free trial available
- [ ] Best autocomplete and refactoring tools

### Recommended Editor Extensions
- [ ] C# extension (syntax highlighting)
- [ ] Unity Code Snippets
- [ ] GitLens (Git integration)

---

## 16. Final Verification Checklist

### Before Starting Phase 1:
- [ ] Unity 2022.3 LTS installed with iOS Build Support
- [ ] URP project created successfully
- [ ] All required packages installed
- [ ] Folder structure created
- [ ] Git repository initialized and pushed to GitHub
- [ ] Xcode installed (macOS)
- [ ] Apple Developer account active
- [ ] Test device connected and recognized
- [ ] Firebase project created
- [ ] Unity Ads account created
- [ ] Code editor configured
- [ ] Test build compiles without errors (even if app crashes)

---

## 17. Troubleshooting Common Issues

### Issue: Unity won't install iOS Build Support
**Solution**:
- Make sure you're on macOS
- Check disk space (need 20GB+)
- Try installing from Unity Hub > Installs > [Your version] > Add Modules

### Issue: Xcode build fails with "No valid signing identity"
**Solution**:
- Xcode > Preferences > Accounts > Download Manual Profiles
- Unity > Player Settings > iOS > Signing Team ID (add your Team ID)
- Enable "Automatically Sign" in Unity

### Issue: Git LFS not tracking large files
**Solution**:
- Run `git lfs install` again
- Check `.gitattributes` file exists
- Run `git lfs track "*.png"` for each file type

### Issue: Package Manager won't load packages
**Solution**:
- Window > Package Manager > Advanced > Reset Packages
- Restart Unity
- Check internet connection

---

## 18. Budget Recap

### Required Costs:
- Apple Developer Account: $99/year ✅ Required
- macOS device: (assumed you have) or rent Mac cloud build service ($10-30/month)

### Optional Costs:
- Paid code editor (Rider): $0 (free trial) to $149/year
- Firebase: $0 (free tier sufficient for MVP)
- Unity Ads: $0 (revenue share model)

**Total Minimum Cost**: $99/year

---

## 19. Next Steps After Setup

Once checklist complete:
1. ✅ Mark Phase 0 Unity setup as DONE
2. → Proceed to Phase 1: Foundation (3 weeks)
3. → Begin coding vehicle controller and basic gameplay

---

*End of Unity Setup Checklist*

**Status**: Ready for Phase 0 Completion
**Estimated Setup Time**: 4-6 hours
