using Starter.Web.Api.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Starter.Web.Api.Binders
{
    public class FileUploadModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                actionContext.Response = actionContext.Request
                       .CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "");
                return false;
            }

            var model = new FileUpload();
            string root = HttpContext.Current.Server.MapPath($"~/uploads/temp");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            

            var provider = new MultipartFormDataStreamProvider(root);

            Task.Run(async() =>
            {
                var result = await actionContext.Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName,
                    fileExt = fileName.Substring(fileName.LastIndexOf('.')).Replace("\"", ""),
                    fileDest = $"{Guid.NewGuid().ToString()}{fileExt}",
                    id = file.Headers.ContentDisposition.Name;

                    File.Move(file.LocalFileName, $"{root}/{fileDest}");

                    if (model.Files.ContainsKey(id))
                        model.Files[id] += $"{fileDest};";
                    else
                        model.Files.Add(id, $"{fileDest};");
                }
            }).Wait();
            bindingContext.Model = model;
            return true;
        }
    }
}