using System;
using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class TransactionDetailDbContext:DbContext
{
    public TransactionDetailDbContext(DbContextOptions<TransactionDetailDbContext> options) : base(options){}
    public DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionDetail>(entity =>
        {
            entity.ToTable("co_transaction_detail");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("td_id");
            entity.Property(e => e.TransactionId).HasColumnName("td_transaction_id");
            entity.Property(e => e.AccountingAccountId).HasColumnName("td_accounting_account_id");
            entity.Property(e => e.CreditFormula).HasColumnName("td_credit_formula");
            entity.Property(e => e.DebitFormula).HasColumnName("td_debit_formula");
            entity.Property(e => e.CreationDate).HasColumnName("td_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("td_alter_date");
            
        });

        base.OnModelCreating(modelBuilder);
    }

}
