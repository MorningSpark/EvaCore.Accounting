using System;
using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class AccountingEntryDbContext:DbContext
{
    public AccountingEntryDbContext(DbContextOptions<AccountingEntryDbContext> options) : base(options){}
    public DbSet<AccountingEntry> AccountingEntries { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountingEntry>(entity =>
        {
            entity.ToTable("co_accounting_entry");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ae_id");
            entity.Property(e => e.TransactionId).HasColumnName("ae_transaction_id");
            entity.Property(e => e.CreationDate).HasColumnName("ae_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("ae_alter_date");
            entity.Property(e => e.Description).HasColumnName("ae_description");
            entity.Property(e => e.Projection).HasColumnName("ae_projection");
            entity.Property(e => e.ReferenceValue).HasColumnName("ae_reference_value");
            entity.Property(e => e.Breed).HasColumnName("ae_breed");

        });

        base.OnModelCreating(modelBuilder);
    }

}
