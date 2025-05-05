using System;
using EvaCore.Accounting.Domain.Entities;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.Transactions.CreateTransaction;

public class CreateTransactionCommand:IRequest<Transaction>
{
    public CreateTransactionCommand()
    {
        TransactionDetails = new List<TransactionDetail>();
    }
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
    
    /// <summary>
    /// Details of transactions
    /// </summary>
    public List<TransactionDetail>? TransactionDetails { get; set; }

}
