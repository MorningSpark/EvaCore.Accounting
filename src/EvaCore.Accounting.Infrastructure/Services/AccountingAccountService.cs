using System;
using System.Text.RegularExpressions;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Services;

public class AccountingAccountService:IAccountingAccountService
{
    public const string LEVEL_1 = @"^[0-9]+$";                
    public const string LEVEL_2 = @"^[0-9]+\.[0-9]+$";        
    public const string LEVEL_3 = @"^[0-9]+\.[0-9]+\.[0-9]+$";
    private readonly AccountingAccountDbContext _context;
    public AccountingAccountService(AccountingAccountDbContext context)
    {
        _context = context;
    }

    public async Task<AccountingAccount> CreateAccountingAccountAsync(AccountingAccount accountingAccount, CancellationToken cancellationToken = default)
    {
        _context.AccountingAccounts.Add(accountingAccount);
        await _context.SaveChangesAsync(cancellationToken);
        return accountingAccount;
    }

    public async Task<AccountingAccount> GetAccountingAccountByIdAsync(int id, string reference, CancellationToken cancellationToken = default)
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

        if (!id.Equals(0))
            filter = filter.Where(c => c.Id.Equals(id)).ToList();
        if (!string.IsNullOrEmpty(reference))
            filter = filter.Where(c => c.Reference==reference).ToList();
        return filter.FirstOrDefault() ?? new AccountingAccount(); 
    }

    public async Task<IEnumerable<AccountingAccount>> GetCustomAccountingAccountByIdAsync(AccountingAccount accountingAccount, int level, CancellationToken cancellationToken = default)
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

        if (level == 1)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_1)).ToList();

        if (level == 2)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_2)).ToList();

        if (level == 3)
            filter = filter.Where(c => Regex.IsMatch(c.ReferenceCode??string.Empty,LEVEL_3)).ToList();

        if (!accountingAccount.Id.GetValueOrDefault().Equals(0))
            filter = filter.Where(c => c.Id.Equals(accountingAccount.Id)).ToList();

        if (!accountingAccount.ParentId.GetValueOrDefault().Equals(0))
            filter = filter.Where(c => c.ParentId.Equals(accountingAccount.ParentId)).ToList();
        
        if (!string.IsNullOrEmpty(accountingAccount.ReferenceCode))
            filter = filter.Where(c => c.ReferenceCode==accountingAccount.ReferenceCode).ToList();

        if (!string.IsNullOrEmpty(accountingAccount.Reference))
            filter = filter.Where(c => c.Reference==accountingAccount.Reference).ToList();

        if (!string.IsNullOrEmpty(accountingAccount.Name))
            filter = filter.Where(c => c.Name==accountingAccount.Name).ToList();

        return filter;
    }
    public async Task<IEnumerable<AccountingAccount>> GetAllAccountingAccountsAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.AccountingAccounts.AsQueryable();
        var accounts = await query.ToListAsync(cancellationToken);

        var filter = accounts.Select(c => new AccountingAccount
        {
            Id = c.Id,
            ParentId = c.ParentId,
            CreationDate = c.CreationDate,
            AlterDate = c.AlterDate,
            ReferenceCode = c.ReferenceCode,
            Reference = c.Reference,
            Name = c.Name,
            Resource = c.Resource
        }).ToList();

        return filter;
    }
}