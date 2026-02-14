# PropTech - Priority Action Plan

**Generated:** February 14, 2026  
**Purpose:** Immediate fixes and production roadmap  
**Status:** DRAFT - Requires team approval

---

## 🔴 CRITICAL - Must Fix Before ANY Public Demo

### 1. Remove False Claims from Documentation
**Timeline:** 2 hours  
**Priority:** P0 - URGENT

**Actions:**
- [ ] Update README.md to remove claims about Invoice/Statement/Payment features
- [ ] Add "PROTOTYPE STATUS" banner at top of README
- [ ] Move unimplemented features to "Future Roadmap" section
- [ ] Update CODE4MZANSI.md evaluation section

**Files to Edit:**
```
/PropTechPrototype/README.md
/PropTechPrototype/CODE4MZANSI.md
/PropTechMaui/README.md (if exists)
/PropTechWeb/README.md (if exists)
```

---

### 2. Test All Demo Workflows
**Timeline:** 4 hours  
**Priority:** P0 - URGENT

**Test Scenarios:**
- [ ] MAUI: Launch app → Dashboard loads → No crashes
- [ ] MAUI: Register tenant → AI screening works → Risk score displays
- [ ] MAUI: Get rental pricing → AI returns recommendation
- [ ] MAUI: Predictive maintenance → Shows urgency + cost
- [ ] MAUI: Virtual tour inspection → Generates findings
- [ ] Blazor: Same 5 tests in web interface
- [ ] Offline mode: Disable Huawei AI → Verify demo responses work

**Acceptance Criteria:**
- Zero crashes during 5 consecutive test runs
- All AI features return results within 5 seconds
- UI displays all data correctly (no "null" or "undefined" text)

---

### 3. Add Error Handling to UI
**Timeline:** 4 hours  
**Priority:** P0 - URGENT

**Implementation:**

#### MAUI Pages (All .xaml.cs files):
```csharp
// Wrap all async event handlers:
private async void OnScreenTenantClicked(object sender, EventArgs e)
{
    try
    {
        IsBusy = true;
        StatusLabel.Text = "Screening tenant...";
        
        var result = await _propertyManager.ScreenTenantAsync(_selectedTenant);
        
        // Display result...
    }
    catch (HttpRequestException ex)
    {
        await DisplayAlert("Connection Error", 
            "Unable to reach AI service. Please check internet connection.", "OK");
        // Log error
    }
    catch (Exception ex)
    {
        await DisplayAlert("Error", 
            $"An unexpected error occurred: {ex.Message}", "OK");
        // Log error
    }
    finally
    {
        IsBusy = false;
        StatusLabel.Text = "";
    }
}
```

#### Blazor Components:
```razor
@code {
    private string errorMessage;
    private bool isLoading;
    
    private async Task ScreenTenant()
    {
        try
        {
            isLoading = true;
            errorMessage = null;
            StateHasChanged();
            
            screeningResult = await PropertyManager.ScreenTenantAsync(selectedTenant);
        }
        catch (Exception ex)
        {
            errorMessage = $"Error: {ex.Message}";
            // Log error
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}

@if (!string.IsNullOrEmpty(errorMessage))
{
    <div class="alert alert-danger">@errorMessage</div>
}
```

**Files to Update:**
- All MAUI Pages: DashboardPage, TenantsPage, LeasesPage, MaintenancePage, VirtualToursPage
- All Blazor Components: Dashboard.razor, Tenants.razor, Leases.razor, Maintenance.razor, VirtualTours.razor

---

## 🟡 HIGH PRIORITY - Production Blockers

### 4. Implement Financial System (Invoices + Payments)
**Timeline:** 8-12 hours  
**Priority:** P1 - High

**Models to Create:**

