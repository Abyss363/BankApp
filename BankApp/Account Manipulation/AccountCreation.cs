using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class AccountCreation
    {
        public bool validEntry = false;
        public string input = "";

        public (string, string) GetUserName()
        {
            string firstName = "";
            string lastName = "";

            // Getting first name
            do
            {
                Console.Write("Enter First Name: ");
                input = Console.ReadLine()!;

                if (!string.IsNullOrEmpty(input))
                {
                    char firstChar = input[0];
                    if (Char.IsLower(firstChar) || Char.IsDigit(firstChar))
                    {
                        Console.WriteLine("Please ensure the first character is capital and not a digit");
                    }
                    else
                    {
                        firstName = input;
                        validEntry = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please do not input an empty name");
                }
            }
            while (!validEntry);

            // Reset validEntry for last name check
            validEntry = false;

            // Getting last name
            do
            {
                Console.Write("Enter Last Name: ");
                input = Console.ReadLine()!;

                if (!string.IsNullOrEmpty(input))
                {
                    char firstChar = input[0];
                    if (Char.IsLower(firstChar) || Char.IsDigit(firstChar))
                    {
                        Console.WriteLine("Please ensure the first character is capital and not a digit");
                    }
                    else
                    {
                        lastName = input;
                        validEntry = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please do not input an empty name");
                }
            }
            while (!validEntry);

            return (firstName, lastName);
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
                    Console.WriteLine("Password must be at least 6 characters long and contain at least one special character.");
                }
            }
            while (!validEntry);
            Console.Clear();

            return password;
        }

        public string GetEmail()
        {
            string email = "";
            validEntry = false;

            do
            {
                Console.Write("Enter Email:");
                input = Console.ReadLine()!;

                if (input.Contains("@") && input.Contains(".com"))
                {
                    email = input;
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid email address, example(JohnSmith@mail.com)");
                }
            }
            while (!validEntry);
            Console.Clear();

            return email;
        }

        public string DetermineAccountType()
        {
            string type = "";
            validEntry = false;

            do
            {
                Console.WriteLine("Please enter 'c' to create a current account and 's' to create a savings account.");
                input = Console.ReadLine()!;

                if (input == "c")
                {
                    type = "Current";
                    validEntry = true;
                }
                else if (input == "s")
                {
                    type = "Savings";
                    validEntry = true;
                }
                else { Console.WriteLine("Invalid Input"); }
            }
            while (!validEntry);

            return type;
        }
    }
}
