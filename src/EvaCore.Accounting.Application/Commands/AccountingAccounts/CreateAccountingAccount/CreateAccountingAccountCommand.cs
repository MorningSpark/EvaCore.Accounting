using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.CreateAccountingAccount;

public class CreateAccountingAccountCommand:IRequest<int>
{
    public int ParentId { get; set; }
    public string? ReferenceCode { get; set; }
    public string? Reference { get; set; }
    public string? Name { get; set; }
    public string? Resource { get; set; }
}

