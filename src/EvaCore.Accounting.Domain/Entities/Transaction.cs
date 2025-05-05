using System;

namespace EvaCore.Accounting.Domain.Entities;

public class Transaction
{
    // Primary key identifier
    public int? Id { get; set; }

    // Name of the transaction
    public string? Name { get; set; }

    // Description of the transaction
    public string? Description { get; set; }

    // Date when the transaction was created
    public DateTime? CreationDate { get; set; }

    // Date when the transaction was last modified (nullable)
    public DateTime? AlterDate { get; set; }
}
