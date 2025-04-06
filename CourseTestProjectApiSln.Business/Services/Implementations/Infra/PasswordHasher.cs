using CourseTestProjectApiSln.Business.Services.Abstractions.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.Business.Services.Implementations.Infra
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 1000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string HashPassword(string password)
        {
           
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize); 

                 
                    byte[] hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

       
        public bool VerifyPassword(string password, string hashPassword)
        {
            
            byte[] hashBytes = Convert.FromBase64String(hashPassword);

            byte[] salt = new byte[SaltSize];
            if(hashBytes.Length < SaltSize + HashSize)
{
              
                throw new ArgumentException("Invalid hash format. The hash is too short.");
            }

            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize); 

                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

}

