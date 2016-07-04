using Starter.Web.Api.Formatter.Csv;
using Starter.Web.Api.Formatter.Xls;
using System.Web.Http;

namespace Starter.Web.Api
{
    public class FormatterConfig
    {
        public static void RegisterFormatters(HttpConfiguration config)
        {
            config.Formatters.Add(new PageTitleModelCsvFormatter());
            config.Formatters.Add(new PageTitleModelXlsxFormatter());
        }
    }
}