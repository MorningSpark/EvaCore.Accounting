using MediatR;
using EvaCore.Accounting.Domain.Entities;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.FetchAccountingAccount;

public class FetchAccountingAccountCommand:IRequest<List<AccountingAccount>>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string? ReferenceCode { get; set; }
    public string? Reference { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int Level { get; set; }
}
