using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class UserInfo
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> TransactionHistory { get; set; }

        public UserInfo(string accountName, string accountNumber, decimal balance, string accountType, string email, string password)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            Balance = balance;
            AccountType = accountType;
            Email = email;
            Password = password;
            TransactionHistory = new List<string>();
        }
    }
}
