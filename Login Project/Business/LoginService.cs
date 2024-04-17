using Login_Project.Models;
using Login_Project.Repository;

namespace Login_Project.Business
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _login;

        public LoginService(ILoginRepository login)
        {
            _login = login;
        }

        public string Authenticate(string UserName)
        {
            return _login.Authenticate(UserName);
        }

        public List<Logins> GetLogin(string UserName, string Password)
        {
            return _login.GetLogin(UserName,Password);
        }

        public List<Student> GetStudent()
        {
            return _login.GetStudent();
        }

        public Task<int> SignUp(Logins login)
        {
            return _login.SignUp(login);
        }

    }
}
