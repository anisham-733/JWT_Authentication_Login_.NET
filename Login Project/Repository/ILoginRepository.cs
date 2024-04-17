using Login_Project.Models;

namespace Login_Project.Repository
{
    public interface ILoginRepository
    {
         public List<Logins> GetLogin(string UserName, string Password);
         public Task<int> SignUp(Logins login);
         public string Authenticate(string UserName);
         public List<Student> GetStudent();

    }
}
