using System;
using EvaCore.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCore.Accounting.Infrastructure.Data;

public class TransactionDbContext:DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options){}
    public DbSet<Transaction> Transactions { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("co_transaction");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("tr_id");
            entity.Property(e => e.Name).HasColumnName("tr_name");
            entity.Property(e => e.Description).HasColumnName("tr_description");
            entity.Property(e => e.CreationDate).HasColumnName("tr_creation_date");
            entity.Property(e => e.AlterDate).HasColumnName("tr_alter_date");
        });

        base.OnModelCreating(modelBuilder);
    }

}
