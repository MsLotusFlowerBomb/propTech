# PropTech - AI-Powered Property Management Platform

> **South Africa's First Agentic AI Property Co-Pilot**

[![.NET](https://img.shields.io/badge/.NET-10-blue)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-MAUI%20%2B%20Blazor-green)](https://learn.microsoft.com/en-us/dotnet/maui/)
[![AI](https://img.shields.io/badge/AI-Huawei%20Cloud-orange)](https://www.huaweicloud.com/)
[![Status](https://img.shields.io/badge/Status-Prototype-yellow)](REVIEW_INDEX.md)

---

## 🎯 Quick Links

📊 **[Competition Review](REVIEW_INDEX.md)** - Complete platform assessment  
🎤 **[Presentation Guide](PRESENTATION_BRIEF.md)** - Code4Mzansi presentation  
⚡ **[Executive Summary](EXECUTIVE_SUMMARY.md)** - Quick overview (9KB)  
🔧 **[Action Plan](ACTION_PLAN.md)** - Technical roadmap  

---

## 📖 What is PropTech?

PropTech (Property Technology) is an AI-powered platform that helps South African small landlords manage township properties, backyard units, and informal rentals with:

- 🤖 **AI Tenant Screening** - Risk scoring with Huawei Pangu LLM
- 💰 **AI Rental Pricing** - Market analysis with confidence scores
- 🔧 **Predictive Maintenance** - Forecasts equipment failures before they happen
- 📸 **Virtual Tour Inspections** - AI defect detection from 360° panoramas
- 📊 **Portfolio Analytics** - Autonomous insights across all properties

---

## 🏗️ Architecture

**Three Platforms, One Codebase:**

```
┌─────────────────────────────────────────────┐
│  🖥️  .NET MAUI Desktop (Windows)           │
│  Property managers' native app              │
├─────────────────────────────────────────────┤
│  🌐 ASP.NET Core Blazor (Web)               │
│  Browser-based access for tenants/agents    │
├─────────────────────────────────────────────┤
│  🤖 Shared Services Layer (80% reuse)       │
│  PropertyManager, AIPropertyAgent           │
├─────────────────────────────────────────────┤
│  ☁️  Huawei Cloud AI Integration            │
│  Pangu LLM + ModelArts Predictions          │
└─────────────────────────────────────────────┘
```

**Projects:**
- `PropTechPrototype/` - Console CLI prototype
- `PropTechMaui/` - Native Windows desktop app
- `PropTechWeb/` - Blazor web application

---

## ⚡ Quick Start

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022+ (for MAUI)
- Windows 10/11 (for desktop app)

### Run Web Application
```bash
cd PropTechWeb
dotnet run
# Open browser to https://localhost:5001
```

### Run Desktop Application
```bash
# Open PropTechMaui/PropTechMaui.csproj in Visual Studio
# Select "Windows Machine" target
# Press F5 to run
```

### Run Console Prototype
```bash
cd PropTechPrototype
dotnet run
```

---

## 📊 Current Status

**Overall: ⭐⭐⭐ (3/5 Stars)** - Strong innovation, needs completion

### ✅ What's Working (45%)
- AI tenant screening with risk scoring
- AI rental pricing recommendations
- Predictive maintenance forecasting
- Virtual tour AI inspections
- Multi-platform UI (MAUI + Blazor)

### ❌ What's Missing (55%)
- Financial system (Invoice, Payment, Statement)
- Database persistence (in-memory only)
- Authentication/authorization
- PDF export (HTML only)
- Unit tests (0% coverage)
- Payment gateway integration

**See [EXECUTIVE_SUMMARY.md](EXECUTIVE_SUMMARY.md) for detailed assessment.**

---

## 🎯 Target Market

**Who:** South African small landlords (10-100 units)

**Market:**
- 🏘️ 8.5M informal dwellings (rooms, shacks, backyard units)
- 💼 85,000 small landlords underserved by enterprise tools
- 💰 R250B+ rental housing market
- 📊 60% rental penetration in townships

**Differentiation:**
- Agentic AI (3-5 years ahead of competitors)
- Informal housing focus (vs formal-only competitors)
- Affordable pricing (vs R300+/unit enterprise tools)

---

## 🚀 Roadmap

### Phase 1: Production MVP (Month 1-2)
- [ ] Implement Invoice/Payment/Statement models
- [ ] Add EF Core + SQL Server database
- [ ] Complete lease document generation
- [ ] Add authentication (ASP.NET Core Identity)
- [ ] Security audit + POPIA compliance

### Phase 2: Market Launch (Month 3-4)
- [ ] Payment gateway integration (Paystack/Stripe)
- [ ] WhatsApp notifications
- [ ] Tenant self-service portal
- [ ] Unit tests (60%+ coverage)
- [ ] Beta testing with 10 Cape Town landlords

### Phase 3: Scale (Month 5-6)
- [ ] Mobile apps (iOS/Android)
- [ ] Multi-language (Zulu, Xhosa, Afrikaans)
- [ ] REST API for integrations
- [ ] Pilot in Johannesburg, Durban, East London

**Total: ~90 hours (3 months for 1 FTE)**

---

## 🤖 AI Features

### 1. Tenant Screening
Analyzes application forms using Huawei Pangu LLM + ModelArts:
```
Risk Level: MEDIUM (0.45)
Factors:
- ✅ Stable employment (2 years)
- ⚠️ No rental references
- ✅ Income 3x rent requirement
```

### 2. Rental Pricing
Market analysis with confidence scoring:
```
Recommended: R4,500/month
Range: R3,800 - R5,200
Confidence: 85%
Market: ↗️ Rising (+5% YoY)
```

### 3. Predictive Maintenance
Forecasts equipment failures:
```
Next Failure: Geyser
Urgency: MEDIUM (60-90 days)
Cost: R4,200
Action: Schedule inspection
```

### 4. Virtual Tour Inspection
AI analyzes 360° panoramas:
```
Overall: FAIR (0.65/1.0)
Findings: 4 issues
- Wall paint peeling (R850)
- Ceiling water damage (R2,200)
Total Repairs: R8,500
```

---

## 📚 Documentation

### Review Documents (Start Here!)
- **[REVIEW_INDEX.md](REVIEW_INDEX.md)** - Navigation guide for all docs
- **[EXECUTIVE_SUMMARY.md](EXECUTIVE_SUMMARY.md)** - 9KB quick reference
- **[COMPETITION_REVIEW.md](COMPETITION_REVIEW.md)** - 24KB full analysis
- **[PRESENTATION_BRIEF.md](PRESENTATION_BRIEF.md)** - 13KB speaker guide
- **[ACTION_PLAN.md](ACTION_PLAN.md)** - 18KB technical roadmap

### Technical Documentation
- **[PropTechPrototype/README.md](PropTechPrototype/README.md)** - Architecture details
- **[PropTechPrototype/CODE4MZANSI.md](PropTechPrototype/CODE4MZANSI.md)** - Competition brief
- **[PropTechPrototype/DIAGRAMS.md](PropTechPrototype/DIAGRAMS.md)** - Mermaid diagrams

---

## 🏆 Competition: Code4Mzansi

**Status:** Presentation-ready  
**Confidence:** 60% win probability  

**Strengths:**
- ✅ World-class AI integration (Huawei Cloud)
- ✅ Innovative agentic AI approach
- ✅ South African market focus
- ✅ Multi-platform strategy

**Weaknesses:**
- ❌ Only 45% complete
- ❌ Missing financial system
- ❌ No database persistence
- ❌ Zero test coverage

**Strategy:** Lead with AI innovation, acknowledge gaps honestly, present clear 3-month roadmap.

---

## 🔒 Security & Compliance

**Current Status:** 🔴 CRITICAL (Prototype only)

| Risk | Status |
|------|--------|
| Authentication | ❌ Not implemented |
| Authorization | ❌ Not implemented |
| Data Encryption | ❌ Not implemented |
| POPIA Compliance | ❌ Non-compliant |

**Warning:** Do NOT deploy to production without implementing authentication and encryption.

---

## 💡 Technology Stack

**Core:**
- .NET 10 (C# 13)
- .NET MAUI (Windows Desktop)
- ASP.NET Core Blazor (Web)
- Huawei Cloud AI (Pangu LLM + ModelArts)

**Data:**
- In-memory DataStore (prototype)
- EF Core + SQL Server (roadmap)

**Frontend:**
- MAUI XAML (native controls)
- Blazor Razor Components
- Bootstrap 5

---

## 🤝 Contributing

This is a Code4Mzansi competition project. Contributions welcome post-competition!

**Priority Areas:**
1. Implement financial system (Invoice, Payment, Statement)
2. Add EF Core database persistence
3. Write unit tests (target 60%+ coverage)
4. Implement authentication (ASP.NET Core Identity)

See [ACTION_PLAN.md](ACTION_PLAN.md) for detailed tasks.

---

## 📄 License

Copyright © 2026 PropTech Team  
All rights reserved.

---

## 📞 Contact

**Repository:** github.com/MsLotusFlowerBomb/propTech  
**Competition:** Code4Mzansi 2026  

---

**Built with ❤️ for South African landlords**

🏠 Making property management accessible to everyone