```csharp
// File: Models/FinancialTransaction.cs
public abstract class FinancialTransaction
{
    public string Id { get; private set; }
    public DateTime Date { get; private set; }
    public decimal Amount { get; private set; }
    public string TenantId { get; private set; }
    public string Description { get; set; }
    
    protected FinancialTransaction(string tenantId, decimal amount, string description)
    {
        Id = Guid.NewGuid().ToString();
        Date = DateTime.Now;
        TenantId = tenantId;
        Amount = amount;
        Description = description ?? string.Empty;
    }
    
    public abstract string TransactionType { get; }
}

// File: Models/Invoice.cs
public class Invoice : FinancialTransaction
{
    public List<LineItem> Items { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime DueDate { get; private set; }
    
    public override string TransactionType => "Invoice";
    
    public Invoice(string tenantId, DateTime dueDate, List<LineItem> items)
        : base(tenantId, items.Sum(i => i.Amount), $"Invoice - {dueDate:yyyy-MM-dd}")
    {
        Items = items ?? new List<LineItem>();
        DueDate = dueDate;
        Status = PaymentStatus.Pending;
    }
    
    public void MarkAsPaid() => Status = PaymentStatus.Paid;
    public void MarkAsOverdue() => Status = PaymentStatus.Overdue;
}

public class LineItem
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public LineItem(string description, decimal amount)
    {
        Description = description;
        Amount = amount;
    }
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Overdue,
    Cancelled
}

// File: Models/Payment.cs
public class Payment : FinancialTransaction
{
    public string InvoiceId { get; private set; }
    public string PaymentMethod { get; private set; }
    public string Reference { get; private set; }
    
    public override string TransactionType => "Payment";
    
    public Payment(string tenantId, string invoiceId, decimal amount, 
                   string paymentMethod, string reference)
        : base(tenantId, amount, $"Payment - {reference}")
    {
        InvoiceId = invoiceId;
        PaymentMethod = paymentMethod ?? "Cash";
        Reference = reference ?? string.Empty;
    }
}

// File: Models/Statement.cs
public class Statement
{
    public string TenantId { get; private set; }
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public List<Invoice> Invoices { get; private set; }
    public List<Payment> Payments { get; private set; }
    
    public Statement(string tenantId, DateTime periodStart, DateTime periodEnd)
    {
        TenantId = tenantId;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        Invoices = new List<Invoice>();
        Payments = new List<Payment>();
    }
    
    public decimal CalculateBalance()
    {
        var totalInvoiced = Invoices.Sum(i => i.Amount);
        var totalPaid = Payments.Sum(p => p.Amount);
        return totalInvoiced - totalPaid;
    }
    
    public string GetStatusSummary()
    {
        var balance = CalculateBalance();
        if (balance == 0) return "Paid in Full";
        if (balance > 0) return $"Outstanding: R{balance:N2}";
        return $"Credit: R{Math.Abs(balance):N2}";
    }
}
```

**Service Methods to Add:**

```csharp
// File: Services/PropertyManager.cs (add these methods)

public Invoice IssueInvoice(string leaseId, List<LineItem> items, DateTime dueDate)
{
    var lease = _dataStore.GetLeaseById(leaseId);
    if (lease == null) throw new ArgumentException($"Lease {leaseId} not found");
    
    var invoice = new Invoice(lease.Lessee.Id, dueDate, items);
    _dataStore.AddInvoice(invoice);
    
    return invoice;
}

public Payment RecordPayment(string invoiceId, decimal amount, 
                             string paymentMethod, string reference)
{
    var invoice = _dataStore.GetInvoiceById(invoiceId);
    if (invoice == null) throw new ArgumentException($"Invoice {invoiceId} not found");
    
    var payment = new Payment(invoice.TenantId, invoiceId, amount, 
                             paymentMethod, reference);
    
    _dataStore.AddPayment(payment);
    
    // Mark invoice as paid if full amount received
    if (payment.Amount >= invoice.Amount)
    {
        invoice.MarkAsPaid();
    }
    
    return payment;
}

public Statement GenerateStatement(string tenantId, DateTime periodStart, DateTime periodEnd)
{
    var statement = new Statement(tenantId, periodStart, periodEnd);
    
    var invoices = _dataStore.GetInvoicesByTenantId(tenantId)
        .Where(i => i.Date >= periodStart && i.Date <= periodEnd)
        .ToList();
    
    var payments = _dataStore.GetPaymentsByTenantId(tenantId)
        .Where(p => p.Date >= periodStart && p.Date <= periodEnd)
        .ToList();
    
    statement.Invoices.AddRange(invoices);
    statement.Payments.AddRange(payments);
    
    return statement;
}
```

**DataStore Updates:**

```csharp
// File: Services/DataStore.cs (add these collections and methods)

public class DataStore
{
    // ... existing properties ...
    
    public List<Invoice> Invoices { get; private set; } = new();
    public List<Payment> Payments { get; private set; } = new();
    
    public void AddInvoice(Invoice invoice) => Invoices.Add(invoice);
    public void AddPayment(Payment payment) => Payments.Add(payment);
    
    public Invoice GetInvoiceById(string id) => 
        Invoices.FirstOrDefault(i => i.Id == id);
    
    public List<Invoice> GetInvoicesByTenantId(string tenantId) => 
        Invoices.Where(i => i.TenantId == tenantId).ToList();
    
    public List<Payment> GetPaymentsByTenantId(string tenantId) => 
        Payments.Where(p => p.TenantId == tenantId).ToList();
}
```

---

