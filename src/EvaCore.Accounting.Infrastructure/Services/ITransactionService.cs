using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Infrastructure.Services;

public interface ITransactionService
{
    Task<Transaction> CreateTransactionAsync(Transaction transaction, List<TransactionDetail> transactionDetails, CancellationToken cancellationToken = default);
}
