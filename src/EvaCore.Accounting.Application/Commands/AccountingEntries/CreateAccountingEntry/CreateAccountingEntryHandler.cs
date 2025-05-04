using System;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;

public class CreateAccountingEntryHandler : IRequestHandler<CreateAccountingEntryCommand, AccountingEntry>
{
    private readonly AccountingEntryDbContext _accountingEntryDbContext;
    private readonly AccountingEntryDetailDbContext _accountingEntryDetailDbContext;
    public CreateAccountingEntryHandler(AccountingEntryDbContext accountingEntryDbContext, AccountingEntryDetailDbContext accountingEntryDetailDbContext)
    {
        _accountingEntryDbContext = accountingEntryDbContext;
        _accountingEntryDetailDbContext = accountingEntryDetailDbContext;
    }
    public async Task<AccountingEntry> Handle(CreateAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        AccountingEntry accountingEntry = new AccountingEntry
        {
            Id = request.Id,
            Description = request.Description,
            Type = request.Type,
            Projection = request.Projection,
            CreationDate = request.Date??DateTime.UtcNow
        };

        _accountingEntryDbContext.Add(accountingEntry);
        await _accountingEntryDbContext.SaveChangesAsync(cancellationToken);
        if (request.AccountingEntryDetails != null && request.AccountingEntryDetails.Any())
        {
            foreach (var detail in request.AccountingEntryDetails)
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
        return accountingEntry;
    }
}
