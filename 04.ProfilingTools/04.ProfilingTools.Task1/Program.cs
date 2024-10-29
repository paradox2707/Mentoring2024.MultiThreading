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

            const int iterate = 10000;
            const int hashSize = 20;
            const int totalSize = 36; // Total size of the resulting byte array (16 bytes salt + 20 bytes hash)

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
            {
                byte[] hash = pbkdf2.GetBytes(hashSize);
                byte[] hashBytes = new byte[totalSize];

                // Copy salt and hash into the result array
                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length); 
                Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length); 

                return Convert.ToBase64String(hashBytes);
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
