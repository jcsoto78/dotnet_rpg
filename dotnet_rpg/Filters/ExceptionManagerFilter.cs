using dotnet_rpg.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Filters
{

    //THere on mvc web api, Filters are code which execute before or after a controller execution, there are filters for auth, response handling
    //and more, here we use Exception handling Filters to replace try catch blocks at every controller, and use custom Exceptions.
    public class ExceptionManagerFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionManagerFilter(IWebHostEnvironment hostEnviroment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostEnviroment = hostEnviroment;
            _modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        { 
            if (context.Exception is AppTypeException || context.Exception is Exception)
            {
                context.Result =
                        new JsonResult($"App Error {_hostEnviroment.ApplicationName} with Exception type {context.Exception.GetType()}"); 
            }
        }
    }
}
