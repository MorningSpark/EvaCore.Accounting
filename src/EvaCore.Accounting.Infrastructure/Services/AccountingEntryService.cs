using System;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Utilitario;

namespace EvaCore.Accounting.Infrastructure.Services;

public class AccountingEntryService : IAccountingEntryService
{
    private readonly AccountingEntryDbContext _accountingEntryDbContext;
    private readonly AccountingEntryDetailDbContext _accountingEntryDetailDbContext;

    private readonly TransactionDetailDbContext _transactionDetailDbContext;
    public AccountingEntryService(AccountingEntryDbContext accountingEntryDbContext, AccountingEntryDetailDbContext accountingEntryDetailDbContext, TransactionDetailDbContext transactionDetailDbContext, IExpresionEvaluator expresionEvaluator)
    {
        _expresionEvaluator = expresionEvaluator;
        _transactionDetailDbContext = transactionDetailDbContext;
        _accountingEntryDbContext = accountingEntryDbContext;
        _accountingEntryDetailDbContext = accountingEntryDetailDbContext;
    }

    private readonly IExpresionEvaluator _expresionEvaluator;
    public async Task<AccountingEntry> CreateAccountingEntryAsync(AccountingEntry accountingEntry, List<AccountingEntryDetail> accountingEntryDetails, CancellationToken cancellationToken = default)
    {
        _accountingEntryDbContext.Add(accountingEntry);
        await _accountingEntryDbContext.SaveChangesAsync(cancellationToken);

        if (!(accountingEntry.TransactionId is null))
        {
            foreach (var transactionDetail in _transactionDetailDbContext.TransactionDetails.Where(x => x.TransactionId == accountingEntry.TransactionId))
            {
                AccountingEntryDetail accountingEntryDetail = new AccountingEntryDetail
                {
                    AccountingEntryId = accountingEntry.Id,
                    CreditAmount = await _expresionEvaluator.Evaluate(transactionDetail.CreditFormula ?? string.Empty, accountingEntry.ReferenceValue ?? 0m),
                    DebitAmount = await _expresionEvaluator.Evaluate(transactionDetail.DebitFormula ?? string.Empty, accountingEntry.ReferenceValue ?? 0m),
                    AccountingAccountId = transactionDetail.AccountingAccountId,
                    CreationDate = DateTime.UtcNow
                };
                _accountingEntryDetailDbContext.Add(accountingEntryDetail);
            }
            await _accountingEntryDetailDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            if (accountingEntryDetails?.Any() == true)
            {
                foreach (var detail in accountingEntryDetails)
                {
                    AccountingEntryDetail accountingEntryDetail = new AccountingEntryDetail
                    {
                        AccountingEntryId = accountingEntry.Id,
                        CreditAmount = detail.CreditAmount,
                        DebitAmount = detail.DebitAmount,
                        AccountingAccountId = detail.AccountingAccountId,
                        CreationDate = DateTime.UtcNow
                    };
                    _accountingEntryDetailDbContext.Add(accountingEntryDetail);
                }
                await _accountingEntryDetailDbContext.SaveChangesAsync(cancellationToken);
            }
        }
        return accountingEntry;
    }
}
