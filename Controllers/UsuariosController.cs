using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoApi.Models;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController: ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuariosController (UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet]
        public IActionResult Listar ()
        {
        return Ok (_usuarioRepository.Listar());
        }
        [HttpPost]
        public IActionResult Cadastrar (Usuario usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return StatusCode(201);
        }
        [HttpGet("{id}")]
        public IActionResult BuscaPorId(int id)
        {
            Usuario usuario = _usuarioRepository.BuscaPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok (usuario);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _usuarioRepository.Atualizar(id, usuario);
           return StatusCode(204);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public IActionResult Login (Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);
            if (usuarioBuscado == null)
            {
                return NotFound(new { mensagem = "E-mail ou senha inválidos"});
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
            };
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao-senai-123"));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
             issuer: "exoapi.webapi",
             audience: "exoapi.webapi",
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: creds   
            );
            return Ok(new {token = new JwtSecurityTokenHandler().
            WriteToken(token) });
        }
    }
}