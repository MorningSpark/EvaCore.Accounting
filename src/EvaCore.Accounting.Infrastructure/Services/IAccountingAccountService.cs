using System;
using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Infrastructure.Services;

public interface IAccountingAccountService
{
    Task<AccountingAccount> CreateAccountingAccountAsync(AccountingAccount accountingAccount, CancellationToken cancellationToken = default);
    Task<AccountingAccount> GetAccountingAccountByIdAsync(int id, string reference, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingAccount>> GetCustomAccountingAccountByIdAsync(AccountingAccount accountingAccount, int level, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingAccount>> GetAllAccountingAccountsAsync(CancellationToken cancellationToken = default);
}
