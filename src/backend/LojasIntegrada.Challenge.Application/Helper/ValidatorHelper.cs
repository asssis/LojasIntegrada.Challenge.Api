using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojasIntegrada.Challenge.Application.Helper
{
    public class ValidatorHelper
    {
        public void CheckValidator(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);
            if (resultadoValidacao.Count > 0)
            {
                throw new ValidatorException("Validação com erro", resultadoValidacao);
            }
        }
    }
}
