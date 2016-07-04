using System.Collections.Generic;

namespace Starter.Web.Api.Models
{
    public class FileUpload
    {
        public Dictionary<string,string> Files { get; }

        public FileUpload()
        {
            Files = new Dictionary<string, string>();
        }
    }
}