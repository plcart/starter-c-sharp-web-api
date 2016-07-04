using OfficeOpenXml;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Starter.Web.Api.Formatter.Xls
{
    public class PageTitleModelXlsxFormatter : BufferedMediaTypeFormatter
    {
        public PageTitleModelXlsxFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(PageTitleModel))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<PageTitleModel>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Sheet1");
                var pages = value as IEnumerable<PageTitleModel>;
                if (pages != null)
                    ws.Cells["A1"].LoadFromCollection(pages);
                else
                {
                    var singleProduct = value as PageTitleModel;
                    if (singleProduct == null)
                        throw new InvalidOperationException("Cannot serialize type");
                }

                var array = pck.GetAsByteArray();

                writeStream.Write(array, 0, array.Length);
            }
        }
    }
}