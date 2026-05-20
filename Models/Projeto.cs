using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Importamos as bibliotecas que permitem usar anotações (os colchetes abaixo)
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExoApi.Models
{
    // A anotação [Table] avisa ao C# que esta classe representa a tabela "tb_projetos" lá no MySQL.
    [Table("tb_projetos")]
    public class Projeto
    {
        // [Key] avisa que a propriedade abaixo é a Chave Primária (o identificador único) da tabela.
        [Key]
        // [Column] faz o mapeamento, ligando a propriedade "Id" do C# com a coluna "cd_projeto" do banco.
        [Column("cd_projeto")]
        public int Id { get; set; }

        // Mapeia o nome do projeto
        [Column("nm_projeto")]
        public string NomeDoProjeto { get; set; }

        // Mapeia a área do projeto
        [Column("nm_area")]
        public string Area { get; set; }

        // Mapeia o status (1 para ativo/verdadeiro, 0 para inativo/falso)
        [Column("fl_status")]
        public bool Status { get; set; }
    }
}