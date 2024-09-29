using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class UserInput
    {
        public string input = "";
        public bool validEntry = false;

        public string GetAccountNumber(string Account)
        {
            string accountNumber = "";

            do
            {
                Console.Write(Account + "Account Number: ");
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

        public decimal GetAmount()
        {
            validEntry = false;
            decimal amount = 0.0m;
            do
            {
                Console.Write("Amount: ");
                input = Console.ReadLine()!;

                if (!decimal.TryParse(input, out decimal number))
                {
                    Console.WriteLine("Please enter a valid amount.");
                }
                else
                {
                    amount = Convert.ToDecimal(input);
                    if (amount <= 0)
                    {
                        Console.WriteLine($"Please enter an amount above 0");
                    }
                    else { validEntry = true; }

                }
            }
            while (!validEntry);

            return amount;
        }
    }
}
