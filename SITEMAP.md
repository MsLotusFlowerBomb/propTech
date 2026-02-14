# PropTech Platform - Complete Sitemap

## Overview

This document provides a comprehensive sitemap for both the **MAUI Desktop Application** (Property Managers) and the **Blazor Web Application** (Tenants & Agents).

---

## MAUI Desktop Application Sitemap (Property Managers)

### Navigation Structure

```
PropMate Desktop App
│
├─ 🏠 Dashboard (Home)
│  ├─ KPI Cards
│  ├─ Recent Activity Feed
│  ├─ AI Insights Panel
│  ├─ Quick Actions
│  └─ Portfolio Performance Chart
│
├─ 🏢 Properties
│  ├─ Properties List View
│  │  ├─ Filter & Search
│  │  ├─ Sort Options
│  │  └─ Bulk Actions
│  │
│  ├─ Property Details
│  │  ├─ Basic Information
│  │  ├─ Photos & Virtual Tour
│  │  ├─ Lease History
│  │  ├─ Maintenance History
│  │  ├─ Financial Summary
│  │  ├─ Documents
│  │  └─ AI Insights (Pricing, Condition)
│  │
│  ├─ Add New Property
│  │  ├─ Property Type Selection
│  │  ├─ Basic Details Form
│  │  ├─ Media Upload
│  │  ├─ Pricing (with AI Recommendation)
│  │  ├─ Features & Amenities
│  │  └─ Review & Publish
│  │
│  ├─ Virtual Tours
│  │  ├─ Schedule Tour
│  │  ├─ Upload 360° Photos
│  │  ├─ View Tour
│  │  └─ AI Inspection Report
│  │
│  └─ Property Reports
│     ├─ Occupancy Report
│     ├─ Maintenance Cost Report
│     └─ Performance Report
│
├─ 👥 Tenants
│  ├─ Tenants List View
│  │  ├─ Filter by Status
│  │  ├─ Search
│  │  └─ Sort Options
│  │
│  ├─ Tenant Profile
│  │  ├─ Personal Information
│  │  ├─ AI Risk Assessment
│  │  ├─ Current Lease
│  │  ├─ Payment History
│  │  ├─ Maintenance Requests
│  │  ├─ Documents
│  │  ├─ Communication Log
│  │  └─ Actions (Edit, Send Message, Create Lease)
│  │
│  ├─ Tenant Applications
│  │  ├─ Pending Applications
│  │  ├─ Application Detail
│  │  │  ├─ Application Form
│  │  │  ├─ Uploaded Documents
│  │  │  ├─ AI Screening Results
│  │  │  └─ Decision Actions
│  │  └─ Application History
│  │
│  ├─ Register New Tenant
│  │  ├─ Personal Information
│  │  ├─ Employment Details
│  │  ├─ References
│  │  ├─ Document Upload
│  │  └─ Submit for AI Screening
│  │
│  └─ Tenant Reports
│     ├─ Arrears Report
│     ├─ Retention Report
│     └─ Screening Analytics
│
├─ 📄 Leases
│  ├─ Leases List View
│  │  ├─ Filter (Active, Expiring, Expired)
│  │  ├─ Search
│  │  └─ Sort Options
│  │
│  ├─ Lease Details
│  │  ├─ Lease Information
│  │  ├─ Tenant Details
│  │  ├─ Property Details
│  │  ├─ Financial Terms
│  │  ├─ Lease Document (View/Download)
│  │  ├─ Amendments
│  │  ├─ Renewal Status
│  │  └─ Actions (Renew, Terminate, Amend)
│  │
│  ├─ Create New Lease (Wizard)
│  │  ├─ Step 1: Select Tenant & Property
│  │  ├─ Step 2: Lease Terms
│  │  ├─ Step 3: AI-Generated Clauses
│  │  ├─ Step 4: Review & Generate
│  │  └─ Step 5: Send for Signatures
│  │
│  ├─ Lease Renewals
│  │  ├─ Upcoming Renewals
│  │  ├─ Renewal Recommendations (AI)
│  │  └─ Renewal Workflow
│  │
│  └─ Lease Templates
│     ├─ Standard Templates
│     ├─ Custom Templates
│     └─ Template Editor
│
├─ 💰 Financials
│  ├─ Dashboard
│  │  ├─ Revenue Summary
│  │  ├─ Collection Rate
│  │  ├─ Outstanding Balance
│  │  └─ Cash Flow Chart
│  │
│  ├─ Invoices
│  │  ├─ Invoice List (All, Paid, Unpaid, Overdue)
│  │  ├─ Invoice Details
│  │  ├─ Generate Invoices (Bulk)
│  │  └─ Send Invoices
│  │
│  ├─ Payments
│  │  ├─ Payment List
│  │  ├─ Payment Details
│  │  ├─ Record Manual Payment
│  │  ├─ Payment Reconciliation
│  │  └─ Payment Methods Setup
│  │
│  ├─ Statements
│  │  ├─ Generate Tenant Statement
│  │  ├─ Generate Portfolio Statement
│  │  └─ Statement History
│  │
│  ├─ Expenses
│  │  ├─ Expense List
│  │  ├─ Add Expense
│  │  ├─ Expense Categories
│  │  └─ Expense Reports
│  │
│  ├─ Banking
│  │  ├─ Bank Accounts
│  │  ├─ Bank Feed
│  │  └─ Reconciliation
│  │
│  └─ Reports
│     ├─ Income Statement
│     ├─ Balance Sheet
│     ├─ Cash Flow Statement
│     ├─ Rent Roll
│     ├─ Arrears Aging
│     └─ Tax Reports
│
├─ 🔧 Maintenance
│  ├─ Maintenance Dashboard
│  │  ├─ Open Tickets
│  │  ├─ Priority Distribution
│  │  ├─ Average Resolution Time
│  │  └─ Cost Summary
│  │
│  ├─ Tickets List
│  │  ├─ Filter (Status, Priority, Property)
│  │  ├─ Search
│  │  └─ Sort Options
│  │
│  ├─ Ticket Details
│  │  ├─ Issue Description
│  │  ├─ Photos/Videos
│  │  ├─ AI Triage Results
│  │  ├─ Assigned Contractor
│  │  ├─ Status History
│  │  ├─ Cost Estimate
│  │  ├─ Communications
│  │  └─ Actions (Assign, Update, Close)
│  │
│  ├─ Create Maintenance Request
│  │  ├─ Select Property
│  │  ├─ Issue Details
│  │  ├─ Upload Evidence
│  │  ├─ Set Priority
│  │  └─ Submit (AI Auto-Triage)
│  │
│  ├─ Contractors
│  │  ├─ Contractor List
│  │  ├─ Contractor Profile
│  │  │  ├─ Contact Info
│  │  │  ├─ Skills & Certifications
│  │  │  ├─ Service Area
│  │  │  ├─ Ratings & Reviews
│  │  │  └─ Work History
│  │  └─ Add Contractor
│  │
│  ├─ Preventive Maintenance
│  │  ├─ Schedules
│  │  ├─ Upcoming Tasks
│  │  ├─ AI Predictions
│  │  └─ Create Schedule
│  │
│  └─ Reports
│     ├─ Maintenance Cost by Property
│     ├─ Average Resolution Time
│     ├─ Contractor Performance
│     └─ Issue Trends
│
├─ 🤖 AI Analytics
│  ├─ Portfolio Health Dashboard
│  │  ├─ Overall Health Score
│  │  ├─ Key Insights
│  │  ├─ Predictive Analytics
│  │  └─ AI Model Performance
│  │
│  ├─ Tenant Screening Analytics
│  │  ├─ Risk Distribution
│  │  ├─ Screening Accuracy
│  │  ├─ Top Risk Factors
│  │  └─ Historical Trends
│  │
│  ├─ Pricing Intelligence
│  │  ├─ Market Analysis
│  │  ├─ Pricing Recommendations
│  │  ├─ Rent Optimization Opportunities
│  │  └─ Competitive Analysis
│  │
│  ├─ Predictive Maintenance
│  │  ├─ Upcoming Issues Prediction
│  │  ├─ Cost Forecasts
│  │  ├─ Property Risk Scores
│  │  └─ Preventive Actions
│  │
│  ├─ Portfolio Optimization
│  │  ├─ Underperforming Properties
│  │  ├─ Growth Opportunities
│  │  ├─ Efficiency Recommendations
│  │  └─ ROI Analysis
│  │
│  └─ AI Settings
│     ├─ AI Configuration
│     ├─ Model Training
│     ├─ Feedback Loop
│     └─ Demo Mode Toggle
│
├─ 🛒 Marketplace (Future)
│  ├─ Browse Listings
│  │  ├─ Property Listings
│  │  ├─ Service Provider Listings
│  │  └─ Filters & Search
│  │
│  ├─ My Listings
│  │  ├─ Active Listings
│  │  ├─ Draft Listings
│  │  └─ Archived Listings
│  │
│  ├─ Create Listing
│  │  ├─ Listing Type
│  │  ├─ Details & Media
│  │  ├─ Pricing
│  │  └─ Publish
│  │
│  ├─ Bookings
│  │  ├─ Pending Bookings
│  │  ├─ Confirmed Bookings
│  │  └─ Booking History
│  │
│  └─ Reviews & Ratings
│     ├─ Received Reviews
│     ├─ Given Reviews
│     └─ Response Management
│
├─ 📊 Reports
│  ├─ Dashboard
│  │  ├─ Favorite Reports
│  │  ├─ Recent Reports
│  │  └─ Scheduled Reports
│  │
│  ├─ Standard Reports
│  │  ├─ Financial Reports
│  │  ├─ Operational Reports
│  │  ├─ Compliance Reports
│  │  └─ Marketing Reports
│  │
│  ├─ Custom Report Builder
│  │  ├─ Select Data Sources
│  │  ├─ Choose Fields
│  │  ├─ Apply Filters
│  │  ├─ Design Layout
│  │  └─ Save & Schedule
│  │
│  └─ Report Library
│     ├─ Saved Reports
│     ├─ Templates
│     └─ Export Options
│
├─ ⚙️ Settings
│  ├─ Profile
│  │  ├─ Personal Information
│  │  ├─ Change Password
│  │  ├─ Security Settings (MFA)
│  │  └─ Notification Preferences
│  │
│  ├─ Organization
│  │  ├─ Company Details
│  │  ├─ Bank Account Information
│  │  ├─ Tax Information
│  │  └─ Business Hours
│  │
│  ├─ Users & Permissions
│  │  ├─ User List
│  │  ├─ Add User
│  │  ├─ Roles & Permissions
│  │  └─ Activity Log
│  │
│  ├─ Property Settings
│  │  ├─ Property Types
│  │  ├─ Features & Amenities
│  │  ├─ Default Terms
│  │  └─ Templates
│  │
│  ├─ Financial Settings
│  │  ├─ Payment Methods
│  │  ├─ Invoice Settings
│  │  ├─ Late Fee Configuration
│  │  ├─ Tax Settings
│  │  └─ Integration Settings
│  │
│  ├─ Communication Settings
│  │  ├─ Email Templates
│  │  ├─ SMS Settings
│  │  ├─ WhatsApp Integration
│  │  └─ Notification Rules
│  │
│  ├─ Maintenance Settings
│  │  ├─ Ticket Categories
│  │  ├─ Priority Levels
│  │  ├─ SLA Configuration
│  │  └─ Contractor Settings
│  │
│  ├─ AI Configuration
│  │  ├─ Huawei Cloud Settings
│  │  ├─ AI Features Toggle
│  │  ├─ Confidence Thresholds
│  │  └─ Training Data Management
│  │
│  └─ System Settings
│     ├─ General Settings
│     ├─ Data & Privacy
│     ├─ Backup & Restore
│     ├─ Audit Log
│     └─ API Keys
│
└─ ❓ Help & Support
   ├─ Help Center
   │  ├─ Getting Started
   │  ├─ Video Tutorials
   │  ├─ User Guides
   │  └─ FAQs
   │
   ├─ Contact Support
   │  ├─ Submit Ticket
   │  ├─ Live Chat
   │  └─ Phone Support
   │
   ├─ What's New
   │  ├─ Release Notes
   │  ├─ Feature Announcements
   │  └─ Roadmap
   │
   └─ About
      ├─ Version Information
      ├─ Terms of Service
      ├─ Privacy Policy
      └─ Licenses
```

