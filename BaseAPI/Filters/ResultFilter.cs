using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Threading.Tasks;

namespace BaseAPI.Filters
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            ObjectResult contextResult = context.Result as ObjectResult;
            
            switch (contextResult.StatusCode)
            {
                case (int)HttpStatusCode.OK:
                    context.Result = GetOKResult(contextResult.Value, contextResult.StatusCode);                    
                    break;                
                default:
                    context.Result = GetErrResult(contextResult.Value, contextResult.StatusCode);
                    break;                    
            }                         
        }

        private OkObjectResult GetOKResult(object? value, int? status_code)
        {
            var Result = new OkObjectResult(new {Data =  value });
            Result.StatusCode = status_code;

            return Result;
        }

        private ObjectResult GetErrResult(object? value, int? status_code)
        {
            var Result = new ObjectResult(new { ErrorMsg = value });
            Result.StatusCode = status_code;

            return Result;
        }
    }        
}
