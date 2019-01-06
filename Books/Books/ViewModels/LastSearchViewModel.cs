namespace Books.ViewModels
{
    using Helpers;
    using Models;
    using System.Collections.ObjectModel;

    public class LastSearchViewModel
    {
        #region Attributes
        private LastSearchHelper LastSearchHelper;
        #endregion

        #region Properties
        public ObservableCollection<LastSearch> LastSearches { get; set; }
        #endregion

        #region Constructors
        public LastSearchViewModel()
        {
            this.LastSearchHelper = new LastSearchHelper();
            this.LastSearches = new ObservableCollection<LastSearch>(this.LastSearchHelper.GetLastSearches());
        }
        #endregion

        #region Methods
        #endregion
    }
}
