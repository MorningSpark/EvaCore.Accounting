namespace EvaCore.Accounting.Application.Dto;

public class ResumeAccountingAccountResult
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
    /// Account total balance
    /// </summary>
    public decimal? Balance { get; set; }   

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Alter Date
    /// </summary>
    public DateTime? AlterDate { get; set; }

}