---

## Blazor Web Application Sitemap (Tenants & Public)

### Navigation Structure

```
PropMate Tenant Portal
│
├─ 🏠 Home (Public - Not Logged In)
│  ├─ Platform Overview
│  ├─ Features
│  ├─ How It Works
│  ├─ Testimonials
│  ├─ Pricing
│  ├─ FAQ
│  ├─ Login
│  └─ Apply Now
│
├─ 🔍 Property Search (Public)
│  ├─ Search & Filters
│  │  ├─ Location
│  │  ├─ Price Range
│  │  ├─ Property Type
│  │  ├─ Bedrooms
│  │  └─ Amenities
│  │
│  ├─ Property Listings
│  │  ├─ List View
│  │  ├─ Map View
│  │  └─ Gallery View
│  │
│  ├─ Property Details
│  │  ├─ Photos & Virtual Tour
│  │  ├─ Description
│  │  ├─ Features & Amenities
│  │  ├─ Location Map
│  │  ├─ Pricing
│  │  ├─ Lease Terms
│  │  ├─ Similar Properties
│  │  └─ Apply Button
│  │
│  └─ Saved Properties
│     └─ Favorites List
│
├─ 📝 Apply (Public)
│  ├─ Application Form
│  │  ├─ Personal Information
│  │  ├─ Employment Details
│  │  ├─ References
│  │  ├─ Document Upload
│  │  └─ Consent & Disclosures
│  │
│  ├─ Application Status
│  │  ├─ Track Application
│  │  ├─ AI Screening Status
│  │  └─ Next Steps
│  │
│  └─ Application History
│     └─ Previous Applications
│
├─ 🔐 Login / Register
│  ├─ Login
│  │  ├─ Email/Password
│  │  ├─ Forgot Password
│  │  └─ Social Login (Future)
│  │
│  └─ Register
│     ├─ Create Account
│     ├─ Verify Email
│     └─ Complete Profile
│
│
├─ 📱 Tenant Dashboard (Logged In)
│  ├─ Overview
│  │  ├─ Your Home Card
│  │  ├─ Account Status
│  │  ├─ Quick Actions
│  │  ├─ Maintenance Summary
│  │  ├─ Important Documents
│  │  └─ Announcements
│  │
│  ├─ Notifications
│  │  ├─ All Notifications
│  │  ├─ Unread
│  │  └─ Archived
│  │
│  └─ Quick Links
│     ├─ Pay Rent
│     ├─ Report Issue
│     ├─ View Statements
│     └─ Contact Landlord
│
├─ 🏡 My Home
│  ├─ Property Details
│  │  ├─ Address & Info
│  │  ├─ Photos & Virtual Tour
│  │  ├─ Features
│  │  └─ Contact Information
│  │
│  ├─ Lease Information
│  │  ├─ Lease Terms
│  │  ├─ Lease Document
│  │  ├─ Renewal Status
│  │  └─ Important Dates
│  │
│  ├─ Move-In Documentation
│  │  ├─ Inspection Report
│  │  ├─ Condition Photos
│  │  └─ Inventory Checklist
│  │
│  └─ House Rules
│     ├─ Property Rules
│     ├─ Contact Procedures
│     └─ Emergency Contacts
│
├─ 💳 Payments & Billing
│  ├─ Account Summary
│  │  ├─ Current Balance
│  │  ├─ Next Payment Due
│  │  ├─ Payment History
│  │  └─ Account Status
│  │
│  ├─ Make Payment
│  │  ├─ Payment Amount
│  │  ├─ Payment Method
│  │  │  ├─ Card Payment
│  │  │  ├─ Bank Transfer (EFT)
│  │  │  └─ Debit Order
│  │  └─ Payment Confirmation
│  │
│  ├─ Payment History
│  │  ├─ Past Payments
│  │  ├─ Receipt Download
│  │  └─ Payment Details
│  │
│  ├─ Invoices
│  │  ├─ Current Invoice
│  │  ├─ Past Invoices
│  │  ├─ Invoice Details
│  │  └─ Download PDF
│  │
│  ├─ Statements
│  │  ├─ Monthly Statements
│  │  ├─ Annual Statement
│  │  └─ Custom Date Range
│  │
│  └─ Auto-Pay Setup
│     ├─ Enable/Disable
│     ├─ Payment Method
│     └─ Backup Payment Method
│
├─ 🔧 Maintenance
│  ├─ Report Issue
│  │  ├─ Issue Category
│  │  ├─ Description
│  │  ├─ Photos/Videos
│  │  ├─ Urgency Level
│  │  └─ Submit Request
│  │
│  ├─ My Requests
│  │  ├─ Open Requests
│  │  ├─ In Progress
│  │  ├─ Resolved
│  │  └─ All History
│  │
│  ├─ Request Details
│  │  ├─ Issue Description
│  │  ├─ Status Updates
│  │  ├─ Assigned Contractor
│  │  ├─ Scheduled Date/Time
│  │  ├─ Photos/Updates
│  │  ├─ Communications
│  │  └─ Rate & Review
│  │
│  └─ Maintenance Tips
│     ├─ Preventive Maintenance
│     ├─ DIY Guides
│     └─ Emergency Procedures
│
├─ 📄 Documents
│  ├─ Lease Agreement
│  │  ├─ Current Lease
│  │  ├─ Previous Leases
│  │  └─ Amendments
│  │
│  ├─ Inspection Reports
│  │  ├─ Move-In Report
│  │  ├─ Periodic Inspections
│  │  └─ Move-Out Report
│  │
│  ├─ Invoices & Receipts
│  │  ├─ Monthly Invoices
│  │  ├─ Payment Receipts
│  │  └─ Annual Summaries
│  │
│  ├─ Personal Documents
│  │  ├─ Uploaded Documents
│  │  ├─ ID/Passport
│  │  ├─ Proof of Income
│  │  └─ References
│  │
│  └─ Important Documents
│     ├─ House Rules
│     ├─ Community Guidelines
│     └─ Emergency Procedures
│
├─ 💬 Messages
│  ├─ Inbox
│  │  ├─ Unread Messages
│  │  ├─ All Messages
│  │  └─ Archived
│  │
│  ├─ Compose Message
│  │  ├─ To: Landlord/Manager
│  │  ├─ Subject
│  │  ├─ Message Body
│  │  └─ Attachments
│  │
│  ├─ Conversation View
│  │  ├─ Message Thread
│  │  ├─ Reply
│  │  └─ Attachments
│  │
│  └─ Announcements
│     ├─ Property Announcements
│     ├─ Community Updates
│     └─ Important Notices
│
├─ 👤 Profile
│  ├─ Personal Information
│  │  ├─ Name & Contact
│  │  ├─ Emergency Contact
│  │  └─ Edit Profile
│  │
│  ├─ Account Settings
│  │  ├─ Change Password
│  │  ├─ Security (MFA)
│  │  ├─ Email Preferences
│  │  └─ Privacy Settings
│  │
│  ├─ Notification Preferences
│  │  ├─ Email Notifications
│  │  ├─ SMS Notifications
│  │  ├─ Push Notifications
│  │  └─ WhatsApp (opt-in)
│  │
│  └─ Language & Region
│     ├─ Preferred Language
│     ├─ Date Format
│     └─ Currency
│
├─ 🌟 Community (Future)
│  ├─ Residents Directory
│  │  ├─ Find Neighbors
│  │  ├─ Contact Residents
│  │  └─ Privacy Controls
│  │
│  ├─ Bulletin Board
│  │  ├─ Community Posts
│  │  ├─ Create Post
│  │  └─ Categories
│  │
│  ├─ Events Calendar
│  │  ├─ Upcoming Events
│  │  ├─ RSVP
│  │  └─ Past Events
│  │
│  └─ Local Services
│     ├─ Recommended Services
│     ├─ Service Categories
│     └─ Reviews & Ratings
│
├─ 📞 Help & Support
│  ├─ Help Center
│  │  ├─ Getting Started
│  │  ├─ FAQs
│  │  ├─ Video Tutorials
│  │  └─ User Guides
│  │
│  ├─ Contact Support
│  │  ├─ Submit Ticket
│  │  ├─ Live Chat
│  │  ├─ Email Support
│  │  └─ Phone Number
│  │
│  └─ Feedback
│     ├─ Rate Your Experience
│     ├─ Suggestions
│     └─ Report Bug
│
└─ ⚙️ Account
   ├─ Lease Management
   │  ├─ Renewal Options
   │  ├─ Move-Out Notice
   │  └─ Lease Transfer
   │
   ├─ Move-Out Process
   │  ├─ Submit Notice
   │  ├─ Schedule Inspection
   │  ├─ Final Walkthrough
   │  └─ Deposit Return Status
   │
   └─ Account Closure
      ├─ Request Account Deletion
      ├─ Download Data
      └─ Final Statement
```

