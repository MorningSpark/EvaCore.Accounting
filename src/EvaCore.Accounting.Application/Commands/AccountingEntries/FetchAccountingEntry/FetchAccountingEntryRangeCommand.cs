using System;
using EvaCore.Accounting.Domain.Entities;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.FetchAccountingEntry;

public class FetchAccountingEntryRangeCommand:IRequest<IEnumerable<AccountingEntry>>
{
    /// <summary>
    /// Initial date of the accounting entry to be resumed
    /// </summary>
    public DateTime? InitialDate { get; set; }

    /// <summary>
    /// Final date of the accounting entry to be resumed
    /// </summary>
    public DateTime? FinalDate { get; set; }

}
