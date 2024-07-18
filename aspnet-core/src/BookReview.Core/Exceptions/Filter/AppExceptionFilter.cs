using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Abp.Web.Models;
using BookReview.Configuration;
using BookReview.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Mime;

namespace BookReview.Exceptions.Filter
{
    public class AppExceptionFilter : IExceptionFilter
    {

        public IAbpSession AbpSession { get; set; }

        private readonly IConfigurationRoot _appConfiguration;

        private static string _rootPath;

        public AppExceptionFilter()
        {
            AbpSession = NullAbpSession.Instance;
            _rootPath ??= WebContentDirectoryFinder.CalculateContentRootFolder();
            _appConfiguration = AppConfigurations.Get(_rootPath);
        }
        public void OnException(ExceptionContext context)
        {

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var exception = context.Exception;

            if (exception is Abp.Authorization.AbpAuthorizationException)
            {
                statusCode = AbpSession.UserId.HasValue
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            if (exception is AbpValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            if (exception is ErrorResponseException)
            {
                statusCode = (exception as ErrorResponseException).StatusCode;
            }
            else if (statusCode == HttpStatusCode.InternalServerError)
            {
                exception = new ErrorResponseException(_appConfiguration["App:MessageServerError"], exception.Message);
            }

            context.Result = new ContentResult
            {
                StatusCode = (int)statusCode,
                ContentType = MediaTypeNames.Application.Json,
                Content = JsonConvert.SerializeObject(new ErrorInfoResponse(new AjaxResponse(
                            SingletonDependency<IErrorInfoBuilder>.Instance.BuildForException(exception),
                            statusCode == HttpStatusCode.Forbidden || statusCode == HttpStatusCode.Unauthorized).Error),
                            new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            })
            };



        }

    }
}
