using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExoApi.Contexts;
using ExoApi.Models;

namespace ExoApi.Repositories
{
    public class UsuarioRepository
    {
     private readonly ExoContext _context;
     public UsuarioRepository(ExoContext context)
        {
            _context = context;
        }   
        public IEnumerable<Usuario> Listar()
        {
            return _context.Usuarios;
        }
        public void Cadastrar (Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            if(usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
            }

            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }
        public void Deletar (int id)
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(usuarioBuscado);
            _context.SaveChanges();
        }
        public Usuario BuscaPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
     }
}