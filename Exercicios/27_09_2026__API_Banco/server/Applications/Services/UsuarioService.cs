using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.UsuarioDTO;
using BancoAPI.Interfaces;
using BancoAPI.Exceptions;

namespace BancoAPI.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repository) => _repository = repository;

        public async Task<List<ListarUsuarioDTO>> Listar()
        {
            List<Usuario> usuarios = await _repository.Listar();
            return usuarios.Select(u => ConvertToDto.UsuarioToDto(u)).ToList();
        }

        public async Task<ListarUsuarioDTO> ObterUsuarioPorId(int usuarioId) => ConvertToDto.UsuarioToDto(await _repository.ObterUsuarioPorId(usuarioId) ?? throw new DomainException("Erro, nenhum usuario encontrado!"));  

        public async Task<ListarUsuarioDTO> ObterUsuarioPorEmail(string email) => ConvertToDto.UsuarioToDto(await _repository.ObterUsuarioPorEmail(email)) ?? throw new DomainException("Erro, nenhum usuario encontrado!");
        public ListarUsuarioDTO Adicionar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                throw new DomainException("Erro! Todos os campos são obrigatorios.");

            byte[] salt = PassToArgon2.GenerateSalt();
            string senha = PassToArgon2.HashPassword(usuario.Senha, salt);

            Usuario usuarioAdicionado = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = senha,
            };

            _repository.Adicionar(usuarioAdicionado);
            return ConvertToDto.UsuarioToDto(usuarioAdicionado);
        }

        public ListarUsuarioDTO Atualizar(Usuario usuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                throw new DomainException("Erro! Todos os campos são obrigatorios.");

            if (!PassToArgon2.VerifyPassword(senha, usuario.Senha))
                throw new DomainException("Erro! senha inválida!");

            _repository.Atualizar(usuario);
            return ConvertToDto.UsuarioToDto(usuario);
        }

        public async Task AtualizarSenha(int usuarioId, string senha)
        {
            Usuario usuario = await _repository.ObterUsuarioPorId(usuarioId);
            if (usuario == null)
                throw new DomainException("Erro! Nenhum usuario encontrado");

            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("A senha é obrigatoria");

            if (!PassToArgon2.VerifyPassword(senha, usuario.Senha))
                throw new DomainException("Erro, senha inválida!");

            byte[] salt = PassToArgon2.GenerateSalt();
            string novaSenha = PassToArgon2.HashPassword(senha, salt);

            _repository.AtualizarSenha(usuarioId, novaSenha);
        }

        public async Task Remover(int usuarioId)
        {
            Usuario usuario = await _repository.ObterUsuarioPorId(usuarioId)
                    ?? throw new DomainException("Erro, nenhum usuario encontrado!");

            _repository.Remover(usuario);

        }
    }
}