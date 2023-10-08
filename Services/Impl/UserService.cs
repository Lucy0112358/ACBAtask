using ACBAbankTask.DataModels;
using ACBAbankTask.Entities;
using ACBAbankTask.Repository.Impl;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string Register(UserDto user)
        {
            return _userRepository.Register(user.Email, user.Password);
        }

        public string SignIn(string email, string password)
        {

            return _userRepository.SignIn(email, password);
        }
    }
}
