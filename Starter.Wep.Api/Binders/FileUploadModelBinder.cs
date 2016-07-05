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
            string root = HttpContext.Current.Server.MapPath($"~/uploads/temp"),
                dest = HttpContext.Current.Server.MapPath($"~/uploads/{actionContext.ControllerContext.ControllerDescriptor.ControllerName.ToLower()}");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

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

                    File.Copy(file.LocalFileName, $"{dest}/{fileDest}");
                    File.Delete(file.LocalFileName);

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