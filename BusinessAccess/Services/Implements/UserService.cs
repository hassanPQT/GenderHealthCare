using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Respository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRespository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task AddUser(User user)
        {
            var checkUser = await _userRepository.FindAccountByUserName(user.Username);
            var checkEmail = await _userRepository.FindAccountByEmail(user.Email);
            if (checkUser != null || checkEmail != null)
            {
                throw new Exception("Username or Email already exists.");
            }

            await _userRepository.AddUser(user);
        }

        public async Task<User?> FindAccountByEmail(string email)
        {
           return await _userRepository.FindAccountByEmail(email);
        }

        public async Task<User?> FindAccountById(Guid userId)
        {
            return await _userRepository.FindAccountById(userId);
        }

        public async Task<User?> FindAccountByUserName(string username)
        {
            return await _userRepository.FindAccountByUserName(username);
        }

        public async Task<User?> UpdateAsync(Guid id, User dto)
        {
            return await _userRepository.UpdateAsync(id, dto);
        }
    }
}