### 5. Add Database Persistence (EF Core + SQLite)
**Timeline:** 6-8 hours  
**Priority:** P1 - High

**Step 1: Install NuGet Packages**
```bash
# In all three projects:
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

**Step 2: Create DbContext**

```csharp
// File: Services/PropTechDbContext.cs

using Microsoft.EntityFrameworkCore;

public class PropTechDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<LeaseAgreement> Leases { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<VirtualTour> VirtualTours { get; set; }
    public DbSet<InspectionReport> InspectionReports { get; set; }
    
    public PropTechDbContext(DbContextOptions<PropTechDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Property inheritance
        modelBuilder.Entity<Property>()
            .HasDiscriminator<string>("PropertyType")
            .HasValue<Room>("Room")
            .HasValue<House>("House")
            .HasValue<Shack>("Shack")
            .HasValue<Land>("Land");
        
        // Configure relationships
        modelBuilder.Entity<LeaseAgreement>()
            .HasOne(l => l.Lessee)
            .WithMany()
            .HasForeignKey("LesseeId");
        
        modelBuilder.Entity<LeaseAgreement>()
            .HasOne(l => l.LeasedProperty)
            .WithMany()
            .HasForeignKey("PropertyId");
        
        // Add seed data
        modelBuilder.Entity<Landlord>().HasData(
            new Landlord("Thabo Mbeki", "6001015800084", 
                        new BankAccount("FNB", "62123456789", "Thabo Mbeki"))
        );
    }
}
```

**Step 3: Update Program.cs**

```csharp
// PropTechWeb/Program.cs
builder.Services.AddDbContext<PropTechDbContext>(options =>
    options.UseSqlite("Data Source=proptech.db"));

// PropTechMaui/MauiProgram.cs
builder.Services.AddDbContext<PropTechDbContext>(options =>
    options.UseSqlite($"Data Source={FileSystem.AppDataDirectory}/proptech.db"));
```

**Step 4: Create Migration**

```bash
dotnet ef migrations add InitialCreate --project PropTechWeb
dotnet ef database update --project PropTechWeb
```

**Step 5: Replace DataStore with Repository Pattern**

```csharp
// File: Services/ITenantRepository.cs
public interface ITenantRepository
{
    Task<Tenant> GetByIdAsync(string id);
    Task<List<Tenant>> GetAllAsync();
    Task AddAsync(Tenant tenant);
    Task UpdateAsync(Tenant tenant);
    Task DeleteAsync(string id);
}

// File: Services/TenantRepository.cs
public class TenantRepository : ITenantRepository
{
    private readonly PropTechDbContext _context;
    
    public TenantRepository(PropTechDbContext context)
    {
        _context = context;
    }
    
    public async Task<Tenant> GetByIdAsync(string id)
    {
        return await _context.Tenants.FindAsync(id);
    }
    
    public async Task<List<Tenant>> GetAllAsync()
    {
        return await _context.Tenants.ToListAsync();
    }
    
    public async Task AddAsync(Tenant tenant)
    {
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
    }
    
    // ... other methods
}
```

---

### 6. Implement Lease Document HTML Generation
**Timeline:** 4 hours  
**Priority:** P1 - High

**Create HTML Template:**

```csharp
// File: Services/LeaseDocumentGenerator.cs

public class LeaseDocumentGenerator
{
    public string GenerateLeaseHtml(LeaseAgreement lease, Landlord landlord)
    {
        var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Lease Agreement</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 2cm; }}
        h1 {{ text-align: center; }}
        .clause {{ margin: 20px 0; }}
        .signature {{ margin-top: 50px; }}
        table {{ width: 100%; border-collapse: collapse; }}
        td {{ padding: 5px; border: 1px solid #ddd; }}
    </style>
</head>
<body>
    <h1>RESIDENTIAL LEASE AGREEMENT</h1>
    
    <div class='clause'>
        <h3>PARTIES</h3>
        <p><strong>LESSOR (Landlord):</strong> {landlord.FullName}<br>
        ID Number: {landlord.IdNumber}</p>
        
        <p><strong>LESSEE (Tenant):</strong> {lease.Lessee.FullName}<br>
        ID Number: {lease.Lessee.IdNumber}</p>
    </div>
    
    <div class='clause'>
        <h3>PROPERTY</h3>
        <p>{lease.LeasedProperty.Address}</p>
    </div>
    
    <div class='clause'>
        <h3>RENTAL AMOUNT</h3>
        <p>Monthly Rent: <strong>R{lease.MonthlyRent:N2}</strong></p>
        <p>Deposit: <strong>R{lease.DepositAmount:N2}</strong></p>
    </div>
    
    <div class='clause'>
        <h3>LEASE PERIOD</h3>
        <p>Start Date: {lease.StartDate:dd MMMM yyyy}<br>
        End Date: {lease.EndDate:dd MMMM yyyy}<br>
        Duration: {(lease.EndDate - lease.StartDate).Days / 30} months</p>
    </div>
    
    {lease.LeasedProperty.GetClause2()}
    
    <div class='signature'>
        <table>
            <tr>
                <td><strong>LESSOR SIGNATURE:</strong><br><br>_____________________<br>Date: __________</td>
                <td><strong>LESSEE SIGNATURE:</strong><br><br>_____________________<br>Date: __________</td>
            </tr>
        </table>
    </div>
</body>
</html>";
        
        return html;
    }
}
```

**Update LeaseAgreement Model:**

```csharp
// File: Models/LeaseAgreement.cs (add this method)

