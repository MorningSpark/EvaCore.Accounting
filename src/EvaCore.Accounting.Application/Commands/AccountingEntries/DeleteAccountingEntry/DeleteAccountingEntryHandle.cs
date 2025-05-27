using EvaCore.Accounting.Application.Commands.AccountingEntries.DeleteAccountingEntry;
using EvaCore.Accounting.Application.Commands.AccountingEntries.FetchAccountingEntry;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.ResumeAccountingEntry;

public class DeleteAccoountingEntryHandle:IRequestHandler<DeleteAccountingEntryCommand, int>
{
    private readonly IAccountingEntryService _accountingEntryService;
    public DeleteAccoountingEntryHandle(IAccountingEntryService accountingEntryService)
    {
        _accountingEntryService = accountingEntryService;
    }
    public async Task<int> Handle(DeleteAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        if (request.AccountingEntryId.Equals(0))
        {
            throw new ArgumentException("InitialDate and EndDate must have a value.");
        }
        return await _accountingEntryService.DeleteAccountingEntryAsync(request.AccountingEntryId, cancellationToken);

    }
}
