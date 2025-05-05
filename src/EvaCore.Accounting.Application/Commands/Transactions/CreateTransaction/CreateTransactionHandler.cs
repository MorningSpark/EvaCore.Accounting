using System;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.Transactions.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Transaction>
{
    private readonly TransactionDbContext _transactionDbContext;
    private readonly TransactionDetailDbContext _transactionDetailDbContext;
    public CreateTransactionHandler(TransactionDbContext transactionDbContext, TransactionDetailDbContext transactionDetailDbContext)
    {
        _transactionDbContext = transactionDbContext;
        _transactionDetailDbContext = transactionDetailDbContext;
    }
    public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        Transaction transaction = new Transaction
        {
            Name = request.Name,
            Description = request.Description,
            CreationDate = DateTime.UtcNow
        };

        _transactionDbContext.Add(transaction);
        await _transactionDbContext.SaveChangesAsync(cancellationToken);
        if (request.TransactionDetails != null && request.TransactionDetails.Any())
        {
            foreach (var detail in request.TransactionDetails)
            {
                TransactionDetail transactionDetail = new TransactionDetail
                {
                    TransactionId = transaction.Id,
                    AccountingAccountId = detail.AccountingAccountId,
                    DebitDistribution = detail.DebitDistribution,
                    CreditDistribution = detail.CreditDistribution,
                    CreationDate = DateTime.UtcNow
                };
                _transactionDetailDbContext.Add(transactionDetail);
            }
            await _transactionDetailDbContext.SaveChangesAsync(cancellationToken);
        }
        return transaction;
    }
}
