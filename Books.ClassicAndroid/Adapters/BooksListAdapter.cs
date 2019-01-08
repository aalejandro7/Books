namespace Books.ClassicAndroid.Adapters
{
    using System.Collections.Generic;
    using Android.App;
    using Android.Views;
    using Android.Widget;
    using Core.Models;
    using Helpers;

    public class BooksListAdapter : BaseAdapter<Book>
    {
        private List<Book> books;
        private Activity context;

        public BooksListAdapter(Activity context, List<Book> books) : base()
        {
            this.context = context;
            this.books = books;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Book this[int position]
        {
            get
            {
                return books[position];
            }
        }

        public override int Count
        {
            get
            {
                return books.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var book = books[position];

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(book.Image);

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.BookRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.titleTextView).Text = book.Title;
            convertView.FindViewById<TextView>(Resource.Id.subtitleTextView).Text = book.Subtitle;
            convertView.FindViewById<ImageView>(Resource.Id.bookImageView).SetImageBitmap(imageBitmap);

            return convertView;
        }
    }
}