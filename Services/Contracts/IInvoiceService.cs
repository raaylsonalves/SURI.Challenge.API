using SURI.Challenge.API.Models;

namespace SURI.Challenge.API.Services.Contracts;

public interface IInvoiceService
{
    Task<InvoiceData> GetAllInvoices();
    Task<Invoice?> GetInvoiceByCPFAndId(string cpf, int id);
    Task<List<Invoice>?> GetInvoicesByCPF(string cpf);
    Task SaveInvoices(string cpf, List<Invoice> bills);
}