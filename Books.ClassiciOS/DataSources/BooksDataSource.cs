using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Books.Core.Models;
using Foundation;
using UIKit;

namespace Books.ClassiciOS.DataSources
{
    public class BooksDataSource : UITableViewSource
    {
        private readonly List<Book> books;

        public BooksDataSource(List<Book> books)
        {
            this.books = books;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, string.Empty);
            cell.TextLabel.Text = this.books[indexPath.Row].Title;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this.books.Count;
        }
    }
}