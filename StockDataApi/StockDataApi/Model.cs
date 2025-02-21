using System.ComponentModel.DataAnnotations;

public class Fund
{
    [Key]
    public int Id { get; set; }

    public string Category { get; set; }
    public string Name { get; set; }
    public int CrisilRank { get; set; }

    public decimal Aum { get; set; } 
    public decimal OneMonth { get; set; }
    public decimal SixMonths { get; set; }
    public decimal OneYear { get; set; }
    public decimal ThreeYears { get; set; }
    public decimal FiveYears { get; set; }
}
