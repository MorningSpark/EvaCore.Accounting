using EvaCore.Accounting.Application.Dto;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.ResumeAccountingAccount;

public class ResumeAccountingAccountCommand : IRequest<IEnumerable<ResumeAccountingAccountResult>>
{
    /// <summary>
    /// Initial date of the accounting account to be resumed
    /// </summary>
    public DateTime? InitialDate { get; set; }

    /// <summary>
    /// Final date of the accounting account to be resumed
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Level of the accounting account to be resumed
    /// </summary>
    public int? Level { get; set; }    

    /// <summary>
    /// is projection included
    /// </summary>
    public bool? ProjectionFlag { get; set; }    
}
