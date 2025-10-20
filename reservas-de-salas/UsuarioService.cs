﻿using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
          var existingUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);
            if (existingUser != null )
            {
                throw new Exception("Já existe um usuário com este email.");
            }
            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return usuario;
        }

        public async Task DeleteUsuarioAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }
            _usuarioRepository.Delete(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(long id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}
