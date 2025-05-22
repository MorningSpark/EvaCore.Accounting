using System;

namespace EvaCore.Accounting.Domain.Entities;

public class AccountingAccount
{
    /// <summary>
    /// Account identifier
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Parent account identifier
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// User Id
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// Reference code
    /// </summary>
    public string? ReferenceCode { get; set; }

    /// <summary>
    /// Reference
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Account name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Account Type
    /// </summary>
    public string? Resource { get; set; }

    /// <summary>
    /// Is transactional?
    /// </summary>
    public bool? Transaction { get; set; }

    /// <summary>
    /// Reference value
    /// </summary>
    public decimal? ReferenceValue { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Alter Date
    /// </summary>
    public DateTime? AlterDate { get; set; }

}
