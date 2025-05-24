using Microsoft.AspNetCore.Mvc;
using SURI.Challenge.API.Models;
using SURI.Challenge.API.Services;
using SURI.Challenge.API.Services.Contracts;

namespace SURI.Challenge.API.Controllers
{
    [ApiController]
    [Route("invoices")]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetBillByCPF(string cpf)
        {
            var invoices = await _invoiceService.GetInvoicesByCPF(cpf);

            if (invoices?.Count == 0)
                return Ok(new { Text = "Nenhum boleto encontrado para este CPF.", Delay = 0, Type = 0 });

            var flowActionCapture = FlowActionService<Invoice>.GenerateFlowActionCapture(
                    title: "Escolha o boleto desejado",
                    body: "Selecione abaixo o boleto para receber a segunda vida:",
                    buttonTitle: "Ver boletos",
                    sectionTitle: "Boletos disponíveis",
                    items: invoices!,
                    mapItem: invoice => new
                    {
                        Title = $"{invoice.Id}",
                        Description = $"Vencimento: {invoice.DueDate}, Valor: R$ {invoice.Amount}"
                    }
                );

            return Ok(flowActionCapture);
        }

        [HttpPost("send-pdf")]
        public async Task<IActionResult> SendPdf([FromBody] InvoiceRequest request)
        {
            var invoice = await _invoiceService.GetInvoiceByCPFAndId(request.CPF, request.Id);

            if (invoice is null)
                return Ok(new { Text = "Não foi possível buscar o boleto.", Delay = 0, Type = 0 });

            var flowActionSendMedia = FlowActionService<Invoice>
                .GenerateFlowActionSendMedia(invoice.Url);

            return Ok(flowActionSendMedia);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateRequest request)
        {
            await _invoiceService.SaveInvoices(request.CPF, request.Invoices);
            return Created();
        }
    }
}