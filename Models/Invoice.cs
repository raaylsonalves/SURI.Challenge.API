namespace SURI.Challenge.API.Models;

public class Invoice
{
    public int Id { get; set; }
    public string Amount { get; set; } = string.Empty;
    public string DueDate { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}