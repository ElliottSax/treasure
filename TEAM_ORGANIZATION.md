# Team Organization Document
## Treasure Excavator - Roles, Responsibilities, and Workflow

**Version**: 1.0
**Last Updated**: 2025-11-18
**Team Size Options**: Solo / 2-person / 3-person

---

## 1. Team Size Decision

### Option A: Solo Developer
**Timeline**: 26 weeks (6 months)
**Budget**: $900 minimum
**Recommended For**: Experienced developer with Unity + iOS experience

**Pros**:
- Full creative control
- No profit sharing
- Flexible schedule

**Cons**:
- Slower development
- High workload (50-60 hours/week)
- Limited skill diversity (need to do art, code, audio, marketing)

---

### Option B: 2-Person Team (Recommended)
**Timeline**: 20 weeks (5 months)
**Budget**: $2,000-3,000
**Recommended Roles**:
- **Developer** (Unity, C#, iOS)
- **Artist/Designer** (3D modeling, UI design, asset sourcing)

**Pros**:
- Faster development (parallel work)
- Better quality (specialization)
- Shared workload (40-50 hours/week each)

**Cons**:
- Profit split (50/50 or negotiated)
- Communication overhead
- Need compatible schedules

---

### Option C: 3-Person Team
**Timeline**: 16 weeks (4 months)
**Budget**: $4,000-5,000
**Recommended Roles**:
- **Lead Developer** (Unity, C#, systems architecture)
- **Artist** (3D models, UI design, animations)
- **Designer/QA** (game design, level design, testing)

**Pros**:
- Fastest development
- Highest quality (each person expert in their domain)
- Best for complex features

**Cons**:
- Profit split (3-way)
- Coordination complexity
- Higher budget requirement

---

## 2. Role Definitions

### Role 1: Unity Developer (Required)

**Responsibilities**:
- C# scripting (vehicle controller, game systems)
- Unity scene setup (levels, prefabs, lighting)
- Physics implementation (vehicle movement, collisions)
- UI programming (HUD, menus, touch input)
- iOS build setup (Xcode, App Store submission)
- Firebase integration (analytics, crashlytics)
- Unity Ads + IAP integration
- Git version control
- Bug fixing and optimization

**Required Skills**:
- **Unity 2022.3 LTS** (intermediate-advanced)
- **C#** (intermediate-advanced)
- **iOS Development** (basic, Xcode familiarity)
- **Git** (basic)
- **URP** (basic)

**Nice to Have**:
- Mobile optimization experience
- Shader programming (URP/Shader Graph)
- Multiplayer/networking (for post-launch features)

**Time Commitment**:
- Solo: 50-60 hours/week
- Team: 40-50 hours/week

---

### Role 2: 3D Artist / UI Designer (Recommended for Team)

**Responsibilities**:
- 3D modeling (vehicles, treasures, environment) OR asset sourcing
- Texturing and materials (PBR, URP)
- UI design (Figma wireframes, UI art)
- Icon design (app icon, UI icons)
- Particle effects (VFX for treasures, gates)
- Animation (vehicle animations, UI transitions)
- Asset optimization (poly reduction, texture compression)
- App Store screenshots and marketing visuals

**Required Skills**:
- **Figma or Adobe XD** (UI design)
- **Blender or Maya** (3D modeling, if creating assets)
- **Photoshop or Affinity Designer** (texturing, UI)
- **Unity basics** (import assets, set up prefabs)

**Nice to Have**:
- Low-poly / stylized art experience
- Mobile game UI/UX knowledge
- Animation skills (Unity Animator)

**Time Commitment**:
- Team: 30-40 hours/week

---

### Role 3: Game Designer / QA Tester (Optional, Recommended for 3-Person Team)

**Responsibilities**:
- Level design (layout, treasure placement, gate positions)
- Game balance (economy, progression, difficulty curve)
- Tutorial script (step-by-step flow)
- Playtesting (find bugs, test UX)
- QA testing (all devices, edge cases)
- Analytics review (track KPIs, suggest improvements)
- Documentation (GDD updates, feature specs)
- Community management (Discord, social media, post-launch)

**Required Skills**:
- **Game design experience** (level design, balancing)
- **Unity basics** (scene editing, prefab placement)
- **Excel/Sheets** (economy balancing, data tracking)
- **Communication** (bug reports, design docs)

**Nice to Have**:
- Mobile game design experience
- F2P monetization knowledge
- Analytics tools (Firebase, Unity Analytics)

**Time Commitment**:
- Team: 20-30 hours/week

---

## 3. Team Structure

### Solo Developer Structure
```
Solo Developer (You)
├── Programming (60%)
├── Art/Asset Sourcing (20%)
├── Design/Testing (10%)
└── Marketing/Admin (10%)
```

**Recommendation**: Outsource art (freelance) to save time.

---

### 2-Person Team Structure (Recommended)
```
Team Lead: Developer
├── Programming (80%)
├── Design (10%)
└── QA (10%)

Team Member 2: Artist/Designer
├── Art/UI (70%)
├── Design (20%)
└── QA (10%)
```

**Decision-Making**: Collaborative, but Developer has final say on technical feasibility.

---

### 3-Person Team Structure
```
Team Lead: Lead Developer
├── Systems Architecture
├── Core Mechanics
└── iOS Build

Artist
├── 3D Models
├── UI Design
└── VFX

Designer/QA
├── Level Design
├── Game Balance
└── Testing
```

**Decision-Making**: Lead Developer is project manager, major decisions voted on.

---

## 4. Communication Tools

### Recommended Setup

#### Daily Communication: Discord (Free)
- **Setup**: Create private server
- **Channels**:
  - #general (daily updates)
  - #bugs (bug reports)
  - #design (game design discussion)
  - #art (asset sharing, feedback)
  - #dev (technical discussion)
- **Voice Calls**: Daily standup (15 minutes)

**Alternative**: Slack (free tier)

---

#### Task Management: Trello (Free) or GitHub Projects
- **Trello Board Structure**:
  ```
  Backlog → To Do → In Progress → Review → Done
  ```
- **Cards**: Each task is a card (assigned to team member)
- **Labels**: Bug, Feature, Art, Design, QA

**Alternative**: GitHub Projects (integrated with Git)

---

#### File Sharing: Google Drive (Free) or Dropbox
- **Folder Structure**:
  ```
  TreasureExcavator_Team/
  ├── Design_Docs/
  ├── Art_Assets/
  ├── Builds/
  ├── Marketing/
  └── Meetings/
  ```

**Alternative**: GitHub (for code + assets with Git LFS)

---

#### Version Control: GitHub (Private Repo)
- **Branching Strategy**:
  ```
  main (stable, production-ready)
  ├── develop (integration branch)
  ├── feature/vehicle-controller
  ├── feature/level-design
  └── bugfix/physics-issues
  ```

---

## 5. Workflow & Schedule

### Daily Routine (Recommended)

#### Morning (9:00 AM - 12:00 PM)
- **9:00 AM**: Daily standup (15 min)
  - What did you do yesterday?
  - What will you do today?
  - Any blockers?
- **9:15 AM - 12:00 PM**: Focused work (deep work, no distractions)

#### Afternoon (1:00 PM - 5:00 PM)
- **1:00 PM - 5:00 PM**: Continued work + collaboration
- **4:00 PM**: Commit & push code (daily backup)

#### Evening (Optional)
- **6:00 PM - 8:00 PM**: Light tasks (testing, documentation)

---

### Weekly Schedule

#### Monday:
- Sprint planning (define week's goals)
- Assign tasks in Trello

#### Tuesday - Thursday:
- Development work
- Daily standup

#### Friday:
- Sprint review (demo completed features)
- Retrospective (what went well, what to improve)
- Deploy weekly build for testing

#### Weekend:
- Optional: Personal playtesting, catch-up work

---

### Milestone Reviews (Every 4 Weeks)

#### End of Each Phase:
- **Demo Day**: Show progress to stakeholders (if any)
- **Playtest Session**: Test with external players
- **Analytics Review**: Check metrics (if live)
- **Budget Review**: Track spending
- **Timeline Adjustment**: Update roadmap if needed

---

## 6. Profit Sharing & Equity

### 2-Person Team Example

#### Option 1: Equal Split (50/50)
- Both members contribute equally
- Fair if skills are balanced

#### Option 2: Weighted Split (60/40)
- Lead Developer: 60% (more hours, technical ownership)
- Artist: 40% (fewer hours, supporting role)

#### Option 3: Equity + Salary (If Budget Allows)
- Lead Developer: 70% equity + $0 salary (founder)
- Artist: 30% equity + $2,000 upfront (contractor)

**Recommendation**: 50/50 for equal contribution, 60/40 if lead invests more time.

---

### 3-Person Team Example

#### Equal Split (33/33/33):
- All members equal contributors

#### Weighted Split (50/30/20):
- Lead Developer: 50% (most critical role)
- Artist: 30% (high-value work)
- Designer/QA: 20% (part-time)

**Recommendation**: Define split before Phase 1 starts (written agreement).

---

## 7. Legal Agreements (Highly Recommended)

### Operating Agreement (If Forming LLC)
- Ownership percentages
- Profit distribution
- Decision-making process
- Exit strategy (what if someone leaves?)

**Cost**: $200-500 (LegalZoom, Rocket Lawyer)

---

### Contractor Agreement (If Hiring Freelancers)
- Scope of work
- Payment terms (hourly, flat fee, revenue share)
- IP ownership (all work belongs to company)
- NDA (non-disclosure agreement)

**Cost**: $100-300 (template + lawyer review)

---

## 8. Onboarding Checklist (For Team Members)

### New Team Member Setup:
- [ ] Add to Discord server
- [ ] Grant access to GitHub repo (collaborator)
- [ ] Share Google Drive folder
- [ ] Add to Trello board
- [ ] Send design docs (GDD, this doc)
- [ ] Schedule kickoff meeting (intro, expectations)
- [ ] Define role and responsibilities
- [ ] Set up development environment (Unity, Git)
- [ ] Review code style guide (if applicable)
- [ ] Assign first task (easy win to build confidence)

---

## 9. Code of Conduct

### Team Values:
1. **Respect**: Constructive feedback only, no personal attacks
2. **Transparency**: Share blockers early, don't hide issues
3. **Accountability**: Meet deadlines or communicate delays
4. **Collaboration**: Help each other, share knowledge
5. **Quality**: Ship polished work, not rushed hacks

### Conflict Resolution:
1. Discuss issue privately (1-on-1 call)
2. If unresolved, bring to team meeting
3. Vote on solution (majority rules)
4. If still deadlocked, lead developer decides

---

## 10. Budget Allocation (Team)

### 2-Person Team Budget ($3,000)
```
Apple Developer Account: $99
Assets (3D, Audio, UI): $400
Legal (LLC, agreements): $500
Marketing (App Icon, Screenshots): $300
Salaries (if applicable): $1,200 ($600 each)
Contingency: $501
────────────────────────
TOTAL: $3,000
```

### 3-Person Team Budget ($5,000)
```
Apple Developer Account: $99
Assets: $600
Legal: $700
Marketing: $500
Salaries: $2,400 ($800 each)
Soft Launch Ads: $500
Contingency: $701
────────────────────────
TOTAL: $5,000
```

---

## 11. Decision-Making Framework

### Technical Decisions (Code, Architecture)
- **Owner**: Lead Developer
- **Input**: Team feedback welcome
- **Final Say**: Lead Developer

### Design Decisions (Gameplay, Levels, Balance)
- **Owner**: Designer (or Lead if solo/2-person)
- **Input**: Team playtesting feedback
- **Final Say**: Majority vote

### Art Decisions (Style, UI, Assets)
- **Owner**: Artist
- **Input**: Team aesthetic preferences
- **Final Say**: Artist (with Lead approval for scope)

### Business Decisions (Monetization, Launch Date, Budget)
- **Owner**: Team Lead / Project Manager
- **Input**: All team members
- **Final Say**: Unanimous or majority vote

---

## 12. Performance Reviews (For Teams)

### Quarterly Reviews:
- **What**: Assess each member's contribution
- **When**: End of Phase 2, 4, 6
- **Criteria**:
  - Quality of work
  - Timeliness (meeting deadlines)
  - Communication (responsiveness, clarity)
  - Collaboration (helping teammates)
- **Outcome**: Feedback session, adjust equity/pay if needed

---

## 13. Exit Strategy (What If Someone Leaves?)

### Before Phase 1:
- Easy to part ways (no shared IP yet)
- Return any upfront payments

### During Development (Phase 1-5):
- **Scenario 1: Mutual Agreement**
  - Buy out leaving member's equity (negotiated price)
  - Transfer IP rights to remaining member(s)

- **Scenario 2: Poor Performance**
  - Warning + improvement plan (2 weeks)
  - If no improvement, vote to remove
  - Compensation: Prorated equity or flat fee

### Post-Launch:
- Leaving member retains equity % (passive income)
- Or: Buy out equity at fair market value (based on revenue)

**Recommendation**: Define exit terms in Operating Agreement.

---

## 14. Solo Developer Survival Guide

### Time Management Tips:
1. **Work in Sprints**: 4-week cycles, clear goals
2. **Focus Days**: 3 days code, 1 day art, 1 day design/QA
3. **Automate**: Use Asset Store instead of creating everything
4. **Outsource**: Hire freelancers for app icon, audio
5. **Time-box**: Set hard limits (e.g., 8 hours/day) to avoid burnout

### Avoiding Burnout:
- Take 1 day off per week (minimum)
- Exercise daily (30 min)
- Join gamedev Discord for motivation
- Celebrate small wins (completed feature = reward yourself)

---

## 15. Team Checklist

### Before Phase 1 Starts:
- [ ] Team size decided (solo, 2-person, 3-person)
- [ ] Roles assigned
- [ ] Communication tools set up (Discord, Trello)
- [ ] Git repository created, all members have access
- [ ] Operating Agreement signed (if team)
- [ ] Profit split agreed upon (in writing)
- [ ] Budget allocated
- [ ] Timeline adjusted for team size
- [ ] Kickoff meeting completed
- [ ] First sprint planned (Phase 1, Week 1)

---

*End of Team Organization Document*

**Status**: Ready for Team Formation
**Next Steps**: Recruit team members (if applicable) or commit to solo development
