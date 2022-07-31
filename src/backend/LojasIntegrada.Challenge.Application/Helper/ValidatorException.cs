using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojasIntegrada.Challenge.Application.Helper
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string Mensagem, List<ValidationResult> ValidatorResult)
        {
            this.Mensagem = Mensagem;
            this.ValidatorResult = ValidatorResult;
        }
        public List<ValidationResult> ValidatorResult { get; set; }
        public string Mensagem { get; set; }

    }
}
