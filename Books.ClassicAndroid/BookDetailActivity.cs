namespace Books.ClassicAndroid
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Helpers;
    using Models;
    using Newtonsoft.Json;

    [Activity(Label = "Book Detail")]
    public class BookDetailActivity : Activity
    {
        private ImageView bookImageView;
        private TextView titleTextView;
        private TextView subtitleTextView;
        private TextView isbn13TextView;
        private TextView priceTextView;
        private TextView urlTextView;
        private Book book;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.BookDetailView);

            var bookString = this.Intent.Extras.GetString("BookString");
            this.book = JsonConvert.DeserializeObject<Book>(bookString);

            this.FindViews();

            this.BindData();
        }

        private void BindData()
        {
            this.titleTextView.Text = this.book.Title;
            this.subtitleTextView.Text = this.book.Subtitle;
            this.isbn13TextView.Text = this.book.Isbn13;
            this.urlTextView.Text = this.book.Url;
            this.priceTextView.Text = $"Price: {this.book.Price:C2}";

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(this.book.Image);
            this.bookImageView.SetImageBitmap(imageBitmap);
        }

        private void FindViews()
        {
            this.bookImageView = FindViewById<ImageView>(Resource.Id.bookImageView);
            this.titleTextView = FindViewById<TextView>(Resource.Id.titleTextView);
            this.subtitleTextView = FindViewById<TextView>(Resource.Id.subtitleTextView);
            this.isbn13TextView = FindViewById<TextView>(Resource.Id.isbn13TextView);
            this.priceTextView = FindViewById<TextView>(Resource.Id.priceTextView);
            this.urlTextView = FindViewById<TextView>(Resource.Id.urlTextView);
        }
    }
}