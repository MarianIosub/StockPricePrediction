using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DomainLayer;


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
                var mailAddress = new MailAddress(userEmail);
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

        public static string EncryptPassword(string plainText)
        {
            byte[] toEncryptedArray = Encoding.UTF8.GetBytes(plainText);

            MD5CryptoServiceProvider objMd5CryptoService = new MD5CryptoServiceProvider();
            byte[] securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));
            objMd5CryptoService.Clear();

            var objTripleDesCryptoService = new TripleDESCryptoServiceProvider();
            objTripleDesCryptoService.Key = securityKeyArray;
            objTripleDesCryptoService.Mode = CipherMode.ECB;
            objTripleDesCryptoService.Padding = PaddingMode.PKCS7;


            var crytpoTransform = objTripleDesCryptoService.CreateEncryptor();
            byte[] resultArray = crytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDesCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}