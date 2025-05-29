using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Infrastructure.Services;

public interface IAccountingEntryService
{
    Task<AccountingEntry> CreateAccountingEntryAsync(AccountingEntry accountingEntry, List<AccountingEntryDetail> accountingEntryDetails, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingEntry>> GetAllAccountingEntryAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingEntry>> GetAllAccountingEntryRangeAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken = default);

    Task<IEnumerable<AccountingEntry>> GetAllAccountingEntryRangeDefaultAsync(CancellationToken cancellationToken = default);
    Task<int> DeleteAccountingEntryAsync(int accountingEntryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AccountingEntryDetail>> GetAccountingEntryDetailRangeAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken = default);
}
