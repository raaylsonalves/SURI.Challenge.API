using SURI.Challenge.API.Models;
using SURI.Challenge.API.Services.Contracts;
using System.Text.Json;

namespace SURI.Challenge.API.Services;

public class InvoiceService : IInvoiceService
{
    private readonly string _filePath = "./data/invoices.json";

    public async Task<InvoiceData> GetAllInvoices()
    {
        if (!File.Exists(_filePath))
            return new InvoiceData();

        var jsonData = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<InvoiceData>(jsonData) ?? new InvoiceData();
    }

    public async Task<Invoice?> GetInvoiceByCPFAndId(string cpf, int id)
    {
        var invoices = await GetInvoicesByCPF(cpf);

        return invoices?.FirstOrDefault(bill => bill.Id == id);
    }

    public async Task<List<Invoice>?> GetInvoicesByCPF(string cpf)
    {
        var billsData = await GetAllInvoices();
        return billsData.Customers.FirstOrDefault(b =>  b.CPF == cpf)?.Invoices ?? [];
    }

    public async Task SaveInvoices(string cpf, List<Invoice> invoices)
    {
        var invoicesData = await GetAllInvoices();

        var existingCustomer = invoicesData.Customers.FirstOrDefault(c => c.CPF == cpf);
        if (existingCustomer != null)
        {
            var invoicesToAdd = invoices.Where(newInvoice =>
            !existingCustomer.Invoices.Any(existingInvoice => existingInvoice.Id == newInvoice.Id)).ToList();

            if (invoicesToAdd.Count > 0)
            {
                existingCustomer.Invoices.AddRange(invoicesToAdd);
            }
        }
        else
        {
            invoicesData.Customers.Add(new Customer { CPF = cpf, Invoices = invoices });
        }

        string jsonData = JsonSerializer.Serialize(invoicesData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, jsonData);
    }
}
