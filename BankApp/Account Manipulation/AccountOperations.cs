using BankApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApp
{
    public class AccountOperations
    {
        private static AccountOperations? _instance;
        private static readonly object _lock = new object();
        private static string saveDirectory = "SavedAccounts";
        public Dictionary<string, UserInfo> newAccount = new Dictionary<string, UserInfo>();

        private AccountOperations() { }

        public static AccountOperations Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountOperations();
                    }
                    return _instance;
                }
            }
        }

        public async Task<UserInfo> LoginAsync(string accountNumber, string password)
        {
            if (newAccount == null || newAccount.Count == 0)
            {
                newAccount = await LoadAsync();
            }

            if (newAccount.ContainsKey(accountNumber) && newAccount[accountNumber].Password == password)
            {
                return newAccount[accountNumber];
            }
            else
            {
                Console.WriteLine("Invalid account number or password.");
                return null!;
            }
        }

        public async Task CreateAccountAsync(string fullName, string accountNumber, decimal accountBalance, string accountType, string email, string password)
        {
            newAccount.Add(accountNumber, new UserInfo(fullName, accountNumber, accountBalance, accountType, email, password));
            await SaveAsync();
            Console.WriteLine($"Account has been created with the following details: ");
            Console.WriteLine($"Account Number: {accountNumber} || Account Name: {fullName} || Account Type: {accountType}");
        }
        public void Deposit(UserInfo userInfo, decimal amount, string note)
        {
            if (userInfo != null)
            {
                userInfo.Balance += amount;
                userInfo.TransactionHistory.Add($"| \t{userInfo.AccountName} \t|\t {userInfo.AccountNumber:C} \t|\t {userInfo.AccountType:C} \t|\t {amount} \t|\t Deposit...{note} \t| |--------------------");

                Console.WriteLine($"Deposit successful! New Balance: {userInfo.Balance}");
            }
        }

        public void Withdraw(UserInfo userInfo, decimal amount, string note)
        {
            if (userInfo != null)
            {
                if (userInfo.AccountType == "Savings")
                {
                    amount += 1000;
                    Console.WriteLine("Please note: You cannot withdraw past 1000 naira on a savings account");
                }
                if (userInfo.Balance >= amount)
                {
                    if (userInfo.AccountType == "Savings")
                    {
                        amount -= 1000;
                    }
                    userInfo.Balance -= amount;
                    userInfo.TransactionHistory.Add($"| \t{userInfo.AccountName} \t|\t {userInfo.AccountNumber} \t|\t {userInfo.AccountType} \t|\t {amount} \t|\t Withdrawal...{note} \t| |--------------------");
                    Console.WriteLine($"Withdrawal successful! New Balance: {userInfo.Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
        }

        public void Transfer(UserInfo fromUserInfo, string toAccount, decimal amount, string note)
        {
            if (fromUserInfo != null && newAccount.ContainsKey(toAccount))
            {
                if (fromUserInfo.Balance >= amount)
                {
                    fromUserInfo.Balance -= amount;
                    fromUserInfo.TransactionHistory.Add($"| \t{fromUserInfo.AccountName} \t|\t {fromUserInfo.AccountNumber} \t|\t {fromUserInfo.AccountType} \t|\t {amount} \t|\t Transfer to: {newAccount[toAccount].AccountName} ... {note} |--------------------");

                    newAccount[toAccount].Balance += amount;
                    newAccount[toAccount].TransactionHistory.Add($"| \t{newAccount[toAccount].AccountName} \t|\t {newAccount[toAccount].AccountNumber} \t|\t {newAccount[toAccount].AccountType} | {amount} \t|\t Transfer from: {fromUserInfo.AccountName} ... {note} |--------------------");
                    Console.WriteLine($"Transfer of {amount} from account {fromUserInfo.AccountNumber} to account {toAccount} successful.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds for transfer.");
                }
            }
        }

        public void PrintFinancialStatement(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                Console.WriteLine($"Financial Statement for Account {userInfo.AccountNumber} - {userInfo.AccountName}");
                Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|----------|");
                Console.WriteLine("|\t FULL NAME \t|\t ACCOUNT NUMBER \t|\t ACCOUNT TYPE \t|\t AMOUNT BAL \t|\t NOTE |");

                foreach (var transaction in userInfo.TransactionHistory)
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine($"Current Balance: {userInfo.Balance}");
            }
        }

        public void PrintBalance(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                Console.WriteLine($"Current Balance: {userInfo.Balance}");
            }
        }

        public async Task SaveAsync()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string path = Path.Combine(saveDirectory, "AllAccounts.json");
            string jsonData = JsonSerializer.Serialize(newAccount, options);

            await File.WriteAllTextAsync(path, jsonData);
        }

        public static async Task<Dictionary<string, UserInfo>> LoadAsync()
        {
            string loadDirectory = Path.Combine(saveDirectory, "AllAccounts.json");
            if (File.Exists(loadDirectory))
            {
                string jsonData = await File.ReadAllTextAsync(loadDirectory);
                Dictionary<string, UserInfo> loadedAccounts = JsonSerializer.Deserialize<Dictionary<string, UserInfo>>(jsonData)!;
                return loadedAccounts;
            }
            return new Dictionary<string, UserInfo>(); // Return an empty dictionary if no file exists
        }


    }
}
