namespace Books.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class LastSearchHelper
    {
        #region Attributes
        private List<LastSearch> lastSearches;
        #endregion

        #region Constructors
        public LastSearchHelper()
        {
            this.LoadSearches();
        }
        #endregion

        #region Methods
        private void LoadSearches()
        {
            var lastSearchesString = Settings.LastSearches;
            var lastSearchArray = lastSearchesString.Split('|');
            this.lastSearches = new List<LastSearch>();
            for (int i = 0; i < lastSearchArray.Length; i++)
            {
                this.lastSearches.Add(new LastSearch { Search = lastSearchArray[i] });
            }
        }

        public List<LastSearch> GetLastSearches()
        {
            return this.lastSearches;
        }

        public void AddSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return;
            }

            var exists = this.lastSearches.Any(s => s.Search.ToLower().Equals(search.ToLower()));
            if (exists)
            {
                return;
            }

            this.lastSearches.Add(new LastSearch { Search = search });
            if (this.lastSearches.Count > 5)
            {
                this.lastSearches.RemoveAt(0);
            }

            this.SaveSearches();
        }

        private void SaveSearches()
        {
            var stringToSave = string.Empty;
            foreach (var item in this.lastSearches)
            {
                if (!string.IsNullOrEmpty(item.Search))
                {
                    stringToSave += $"{item.Search}|";
                }
            }

            stringToSave = stringToSave.Substring(0, stringToSave.Length - 1);
            Settings.LastSearches = stringToSave;
        }
        #endregion
    }
}