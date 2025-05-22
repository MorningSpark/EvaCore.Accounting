using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Infrastructure.Services;

public interface IAccountingTransactionService
{
    Task<AccountingTransaction> CreateTransactionAsync(AccountingTransaction accountingTransaction, List<AccountingTransactionDetail> accountingTransactionDetails, CancellationToken cancellationToken = default);
}
