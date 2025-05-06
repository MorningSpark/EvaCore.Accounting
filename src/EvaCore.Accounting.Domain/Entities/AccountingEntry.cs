using System;

namespace EvaCore.Accounting.Domain.Entities;

public class AccountingEntry
{
    /// <summary>
    /// Identifier of the accounting entry
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Identifier of the transaction
    /// </summary>
    public int? TransactionId { get; set; }

    /// <summary>
    /// Identifier of the accounting account
    /// </summary>
    public string? Description { get; set; } 

    /// <summary>
    /// Identifier of the accounting account
    /// </summary>
    public int? Type { get; set; } 

    /// <summary>
    /// Identifier of the projection
    /// </summary>
    public bool? Projection { get; set; } 

    /// <summary>
    /// Total value of the accounting entry
    /// </summary>
    public decimal? ValorTotal { get; set; }

    /// <summary>
    /// Creation date of the accounting entry
    /// </summary>
    public DateTime? CreationDate { get; set; } 

    /// <summary>
    /// Date of last modification of the accounting entry
    /// </summary>
    public DateTime? AlterDate { get; set; } 

}
