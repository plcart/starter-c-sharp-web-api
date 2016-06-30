namespace Starter.Web.Api.Models
{
    public class Paginate
    {
        int page = 0;
        int items = 0;

        public Paginate(string page, string items)
        {
            int.TryParse(page, out this.page);
            int.TryParse(items, out this.items);
        }

        public int Page { get { return page; } }
        public int ItemsPerPage { get { return items; } }
    }
}