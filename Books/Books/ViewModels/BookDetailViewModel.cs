namespace Books.ViewModels
{
    using Models;

    public class BookDetailViewModel
    {
        #region Properties
        public Book Book { get; set; }
        #endregion

        #region Constructors
        public BookDetailViewModel(Book book)
        {
            this.Book = book;
        }
        #endregion
    }
}