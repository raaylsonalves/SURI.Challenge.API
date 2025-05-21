namespace SURI.Challenge.API.Models;

public class Bill
{
    public int Id { get; set; }
    public string CPF { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Url { get; set; } = string.Empty;
}
