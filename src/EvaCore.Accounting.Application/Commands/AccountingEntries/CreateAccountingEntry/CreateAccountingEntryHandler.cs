using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Services;
using EvaCore.Accounting.Infrastructure.Utilitario;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;

public class CreateAccountingEntryHandler : IRequestHandler<CreateAccountingEntryCommand, AccountingEntry>
{
    private readonly IAccountingEntryService _accountingEntryService;
    
    public CreateAccountingEntryHandler(AccountingEntryDbContext accountingEntryDbContext, AccountingEntryDetailDbContext accountingEntryDetailDbContext, TransactionDetailDbContext transactionDetailDbContext, IExpresionEvaluator expresionEvaluator, IAccountingEntryService accountingEntryService)
    {
        _accountingEntryService = accountingEntryService;
    }
    public async Task<AccountingEntry> Handle(CreateAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        AccountingEntry accountingEntry = new AccountingEntry
        {
            Id = request.Id,
            TransactionId = request.TransactionId,
            Description = request.Description,
            Breed = request.Breed,
            Projection = request.Projection,
            ReferenceValue = request.ReferenceValue ?? request.AccountingEntryDetails?.Sum(x => x.CreditAmount),
            CreationDate = request.Date ?? new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second)
        };
        return await _accountingEntryService.CreateAccountingEntryAsync(accountingEntry, request.AccountingEntryDetails ?? new List<AccountingEntryDetail>(), cancellationToken);
    }
}
