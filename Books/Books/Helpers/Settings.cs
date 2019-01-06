namespace Books.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        #region Setting Constants
        private const string lastSearches = "lastSearches";
        private static readonly string StringDefault = string.Empty;
        #endregion

        #region Properties
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string LastSearches
        {
            get
            {
                return AppSettings.GetValueOrDefault(lastSearches, StringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(lastSearches, value);
            }
        }
        #endregion
    }
}