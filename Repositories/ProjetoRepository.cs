using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoApi.Contexts;
using ExoApi.Models;

namespace ExoApi.Repositories
{
    public class ProjetoRepository
    {
        // Variável privada que vai guardar o nosso Contexto (nossa ponte com o banco).
        // A palavra "readonly" significa que, após ser criada, essa ponte não pode ser alterada.
        private readonly ExoContext _context;

        // Quando o Repositório for chamado, ele pedirá o Contexto emprestado (Injeção de Dependência).
        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }

        // Método que busca e retorna todos os projetos salvos no banco de dados.
        // Usamos IEnumerable porque ele é excelente e leve para apenas "ler" uma coleção de dados,
        // sem precisar forçar a criação de recursos extras na memória.
        public IEnumerable<Projeto> Listar()
        {
            // Pega a "gaveta" de Projetos do contexto e devolve para quem pediu.
            return _context.Projetos;
        }
        public void Cadastrar(Projeto projeto)
        {
            _context.Projetos.Add(projeto); //Preparando a gaveta dentro do armario
            _context.SaveChanges(); // salva o que foi alterado
        }
        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }
        public void Atualizar (int id, Projeto projeto)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);
            if(projetoBuscado != null)
            {
                projetoBuscado.NomeDoProjeto = projeto.NomeDoProjeto;
                projetoBuscado.Area = projeto.Area;
                projetoBuscado.Status = projeto.Status;
            }
            _context.Projetos.Update(projetoBuscado); //UPDATE serve para salvar as atualizações
            _context.SaveChanges(); //Salvar as mudanças
        }
        public void Deletar(int id)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);
            _context.Projetos.Remove(projetoBuscado);
            _context.SaveChanges();
        }
    }
}