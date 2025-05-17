using System.Linq;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Services;
using Xunit;

namespace ConsoleDoc.Tests
{
    public class NotificationServiceTests
    {
        public NotificationServiceTests()
        {
            NotificationService.PendingNotifications.Clear();
        }

        [Fact]
        public void AddNotification_Should_StoreNotification()
        {
            NotificationService.AddNotification("Test message", "UserA");
            Assert.Equal(1, NotificationService.PendingNotifications.Count);
            Assert.Equal("Test message", NotificationService.PendingNotifications.First().Message);
        }

        [Fact]
        public void ShowNotificationsForUser_Should_FilterOutOwnNotifications()
        {
            Document doc = new RichTextDocument("test");
            NotificationService.AddNotification("Message from UserA", "UserA");
            NotificationService.AddNotification("Message from UserB", "UserB");
            var user = new User("UserA", "pass");
            NotificationService.ShowNotificationsForUser(user, doc);
            Assert.All(NotificationService.PendingNotifications, n =>
                Assert.Equal("UserA", n.Originator, ignoreCase: true));
        }

        [Fact]
        public void ShowNotificationsForUser_Should_RemoveShownNotifications()
        {
            Document doc = new RichTextDocument("test");
            NotificationService.AddNotification("Msg1", "UserA");
            NotificationService.AddNotification("Msg2", "UserB");
            int initialCount = NotificationService.PendingNotifications.Count;
            var user = new User("UserC", "pass");
            NotificationService.ShowNotificationsForUser(user, doc);
            int finalCount = NotificationService.PendingNotifications.Count;
            Assert.True(finalCount < initialCount);
        }
    }
}