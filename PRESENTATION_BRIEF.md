# PropTech Platform - Competition Presentation Brief

**FOR:** Code4Mzansi Final Presentation  
**DATE:** February 2026  
**DURATION:** 20 minutes (15 min presentation + 5 min Q&A)  

---

## 🎯 Elevator Pitch (30 seconds)

> "PropMate is South Africa's first AI-powered property co-pilot that helps small landlords manage townships, backyard units, and informal rentals with intelligent tenant screening, dynamic pricing, and predictive maintenance—all from a desktop or web browser."

---

## 📊 Presentation Structure

### Slide 1: THE PROBLEM (2 minutes)
**Hook:** "In South Africa, 13 million people rent homes, but 60% of landlords still use Excel and WhatsApp."

**Pain Points:**
- 🏚️ Informal housing (rooms, shacks) ignored by expensive enterprise software
- 📝 Manual lease generation takes 2-3 hours per tenant
- 💸 Pricing guesswork leads to 15-30% revenue loss
- 🔧 Reactive maintenance costs 3x more than preventive
- 📱 No digital record keeping → disputes and legal risks

**The Gap:** Current solutions target 1,000+ unit property firms. Nobody serves the 10-100 unit township landlord.

---

### Slide 2: OUR SOLUTION (2 minutes)
**What We Built:** AI-First Property Management Platform

**Three Platforms, One Codebase:**
- 🖥️ **.NET MAUI Desktop** → Property managers' daily workflow (Windows native)
- 🌐 **Blazor Web** → Tenants and remote access (browser-based)
- 🤖 **Agentic AI** → Autonomous decision-making powered by Huawei Cloud

**Core Innovation:** Plan-Act-Observe AI loop
```
Context → AI Analysis → Recommendation → Human Decision → Action
```

---

### Slide 3: KEY FEATURES - AI INTELLIGENCE (3 minutes)

#### 1. AI Tenant Screening 🕵️
- Analyzes application forms using Huawei Pangu LLM
- Risk scoring: Low (0-0.3), Medium (0.3-0.7), High (0.7-1.0)
- **Demo:** Show screening result with risk factors

**Example Output:**
```
Risk Level: MEDIUM (0.45)
Factors:
- ✅ Stable employment (2 years)
- ⚠️ No previous rental references
- ✅ Income 3x rent requirement
```

#### 2. AI Rental Pricing 💰
- Market analysis + property attributes → optimal rent range
- Confidence scoring for recommendations
- **Demo:** Show pricing for Room vs House comparison

**Example Output:**
```
Recommended Rent: R4,500/month
Range: R3,800 - R5,200
Confidence: 85%
Market Trend: ↗️ Rising (+5% YoY)
```

#### 3. Predictive Maintenance 🔧
- Forecasts equipment failures before they happen
- Cost estimates and urgency prioritization
- **Demo:** Show maintenance prediction for 3-year-old property

**Example Output:**
```
Next Failure: Geyser (Urgency: MEDIUM)
Time Frame: 60-90 days
Estimated Cost: R4,200
Recommendation: Schedule inspection now
```

#### 4. Virtual Tour AI Inspection 📸
- Analyzes 360° room panoramas for defects
- Generates inspection reports with repair cost estimates
- **Demo:** Show room findings (wall damage, ceiling cracks)

**Example Output:**
```
Overall Condition: FAIR (0.65/1.0)
Findings:
- Living Room: Wall paint peeling (R850)
- Kitchen: Ceiling water damage (R2,200)
- Bathroom: Broken tiles (R1,100)
Total Estimated Repairs: R8,500
```

---

### Slide 4: TECHNICAL ARCHITECTURE (2 minutes)

