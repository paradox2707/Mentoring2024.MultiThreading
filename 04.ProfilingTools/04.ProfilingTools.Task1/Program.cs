using System.Security.Cryptography;

namespace _04.ProfilingTools.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter password for hashing");
            var passwordText = Console.ReadLine() ?? string.Empty;
            //var passHash = GeneratePasswordHashUsingSalt(passwordText, GenerateSalt());
            var passHash = GeneratePasswordHashUsingSalt_Optimized(passwordText, GenerateSalt());
            Console.WriteLine($"Password hash is {passHash}");
            Console.ReadKey();
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {

            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;

        }

        public static string GeneratePasswordHashUsingSalt_Optimized(string passwordText, byte[] salt)
        {

            const int iterate = 10000; // Number of iterations
            const int hashSize = 20; // Size of the hash in bytes
            const int totalSize = 36; // Total size of the resulting byte array (16 bytes salt + 20 bytes hash)

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
            {
                byte[] hash = pbkdf2.GetBytes(hashSize);
                byte[] hashBytes = new byte[totalSize];

                // Copy salt and hash into the result array
                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length); // Copy salt
                Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length); // Copy hash

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
