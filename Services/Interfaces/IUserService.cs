using ACBAbankTask.DataModels;

namespace ACBAbankTask.Services.Interfaces
{
    public interface IUserService
    {
        public string Register(UserDto user);
        public string SignIn(string email, string password);
    }
}
