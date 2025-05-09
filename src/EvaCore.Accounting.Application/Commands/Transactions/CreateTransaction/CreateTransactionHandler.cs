using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.Transactions.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Transaction>
{
    private readonly ITransactionService _transactionService;

    public CreateTransactionHandler(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        Transaction transaction = new Transaction
        {
            Name = request.Name,
            Description = request.Description,
            CreationDate = DateTime.UtcNow
        };

        return await _transactionService.CreateTransactionAsync(transaction, request.TransactionDetails ?? new List<TransactionDetail>(), cancellationToken);
    }
}
