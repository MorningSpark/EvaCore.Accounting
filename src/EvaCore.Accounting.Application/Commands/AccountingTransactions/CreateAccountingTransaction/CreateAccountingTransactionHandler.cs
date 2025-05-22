using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingTransactions.CreateAccountingTransaction;

public class CreateAccountingTransactionHandler : IRequestHandler<CreateAccountingTransactionCommand, AccountingTransaction>
{
    private readonly IAccountingTransactionService _transactionService;

    public CreateAccountingTransactionHandler(IAccountingTransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    public async Task<AccountingTransaction> Handle(CreateAccountingTransactionCommand request, CancellationToken cancellationToken)
    {
        AccountingTransaction transaction = new AccountingTransaction
        {
            Name = request.Name,
            Description = request.Description,
            CreationDate = DateTime.UtcNow
        };

        return await _transactionService.CreateTransactionAsync(transaction, request.TransactionDetails ?? new List<AccountingTransactionDetail>(), cancellationToken);
    }
}
