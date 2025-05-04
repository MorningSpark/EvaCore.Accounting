using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.CreateAccountingAccount;

public class CreateAccountingAccountHandler : IRequestHandler<CreateAccountingAccountCommand, int>
{
    private readonly AccountingAccountDbContext _context;

    public CreateAccountingAccountHandler(AccountingAccountDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateAccountingAccountCommand request, CancellationToken cancellationToken)
    {
        AccountingAccount account = new AccountingAccount
        {
            ParentId = request.ParentId,
            ReferenceCode = request.ReferenceCode,
            Reference = request.Reference,
            Name = request.Name,
            Resource = request.Resource,
            CreationDate = DateTime.UtcNow
        };
        _context.AccountingAccounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);
        return account.Id ?? throw new InvalidOperationException("Account ID cannot be null.");
    }
}
