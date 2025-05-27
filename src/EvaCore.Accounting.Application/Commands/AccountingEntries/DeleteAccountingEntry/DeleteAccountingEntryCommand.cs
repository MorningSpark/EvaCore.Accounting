using System;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.DeleteAccountingEntry;

public class DeleteAccountingEntryCommand : IRequest<int>
{
    /// <summary>
    /// Id of the accounting entry to be deleted
    /// </summary>
    public int AccountingEntryId { get; set; }

}
