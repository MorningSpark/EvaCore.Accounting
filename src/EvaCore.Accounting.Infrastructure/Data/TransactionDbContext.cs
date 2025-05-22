using System;
using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class TransactionDbContext:DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options){}
    public DbSet<AccountingTransaction> Transactions { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountingTransaction>(entity =>
        {
            entity.ToTable("co_accounting_transaction");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("at_id");
            entity.Property(e => e.Name).HasColumnName("at_name");
            entity.Property(e => e.Description).HasColumnName("at_description");
            entity.Property(e => e.CreationDate).HasColumnName("at_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("at_alter_date");
        });

        base.OnModelCreating(modelBuilder);
    }

}
