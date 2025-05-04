using System;

namespace EvaCore.Accounting.Domain.Entities;

public class AccountingEntryDetail
{
    /// <summary>
    /// Identifier of the accounting entry detail
    /// </summary>
    public int? Id { get; set; } 

    /// <summary>
    /// Identifier of the accounting entry
    /// </summary>
    public int? AccountingAccountId { get; set; } 

    /// <summary>
    /// Identifier of the accounting entry
    /// </summary>
    public int? AccountingEntryId { get; set; }

    /// <summary>
    /// Credit amount of the accounting entry detail
    /// </summary>
    public decimal? CreditAmount { get; set; }

    /// <summary>
    /// Debit amount of the accounting entry detail
    /// </summary>
    public decimal? DebitAmount { get; set; }

    /// <summary>
    /// Creation date of the accounting entry detail
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Date of last modification of the accounting entry detail
    /// </summary>
    public DateTime? AlterDate { get; set; }


}
