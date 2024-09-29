using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Login
    {
        public string input = "";
        public bool validEntry = false;

        public string GetAccountNumber()
        {
            string accountNumber = "";

            do
            {
                Console.Write("Account Number: ");
                input = Console.ReadLine()!;

                if (input.Length < 11 || input.Length > 11 || !long.TryParse(input, out long number))
                {
                    Console.WriteLine("Please enter a valid account number");
                }
                else
                {
                    accountNumber = input;
                    validEntry = true;
                }
            }
            while (!validEntry);
            return accountNumber;
        }

        public string GetPassword()
        {
            string password = "";
            validEntry = false;

            do
            {
                Console.Write("Enter Password: ");
                input = Console.ReadLine()!;
                List<string> specialCharacters = new List<string> { "@", "#", "$", "%", "^", "&", "!" };
                int specialCharacterCount = 0;
                foreach (var character in specialCharacters)
                {
                    if (input.Contains(character))
                    {
                        specialCharacterCount++;
                    }
                }

                if (!string.IsNullOrEmpty(input) && input.Length >= 6 && specialCharacterCount > 0)
                {
                    password = input;
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Invalid Password!");
                }
            }
            while (!validEntry);
            Console.Clear();

            return password;
        }

        
    }
}
