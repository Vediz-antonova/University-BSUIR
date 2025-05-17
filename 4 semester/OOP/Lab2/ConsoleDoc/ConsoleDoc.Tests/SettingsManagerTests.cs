using System.Reflection;
using ConsoleDoc.ConsoleDocumentEditor.Services;

namespace ConsoleDoc.Tests
{
    public class SettingsManagerTests
    {
        [Fact]
        public void ResetSettings_Should_SetDefaultValues()
        {
            var sm = SettingsManager.Instance;
            sm.SetForegroundColor(ConsoleColor.Red);
            sm.SetBackgroundColor(ConsoleColor.Blue);
            sm.ResetSettings();
            var fg = (ConsoleColor)typeof(SettingsManager).GetProperty("CurrentForegroundColor", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sm);
            var bg = (ConsoleColor)typeof(SettingsManager).GetProperty("CurrentBackgroundColor", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sm);
            Assert.Equal(ConsoleColor.Gray, fg);
            Assert.Equal(ConsoleColor.Black, bg);
        }

        [Fact]
        public void SetForegroundColor_Should_ChangeColor()
        {
            var sm = SettingsManager.Instance;
            sm.SetForegroundColor(ConsoleColor.Green);
            var fg = (ConsoleColor)typeof(SettingsManager).GetProperty("CurrentForegroundColor", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sm);
            Assert.Equal(ConsoleColor.Green, fg);
        }
    }
}