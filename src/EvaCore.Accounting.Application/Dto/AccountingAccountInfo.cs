namespace EvaCore.Accounting.Application.Dto;

public class AccountingAccountInfo
{
    /// <summary>
    /// Account identifier
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Account name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Reference code
    /// </summary>
    public string? ReferenceCode { get; set; }

    /// <summary>
    /// Account total balance
    /// </summary>
    public decimal? Balance { get; set; }   

}
