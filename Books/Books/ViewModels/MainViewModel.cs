namespace Books.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties
        public BooksViewModel Books { get; set; }

        public BookDetailViewModel BookDetail { get; set; }

        public LastSearchViewModel LastSearch { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Books = new BooksViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand LastSearchCommand
        {
            get
            {
                return new RelayCommand(GoLastSearch);
            }
        }

        private async void GoLastSearch()
        {
            this.LastSearch = new LastSearchViewModel(); 
            await Application.Current.MainPage.Navigation.PushAsync(new LastSearchPage());
        }
        #endregion
    }
}