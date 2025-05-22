using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;

namespace EvaCore.Accounting.Infrastructure.Services;

public class AccountingTransactionService : IAccountingTransactionService
{
    private readonly TransactionDbContext _transactionDbContext;
    private readonly TransactionDetailDbContext _transactionDetailDbContext;

    public AccountingTransactionService(TransactionDbContext transactionDbContext, TransactionDetailDbContext transactionDetailDbContext)
    {
        _transactionDbContext = transactionDbContext;
        _transactionDetailDbContext = transactionDetailDbContext;
    }
    public async Task<AccountingTransaction> CreateTransactionAsync(AccountingTransaction transaction, List<AccountingTransactionDetail> transactionDetails, CancellationToken cancellationToken = default)
    {
        _transactionDbContext.Add(transaction);
        await _transactionDbContext.SaveChangesAsync(cancellationToken);
        if (transactionDetails != null && transactionDetails.Any())
        {
            foreach (var detail in transactionDetails)
            {
                AccountingTransactionDetail transactionDetail = new AccountingTransactionDetail
                {
                    TransactionId = transaction.Id,
                    AccountingAccountId = detail.AccountingAccountId,
                    DebitFormula = detail.DebitFormula,
                    CreditFormula = detail.CreditFormula,
                    CreationDate = DateTime.UtcNow
                };
                _transactionDetailDbContext.Add(transactionDetail);
            }
            await _transactionDetailDbContext.SaveChangesAsync(cancellationToken);
        }
        return transaction;
    }
}
