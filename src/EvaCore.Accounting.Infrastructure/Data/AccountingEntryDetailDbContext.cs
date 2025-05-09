using System;
using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class AccountingEntryDetailDbContext:DbContext
{
    public AccountingEntryDetailDbContext(DbContextOptions<AccountingEntryDetailDbContext> options) : base(options){}
    public DbSet<AccountingEntryDetail> AccountingEntryDetails { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountingEntryDetail>(entity =>
        {
            entity.ToTable("co_accounting_entry_detail");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ed_id");
            entity.Property(e => e.AccountingEntryId).HasColumnName("ed_accounting_entry_id");
            entity.Property(e => e.AccountingAccountId).HasColumnName("ed_accounting_account_id");
            entity.Property(e => e.CreationDate).HasColumnName("ed_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("ed_alter_date");
            entity.Property(e => e.CreditAmount).HasColumnName("ed_credit_amount");
            entity.Property(e => e.DebitAmount).HasColumnName("ed_debit_amount");
            
        });

        base.OnModelCreating(modelBuilder);
    }

}
