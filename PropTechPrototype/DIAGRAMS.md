# PropTech Prototype — Diagrams

Below are Mermaid diagrams that represent the main domain classes and two typical flows: lease generation and invoicing/payment.

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
      +RegisterNewTenant(data)
      +CreateLease(tenantId, propertyId)
      +IssueInvoice(leaseId)
    }

    DataStore "1" -- "1" Landlord : manages
    DataStore "1" -- "0..*" Tenant : manages
    DataStore "1" -- "0..*" Property : manages
    Property <|-- Room
    Property <|-- House
    LeaseAgreement "1" -- "1" Tenant : has
    LeaseAgreement "1" -- "1" Property : has
    PropertyManager "1" -- "1" DataStore : uses
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