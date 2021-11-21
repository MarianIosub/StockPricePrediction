using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DomainLayer;
using Microsoft.IdentityModel.Tokens;

namespace ServiceLayer
{
    public class Utils
    {
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

            Regex regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            Match match = regex.Match(userPassword);
            return match.Success;
        }
    }
}