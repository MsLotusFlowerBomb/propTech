using System;
using System.Collections.Generic;
using PropTechWeb.Models;

namespace PropTechWeb.Services
{
    /// <summary>
    /// Simple in-memory data store for domain collections.
    /// Exposes read-only views and encapsulates mutation methods.
    /// </summary>
    public class DataStore
    {
        private List<Tenant> _tenants = new();
        private readonly List<Property> _properties = new();
        private readonly List<LeaseAgreement> _leases = new();

        public Landlord CurrentLandlord { get; private set; }

        public IReadOnlyList<Tenant> Tenants => _tenants.AsReadOnly();
        public IReadOnlyList<Property> Properties => _properties.AsReadOnly();
        public IReadOnlyList<LeaseAgreement> Leases => _leases.AsReadOnly();

        public DataStore()
        {
            // initialize with a default landlord; can be replaced via UpdateLandlord
            CurrentLandlord = new Landlord("Default Landlord", "0000000000", "000-000-000");
        }

        public void UpdateLandlord(Landlord landlord)
        {
            if (landlord == null) throw new ArgumentNullException(nameof(landlord));
            CurrentLandlord = landlord;
        }

        public void AddTenant(Tenant tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            _tenants.Add(tenant);
        }

        public void AddProperty(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            _properties.Add(property);
        }

        public void AddLease(LeaseAgreement lease)
        {
            if (lease == null) throw new ArgumentNullException(nameof(lease));
            _leases.Add(lease);
        }

        public Property? GetPropertyById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            return _properties.Find(p => string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase));
        }
    }
}

namespace PropTechWeb.Models
{
    public class Landlord
    {
        public string FullName { get; private set; }
        public string IdNumber { get; private set; }
        public string BankAccount { get; private set; }

        public Landlord(string fullName, string idNumber, string bankAccount)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            IdNumber = idNumber ?? throw new ArgumentNullException(nameof(idNumber));
            BankAccount = bankAccount ?? string.Empty;
        }

        public void UpdateBankAccount(string newAccount)
        {
            BankAccount = newAccount ?? string.Empty;
        }
    }

    // Minimal Tenant model used by the DataStore. Encapsulated with private setters.
    public class Tenant
    {
        public string Id { get; private set; }
        public string FullName { get; private set; }

        public Tenant(string id, string fullName)
        {
            Id = id ?? Guid.NewGuid().ToString();
            FullName = fullName ?? string.Empty;
        }
    }

    // Minimal Property model.
    public class Property
    {
        public string Id { get; private set; }
        public string Address { get; private set; }
        public decimal MonthlyRent { get; private set; }

        public Property(string id, string address, decimal monthlyRent)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Address = address ?? string.Empty;
            MonthlyRent = monthlyRent;
        }
    }

    // Minimal LeaseAgreement model.
    public class LeaseAgreement
    {
        public string Id { get; private set; }
        public Tenant Lessee { get; private set; }
        public Property LeasedProperty { get; private set; }
        public decimal MonthlyRent { get; private set; }

        public LeaseAgreement(string id, Tenant lessee, Property property, decimal monthlyRent)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Lessee = lessee ?? throw new ArgumentNullException(nameof(lessee));
            LeasedProperty = property ?? throw new ArgumentNullException(nameof(property));
            MonthlyRent = monthlyRent;
        }
    }
}