namespace EvaCore.Accounting.Domain.Entities;

public class AccountingTransactionDetail
{
    /// <summary>
    /// Primary key identifier
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Reference to the related transaction
    /// </summary>
    public int? TransactionId { get; set; }

    /// <summary>
    /// Reference to the related accounting account
    /// </summary>
    public int? AccountingAccountId { get; set; }

    /// <summary>
    /// Debit distribution amount formula
    /// </summary>
    public string? DebitFormula { get; set; }

    /// <summary>
    /// Credit distribution amount formula
    /// </summary>
    public string? CreditFormula { get; set; }

    /// <summary>
    ///  Date when the transaction detail was created
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Date when the transaction detail was last modified (nullable)
    /// </summary>
    public DateTime? AlterDate { get; set; }
}
