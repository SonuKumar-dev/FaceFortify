using System;
using System.IO;
using System.Security.Cryptography;

namespace FaceFortify.Examples
{
    public static class FileEncryptionExample
    {
        public static void EncryptFile(string inputPath, string outputPath, byte[] key)
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.GenerateIV(); // random IV per file

            using var fsOut = new FileStream(outputPath, FileMode.Create);
            
            // Write IV at start of file
            fsOut.Write(aes.IV, 0, aes.IV.Length);

            using var cryptoStream = new CryptoStream(fsOut, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using var fsIn = new FileStream(inputPath, FileMode.Open);

            fsIn.CopyTo(cryptoStream);
        }

        public static void DecryptFile(string inputPath, string outputPath, byte[] key)
        {
            using var fsIn = new FileStream(inputPath, FileMode.Open);

            byte[] iv = new byte[16];
            fsIn.Read(iv, 0, iv.Length);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            using var cryptoStream = new CryptoStream(fsIn, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var fsOut = new FileStream(outputPath, FileMode.Create);

            cryptoStream.CopyTo(fsOut);
        }
    }
}
