using BancoAPI.DTOs.Autenticacao;
using BancoAPI.Applications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutentificacaoService _service;
        public AutenticacaoController(AutentificacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                return Ok(await _service.Login(loginDto.email, loginDto.senha));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
    }
}