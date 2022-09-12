using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Survey_API.Database;
using Survey_API.Models;
using MongoDB.Bson;
using System.Security.Cryptography;

namespace Survey_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;
        private readonly string key;
        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("Survey_DB"));
            
            var database = client.GetDatabase("Survey_DB");

            users = database.GetCollection<User>("Users");

            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public List<User> GetUsers()
        {
            return users.Find(user => true).ToList();
        }

        public User GetUser(string id)
        {
            return users.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public static string encrypt(string tobeEncrypted)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(tobeEncrypted));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public User CreateUser(User user)
        {
            user.Password = encrypt(user.Password);
            users.InsertOne(user);

            return user;
        }

        public User GetUserByEmail(string email)
        {
            return users.Find<User>(user => user.Email == email).FirstOrDefault();
        }

        public string Authenticate(string email, string password)
        {
            var user = this.users.Find(u => u.Email == email && u.Password == encrypt(password)).FirstOrDefault();

            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            
        }
    }
}
