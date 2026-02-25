using System;
using System.Collections.Generic;
using System.Linq;

namespace PropTechPrototype.Models
{
    /// <summary>
    /// Payment status for an invoice.
    /// </summary>
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Overdue,
        PartiallyPaid,
        Cancelled
    }

    /// <summary>
    /// A single line item on an invoice (e.g. monthly rent, water, electricity, admin fee).
    /// </summary>
    public class LineItem
    {
        public string Description { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total => UnitPrice * Quantity;

        public LineItem(string description, decimal unitPrice, int quantity = 1)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            UnitPrice = unitPrice;
            Quantity = quantity > 0 ? quantity : 1;
        }
    }

    /// <summary>
    /// Abstract base for financial transactions (Invoice and Statement both extend this).
    /// </summary>
    public abstract class FinancialTransaction
    {
        public string Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Amount { get; protected set; }

        protected FinancialTransaction(string id, DateTime date)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Date = date;
        }

        /// <summary>Calculates the total amount for this transaction.</summary>
        public abstract decimal CalculateTotal();
    }

    /// <summary>
    /// A monthly rental invoice issued to a tenant against a lease.
    /// Extends FinancialTransaction; contains one or more line items.
    /// </summary>
    public class Invoice : FinancialTransaction
    {
        public string LeaseId { get; private set; }
        public string TenantId { get; private set; }
        public string TenantName { get; private set; }
        public string PropertyAddress { get; private set; }
        public List<LineItem> Items { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? PaidDate { get; private set; }

        public Invoice(
            string id,
            string leaseId,
            string tenantId,
            string tenantName,
            string propertyAddress,
            List<LineItem> items,
            DateTime date,
            DateTime dueDate)
            : base(id, date)
        {
            LeaseId = leaseId ?? throw new ArgumentNullException(nameof(leaseId));
            TenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            TenantName = tenantName ?? string.Empty;
            PropertyAddress = propertyAddress ?? string.Empty;
            Items = items ?? new List<LineItem>();
            Status = PaymentStatus.Pending;
            DueDate = dueDate;
            Amount = CalculateTotal();
        }

        /// <summary>Returns the sum of all line item totals.</summary>
        public override decimal CalculateTotal() => Items.Sum(i => i.Total);

        /// <summary>Marks the invoice as paid on the specified date.</summary>
        public void MarkAsPaid(DateTime paidDate)
        {
            Status = PaymentStatus.Paid;
            PaidDate = paidDate;
        }

        /// <summary>Marks the invoice as overdue.</summary>
        public void MarkAsOverdue()
        {
            if (Status == PaymentStatus.Pending)
                Status = PaymentStatus.Overdue;
        }

        /// <summary>Cancels the invoice.</summary>
        public void Cancel()
        {
            Status = PaymentStatus.Cancelled;
        }
    }

    /// <summary>
    /// A tenant account statement summarising all invoices over a period.
    /// Extends FinancialTransaction; calculates running balance from invoices.
    /// </summary>
    public class Statement : FinancialTransaction
    {
        public string TenantId { get; private set; }
        public string TenantName { get; private set; }
        public DateTime PeriodStart { get; private set; }
        public DateTime PeriodEnd { get; private set; }
        public List<Invoice> Invoices { get; private set; }
        public decimal OpeningBalance { get; private set; }

        public decimal TotalBilled => Invoices.Sum(inv => inv.CalculateTotal());
        public decimal TotalPaid => Invoices
            .Where(inv => inv.Status == PaymentStatus.Paid)
            .Sum(inv => inv.CalculateTotal());
        public decimal ClosingBalance => OpeningBalance + TotalBilled - TotalPaid;

        public Statement(
            string id,
            string tenantId,
            string tenantName,
            DateTime periodStart,
            DateTime periodEnd,
            List<Invoice>? invoices = null,
            decimal openingBalance = 0m)
            : base(id, periodStart)
        {
            TenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            TenantName = tenantName ?? string.Empty;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            Invoices = invoices ?? new List<Invoice>();
            OpeningBalance = openingBalance;
            Amount = CalculateTotal();
        }

        /// <summary>Returns the closing balance (amount outstanding).</summary>
        public override decimal CalculateTotal() => ClosingBalance;

        /// <summary>Calculates the balance due (alias for ClosingBalance).</summary>
        public decimal CalculateBalance() => ClosingBalance;
    }
}
