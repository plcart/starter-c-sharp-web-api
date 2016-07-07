namespace Starter.Web.Api.Models
{
    public class Paginate
    {
        int page = 0;
        int items = 0;
        string order = "";
        bool reverse = false;
        public Paginate(string page, string items, string order, string reverse)
        {
            int.TryParse(page, out this.page);
            int.TryParse(items, out this.items);
            this.order = order;
            bool.TryParse(reverse, out this.reverse);
        }

        public int Page { get { return page==0?page:page-1; } }
        public int ItemsPerPage { get { return items; } }
        public string Order { get { return order; } }
        public bool Reverse { get { return reverse; } }
    }
}