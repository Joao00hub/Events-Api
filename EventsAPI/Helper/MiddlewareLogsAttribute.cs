using EventsAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MiddlewareLogsAttribute : Attribute, IAsyncActionFilter
{
    private readonly string _controller;
    private readonly string _method;
    private ClaimsPrincipal _user;
    private ILogger _logger;

    public MiddlewareLogsAttribute(string controller, string method)
    {
        _controller = controller;
        _method = method;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string requestId = context.HttpContext.Request.Headers["x-request-id"];
        _user = context.HttpContext.User;
        if (context.Controller is MainController mainController)
        {
            _logger = mainController.Logger;
        }

        if (_logger != null)
        {
            var scope = new List<KeyValuePair<string, object>>
            {
                new ("Request", requestId ?? string.Empty),
                new ("Controller", _controller),
                new ("Method", _method),
                new ("Username", _user?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous")
            };

            if (context.HttpContext.Request.Query.Keys.Count > 0)
            {
                scope.Add(new("QueryParams", context.HttpContext.Request.Query));
            }

            using (_logger.BeginScope(scope))
            {
                await next();
            }
        }
        else
        {
            await next();
        }
    }
}
