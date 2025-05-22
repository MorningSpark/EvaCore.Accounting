using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.CreateAccountingAccount;

public class CreateAccountingAccountHandler : IRequestHandler<CreateAccountingAccountCommand, int>
{
    readonly IAccountingAccountService _accountingAccountService;
    public CreateAccountingAccountHandler(IAccountingAccountService accountingAccountService)
    {
        _accountingAccountService = accountingAccountService;
    }

    public async Task<int> Handle(CreateAccountingAccountCommand request, CancellationToken cancellationToken)
    {
        AccountingAccount account = new AccountingAccount
        {
            ParentId = request.ParentId,
            ReferenceCode = request.ReferenceCode,
            UserId = request.UserId,
            Reference = request.Reference,
            Name = request.Name,
            Transaction = request.Transaction,
            Resource = request.Resource,
            CreationDate = DateTime.UtcNow
        };
        return (await _accountingAccountService.CreateAccountingAccountAsync(account)).Id ?? 0;
    }
}
