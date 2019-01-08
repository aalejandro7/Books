namespace Books.ClassicAndroid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Adapters;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Widget;
    using Models;
    using Newtonsoft.Json;
    using Services;

    [Activity(Label = "Books", MainLauncher = true)]
    public class BooksActivity : Activity
    {
        private ListView booksListView;
        private List<Book> books;
        private ApiService apiService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.BooksView);

            this.booksListView = FindViewById<ListView>(Resource.Id.booksListView);
            this.booksListView.ItemClick += BooksListView_ItemClick;

            this.apiService = new ApiService();
            this.LoadBooks();
        }

        private void BooksListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var book = this.books[e.Position];
            var bookString = JsonConvert.SerializeObject(book);
            var intent = new Intent();
            intent.SetClass(this, typeof(BookDetailActivity));
            intent.PutExtra("BookString", bookString);
            StartActivityForResult(intent, 100);
        }

        private async Task LoadBooks()
        {
            var response = await this.apiService.Get<ServiceResponse>("https://api.itbook.store", "/1.0", "/new");
            if (!response.IsSuccess)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Error");
                dialog.SetMessage("The books information can't be recovered.");
                dialog.Show();
                return;
            }

            var serviceResponse = (ServiceResponse)response.Result;
            this.books = serviceResponse.Books;
            this.booksListView.Adapter = new BooksListAdapter(this, this.books);
            this.booksListView.FastScrollEnabled = true;
        }
    }
}