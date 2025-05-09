using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Infrastructure.Services;

public interface IAccountingEntryService
{
    Task<AccountingEntry> CreateAccountingEntryAsync(AccountingEntry accountingEntry, List<AccountingEntryDetail> accountingEntryDetails, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingEntry>> GetAllAccountingEntriesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingEntryDetail>> GetAccountingEntryRangeAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken = default);
}