public string GenerateDocumentHtml(Landlord landlord)
{
    var generator = new LeaseDocumentGenerator();
    return generator.GenerateLeaseHtml(this, landlord);
}
```

---

## 🟢 NICE TO HAVE - Future Iterations

### 7. Add Unit Tests
**Timeline:** 8 hours  
**Priority:** P2 - Medium

Create test project:
```bash
dotnet new xunit -n PropTechTests
dotnet add PropTechTests reference PropTechPrototype
```

Example tests:
```csharp
public class AIPropertyAgentTests
{
    [Fact]
    public async Task ScreenTenantAsync_ReturnsLowRisk_ForStableEmployment()
    {
        // Arrange
        var config = AIConfiguration.CreateDemo();
        var aiService = new HuaweiAIService(config);
        var dataStore = new DataStore();
        var agent = new AIPropertyAgent(aiService, dataStore);
        
        var tenant = new Tenant("John Doe", "8001015800084", "john@example.com");
        
        // Act
        var result = await agent.ScreenTenantAsync(tenant);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(RiskLevel.Low, result.Risk);
        Assert.InRange(result.RiskScore, 0, 0.3);
    }
}
```

---

### 8. Add Authentication (ASP.NET Core Identity)
**Timeline:** 6 hours  
**Priority:** P2 - Medium

Install package:
```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Update DbContext and add login pages (standard ASP.NET Core Identity scaffolding).

---

### 9. Implement PDF Export
**Timeline:** 4 hours  
**Priority:** P2 - Medium

Install package:
```bash
dotnet add package itext7
```

Convert HTML to PDF:
```csharp
public byte[] GenerateLeasePdf(LeaseAgreement lease, Landlord landlord)
{
    var html = lease.GenerateDocumentHtml(landlord);
    
    using var stream = new MemoryStream();
    var writer = new PdfWriter(stream);
    var pdf = new PdfDocument(writer);
    HtmlConverter.ConvertToPdf(html, pdf);
    
    return stream.ToArray();
}
```

---

## 📊 Progress Tracking

### Current Status: 45% Complete

| Component | Status | Priority | ETA |
|-----------|--------|----------|-----|
| Documentation Cleanup | 🔴 Not Started | P0 | 2h |
| Demo Testing | 🔴 Not Started | P0 | 4h |
| Error Handling | 🔴 Not Started | P0 | 4h |
| Financial System | 🔴 Not Started | P1 | 12h |
| Database (EF Core) | 🔴 Not Started | P1 | 8h |
| Lease HTML Generation | 🔴 Not Started | P1 | 4h |
| Unit Tests | 🔴 Not Started | P2 | 8h |
| Authentication | 🔴 Not Started | P2 | 6h |
| PDF Export | 🔴 Not Started | P2 | 4h |

### Timeline Estimates:

- **P0 (Pre-Demo):** 10 hours (1-2 days)
- **P1 (Production MVP):** 24 hours (3-5 days)
- **P2 (Production Ready):** 18 hours (2-3 days)

**Total:** 52 hours (~7 working days for 1 developer)

---

## 📝 Next Steps

1. **Immediate (Today):** 
   - Update documentation to remove false claims
   - Test all demo workflows
   - Record backup demo video

2. **Pre-Presentation (48h):**
   - Add error handling to all UI pages
   - Rehearse presentation 5+ times
   - Prepare Q&A responses

3. **Post-Competition (Week 1):**
   - Implement financial system (Invoice, Payment, Statement)
   - Add EF Core database persistence
   - Complete lease HTML generation

4. **Production Readiness (Month 1):**
   - Unit tests (60%+ coverage)
   - Authentication + authorization
   - PDF export
   - Security audit

---

**Prepared by:** Development Team  
**Last Updated:** February 14, 2026  
**Review Frequency:** Weekly (post-competition)
