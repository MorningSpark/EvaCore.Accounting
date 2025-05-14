using System;

namespace EvaCore.Accounting.Application.Dto;

public class AccountingEntryResume
{
    /// <summary>
    /// Accounting entry detail identifier
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Transaction date
    /// </summary>
    public DateTime? TransactionDate { get; set; }

    /// <summary>
    /// Identifier of the accounting account
    /// </summary>
    public string? Description { get; set; } 

    /// <summary>
    /// Balance of the accounting entry
    /// </summary>
    public decimal? Saldo { get; set; }
}
