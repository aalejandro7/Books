namespace Books.Models
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using ViewModels;
    using Xamarin.Forms;

    public class LastSearch
    {
        public string Search { get; set; }

        public ICommand SelectSearchCommand
        {
            get
            {
                return new RelayCommand(SelectSearch);
            }
        }

        private async void SelectSearch()
        {
            BooksViewModel.GetInstance().Filter = this.Search;
            BooksViewModel.GetInstance().RefreshBooks();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}