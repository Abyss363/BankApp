using BankApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class LoggedInMenu
    {



        public LoggedInMenu()
        {
            InitializeMenuAsync().Wait();
        }

        public async Task InitializeMenuAsync()
        {
            UserInfo? userInfo;
            string input = "";
            var accountOperations = AccountOperations.Instance;
            var getUserInput = new UserInput();
            var login = new Login();

            string menuSelection = "";

            string accountNumber2;

            decimal amount;
            string note = "";



            string accountNumber = login.GetAccountNumber();
            string password = login.GetPassword();
            userInfo = await accountOperations.LoginAsync(accountNumber, password);
            if (userInfo == null) return; // Exit if login failed

            Console.WriteLine("||--------------------------------||");
            Console.WriteLine($"Welcome to the Bank {userInfo.AccountName}.\n");
            Console.WriteLine("||--------------------------------||");

            do
            {
                Console.WriteLine("\n1. Create another Account");
                Console.WriteLine("2. Make a Deposit");
                Console.WriteLine("3. Make a Withdrawal");
                Console.WriteLine("4. Make a transfer");
                Console.WriteLine("5. Print Financial Statement");
                Console.WriteLine("6. Check Balance");
                Console.WriteLine("7. Logout\n");

                Console.Write("Please enter 1, 2, 3, 4, 5, 6 or 7 to proceed.");
                input = Console.ReadLine()!;
                Console.Clear();
                if (input != null)
                {
                    menuSelection = input.ToLower();
                }

                switch (menuSelection)
                {
                    case "1":
                        await getUserInput.CreateAccount_Info(userInfo);
                        break;

                    case "2":
                        Console.WriteLine("Please enter amount to deposit");
                        amount = getUserInput.GetAmount();
                        Console.WriteLine("Please enter a note(Press Enter to leave blank");
                        note = Console.ReadLine()!;
                        accountOperations.Deposit(userInfo, amount, note);

                        break;

                    case "3":
                        Console.WriteLine("Please enter amount to withdraw.");
                        amount = getUserInput.GetAmount();
                        Console.WriteLine("Please enter a note(Press Enter to leave blank");
                        note = Console.ReadLine()!;
                        accountOperations.Withdraw(userInfo, amount, note);
                        break;

                    case "4":
                        Console.WriteLine("Please enter beneficiary account number and amount.");
                        accountNumber2 = getUserInput.GetAccountNumber("Beneficiary ");
                        amount = getUserInput.GetAmount();
                        Console.WriteLine("Please enter a note(Press Enter to leave blank");
                        note = Console.ReadLine()!;
                        accountOperations.Transfer(userInfo, accountNumber2, amount, note);
                        break;

                    case "5":
                        accountOperations.PrintFinancialStatement(userInfo);
                        break;

                    case "6":
                        accountOperations.PrintBalance(userInfo);
                        break;

                    case "7":
                        Console.WriteLine("Logging Out...");
                        return;

                    case "exit":
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
