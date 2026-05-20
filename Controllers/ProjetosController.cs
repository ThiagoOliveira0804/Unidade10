using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoApi.Models;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExoApi.Controllers
{
    // Define como as pessoas vão acessar este controlador pela internet.
    // O "[controller]" será substituído pelo nome da classe sem a palavra Controller.
    // Ou seja, a rota ficará: http://localhost:PORTA/api/projetos
    [Route("api/[controller]")]
    
    // Avisa que esta classe é um Controlador focado em API (trabalhando com JSON).
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        // Variável que guarda o nosso Repositório.
        private readonly ProjetoRepository _projetoRepository;

        // O Controlador pede o Repositório emprestado quando é iniciado.
        public ProjetosController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        // O [HttpGet] avisa que este método será ativado quando alguém acessar a rota no navegador.
        [HttpGet]
        // O IActionResult permite retornar resultados no formato padrão da web (como erros 404 ou sucesso 200).
        public IActionResult Listar()
        {
            // Retorna o status 200 (Ok) junto com a coleção de projetos que vieram do banco de dados.
            return Ok(_projetoRepository.Listar());
        }// {} serve para abrir metodos [] abre 
        [HttpPost ]
        public IActionResult Cadastrar(Projeto projeto) 
        {
            _projetoRepository.Cadastrar(projeto);
            return StatusCode(201); // retorna para saber se há algum erro
        }
        [HttpGet("{id}")] //Get é o comando para buscar o Id
        public IActionResult BuscarPorId (int id)
        {
            Projeto projeto = _projetoRepository.BuscarPorId(id);
            {
                return NotFound();
            }
            return Ok(projeto);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            _projetoRepository.Atualizar(id, projeto);
            return StatusCode(204); // Retorna o código 204 que quer dizer que está tudo certo.
        }
        [HttpDelete("{id}")] // [] serve para indicar um método
        public IActionResult Deletar(int id)
        {
            try
            {
                _projetoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}