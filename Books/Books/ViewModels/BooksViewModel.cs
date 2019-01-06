namespace Books.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Books.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class BooksViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private ObservableCollection<BookItemViewModel> books;
        private List<Book> myBooks;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties
        public ObservableCollection<BookItemViewModel> Books
        {
            get { return this.books; }
            set { this.SetValue(ref this.books, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.SetValue(ref this.filter, value);
                if (string.IsNullOrEmpty(value))
                {
                    this.RefreshBooks();
                }
            }
        }

        #endregion

        #region Constructors
        public BooksViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadBooks();
        }
        #endregion

        #region Singleton
        private static BooksViewModel instance;

        public static BooksViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new BooksViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private async void LoadBooks()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            var serviceUrl = Application.Current.Resources["ServiceURL"].ToString();
            var response = await this.apiService.Get<ServiceResponse>(
                serviceUrl,
                "/1.0",
                "/new");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var serviceResponse = (ServiceResponse)response.Result;
            this.myBooks = serviceResponse.Books;
            this.RefreshBooks();
            this.IsRefreshing = false;
        }

        public void RefreshBooks()
        {
            if (string.IsNullOrEmpty(this.filter))
            {
                this.Books = new ObservableCollection<BookItemViewModel>(this.myBooks.Select(b => new BookItemViewModel
                {
                    Image = b.Image,
                    Isbn13 = b.Isbn13,
                    Price = b.Price,
                    Subtitle = b.Subtitle,
                    Title = b.Title,
                    Url = b.Url
                })
                .OrderBy(b => b.Title)
                .ToList());
            }
            else
            {
                this.Books = new ObservableCollection<BookItemViewModel>(this.myBooks.Select(b => new BookItemViewModel
                {
                    Image = b.Image,
                    Isbn13 = b.Isbn13,
                    Price = b.Price,
                    Subtitle = b.Subtitle,
                    Title = b.Title,
                    Url = b.Url
                })
                .Where(b => b.Title.ToLower().Contains(this.filter.ToLower()) || b.Subtitle.ToLower().Contains(this.filter.ToLower()))
                .OrderBy(b => b.Title)
                .ToList());

                var searches = new LastSearchHelper();
                searches.AddSearch(this.filter);
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshBooks);
            }
        }
        #endregion
    }
}