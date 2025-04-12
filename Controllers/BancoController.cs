using ApiBrasil.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ApiBrasil.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class BancoController : Controller
    {
        private readonly IBankInterface _bancoService;

        public BancoController(IBankInterface banco)
        {
            _bancoService = banco;
        }

        [HttpGet("GetAllBanks")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllBanks()
        {
            var response = await _bancoService.GetAllBanks();

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound("Banks not found.");
            }

            return Ok(response.Data);
        }

        [HttpGet("GetById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BankById(int id)
        {
            if (id == null)
            {
                return BadRequest("ID inválido.");
            }

            var response = await _bancoService.GetBankById(id);

            if (response.Data == null)
            {
                return NotFound($"Bank with {id} not found.");
            }

            return Ok(response.Data);
        }
    }
}