---

## Mobile App Navigation (Future)

### Bottom Tab Navigation (Primary)

```
┌─────────┬─────────┬─────────┬─────────┐
│  Home   │Payments │ Issues  │ Profile │
│   🏠    │   💰    │   🔧    │   👤    │
└─────────┴─────────┴─────────┴─────────┘
```

### Mobile-Specific Screens
- Camera capture for maintenance issues
- Quick payment flow
- Notification center
- Offline mode indicators
- Swipe gestures for navigation

---

## URL Structure (Blazor Web App)

### Public URLs
```
/                           → Home page
/search                     → Property search
/properties/{id}            → Property details
/apply                      → Application form
/apply/{id}                 → Application status
/login                      → Login page
/register                   → Registration
/forgot-password            → Password reset
```

### Authenticated URLs (Tenant)
```
/dashboard                  → Tenant dashboard
/home                       → My home details
/payments                   → Payment center
/payments/make              → Make payment
/payments/history           → Payment history
/maintenance                → Maintenance requests
/maintenance/new            → Report new issue
/maintenance/{id}           → Request details
/documents                  → Document library
/messages                   → Messaging center
/profile                    → User profile
/settings                   → Account settings
```

### Admin URLs (Property Manager - if web-based)
```
/admin                      → Admin dashboard
/admin/properties           → Properties management
/admin/tenants              → Tenants management
/admin/leases               → Leases management
/admin/financials           → Financial management
/admin/maintenance          → Maintenance management
/admin/reports              → Reports
/admin/settings             → System settings
```

