using Microsoft.AspNetCore.Mvc;
using SURI.Challenge.API.Models;
using SURI.Challenge.API.Services;

namespace SURI.Challenge.API.Controllers
{
    [ApiController]
    [Route("bills")]
    public class BillController : ControllerBase
    {
        private readonly JsonDataService _jsonDataService;

        public BillController()
        {
            _jsonDataService = new JsonDataService();
        }

        [HttpGet("{cpf}")]
        public IActionResult GetBillByCPFAsync(string cpf)
        {
            var bills = _jsonDataService.GetBills()?.Where(bill => bill.CPF == cpf).ToList();

            if (bills?.Count == 0)
                return NotFound(new { Success = false, Message = "Nenhum boleto encontrado." });

            return Ok(new { Success = true, Data = bills });
        }

        [HttpPost]
        public IActionResult AddBillAsync([FromBody] Bill bill)
        {
            var bills = _jsonDataService.GetBills();
            bills?.Add(bill);
            _jsonDataService.SaveBills(bills!);

            return Created();
        }
    }
}
