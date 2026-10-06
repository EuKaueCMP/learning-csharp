using BancoAPI.Applications.Authentication;
using BancoAPI.Domains;
using BancoAPI.DTOs.Autenticacao;
using BancoAPI.Interfaces;
using BancoAPI.Applications.Conversions;
using BancoAPI.Exceptions;

namespace BancoAPI.Applications.Services
{
    public class AutentificacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly TokenJWT _jwt;

        public AutentificacaoService(IUsuarioRepository repository, TokenJWT jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<TokenResponseDto> Login(string email, string senha)
        {
            usuario usuario = await _repository.ObterUsuarioPorEmail(email);
            if (usuario == null)
                throw new DomainException("Erro! email ou senha inválidos!");

            if (!PassToArgon2.VerifyPassword(senha, usuario.senha))
                throw new DomainException("Erro! email ou senha inválidos!");

            string tokenJwt = _jwt.GerarToken(usuario);

            TokenResponseDto tokenDto = new TokenResponseDto
            {
                Token = tokenJwt,
                TipoUsuario = usuario.tipo_usuario.ToString()
            };

            return tokenDto;
        }
    }
}