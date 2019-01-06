namespace Books.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;
    using Xamarin.Forms;

    public class BookItemViewModel : Book
    {
        #region Commands
        public ICommand SelectBookCommand
        {
            get
            {
                return new RelayCommand(SelectBook);
            }
        }

        private async void SelectBook()
        {
            MainViewModel.GetInstance().BookDetail = new BookDetailViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage());
        }
        #endregion
    }
}