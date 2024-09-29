using BankApp;

namespace BankAppTests
{
    [TestClass]
    public class AccountOperationsTest
    {
        public AccountOperations? accountOperations;
        UserInfo? userInfo;
        string note = "This is a note";

        [TestInitialize]
        public void TestInitialize()
        {
            // Create a new instance of AccountOperations before each test
            accountOperations = AccountOperations.Instance;
            accountOperations.newAccount = new Dictionary<string, UserInfo>();
            

        }

        [TestMethod]
        public async Task Login_ValidInfo_ReturnsUserInfo()
        {
            // Arrange
            userInfo = new UserInfo("Fortune Ukpata", "12345678901", 0m, "Current", "fortune@gmail.com", "password$");
            accountOperations!.newAccount.Add("12345678901", userInfo!);

            // Act
            var result = await accountOperations.LoginAsync("12345678901", "password$");

            // Assert
            Assert.IsNotNull(result, "Login should return a valid UserInfo object.");
            Assert.AreEqual("Fortune Ukpata", result.AccountName, "The account name should match the expected value.");
        }

        [TestMethod]
        public async Task TestLoginAsync_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string accountNumber = "12345678901";
            string password = "password123";

            // Act
            var result = await accountOperations!.LoginAsync(accountNumber, password);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CreateAccountAsync_CreatesNewAccount()
        {
            // Act
            await accountOperations!.CreateAccountAsync("Fortune Ukpata", "12345678901", 0m, "Savings", "fortune@gmail.com", "securepassword");

            // Assert
            Assert.IsTrue(accountOperations.newAccount.ContainsKey("12345678901"));
            Assert.AreEqual("Fortune Ukpata", accountOperations.newAccount["12345678901"].AccountName);
        }

        [TestMethod]
        public void Deposit_AddsToUserBalance()
        {
            // Arrange
            userInfo = new UserInfo("Fortune Ukpata", "12345678901", 0m, "Current", "fortune@gmail.com", "password$");
            decimal depositAmount = 200m;

            // Act
            accountOperations!.Deposit(userInfo!, depositAmount, note);

            // Assert
            Assert.AreEqual(200m, userInfo.Balance);
        }

        [TestMethod]
        public void Withdraw_ValidAmount_SubtractsFromUserBalance()
        {
            // Arrange
            userInfo = new UserInfo("Fortune Ukpata", "12345678901", 1000m, "Current", "fortune@gmail.com", "password$");
            decimal withdrawAmount = 500m;

            // Act
            accountOperations!.Withdraw(userInfo, withdrawAmount, note);

            // Assert
            Assert.AreEqual(500m, userInfo.Balance);
        }

        [TestMethod]
        public void Withdraw_SavingsInsufficientFunds()
        {
            // Arrange
            userInfo = new UserInfo("Fortune Ukpata", "12345678901", 100m, "Savings", "fortune@gmail.com", "password$");
            decimal withdrawAmount = 500m;

            // Act
            accountOperations!.Withdraw(userInfo, withdrawAmount, note);

            // Assert
            Assert.AreEqual(100m, userInfo.Balance); // No withdrawal should happen
        }

        [TestMethod]
        public void Transfer_ValidTransfer_UpdatesBothBalances()
        {
            // Arrange
            var fromUser = new UserInfo("Fortune Ukpata", "12345678901", 500m, "Savings", "ukpata@gmail.com", "password123");
            var toUser = new UserInfo("JFortune Fortune", "10987654321", 500m, "Savings", "fortune@gmail.com", "password456");
            accountOperations!.newAccount.Add("12345678901", fromUser);
            accountOperations.newAccount.Add("10987654321", toUser);
            decimal transferAmount = 500m;

            // Act
            accountOperations.Transfer(fromUser, "10987654321", transferAmount, note);

            // Assert
            Assert.AreEqual(0m, fromUser.Balance);
            Assert.AreEqual(1000m, toUser.Balance);
        }

        [TestMethod]
        public async Task SaveAndLoadAsync_SavesAndLoadsAccountsCorrectly()
        {
            // Arrange
            userInfo = new UserInfo("Fortune Ukpata", "12345678901", 0m, "Current", "fortune@gmail.com", "password$");
            accountOperations!.newAccount.Add("12345678901", userInfo);
            await accountOperations.SaveAsync();

            // Act
            var loadedAccounts = await AccountOperations.LoadAsync();

            // Assert
            Assert.IsTrue(loadedAccounts.ContainsKey("12345678901"));
            Assert.AreEqual("Fortune Ukpata", loadedAccounts["12345678901"].AccountName);
        }
    }
}