namespace SURI.Challenge.API.Models;

public class InvoiceCreateRequest
{
    public required string CPF { get; set; }
    public required List<Invoice> Invoices { get; set; }
}
