using System.Security.Claims;
using BancoAPI.Applications.Conversions;
using BancoAPI.Applications.Services;
using BancoAPI.Domains;
using BancoAPI.DTOs.UsuarioDTO;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<ListarUsuarioDTO>>> Listar()
        {
            try
            {
                return Ok(await _service.Listar());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("UsuarioId/{usuarioId}")]
        public async Task<ActionResult<ListarUsuarioDTO>> ObterUsuarioPorId(int usuarioId)
        {
            try
            {
                return Ok(await _service.ObterUsuarioPorId(usuarioId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("UsuarioEmail/{usuarioEmail}")]
        public async Task<ActionResult<ListarUsuarioDTO>> ObterUsuarioPorEmail(string usuarioEmail)
        {
            try
            {
                return Ok(await _service.ObterUsuarioPorEmail(usuarioEmail));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //TODO Fazendo os endpoints de inserção 
        //TODO Update e remoção de usuarios no banco
        [HttpPost]
        public async Task<ActionResult> Cadastrar(string nome, string email, string senha, string tipoUsuario)
        {
            try
            {
                tipo_usuario_enum tipo = tipoUsuario == "PESSOA_FISICA" ? tipo_usuario_enum.PESSOA_FISICA : tipo_usuario_enum.PESSOA_JURIDICA;
                usuario usuarioAdicionado = new usuario
                {
                    nome = nome,
                    email = email,
                    senha = senha,
                    tipo_usuario = tipo
                };
                _service.Adicionar(usuarioAdicionado);

                return Ok(new
                {
                    mensagem = "Usuario cadastrado",
                    nome = nome,
                    email = email
                });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Editar([FromBody] string nome, string email, string senha)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized("Erro, usuario não autenticado");

                ListarUsuarioDTO usuarioEditado = await _service.ObterUsuarioPorId(int.Parse(userId));

                if (usuarioEditado == null)
                    return NotFound("Erro, usuario não encontrado!");

                usuario usuario = new usuario
                {
                    nome = nome,
                    email = email,
                };

                _service.Atualizar(usuario, senha);
                return Ok("Usuario editado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}