---

## Navigation Depth Analysis

### MAUI Desktop (Property Manager)
- **Average Depth**: 3-4 clicks to any feature
- **Maximum Depth**: 5 clicks (rare cases)
- **Quick Access**: 1-2 clicks for 80% of tasks

### Blazor Web (Tenant)
- **Average Depth**: 2-3 clicks to any feature
- **Maximum Depth**: 4 clicks
- **Quick Access**: 1-2 clicks for 90% of tasks

---

## Search & Global Navigation

### Global Search Scopes (MAUI Desktop)
- Properties (by address, ID, type)
- Tenants (by name, ID, email)
- Leases (by ID, property, tenant)
- Invoices (by number, tenant)
- Maintenance (by ticket number, property)
- Documents (by name, type)

### Quick Actions (Always Accessible)
- Add Property
- Register Tenant
- Create Lease
- Generate Invoice
- Report Maintenance

---

## Accessibility Features

### Keyboard Navigation
- Tab through all interactive elements
- Shortcuts for common actions
- Focus indicators
- Skip navigation links

### Screen Reader Support
- ARIA labels on all controls
- Semantic HTML structure
- Alt text for images
- Meaningful page titles

---

## Conclusion

This comprehensive sitemap ensures:
1. **Complete Coverage**: Every feature has a defined place
2. **Logical Grouping**: Related features are grouped together
3. **Clear Hierarchy**: Navigation depth is minimized
4. **Intuitive Paths**: Users can predict where to find features
5. **Consistent Structure**: Both apps follow similar patterns
6. **Accessibility**: Navigation works for all users

Use this sitemap as a reference for:
- Feature development planning
- UX design decisions
- User training materials
- QA test case development
- Documentation structure
