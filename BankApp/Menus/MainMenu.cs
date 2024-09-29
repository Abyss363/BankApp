using BankApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class MainMenu
    {
        public string type = "";
        public MainMenu()
        {
            InitialiseMainMenu().Wait();
        }

        public async Task InitialiseMainMenu()
        {
            var accountOperations = AccountOperations.Instance;
            var getUserInput = new UserInput();
            string input = "";
            string menuSelection = "";

            Console.WriteLine("||-------------------------------||");
            Console.WriteLine("\tWelcome to the Bank.");
            Console.WriteLine("||-------------------------------||");
            do
            {
                Console.WriteLine("\n1. Create Account");
                Console.WriteLine("2. Login\n");

                Console.Write("Please enter 1 or 2 to proceed or type 'exit' to exit: ");
                input = Console.ReadLine()!;
                Console.Clear();
                if (input != null)
                {
                    menuSelection = input.ToLower();
                }

                switch (menuSelection)
                {
                    case "1":
                        await accountOperations.CreateAccountAsync();
                        break;

                    case "2":
                        var loggedInMenu = new LoggedInMenu();
                        break;

                    case "exit":
                        Console.WriteLine("");
                        return;

                    default:
                        Console.WriteLine("Please select an option");
                        break;
                }
            }
            while (true);
        }
    }
}
