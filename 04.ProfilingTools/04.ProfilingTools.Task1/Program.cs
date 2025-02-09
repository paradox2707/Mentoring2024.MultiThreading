using System.Security.Cryptography;

namespace _04.ProfilingTools.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter password for hashing");
            var passwordText = Console.ReadLine() ?? string.Empty;
            var passHash = GeneratePasswordHashUsingSalt(passwordText, GenerateSalt());
            Console.WriteLine($"Password hash is {passHash}");
            Console.ReadKey();
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];

                Parallel.Invoke(
                    () => Array.Copy(salt, 0, hashBytes, 0, 16),
                    () => Array.Copy(hash, 0, hashBytes, 16, 20)
                );

                var passwordHash = Convert.ToBase64String(hashBytes);

                return passwordHash;
            }
        }

        public static byte[] GenerateSalt(int size = 16) // Default size is 16 bytes
        {
            byte[] salt = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt); // Fill the byte array with cryptographically strong random bytes
            }
            return salt;
        }
    }
}
