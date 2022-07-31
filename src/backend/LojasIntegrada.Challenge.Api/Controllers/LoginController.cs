using LojasIntegrada.Challenge.Api.Services;
using LojasIntegrada.Challenge.Application;
using LojasIntegrada.Challenge.Application.Helper;
using LojasIntegrada.Challenge.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        private readonly UserService userService;
        public LoginController(UserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> AuthenticationAsybcAsync([FromBody] UserLoginDto userDto)
        {
            var user = await userService.GetAutheticationAsync(userDto.UserName, userDto.Password);

            if (user == null)
                return NotFound(new { message = "Usuário Inválidos" });

            var token = TokenService.GenerateToken(user);

            return Ok(new
            {
                user = user,
                token = token
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(UserDto userDto)
        {
            try
            {

                await userService.SaveAsync(userDto);
                return Ok();
            }
            catch (ValidatorException error)
            {
                return BadRequest(
                        new { 
                              Mensagem = error.Mensagem,  
                              Error = error.ValidatorResult});
            } 
        }
    }
}
