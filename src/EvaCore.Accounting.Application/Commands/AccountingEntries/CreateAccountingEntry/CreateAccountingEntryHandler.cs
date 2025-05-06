using System;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Utilitario;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;

public class CreateAccountingEntryHandler : IRequestHandler<CreateAccountingEntryCommand, AccountingEntry>
{
    private readonly AccountingEntryDbContext _accountingEntryDbContext;
    private readonly AccountingEntryDetailDbContext _accountingEntryDetailDbContext;
    private readonly IExpresionEvaluator _expresionEvaluator;

    private readonly TransactionDetailDbContext _transactionDetailDbContext;
    public CreateAccountingEntryHandler(AccountingEntryDbContext accountingEntryDbContext, AccountingEntryDetailDbContext accountingEntryDetailDbContext, TransactionDetailDbContext transactionDetailDbContext, IExpresionEvaluator expresionEvaluator)
    {
        _expresionEvaluator = expresionEvaluator;
        _transactionDetailDbContext = transactionDetailDbContext;
        _accountingEntryDbContext = accountingEntryDbContext;
        _accountingEntryDetailDbContext = accountingEntryDetailDbContext;
    }
    public async Task<AccountingEntry> Handle(CreateAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        AccountingEntry accountingEntry = new AccountingEntry
        {
            Id = request.Id,
            TransactionId = request.TransactionId,
            Description = request.Description,
            Type = request.Type,
            Projection = request.Projection,
            ValorTotal = request.ValorTotal ?? request.AccountingEntryDetails?.Sum(x => x.CreditAmount),
            CreationDate = request.Date ?? DateTime.UtcNow
        };
        _accountingEntryDbContext.Add(accountingEntry);
        await _accountingEntryDbContext.SaveChangesAsync(cancellationToken);

        if (!(request.TransactionId is null))
        {
            foreach (var transactionDetail in _transactionDetailDbContext.TransactionDetails.Where(x => x.TransactionId == request.TransactionId))
            {
                AccountingEntryDetail accountingEntryDetail = new AccountingEntryDetail
                {
                    AccountingEntryId = accountingEntry.Id,
                    CreditAmount = await _expresionEvaluator.Evaluate(transactionDetail.CreditFormula ?? string.Empty, accountingEntry.ValorTotal ?? 0m),
                    DebitAmount = await _expresionEvaluator.Evaluate(transactionDetail.DebitFormula ?? string.Empty, accountingEntry.ValorTotal ?? 0m),
                    AccountingAccountId = transactionDetail.AccountingAccountId,
                    CreationDate = DateTime.UtcNow
                };
                _accountingEntryDetailDbContext.Add(accountingEntryDetail);
            }
            await _accountingEntryDetailDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            if (request.AccountingEntryDetails?.Any() == true)
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
        }
        return accountingEntry;
    }
}
