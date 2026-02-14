# PropTech Prototype — Diagrams

Below are Mermaid diagrams that represent the main domain classes, AI services, and typical flows: lease generation, invoicing/payment, and AI-powered workflows.

## Class Diagram (Mermaid)

```mermaid
classDiagram
    direction LR

    class DataStore {
      +Landlord CurrentLandlord
      +List~Tenant~ Tenants
      +List~Property~ Properties
      +List~LeaseAgreement~ Leases
      +AddTenant(t: Tenant)
      +GetProperty(id)
    }

    class Landlord {
      +string FullName
      +string IdNumber
      +string BankAccount
    }

    class Property {
      <<abstract>>
      +string Id
      +string Address
      +decimal MonthlyRent
      +abstract GetClause2(): string
    }

    class Room {
      +string SharedSpaces
    }

    class House {
      +int Bedrooms
    }

    class Tenant {
      +string FullName
      +string IdNumber
      +string Email
    }

    class LeaseAgreement {
      +Tenant Lessee
      +Property LeasedProperty
      +decimal MonthlyRent
      +string GenerateDocumentHtml(Landlord)
    }

    class FinancialTransaction {
      <<abstract>>
      +DateTime Date
      +decimal Amount
      +abstract CalculateTotal(): decimal
    }

    class Invoice {
      +List~LineItem~ Items
      +PaymentStatus Status
      +decimal CalculateTotal()
    }

    class Statement {
      +List~Invoice~ Invoices
      +decimal CalculateBalance()
    }

    class PropertyManager {
      -DataStore store
      -AIPropertyAgent aiAgent
      +RegisterNewTenantAsync(fullName)
      +CreateLeaseAsync(tenantId, propertyId)
      +GetPricingRecommendationAsync(propertyId)
      +GetMaintenancePredictionAsync(propertyId)
    }

    class AIConfiguration {
      +string Region
      +string ProjectId
      +string PanguModelId
      +bool IsEnabled
      +CreateDemo(): AIConfiguration
    }

    class HuaweiAIService {
      -AIConfiguration config
      +GetAuthTokenAsync(): string
      +GenerateTextAsync(prompt): string
      +PredictAsync(modelId, features): Dictionary
    }

    class AIPropertyAgent {
      -HuaweiAIService aiService
      -DataStore dataStore
      +ScreenTenantAsync(tenant): TenantScreeningResult
      +RecommendRentalPriceAsync(property): RentalPricingRecommendation
      +PredictMaintenanceAsync(property): MaintenancePrediction
      +GenerateLeaseClauseAsync(property, tenant, clauseType): string
      +RunPortfolioAnalysisAsync(): List~AIInsight~
    }

    class AIInsight {
      +InsightCategory Category
      +string Title
      +double ConfidenceScore
    }

    DataStore "1" -- "1" Landlord : manages
    DataStore "1" -- "0..*" Tenant : manages
    DataStore "1" -- "0..*" Property : manages
    Property <|-- Room
    Property <|-- House
    LeaseAgreement "1" -- "1" Tenant : has
    LeaseAgreement "1" -- "1" Property : has
    PropertyManager "1" -- "1" DataStore : uses
    PropertyManager "1" -- "1" AIPropertyAgent : uses
    AIPropertyAgent "1" -- "1" HuaweiAIService : uses
    AIPropertyAgent "1" -- "1" DataStore : reads
    HuaweiAIService "1" -- "1" AIConfiguration : configured-by
    Invoice "1" -- "1" Tenant : billed-to

```

## Sequence Diagram (Mermaid) — Lease Generation Flow

```mermaid
sequenceDiagram
    participant User
    participant UI
    participant PropertyManager
    participant DataStore
    participant LeaseAgreement

    User->>UI: Request new lease (form)
    UI->>PropertyManager: RegisterNewTenant(form)
    PropertyManager->>DataStore: AddTenant(...)
    UI->>PropertyManager: CreateLease(tenantId, propertyId)
    PropertyManager->>DataStore: GetProperty(propertyId)
    PropertyManager->>LeaseAgreement: new LeaseAgreement(tenant, property)
    LeaseAgreement->>PropertyManager: GenerateDocumentHtml(landlord)
    PropertyManager->>UI: Return lease document

```

## Sequence Diagram (Mermaid) — AI-Powered Tenant Screening & Lease Creation

```mermaid
sequenceDiagram
    participant User
    participant PropertyManager
    participant AIPropertyAgent
    participant HuaweiAIService
    participant HuaweiCloud as Huawei Cloud (Pangu/ModelArts)
    participant DataStore

    User->>PropertyManager: RegisterNewTenantAsync(name)
    PropertyManager->>DataStore: AddTenant(tenant)
    PropertyManager->>AIPropertyAgent: ScreenTenantAsync(tenant)
    AIPropertyAgent->>HuaweiAIService: GenerateTextAsync(screening prompt)
    HuaweiAIService->>HuaweiCloud: POST /predict (Pangu LLM)
    HuaweiCloud-->>HuaweiAIService: NLP analysis response
    AIPropertyAgent->>HuaweiAIService: PredictAsync(risk model, features)
    HuaweiAIService->>HuaweiCloud: POST /predict (ModelArts)
    HuaweiCloud-->>HuaweiAIService: Risk scores
    AIPropertyAgent-->>PropertyManager: TenantScreeningResult
    PropertyManager-->>User: Tenant + Screening result

    User->>PropertyManager: CreateLeaseAsync(tenantId, propertyId)
    PropertyManager->>AIPropertyAgent: GenerateLeaseClauseAsync(property, tenant, clauseType)
    AIPropertyAgent->>HuaweiAIService: GenerateTextAsync(clause prompt)
    HuaweiAIService->>HuaweiCloud: POST /predict (Pangu LLM)
    HuaweiCloud-->>HuaweiAIService: Generated clause text
    AIPropertyAgent-->>PropertyManager: AI-generated lease clause
    PropertyManager->>DataStore: AddLease(lease)
    PropertyManager-->>User: Lease + AI clause
```

## Sequence Diagram (Mermaid) — Invoice & Payment Flow

```mermaid
sequenceDiagram
    participant Tenant
    participant PropertyManager
    participant Invoice
    participant PaymentGateway
    participant DataStore

    PropertyManager->>Invoice: IssueInvoice(lease)
    Invoice->>Tenant: Send invoice
    Tenant->>PaymentGateway: Pay(amount)
    PaymentGateway-->>PropertyManager: Payment success/failure
    alt success
        PropertyManager->>DataStore: Record payment
        PropertyManager->>Tenant: Send receipt
    else failure
        PropertyManager->>Tenant: Notify failure
    end
```