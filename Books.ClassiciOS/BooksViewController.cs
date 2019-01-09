using Books.Core.Models;
using Books.Core.Services;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Books.ClassiciOS
{
    public partial class BooksViewController : UIViewController
    {
        private ApiService apiService;
        private List<Book> books;

        public BooksViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.apiService = new ApiService();
            this.LoadBooks();
        }

        private async void LoadBooks()
        {
            var response = await this.apiService.Get<ServiceResponse>("https://api.itbook.store", "/1.0", "/new");
            if (!response.IsSuccess)
            {
                var message = new UIAlertView("Error", response.Message, null, "Ok");
                message.Show();
                return;
            }

            var serviceResponse = (ServiceResponse)response.Result;
            this.books = serviceResponse.Books;
        }
    }
}
