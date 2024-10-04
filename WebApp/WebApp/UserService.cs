using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp
{
    public class UserService
    {
        private readonly PodatkiDb _context;
        private readonly IPasswordHasher<Uporabnik> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(PodatkiDb context, IPasswordHasher<Uporabnik> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public void CreateUser(Uporabnik user)
        {
            _context.Uporabniki.Add(user);
            _context.SaveChanges();
        }

        public Uporabnik GetUserByUsername(string username)
        {
            return _context.Uporabniki.SingleOrDefault(u => u.ime == username);
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, password) == PasswordVerificationResult.Success;
        }
        public string GenerateJwtToken(Uporabnik user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ime)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
