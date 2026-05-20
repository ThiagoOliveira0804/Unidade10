using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExoApi.Contexts
{
    // DbContext é a classe base do Entity Framework. Nossa classe herda os "poderes" dela.
    public class ExoContext : DbContext
    {
        // Este construtor vazio é necessário para algumas configurações internas do Entity Framework.
        public ExoContext() {}

        // Este construtor é a grande mágica da segurança! 
        // Ele recebe as opções de conexão (que configuraremos no Program.cs) e passa para a classe base.
        // Assim, o ExoContext não precisa saber a senha, ele apenas recebe a conexão já pronta.
        public ExoContext(DbContextOptions<ExoContext> options) : base(options) {}

        // O DbSet representa a tabela em si. É através dele que faremos buscas, cadastros, etc.
        // Pense nele como a "gaveta" de Projetos dentro do nosso banco de dados.
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}