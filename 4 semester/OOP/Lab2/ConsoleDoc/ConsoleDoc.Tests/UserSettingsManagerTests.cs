using ConsoleDoc.ConsoleDocumentEditor.Services;

namespace ConsoleDoc.Tests
{
    public class UserSettingsManagerTests
    {
        private readonly string _settingsFile = "userSettings.json";

        public UserSettingsManagerTests()
        {
            if (File.Exists(_settingsFile))
                File.Delete(_settingsFile);
        }

        [Fact]
        public void GetSettingsForUser_Should_ReturnDefaultSettings()
        {
            var settings = UserSettingsManager.GetSettingsForUser("testuser");
            Assert.Equal("Gray", settings.ForegroundColor);
            Assert.Equal("Black", settings.BackgroundColor);
        }

        [Fact]
        public void UpdateSettings_Should_ChangeUserSettings()
        {
            var settings = UserSettingsManager.GetSettingsForUser("user1");
            settings.ForegroundColor = "White";
            UserSettingsManager.UpdateSettings(settings);
            var updated = UserSettingsManager.GetSettingsForUser("user1");
            Assert.Equal("White", updated.ForegroundColor);
        }

        [Fact]
        public void SettingsPersistence_Should_WorkAcrossCalls()
        {
            var settings1 = UserSettingsManager.GetSettingsForUser("persistUser");
            settings1.BackgroundColor = "Blue";
            UserSettingsManager.UpdateSettings(settings1);
            var settings2 = UserSettingsManager.GetSettingsForUser("persistUser");
            Assert.Equal("Blue", settings2.BackgroundColor);
        }
    }
}