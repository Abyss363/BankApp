using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal class AccountOperationChecks
    {
        public string? testWarning = "!!Should only appear during testing!!";
        public bool checkName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                char firstChar = name[0];
                if (Char.IsLower(firstChar) || Char.IsDigit(firstChar))
                {
                    Console.WriteLine(testWarning);
                    Console.WriteLine("Please ensure the first character is capital and is not a digit");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine(testWarning);
                Console.WriteLine("Please do not input an empty name");
                return false;
            }
            
        }
        public bool checkPassword(string password)
        {
            List<string> specialCharacters = new List<string> { "@", "#", "$", "%", "^", "&", "!" };
            int specialCharacterCount = 0;
            foreach (var character in specialCharacters)
            {
                if (password.Contains(character))
                {
                    specialCharacterCount++;
                }
            }

            if (!string.IsNullOrEmpty(password) && password.Length >= 6 && specialCharacterCount > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine(testWarning);
                Console.WriteLine("Password must be at least 6 characters long and contain at least one special character.");
                return false;
            }
        }
        public bool checkEmail(string email)
        {
            if (email.Contains("@") && email.Contains(".com"))
            {
                return true;
            }
            else
            {
                Console.WriteLine(testWarning);
                Console.WriteLine("Please enter a valid email address, example(JohnSmith@mail.com)");
                return false;
            }
        }
        public bool checkType(string type)
        {
            if (type == "Current" || type == "Savings")
            {
                return true;
            }
            else 
            { 
                Console.WriteLine(testWarning);
                Console.WriteLine("Invalid Input");
                return false;
            }
        }

        public bool CheckAmount(decimal amount)
        {
                if (amount <= 0)
                {
                    Console.WriteLine(testWarning);
                    Console.WriteLine($"Please enter an amount above 0");
                    return false;
                }
                else { return true; }
        }
    }
}
