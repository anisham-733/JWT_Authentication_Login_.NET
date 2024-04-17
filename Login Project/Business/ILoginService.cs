using Login_Project.Models;

namespace Login_Project.Business
{
    public interface ILoginService
    {
        public List<Logins> GetLogin(string UserName, string Password);
        public Task<int> SignUp(Logins login);
        public string Authenticate(string UserName);
        public List<Student> GetStudent();

    }
}
