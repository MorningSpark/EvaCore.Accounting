using EvaCore.Accounting.Infrastructure.Data;
using EvaCore.Accounting.Infrastructure.Services;
using EvaCore.Accounting.Infrastructure.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EvaCore.Accounting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AccountingAccountDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<AccountingEntryDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<AccountingEntryDetailDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<TransactionDetailDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(connectionString));
        return services;
    }

    public static IServiceCollection AddUtils(this IServiceCollection services)
    {
        services.AddScoped<IExpresionEvaluator, ExpresionEvaluator>();
        services.AddScoped<IAccountingEntryService, AccountingEntryService>();
        services.AddScoped<IAccountingAccountService, AccountingAccountService>();
        services.AddScoped<IAccountingTransactionService, AccountingTransactionService>();
        return services;
    }
}
