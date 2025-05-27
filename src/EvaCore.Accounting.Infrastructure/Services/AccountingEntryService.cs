using System;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Utilitario;
using Microsoft.EntityFrameworkCore;

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
            foreach (var transactionDetail in _transactionDetailDbContext.AccountingTransactionDetails.Where(x => x.TransactionId == accountingEntry.TransactionId))
            {
                AccountingEntryDetail accountingEntryDetail = new AccountingEntryDetail
                {
                    AccountingEntryId = accountingEntry.Id,
                    CreditAmount = await _expresionEvaluator.Evaluate(transactionDetail.CreditFormula ?? string.Empty, accountingEntry.ReferenceValue ?? 0m),
                    DebitAmount = await _expresionEvaluator.Evaluate(transactionDetail.DebitFormula ?? string.Empty, accountingEntry.ReferenceValue ?? 0m),
                    AccountingAccountId = transactionDetail.AccountingAccountId,
                    CreationDate = accountingEntry.CreationDate
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
                        CreationDate = accountingEntry.CreationDate
                    };
                    _accountingEntryDetailDbContext.Add(accountingEntryDetail);
                }
                await _accountingEntryDetailDbContext.SaveChangesAsync(cancellationToken);
            }
        }
        return accountingEntry;
    }

    public async Task<IEnumerable<AccountingEntry>> GetAllAccountingEntryAsync(CancellationToken cancellationToken = default)
    {
        var query = _accountingEntryDbContext.AccountingEntries.AsQueryable();
        var accounts = await query.ToListAsync(cancellationToken);

        var filter = accounts.Select(c => new AccountingEntry
        {
            Id = c.Id,
            TransactionId = c.TransactionId,
            Description = c.Description,
            Breed = c.Breed,
            Projection = c.Projection,
            ReferenceValue = c.ReferenceValue ?? 0m,
            CreationDate = c.CreationDate,
            AlterDate = c.AlterDate
        }).ToList();

        return filter;
    }

    public async Task<IEnumerable<AccountingEntryDetail>> GetAccountingEntryDetailRangeAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken = default)
    {
        var query = _accountingEntryDetailDbContext.AccountingEntryDetails.AsQueryable();
        var accounts = await query.ToListAsync(cancellationToken);

        var filter = accounts.Select(c => new AccountingEntryDetail
        {
            Id = c.Id,
            AccountingEntryId = c.AccountingEntryId,
            AccountingAccountId = c.AccountingAccountId,
            CreditAmount = c.CreditAmount ?? 0m,
            DebitAmount = c.DebitAmount ?? 0m,
            CreationDate = c.CreationDate,
            AlterDate = c.AlterDate
        }).ToList();
        filter = filter.Where(x => x.CreationDate >= initialDate && x.CreationDate < finalDate.AddDays(1)).ToList();
        return filter;
    }

    public async Task<IEnumerable<AccountingEntry>> GetAllAccountingEntryRangeAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken = default)
    {
        var query = _accountingEntryDbContext.AccountingEntries.AsQueryable();
        var accounts = await query.ToListAsync(cancellationToken);

        var filter = accounts.Select(c => new AccountingEntry
        {
            Id = c.Id,
            TransactionId = c.TransactionId,
            Description = c.Description,
            Breed = c.Breed,
            Projection = c.Projection,
            ReferenceValue = c.ReferenceValue ?? 0m,
            CreationDate = c.CreationDate,
            AlterDate = c.AlterDate
        }).ToList();
        filter = filter.Where(x => x.CreationDate >= initialDate && x.CreationDate < finalDate.AddDays(1)).ToList();
        return filter;
    }

    public async Task<int> DeleteAccountingEntryAsync(int accountingEntryId, CancellationToken cancellationToken = default)
    {
        var cabecera = await _accountingEntryDbContext.AccountingEntries.Where(x => x.Id == accountingEntryId).FirstOrDefaultAsync();
        var detalles = await _accountingEntryDetailDbContext.AccountingEntryDetails.Where(x => x.AccountingEntryId == accountingEntryId).ToListAsync();

        if (cabecera != null)
        {
            _accountingEntryDbContext.AccountingEntries.Remove(cabecera);
            await _accountingEntryDbContext.SaveChangesAsync(cancellationToken);
            if (detalles.Any())
            {
                _accountingEntryDetailDbContext.AccountingEntryDetails.RemoveRange(detalles);
                await _accountingEntryDetailDbContext.SaveChangesAsync(cancellationToken);
            }
            return accountingEntryId;
        }
        return -1;
    }
}
