using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using TSM.Enums;
using TSM.Models;

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

        public static int UserId
        {
            get { return AppSettings.GetValueOrDefault("UserId", 0); }
            set { AppSettings.AddOrUpdateValue("UserId", value); }
        }

        public static bool HasManagerPermissions
        {
            get { return AppSettings.GetValueOrDefault("HasManagerPermissions", false); }
            private set { AppSettings.AddOrUpdateValue("HasManagerPermissions", value); }
        }

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken) && AccessTokenExpirationDate > DateTime.Now;

        public static void Logout()
        {
            AccessToken = string.Empty;
            AccessTokenExpirationDate = DateTime.MinValue;
        }

        public static void UpdatePermissions(List<Member> members)
        {
            var userTitle = members.SingleOrDefault(x => x.UserId == UserId)?.Title;
            HasManagerPermissions = userTitle == JobTitle.Manager;
        }
    }
}
