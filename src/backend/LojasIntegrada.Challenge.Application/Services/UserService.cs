using AutoMapper;
using LojasIntegrada.Challenge.Application.Helper;
using LojasIntegrada.Challenge.DataBase.Repositories;
using LojasIntegrada.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojasIntegrada.Challenge.Application.Services
{
    public class UserService
    {
        private UserRepository userRepository;
        private ValidatorHelper validatorHelper;
        private IMapper mapper;
        public UserService(IMapper _mapper, UserRepository _userRepository, ValidatorHelper _validatorHelper)
        { 
            validatorHelper = _validatorHelper;
            userRepository = _userRepository;
            mapper = _mapper;
        }
        public async Task<UserDto> GetUser(string username)
        {
            var list = await userRepository.GetListAsync();
            List<UserDto> users = mapper.Map<List<UserDto>>(list);

            users.AddRange(new List<UserDto>
            {
                 new UserDto { Id = 1, UserName = "client", Password = "client", Role = "client" },
                 new UserDto { Id = 1, UserName = "admin", Password = "admin", Role = "admin" }
            });

            var user = users
                .FirstOrDefault(x =>
                    x.UserName.ToLower() == username.ToLower());
            if (user != null)
                user.Password = "";
            return user;
        }

        public async Task<UserDto> GetAutheticationAsync(string username, string password)
        {
            var list = await userRepository.GetListAsync(); 
            List<UserDto> users = mapper.Map<List<UserDto>>(list);

            users.AddRange(new List<UserDto>
            {
                 new UserDto { Id = 1, UserName = "client", Password = "client", Role = "client" },
                 new UserDto { Id = 1, UserName = "admin", Password = "admin", Role = "admin" }
            });

            var user = users
                .FirstOrDefault(x =>
                    x.UserName.ToLower() == username.ToLower() &&
                    x.Password == password);
            if(user != null)
            user.Password = "";
            return user;
        }
        public async Task SaveAsync(UserDto obj)
        {
            User objMap = mapper.Map<User>(obj);
            objMap.Role = "client";
            validatorHelper.CheckValidator(objMap);
            await userRepository.SaveAsync(objMap);
        }
    }
}
