using System;
using System.Text.RegularExpressions;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.FetchAccountingAccount;

public class FetchAccountingAccountHandler : IRequestHandler<FetchAccountingAccountCommand, List<AccountingAccount>>
{
    private readonly AccountingAccountDbContext _context;
    public const string LEVEL_1 = @"^[0-9]+$";                
    public const string LEVEL_2 = @"^[0-9]+\.[0-9]+$";        
    public const string LEVEL_3 = @"^[0-9]+\.[0-9]+\.[0-9]+$";
    public FetchAccountingAccountHandler(AccountingAccountDbContext context)
    {
        _context = context;
    }
    public async Task<List<AccountingAccount>> Handle(FetchAccountingAccountCommand request, CancellationToken cancellationToken)
    {
    
        var query = _context.AccountingAccounts.AsQueryable();
        var accounts = await query.ToListAsync(cancellationToken);

        var filter = accounts.Select(c => new AccountingAccount
        { 
            Id = c.Id,
            ParentId = c.ParentId ?? 0,
            CreationDate = c.CreationDate,
            AlterDate = c.AlterDate,
            ReferenceCode = c.ReferenceCode,
            Reference = c.Reference,
            Name = c.Name,
            Resource = c.Resource
        }).ToList();

        if (request.Level == 1)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_1)).ToList();

        if (request.Level == 2)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_2)).ToList();

        if (request.Level == 3)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_3)).ToList();

        if (!request.Id.Equals(0))
            filter = filter.Where(c => c.Id.Equals(request.Id)).ToList();

        if (!request.ParentId.Equals(0))
            filter = filter.Where(c => c.ParentId.Equals(request.ParentId)).ToList();
        
        if (!string.IsNullOrEmpty(request.ReferenceCode))
            filter = filter.Where(c => c.ReferenceCode==request.ReferenceCode).ToList();

        if (!string.IsNullOrEmpty(request.Reference))
            filter = filter.Where(c => c.Reference==request.Reference).ToList();

        if (!string.IsNullOrEmpty(request.Name))
            filter = filter.Where(c => c.Name==request.Name).ToList();

        if (!string.IsNullOrEmpty(request.Type))    
            filter = filter.Where(c => c.Resource==request.Type).ToList();

        return filter;;
    }
}
