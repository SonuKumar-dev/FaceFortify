using System;
using System.Security.Cryptography;
using System.Text;

namespace FaceFortify.Examples
{
    public static class KeyDerivationExample
    {
        public static byte[] DeriveKey(byte[] faceEmbedding, string hardwareId, byte[] salt)
        {
            // Combine inputs
            byte[] hardwareBytes = Encoding.UTF8.GetBytes(hardwareId);
            byte[] input = new byte[faceEmbedding.Length + hardwareBytes.Length];

            Buffer.BlockCopy(faceEmbedding, 0, input, 0, faceEmbedding.Length);
            Buffer.BlockCopy(hardwareBytes, 0, input, faceEmbedding.Length, hardwareBytes.Length);

            // PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password: input,
                salt: salt,
                iterations: 100_000,
                hashAlgorithm: HashAlgorithmName.SHA256);

            return pbkdf2.GetBytes(32); // 256-bit key
        }

        public static void Demo()
        {
            byte[] fakeEmbedding = new byte[512]; // demo data
            RandomNumberGenerator.Fill(fakeEmbedding);

            byte[] salt = RandomNumberGenerator.GetBytes(32);
            string hardwareId = "CPU|BOARD|MAC-DEMO";

            byte[] key = DeriveKey(fakeEmbedding, hardwareId, salt);

            Console.WriteLine($"Derived Key: {Convert.ToBase64String(key)}");
        }
    }
}
