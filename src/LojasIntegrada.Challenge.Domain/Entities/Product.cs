using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojasIntegrada.Challenge.Domain.Entities
{
    public class Product
    { 

        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Digito o Nome!")]
        [MinLength(5, ErrorMessage = "O tamanho mínimo do nome de Usuário são 5 caracteres.")]
        [StringLength(30, ErrorMessage = "O tamanho máximo são 30 caracteres.")]
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
    }
}
