namespace SURI.Challenge.API.Models;

public class Customer
{
    public string CPF { get; set; } = string.Empty;
    public List<Invoice> Invoices { get; set; } = [];
}
