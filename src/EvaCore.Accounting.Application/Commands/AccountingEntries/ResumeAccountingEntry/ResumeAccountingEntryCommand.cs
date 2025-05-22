using EvaCore.Accounting.Application.Dto.Responses;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.ResumeAccountingEntry;

public class ResumeAccountingEntryCommand:IRequest<IEnumerable<AccountingEntryResume>>
{
    /// <summary>
    /// Initial date of the accounting entry to be resumed
    /// </summary>
    public DateTime? InitialDate { get; set; }

    /// <summary>
    /// Final date of the accounting entry to be resumed
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Id of the accounting entry to be resumed
    /// </summary>
    public int? AccountingAccountId { get; set; }

    /// <summary>
    /// is projection included
    /// </summary>
    public bool? ProjectionFlag { get; set; }    
}
