using EvaCore.Accounting.Application.Commands.AccountingEntries.FetchAccountingEntry;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.ResumeAccountingEntry;

public class FetchAccountingEntryRangeHandle:IRequestHandler<FetchAccountingEntryRangeCommand, IEnumerable<AccountingEntry>>
{
    private readonly IAccountingEntryService _accountingEntryService;
    public FetchAccountingEntryRangeHandle(IAccountingEntryService accountingEntryService)
    {
        _accountingEntryService = accountingEntryService;
    }
    public async Task<IEnumerable<AccountingEntry>> Handle(FetchAccountingEntryRangeCommand request, CancellationToken cancellationToken)
    {
        if (!request.InitialDate.HasValue || !request.EndDate.HasValue)
        {
            throw new ArgumentException("InitialDate and EndDate must have a value.");
        }
        return await _accountingEntryService.GetAllAccountingEntryRangeAsync(request.InitialDate.Value, request.EndDate.Value, cancellationToken);

    }
}
