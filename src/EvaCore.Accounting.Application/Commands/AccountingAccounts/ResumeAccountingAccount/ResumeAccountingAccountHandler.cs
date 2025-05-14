using EvaCore.Accounting.Application.Dto;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingAccounts.ResumeAccountingAccount;

/// <summary>
/// handler for the ResumeAccountingAccountCommand.
/// </summary>
public class ResumeAccountingAccountHandler : IRequestHandler<ResumeAccountingAccountCommand, IEnumerable<AccountingAccountInfo>>
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
    /// 2. get all the entries in the range
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AccountingAccountInfo>> Handle(ResumeAccountingAccountCommand request, CancellationToken cancellationToken)
    {
        List<AccountingAccountInfo> _resumeAccountingAccountResults = new List<AccountingAccountInfo>();

        IEnumerable<AccountingAccount> accounts = await _accountingAccountService.GetAllAccountingAccountsAsync(cancellationToken);

        if (!request.InitialDate.HasValue || !request.EndDate.HasValue)
        {
            throw new ArgumentException("InitialDate and EndDate must have a value.");
        }

        IEnumerable<AccountingEntry> entryHeader = await _accountingEntryService.GetAllAccountingEntryAsync(cancellationToken);
        
        IEnumerable<AccountingEntryDetail> entries = !(request.ProjectionFlag ?? false)?
        (await _accountingEntryService.GetAccountingEntryDetailRangeAsync(request.InitialDate.Value, request.EndDate.Value, cancellationToken)).Where(d => entryHeader.Any(f => f.Id == d.AccountingEntryId && f.Projection == false)).ToList():
        (await _accountingEntryService.GetAccountingEntryDetailRangeAsync(request.InitialDate.Value, request.EndDate.Value, cancellationToken));

        var accountDict = accounts.Where(a => a.ReferenceCode != null)
                                   .ToDictionary(a => a.ReferenceCode!);
        var parentChildRelations = accounts.GroupBy(a => a.ParentId)
                                           .Where(g => g.Key.HasValue)
                                           .ToDictionary(g => g.Key!.Value, g => g.Select(a => a.ReferenceCode!).ToList());

        foreach (var transaction in entries)
        {
            var account = accounts.FirstOrDefault(a => a.Id == transaction.AccountingAccountId);
            if (account != null)
            {
                account.ReferenceValue = (account.ReferenceValue ?? 0) + (transaction.DebitAmount ?? 0) - (transaction.CreditAmount ?? 0);
            }
        }


        CalculateValues(accountDict, parentChildRelations);
        ProcessAccount(accountDict, parentChildRelations, accounts.Where(a => a.ParentId == null), _resumeAccountingAccountResults);


        return _resumeAccountingAccountResults;
    }

    /// <summary>
    /// Calculate the values for each account based on its children.
    /// </summary>
    /// <param name="accountDict"></param>
    /// <param name="relations"></param>
    private static void CalculateValues(Dictionary<string, AccountingAccount> accountDict, Dictionary<int, List<string>> relations)
    {
        var orderedAccounts = accountDict.Values.OrderByDescending(a => a.ReferenceCode?.Length ?? 0).ToList();

        foreach (var account in orderedAccounts)
        {
            if (account.Id.HasValue && relations.TryGetValue(account.Id.Value, out var childCodes))
            {
                foreach (var childCode in childCodes.Where(code => accountDict.ContainsKey(code)))
                {
                    account.ReferenceValue = (account.ReferenceValue ?? 0) + (accountDict[childCode].ReferenceValue ?? 0);
                }
            }
        }
    }

    /// <summary>
    /// Recursively process the accounts and their children to calculate the values.
    /// </summary>
    /// <param name="accountDict"></param>
    /// <param name="relations"></param>
    /// <param name="accounts"></param>
    /// <param name="resumeAccountingAccountResults"></param>
    private static void ProcessAccount(Dictionary<string, AccountingAccount> accountDict, Dictionary<int, List<string>> relations, IEnumerable<AccountingAccount> accounts, List<AccountingAccountInfo> resumeAccountingAccountResults)
    {
        foreach (var account in accounts.OrderBy(a => a.ReferenceCode))
        {
            AccountingAccountInfo result = new AccountingAccountInfo
            {
                Id = account.Id,
                ReferenceCode = account.ReferenceCode,
                Name = account.Name,
                Balance = Math.Abs(account.ReferenceValue?? 0) ,
            };
            resumeAccountingAccountResults.Add(result);
            if (account.Id.HasValue && relations.ContainsKey(account.Id.Value))
            {
                ProcessAccount(accountDict, relations, relations[account.Id.Value].Select(code => accountDict[code]), resumeAccountingAccountResults);
            }
        }
    }
}
