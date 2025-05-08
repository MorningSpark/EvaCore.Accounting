using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.FetchAccountingAccount;

public class FetchAccountingAccountHandler : IRequestHandler<FetchAccountingAccountCommand, List<AccountingAccount>>
{
    private readonly IAccountingAccountService _accountingAccountService;
    public FetchAccountingAccountHandler(IAccountingAccountService accountingAccountService)
    {
        _accountingAccountService = accountingAccountService;
    }
    
    public async Task<List<AccountingAccount>> Handle(FetchAccountingAccountCommand request, CancellationToken cancellationToken)
    {
        AccountingAccount account = new AccountingAccount
        {
            ParentId = request.ParentId,
            ReferenceCode = request.ReferenceCode,
            Reference = request.Reference,
            Name = request.Name,
            CreationDate = DateTime.UtcNow
        };

        return (await _accountingAccountService.GetCustomAccountingAccountByIdAsync(account, request.Level, cancellationToken)).ToList();
    }
}
