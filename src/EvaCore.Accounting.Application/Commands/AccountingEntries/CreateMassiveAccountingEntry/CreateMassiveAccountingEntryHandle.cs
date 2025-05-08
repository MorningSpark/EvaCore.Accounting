using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;
namespace EvaCore.Accounting.Application.Commands.AccountingEntries.CreateMassiveAccountingEntry;

public class CreateMassiveAccountingEntryHandler : IRequestHandler<CreateMassiveAccountingEntryCommand, int>
{
    private readonly IAccountingEntryService _accountingEntryService;
    public CreateMassiveAccountingEntryHandler(IAccountingEntryService accountingEntryService)
    {
        _accountingEntryService = accountingEntryService;
    }

    public async Task<int> Handle(CreateMassiveAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        decimal totalValue = 0;
        foreach (var accountingEntry in request.AccountingEntries)
        {
            var accountingEntryDetails = accountingEntry.AccountingEntryDetails ?? new List<AccountingEntryDetail>();
            var entry = new AccountingEntry
            {
                Id = accountingEntry.Id,
                TransactionId = accountingEntry.TransactionId,
                Description = accountingEntry.Description,
                Type = accountingEntry.Type,
                Projection = accountingEntry.Projection,
                ReferenceValue = accountingEntry.ReferenceValue ?? accountingEntryDetails.Sum(x => x.CreditAmount),
                CreationDate = accountingEntry.Date ?? DateTime.UtcNow
            };
            await _accountingEntryService.CreateAccountingEntryAsync(entry, accountingEntryDetails, cancellationToken);
            totalValue += entry.ReferenceValue ?? 0;
        }
        return totalValue > 0 ? 1 : 0; 
    }
}