using System;

namespace EvaCore.Accounting.Domain.Entities;

public class TransactionDetail
{
    // Primary key identifier
    public int? Id { get; set; }

    // Reference to the related transaction
    public int? TransactionId { get; set; }

    // Reference to the related accounting account
    public int? AccountingAccountId { get; set; }

    // Debit distribution amount formula
    public string? DebitFormula { get; set; }

    // Credit distribution amount formula
    public string? CreditFormula { get; set; }

    // Date when the transaction detail was created
    public DateTime? CreationDate { get; set; }

    // Date when the transaction detail was last modified (nullable)
    public DateTime? AlterDate { get; set; }
}
