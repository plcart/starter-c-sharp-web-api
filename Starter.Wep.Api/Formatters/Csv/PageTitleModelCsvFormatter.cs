﻿using Starter.Infra.Data.Helpers.Extensions;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace Starter.Web.Api.Formatter.Csv
{
    public class PageTitleModelCsvFormatter : BufferedMediaTypeFormatter
    {
        public PageTitleModelCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
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
            using (var writer = new StreamWriter(writeStream))
            {
                var result = "Id,Page,Title,Description,MediaType,Language\n";
                var pages = value as IEnumerable<PageTitleModel>;
                
                if (pages != null)
                    result+= pages.Aggregate(new StringBuilder(), (sb, c) =>  sb.AppendLine($"{c.Id},{c.Page},{c.Title},{c.Description},{c.MediaType},{c.Language}"));
                else
                {
                    var singleProduct = value as PageTitleModel;
                    if (singleProduct == null)
                        throw new InvalidOperationException("Cannot serialize type");
                    result += $"{singleProduct.Id},{singleProduct.Page},{singleProduct.Title},{singleProduct.Description},{singleProduct.MediaType},{singleProduct.Language}";
                }
                var base64 = Convert.ToBase64String(result.ToByteArray());
                writer.WriteLine(base64);
            }
        }
    }
}