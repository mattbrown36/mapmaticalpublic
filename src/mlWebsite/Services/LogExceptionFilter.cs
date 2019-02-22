using Google.Api;
using Google.Cloud.Logging.Type;
using Google.Cloud.Logging.V2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using mlShared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mlWebsite.Services
{
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public LogExceptionFilter(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            var bytes = Encoding.UTF8.GetBytes(new MlExceptionLog(context.Exception).ToJson());
            using (var customErr = File.OpenWrite("/var/log/app_engine/custom_logs/unhandled.log.json")) {
                await customErr.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
