using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
using ConsoleDoc.ConsoleDocumentEditor.Services;

namespace ConsoleDoc.Tests
{
    public class UserManagerTests
    {
        private UserManager _userManager;

        public UserManagerTests()
        {
            _userManager = new UserManager();
            foreach (var user in _userManager.ListUsers().ToList())
            {
                _userManager.RemoveUser(user.Username);
            }
        }

        [Fact]
        public void AddUser_Should_AddNewUser_And_LoginWorks()
        {
            var user = new User("testuser", "password");
            bool added = _userManager.AddUser(user);
            Assert.True(added);
            var loggedUser = _userManager.Login("testuser", "password");
            Assert.NotNull(loggedUser);
            Assert.Equal("testuser", loggedUser.Username);
        }

        [Fact]
        public void RemoveUser_Should_RemoveExistingUser()
        {
            var user = new User("removeuser", "pass");
            _userManager.AddUser(user);
            bool removed = _userManager.RemoveUser("removeuser");
            Assert.True(removed);
            var result = _userManager.Login("removeuser", "pass");
            Assert.Null(result);
        }
    }
}