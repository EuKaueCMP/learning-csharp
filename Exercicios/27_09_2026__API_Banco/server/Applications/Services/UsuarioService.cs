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
            List<usuario> usuarios = await _repository.Listar();
            return usuarios.Select(u => ConvertToDto.UsuarioToDto(u)).ToList();
        }

        public ListarUsuarioDTO Adicionar(usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.nome) || string.IsNullOrWhiteSpace(usuario.email) || string.IsNullOrWhiteSpace(usuario.senha))
                throw new DomainException("Erro! Todos os campos são obrigatorios.");

            byte[] salt = PassToArgon2.GenerateSalt();
            string senha = PassToArgon2.HashPassword(usuario.senha, salt);

            usuario usuarioAdicionado = new usuario
            {
                nome = usuario.nome,
                email = usuario.email,
                senha = senha,
            };

            _repository.Adicionar(usuarioAdicionado);
            return ConvertToDto.UsuarioToDto(usuarioAdicionado);
        }

        public ListarUsuarioDTO Atualizar(usuario usuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(usuario.nome) || string.IsNullOrWhiteSpace(usuario.email) || string.IsNullOrWhiteSpace(usuario.senha))
                throw new DomainException("Erro! Todos os campos são obrigatorios.");

            if (!PassToArgon2.VerifyPassword(senha, usuario.senha))
                throw new DomainException("Erro! senha inválida!");

            _repository.Atualizar(usuario);
            return ConvertToDto.UsuarioToDto(usuario);
        }

        public async Task AtualizarSenha(int usuarioId, string senha)
        {
            usuario usuario = await _repository.ObterUsuarioPorId(usuarioId);
            if (usuario == null)
                throw new DomainException("Erro! Nenhum usuario encontrado");

            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("A senha é obrigatoria");

            if (!PassToArgon2.VerifyPassword(senha, usuario.senha))
                throw new DomainException("Erro, senha inválida!");

            byte[] salt = PassToArgon2.GenerateSalt();
            string novaSenha = PassToArgon2.HashPassword(senha, salt);

            _repository.AtualizarSenha(usuarioId, novaSenha);
        }

        public async Task Remover(int usuarioId)
        {
            usuario usuario = await _repository.ObterUsuarioPorId(usuarioId)
                    ?? throw new DomainException("Erro, nenhum usuario encontrado!");

            _repository.Remover(usuario);

        }
    }
}