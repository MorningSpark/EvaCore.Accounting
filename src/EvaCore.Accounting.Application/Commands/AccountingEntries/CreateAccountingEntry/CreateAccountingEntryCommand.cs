using System;
using EvaCore.Accounting.Domain.Entities;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.CreateAccountingEntry;

public class CreateAccountingEntryCommand:IRequest<AccountingEntry>
{
    public CreateAccountingEntryCommand()
    {
        AccountingEntryDetails = new List<AccountingEntryDetail>();
    }
    /// <summary>
    /// Identifier of the accounting entry
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Identifier of the accounting account
    /// </summary>
    public string? Description { get; set; } 

    /// <summary>
    /// Identifier of the type entry
    /// </summary>
    public int? Type { get; set; } 

    /// <summary>
    /// Transaction Date
    /// </summary>
    public DateTime? Date { get; set; } 
    
    /// <summary>
    /// Details Accounting entry
    /// </summary>
    public List<AccountingEntryDetail>? AccountingEntryDetails { get; set; }

}
