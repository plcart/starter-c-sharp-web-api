namespace Starter.Domain.Entities
{
    public class PageHighlight:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaValue { get; set; }
        public long PageTitleId { get; set; }
        public virtual PageTitle PageTitle { get; set; }
    }
}
