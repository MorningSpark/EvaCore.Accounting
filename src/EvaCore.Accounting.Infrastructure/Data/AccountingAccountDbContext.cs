using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class AccountingAccountDbContext : DbContext
{
    public AccountingAccountDbContext(DbContextOptions<AccountingAccountDbContext> options) : base(options) { }
    public DbSet<AccountingAccount> AccountingAccounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountingAccount>(entity =>
        {
            entity.ToTable("co_accounting_account");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("aa_id");
            entity.Property(e => e.ParentId).HasColumnName("aa_parent_id");
            entity.Property(e => e.UserId).HasColumnName("aa_user_id");
            entity.Property(e => e.CreationDate).HasColumnName("aa_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("aa_alter_date");
            entity.Property(e => e.ReferenceCode).HasColumnName("aa_reference_code");
            entity.Property(e => e.Reference).HasColumnName("aa_reference");
            entity.Property(e => e.Transaction).HasColumnName("aa_transaction");
            entity.Property(e => e.ReferenceValue).HasColumnName("aa_reference_value");
            entity.Property(e => e.Name).HasColumnName("aa_name");
            entity.Property(e => e.Resource).HasColumnName("aa_resource");
        });

    }
}
