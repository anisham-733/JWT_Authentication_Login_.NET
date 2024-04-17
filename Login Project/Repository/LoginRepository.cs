using Login_Project.DBContext;
using Login_Project.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Login_Project.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _con;
        public readonly IConfiguration configuration;

        public LoginRepository(AppDbContext con, IConfiguration configuration)
        {
            _con = con;
            this.configuration = configuration;
        }
        public List<Logins> GetLogin(string UserName, string Password)
        {
            Password = PagalSibayan(Password);
            return _con.Logins.Where(x => x.UserName == UserName && x.Password == Password).ToList();
           

        }

        public async Task<int> SignUp(Logins login)
        {
            login.Password=PagalSibayan(login.Password);
            await _con.Logins.AddAsync(login);
            return await _con.SaveChangesAsync();

        }
        public string Authenticate(string UserName)
        {
            //var key = "abcdefghijklmnopqrstuvwxyzaaaabbbbcccdddeeefffggg";
            var key = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                                        Build().GetSection("Jwt")["key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = UTF8Encoding.UTF8.GetBytes(key);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, UserName)
                }),
                Issuer = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                                        Build().GetSection("Jwt")["Issuer"],
                Audience = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                                        Build().GetSection("Jwt")["Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            tokenHandler.WriteToken(token);
            return tokenHandler.WriteToken(token);
        }
        public string PagalSibayan(string aid)
        {//Encrypting the password
            string Key = "abcdefg";
            byte[] data = UTF8Encoding.UTF8.GetBytes(aid);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            tripleDES.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
            byte[] output = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(output);
        }
        public string MahaPagalSibayan(string aid)
        {//Decrypting the password
            string Key = "abcdefg";
            byte[] data = Convert.FromBase64String(aid);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            tripleDES.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
            byte[] output = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(output);
        }

        public List<Student> GetStudent()
        {
            return _con.Student.ToList();
        }
    }
}
