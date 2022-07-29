using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LojasIntegrada.Challenge.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digito o Nome!")]
        [MinLength(3, ErrorMessage = "O tamanho mínimo do nome de Usuário são 3 caracteres.")]
        [StringLength(30, ErrorMessage = "O tamanho máximo são 30 caracteres.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Digito o Password!")]
        [MinLength(8, ErrorMessage = "O tamanho mínimo do nome de Usuário são 8 caracteres.")]
        [StringLength(15, ErrorMessage = "O tamanho máximo são 15 caracteres.")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
