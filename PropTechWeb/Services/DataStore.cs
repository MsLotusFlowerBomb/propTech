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
        private readonly List<Tenant> _tenants = new();
        private readonly List<Property> _properties = new();
        private readonly List<LeaseAgreement> _leases = new();
        private readonly List<VirtualTour> _virtualTours = new();

        public Landlord CurrentLandlord { get; private set; }

        public IReadOnlyList<Tenant> Tenants => _tenants.AsReadOnly();
        public IReadOnlyList<Property> Properties => _properties.AsReadOnly();
        public IReadOnlyList<LeaseAgreement> Leases => _leases.AsReadOnly();
        public IReadOnlyList<VirtualTour> VirtualTours => _virtualTours.AsReadOnly();

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

        public void AddVirtualTour(VirtualTour tour)
        {
            if (tour == null) throw new ArgumentNullException(nameof(tour));
            _virtualTours.Add(tour);
        }

        public VirtualTour? GetVirtualTourByPropertyId(string propertyId)
        {
            if (string.IsNullOrWhiteSpace(propertyId)) return null;
            return _virtualTours.Find(t => string.Equals(t.PropertyId, propertyId, StringComparison.OrdinalIgnoreCase));
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

    /// <summary>
    /// Represents a 360° virtual tour for a property, containing room panoramas
    /// and an optional AI-generated inspection report.
    /// </summary>
    public class VirtualTour
    {
        public string Id { get; private set; }
        public string PropertyId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<RoomPanorama> Rooms { get; private set; }
        public InspectionReport? Inspection { get; private set; }

        public VirtualTour(string id, string propertyId, List<RoomPanorama> rooms)
        {
            Id = id ?? Guid.NewGuid().ToString();
            PropertyId = propertyId ?? throw new ArgumentNullException(nameof(propertyId));
            CreatedAt = DateTime.UtcNow;
            Rooms = rooms ?? new List<RoomPanorama>();
        }

        public void SetInspection(InspectionReport report)
        {
            Inspection = report ?? throw new ArgumentNullException(nameof(report));
        }
    }

    /// <summary>
    /// A single room panorama within a 360° virtual tour.
    /// </summary>
    public class RoomPanorama
    {
        public string RoomName { get; private set; }
        public string PanoramaUrl { get; private set; }
        public string Description { get; private set; }

        public RoomPanorama(string roomName, string panoramaUrl, string description = "")
        {
            RoomName = roomName ?? throw new ArgumentNullException(nameof(roomName));
            PanoramaUrl = panoramaUrl ?? string.Empty;
            Description = description ?? string.Empty;
        }
    }

    /// <summary>
    /// AI-generated inspection report from a virtual tour analysis.
    /// Uses Huawei Cloud AI to detect defects, assess condition, and estimate repair costs.
    /// </summary>
    public class InspectionReport
    {
        public string PropertyId { get; private set; }
        public string OverallCondition { get; private set; }
        public double ConditionScore { get; private set; }
        public List<InspectionFinding> Findings { get; private set; }
        public decimal EstimatedRepairCost { get; private set; }
        public DateTime InspectedAt { get; private set; }
        public double ConfidenceScore { get; private set; }

        public InspectionReport(
            string propertyId,
            string overallCondition,
            double conditionScore,
            List<InspectionFinding> findings,
            decimal estimatedRepairCost,
            double confidenceScore)
        {
            PropertyId = propertyId ?? throw new ArgumentNullException(nameof(propertyId));
            OverallCondition = overallCondition ?? "Unknown";
            ConditionScore = Math.Clamp(conditionScore, 0.0, 1.0);
            Findings = findings ?? new List<InspectionFinding>();
            EstimatedRepairCost = estimatedRepairCost;
            ConfidenceScore = Math.Clamp(confidenceScore, 0.0, 1.0);
            InspectedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// A single finding from an AI-powered virtual inspection.
    /// </summary>
    public class InspectionFinding
    {
        public string Room { get; private set; }
        public string Issue { get; private set; }
        public string Severity { get; private set; }
        public decimal EstimatedCost { get; private set; }

        public InspectionFinding(string room, string issue, string severity, decimal estimatedCost)
        {
            Room = room ?? string.Empty;
            Issue = issue ?? string.Empty;
            Severity = severity ?? "Low";
            EstimatedCost = estimatedCost;
        }
    }
}