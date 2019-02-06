using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TSM.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault("AccessToken", ""); }
            set { AppSettings.AddOrUpdateValue("AccessToken", value); }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get { return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow); }
            set { AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value); }
        }

        public static string BaseAddress
        {
            get { return AppSettings.GetValueOrDefault("BaseAddress", ""); }
            set { AppSettings.AddOrUpdateValue("BaseAddress", value); }
        }

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken) && AccessTokenExpirationDate > DateTime.Now;

        public static void Logout()
        {
            AccessToken = string.Empty;
            AccessTokenExpirationDate = DateTime.MinValue;
        }
    }
}
