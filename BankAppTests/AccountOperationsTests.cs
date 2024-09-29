using BankApp;

namespace BankAppTests
{
    [TestClass]
    public class AccountOperationsTest
    {
        [TestMethod]
        public async Task Login_ValidInfo_ReturnsUserInfo()
        {
            // Arrange
            var accountOperations = AccountOperations.Instance;
            var userInfo = new UserInfo("Fortune Ukpata", "12345678901", 30000m, "Current", "fortune@gmail.com", "password$");
            accountOperations.newAccount = new Dictionary<string, UserInfo>
            {
                { "12345678901", userInfo }
            };

            // Act
            var result = await accountOperations.LoginAsync("12345678901", "password$");

            // Assert
            Assert.IsNotNull(result, "Login should return a valid UserInfo object.");
            Assert.AreEqual("Fortune Ukpata", result.AccountName, "The account name should match the expected value.");
        }
    }
}