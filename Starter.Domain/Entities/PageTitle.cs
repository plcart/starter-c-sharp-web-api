using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Entities
{
    public class PageTitle:BaseEntity
    {

        public PageTitle()
        {
            PageHighlights = new HashSet<PageHighlight>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public Page Page { get; set; }
        public Language Language { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaValue { get; set; }
        public ICollection<PageHighlight> PageHighlights { get; set; }
    }
}
