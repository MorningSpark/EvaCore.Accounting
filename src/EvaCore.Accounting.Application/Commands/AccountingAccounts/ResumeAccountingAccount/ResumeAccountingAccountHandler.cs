using System;
using EvaCore.Accounting.Application.Dto;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.ResumeAccountingAccount;

public class ResumeAccountingAccountHandler : IRequestHandler<ResumeAccountingAccountCommand, IEnumerable<ResumeAccountingAccountResult>>
{
    private readonly IAccountingAccountService _accountingAccountService;
    private readonly IAccountingEntryService _accountingEntryService;

    public ResumeAccountingAccountHandler(IAccountingAccountService accountingAccountService, IAccountingEntryService accountingEntryService)
    {
        _accountingAccountService = accountingAccountService;
        _accountingEntryService = accountingEntryService;
    }

    /// <summary>
    /// 1. first get all the accounts
    /// 2.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ResumeAccountingAccountResult>> Handle(ResumeAccountingAccountCommand request, CancellationToken cancellationToken)
    {
        List<ResumeAccountingAccountResult> _resumeAccountingAccountResults = new List<ResumeAccountingAccountResult>();

        IEnumerable<AccountingAccount> accounts = await _accountingAccountService.GetAllAccountingAccountsAsync(cancellationToken);

        if (!request.InitialDate.HasValue || !request.EndDate.HasValue)
        {
            throw new ArgumentException("InitialDate and EndDate must have a value.");
        }

        IEnumerable<AccountingEntryDetail> entries = await _accountingEntryService.GetAccountingEntryRangeAsync(request.InitialDate.Value, request.EndDate.Value, cancellationToken);

        var accountDict = accounts.ToDictionary(a => a.ReferenceCode);
        var parentChildRelations = accounts.GroupBy(a => a.ParentId)
                                           .Where(g => g.Key != null)
                                           .ToDictionary(g => g.Key, g => g.Select(a => a.ReferenceCode).ToList());

        foreach (var transaction in entries)
        {
            var account = accounts.FirstOrDefault(a => a.Id == transaction.AccountingAccountId);
            if (account != null)
            {
                account.ReferenceValue = (account.ReferenceValue ?? 0) + (transaction.DebitAmount ?? 0) - (transaction.CreditAmount ?? 0);
            }
        }


        CalculateValues(accountDict, parentChildRelations);
        DisplayAccounts(accountDict, parentChildRelations, accounts.Where(a => a.ParentId == null), 0);


        return _resumeAccountingAccountResults;
    }

    static void CalculateValues(Dictionary<string, AccountingAccount> accountDict, Dictionary<int?, List<string>> relations)
    {
        var orderedAccounts = accountDict.Values.OrderByDescending(a => a.ReferenceCode.Length).ToList();

        foreach (var account in orderedAccounts)
        {
            if (relations.ContainsKey(account.Id))
            {
                foreach (var childCode in relations[account.Id])
                {
                    if (accountDict.ContainsKey(childCode))
                    {
                        account.ReferenceValue = (account.ReferenceValue ?? 0) + (accountDict[childCode].ReferenceValue ?? 0);
                    }
                }
            }
        }
    }


    static void DisplayAccounts(Dictionary<string, AccountingAccount> accountDict, Dictionary<int?, List<string>> relations, IEnumerable<AccountingAccount> accounts, int level)
    {
        foreach (var account in accounts.OrderBy(a => a.ReferenceCode))
        {
            Console.WriteLine($"{new string(' ', level * 4)}{account.ReferenceCode} - {account.Name}: {account.ReferenceValue:C}");
            if (relations.ContainsKey(account.Id))
            {
                DisplayAccounts(accountDict, relations, relations[account.Id].Select(code => accountDict[code]), level + 1);
            }
        }
    }
}
