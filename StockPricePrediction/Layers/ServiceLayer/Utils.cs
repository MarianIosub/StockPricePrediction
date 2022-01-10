using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DomainLayer;

[assembly: AssemblyTitle("ServicelayerAssembly")]
[assembly: AssemblyVersion("1.0")]

namespace ServiceLayer
{
    public static class Utils
    {
        private static readonly string SecurityKey =
            File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "secrets.txt"));

        public static bool IsValid(User user)
        {
            return IsValidPassword(user.Password) && IsValidEmail(user.Email);
        }

        private static bool IsValidEmail(string userEmail)
        {
            try
            {
                var mail = new MailAddress(userEmail);
                
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool IsValidPassword(string userPassword)
        {
            if (userPassword.Length < 8)
            {
                return false;
            }

            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            var match = regex.Match(userPassword);
            return match.Success;
        }

        public static string EncryptPassword(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(SecurityKey,
                    new byte[] {0x45, 0x76, 0x61, 0x6e, 0x20, 0x41, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x16});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }
    }
}