**Modern Stack:**
- **.NET 10** (Latest C# features)
- **.NET MAUI** (Cross-platform native apps)
- **ASP.NET Core Blazor** (Server-side rendering)
- **Huawei Cloud AI** (Pangu LLM + ModelArts prediction)

**Clean Architecture:**
```
┌─────────────────────────────────────┐
│  UI Layer (MAUI/Blazor)            │
├─────────────────────────────────────┤
│  Services (PropertyManager, AI)    │
├─────────────────────────────────────┤
│  Models (Tenant, Property, Lease)  │
├─────────────────────────────────────┤
│  DataStore (In-Memory)             │
└─────────────────────────────────────┘
         ↓
   Huawei Cloud AI
```

**Why This Matters:**
- ✅ Code reuse: 80% shared between desktop and web
- ✅ Testable: Clear separation of concerns
- ✅ Scalable: Add mobile (iOS/Android) without rewriting logic
- ✅ Maintainable: Single business logic, multiple UIs

---

### Slide 5: COMPETITIVE ADVANTAGE (2 minutes)

#### vs. Established Players

| Feature | PropMate | MDA Property | PayProp | Roprop |
|---------|----------|--------------|---------|---------|
| **AI Tenant Screening** | ✅ Agentic | ❌ None | ❌ Manual | ❌ None |
| **AI Pricing** | ✅ Live market | ❌ Static | ❌ None | ❌ None |
| **Virtual Inspections** | ✅ 360° + AI | ❌ Photos only | ❌ None | ❌ None |
| **Informal Housing Support** | ✅ Rooms/Shacks | ❌ No | ❌ No | 🟡 Basic |
| **Target Market** | 10-100 units | 1,000+ units | Agencies | 10-100 units |
| **Price Point** | Affordable | R300+/unit | Transaction% | R99-299/mo |

**Our Unfair Advantage:** Agentic AI is 3-5 years ahead of competitors.

---

### Slide 6: SOUTH AFRICAN MARKET FIT (2 minutes)

**Why South Africa Needs This:**
- 🏘️ **8.5M informal dwellings** (backyard rooms, shacks, granny flats)
- 📊 **60% rental penetration** in townships
- 💼 **85,000 small landlords** (10-100 units) underserved
- 📱 **88% smartphone penetration** → web access everywhere
- 🇿🇦 **Local compliance:** SA Rental Housing Act, POPIA-aware

**Rand Currency Throughout:** All pricing, invoices, statements in ZAR (R)

**Localization Ready:** Framework supports multi-language (Zulu, Xhosa, Afrikaans)

---

### Slide 7: LIVE DEMO (5 minutes)

**Demo Flow:**
1. **Dashboard Overview** (30s)
   - Show 3 properties, 5 tenants, occupancy rate
   - Display AI insights panel

2. **Register New Tenant** (1 min)
   - Enter: John Mokoena, ID: 8501015800089, Income: R12,000
   - Click "Screen Tenant" → AI analyzes → Risk: LOW (0.28)

3. **Rental Pricing Recommendation** (1 min)
   - Select: 2-Bedroom House, Soweto
   - Click "Get AI Pricing" → Recommended: R5,200 (Range: R4,800-5,800)

4. **Virtual Tour Inspection** (1.5 min)
   - Select: Room 101 Panorama
   - Click "Run AI Inspection" → Report generated with 4 findings

5. **Predictive Maintenance** (1 min)
   - Select: Property A (Age: 5 years)
   - Click "Predict Maintenance" → Geyser replacement needed in 90 days (R4,200)

**Backup Plan:** If live demo fails, show pre-recorded demo video + architecture diagrams.

---

### Slide 8: DEVELOPMENT JOURNEY (1 minute)

**What We Built:**
- ✅ 3 applications: Prototype CLI + MAUI Desktop + Blazor Web
- ✅ 4 AI features: Screening, Pricing, Maintenance, Inspections
- ✅ Full agentic AI agent with plan-act-observe loop
- ✅ Comprehensive documentation (README, CODE4MZANSI, DIAGRAMS)
- ✅ Multi-platform architecture (shared Models/Services)

**Lines of Code:** ~3,500 lines C# (Models: 800, Services: 1,200, UI: 1,500)

**Development Time:** 6 weeks (Architecture: 2w, AI Integration: 2w, UI: 2w)

---

### Slide 9: CHALLENGES & LEARNINGS (1 minute)

**Technical Challenges:**
- 🧠 **Huawei Cloud AI Integration:** Learning Pangu LLM + ModelArts APIs
- 🎨 **Multi-Platform UI:** MAUI XAML vs Blazor Razor syntax differences
- 🤖 **Agentic AI Design:** Implementing autonomous decision-making loop

**What We Learned:**
- AI "co-pilot" > full automation → humans make final decisions
- Desktop + Web > Mobile-only → property managers prefer desktop speed
- Demo mode critical → offline testing without API costs

**What We'd Do Differently:**
- Start with database (EF Core) from Day 1
- Add unit tests incrementally, not at the end
- Implement financial system (invoices) earlier

---

### Slide 10: ROADMAP - NEXT 6 MONTHS (2 minutes)

#### Phase 1: Production Readiness (Month 1-2)
- [ ] Add SQL Server database persistence
- [ ] Implement authentication + authorization
- [ ] Complete financial system (Invoices, Payments, Statements)
- [ ] Generate PDF lease documents
- [ ] Security audit + POPIA compliance

#### Phase 2: Market Launch (Month 3-4)
- [ ] Payment gateway integration (Paystack, Stripe)
- [ ] WhatsApp notifications for tenants
- [ ] SMS rent reminders
- [ ] Tenant self-service portal (view statements, make payments)
- [ ] Beta testing with 10 Cape Town landlords

#### Phase 3: Scale & Expand (Month 5-6)
- [ ] Mobile apps (iOS/Android via MAUI)
- [ ] Multi-language support (Zulu, Xhosa, Afrikaans)
- [ ] REST API for integrations
- [ ] Agent collaboration features
- [ ] Pilot in Johannesburg, Durban, East London

---

### Slide 11: BUSINESS MODEL (1 minute)

**Pricing Strategy:** Freemium + Usage-Based

#### Tiers:
1. **Free** (0-5 units)
   - Basic tenant management
   - Manual lease generation
   - Limited AI credits (10/month)

2. **Pro** (R199/month, 6-50 units)
   - Unlimited AI screening + pricing
   - Virtual tour inspections
   - Invoice generation
   - Email support

3. **Enterprise** (Custom, 51+ units)
   - API access
   - Multi-user accounts
   - Dedicated AI training
   - WhatsApp integration
   - Phone support

**Revenue Projections (Year 1):**
- Target: 500 Pro subscribers → R1.2M ARR
- Conversion rate: 15% of free users upgrade
- Churn target: <10% monthly

---

### Slide 12: IMPACT & VISION (1 minute)

**Social Impact:**
- 📈 **Formalize informal sector:** Bring 100,000 backyard rentals into digital economy
- 💼 **Empower small landlords:** AI levels playing field vs large firms
- 🏠 **Improve tenant experience:** Transparent pricing, professional leases
- 📊 **Data-driven policy:** Rental market insights for government

**5-Year Vision:**
> "Become the operating system for South African small-scale property management—powering 1 million rental units across townships, RDP homes, and informal settlements."

**Why This Matters:**
Housing is a R250B+ market in SA. Better management → more supply → lower rents → improved quality of life.

---

### Slide 13: THANK YOU + Q&A (5 minutes)

**Team Contacts:**
- GitHub: github.com/MsLotusFlowerBomb/propTech
- Email: [Your Email]
- Demo: [proptech-demo.azurewebsites.net] (if deployed)

**Questions We Expect:**
1. **"Why no payment processing yet?"**
   → "Focus on AI innovation first; payments in Sprint 1 of roadmap (Month 1)"

2. **"How accurate is AI screening?"**
   → "Demo mode shows concept; production will train on 10,000+ SA rental applications"

3. **"What about data privacy (POPIA)?"**
   → "Architecture supports encryption; auth + compliance in pre-launch Phase 1"

4. **"Why Huawei Cloud vs OpenAI/Azure?"**
   → "Pangu LLM optimized for emerging markets; ModelArts offers SA data residency"

5. **"Can it replace my current system?"**
   → "Not yet—this is MVP. Production-ready in 3 months with database + payments"

6. **"What's your competitive moat?"**
   → "Agentic AI + South African market focus—competitors are either global (not localized) or local (no AI)"

---

## 🎤 Speaker Notes

### Delivery Tips:
- ✅ **Speak slowly:** Judges need to absorb technical content
- ✅ **Show enthusiasm:** Believe in the vision
- ✅ **Acknowledge gaps:** "Yes, we need a database—it's Sprint 1 priority"
- ✅ **Emphasize AI:** This is the differentiator—spend 40% of time here
- ❌ **Don't oversell:** Call it a "prototype" not "production-ready"
- ❌ **Don't apologize:** Frame missing features as "roadmap" not "failures"

### Demo Contingency:
If any part of the demo fails:
1. Stay calm: "Let me show you the architecture diagram instead..."
2. Switch to screenshots: Have backups in slides
3. Explain the intent: "This would show a risk score of 0.45..."

### Q&A Strategy:
- **Tough questions:** Acknowledge honestly, pivot to strengths
- **Technical depth:** Have class diagrams ready (DIAGRAMS.md)
- **Business questions:** Cite market sizing (8.5M informal units)
- **Competition questions:** Use feature comparison table (Slide 5)

---

## 📋 Pre-Presentation Checklist

### 48 Hours Before:
- [ ] Test demo flow 5+ times (record timings)
- [ ] Confirm Huawei AI service is responding (or use demo mode)
- [ ] Create backup demo video (screen recording)
- [ ] Print class diagrams as handouts
- [ ] Prepare answers to expected questions
- [ ] Rehearse presentation with timer (aim for 13-14 min, leave 1-2 min buffer)

### 24 Hours Before:
- [ ] Test on presentation laptop (projector compatibility)
- [ ] Verify internet connection (or prepare offline mode)
- [ ] Prepare USB drive with backup slides (PDF)
- [ ] Test microphone and audio
- [ ] Get good night's sleep!

### 1 Hour Before:
- [ ] Open all demo apps (MAUI + Blazor in separate windows)
- [ ] Verify DataStore has seed data (3 properties, 5 tenants)
- [ ] Close distracting apps (email, Slack)
- [ ] Set phone to airplane mode
- [ ] Breathe and visualize success

---

## 🏆 Winning Formula

**What Judges Want to See:**
1. ✅ **Innovation:** Our agentic AI approach is genuinely novel
2. ✅ **Technical excellence:** Clean architecture, modern stack
3. ✅ **Market relevance:** Solves real SA housing management problem
4. ✅ **Scalability:** Multi-platform strategy shows forward thinking
5. ✅ **Execution:** Working demo (even if incomplete) beats perfect slides

**Our Narrative:**
> "We're not just another property management tool—we're building the AI co-pilot that democratizes professional property management for South Africa's 85,000 small landlords. Yes, it's early. Yes, we need to add payments and a database. But the AI foundation is world-class, and that's the hardest part to build. Give us 3 months, and we'll have a production-ready platform that no competitor can match."

**Confidence Score: 75%** ⭐⭐⭐⭐

---

**GOOD LUCK!** 🚀
