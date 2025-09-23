using EvaCore.Accounting.Application.Dto.Responses;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.ResumeAccountingAccount;

public class ResumeAccountingAccountCommand : IRequest<IEnumerable<AccountingAccountInfo>>
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

    /// <summary>
    /// configuration set
    /// </summary>
    public int? Configuration { get; set; }

    /// <summary>
    /// configuration sexclude
    /// </summary>
    public int? ConfigurationExclude { get; set; }

    /// <summary>
    /// Absolute balance
    /// </summary>
    public bool? AbsoluteBalance { get; set; }    
    
}
