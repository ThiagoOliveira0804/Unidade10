using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // biblioteca necessária para acessar corretamente o banco de dados. 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers; // biblioteca necessária para acessar corretamente o banco de dados.

namespace ExoApi.Models
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        [Key]
        [Column("cd_usuario")]
        public int Id {get; set;}
        [Column("ds_email")]
        public string Email { get; set;}
        [Column("ds_senha")]
        public string Senha { get; set;}
    }
}