using System;
using EvaCore.Accounting.Application.Dto;
using EvaCore.Accounting.Domain.Entities;
using EvaCore.Accounting.Infrastructure.Services;
using MediatR;

namespace EvaCore.Accounting.Application.Commands.AccountingEntries.ResumeAccountingEntry;

public class ResumeAccountingEntryHandle:IRequestHandler<ResumeAccountingEntryCommand, IEnumerable<AccountingEntryResume>>
{
    private readonly IAccountingEntryService _accountingEntryService;
    private readonly IAccountingAccountService _accountingAccountService;
    public ResumeAccountingEntryHandle(IAccountingEntryService accountingEntryService, IAccountingAccountService accountingAccountService)
    {
        _accountingEntryService = accountingEntryService;
        _accountingAccountService = accountingAccountService;
    }
    public async Task<IEnumerable<AccountingEntryResume>> Handle(ResumeAccountingEntryCommand request, CancellationToken cancellationToken)
    {
        if (!request.InitialDate.HasValue || !request.EndDate.HasValue)
        {
            throw new ArgumentException("InitialDate and EndDate must have a value.");
        }
        List<AccountingEntry> accountingEntries = (await _accountingEntryService.GetAllAccountingEntryAsync(cancellationToken)).ToList();
        List<AccountingEntryDetail> accountingEntryDetails = (await _accountingEntryService.GetAccountingEntryDetailRangeAsync(request.InitialDate.Value, request.EndDate.Value, cancellationToken)).ToList();
        List<AccountingAccount> accountingAccounts = (await _accountingAccountService.GetAllAccountingAccountsAsync(cancellationToken)).ToList();

        bool mostrarTodo = request.ProjectionFlag ?? false;
        List<AccountingEntryResume> resultado = (from accountingEntryDetail in accountingEntryDetails
                join accountEntry in accountingEntries on accountingEntryDetail.AccountingEntryId equals accountEntry.Id  
                join accountingAccount in accountingAccounts on accountingEntryDetail.AccountingAccountId equals accountingAccount.Id
                where accountingAccount.Id == request.AccountingAccountId && (mostrarTodo || accountEntry.Projection == false)
                select new AccountingEntryResume
                {
                    Id = accountingEntryDetail.Id,
                    Description = accountEntry.Description,
                    Saldo = accountingEntryDetail.DebitAmount - accountingEntryDetail.CreditAmount,
                    TransactionDate= accountingEntryDetail.CreationDate
                }).ToList();
                
        return resultado;
    }
}
