using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace DrMadWill.Extensions.Explorer.Attributes;

public class AccessValidationAttribute : ActionFilterAttribute
{
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        
        var controllerName = context.Controller.GetType().Name;
        var actionName = context.ActionDescriptor.RouteValues["action"];
        var operation = context.Controller.GetType().GetMethods()
            .FirstOrDefault(s => s.Name == actionName)?.GetType().GetOperation();
        var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
        var key = configuration["Key"];
        var hash = context.HttpContext.User.Claims
            .Where(s => s.Type == ("Access" + controllerName.Replace("Controller","")))?.Select(s => s.Value).FirstOrDefault();
        if ( !string.IsNullOrEmpty(hash) && !string.IsNullOrEmpty(key) && (ExplorerHelper.ValidationHash(key, (controllerName + operation), hash)))
            base.OnActionExecuting(context);
        else
            context.Result = new UnauthorizedResult();
    }
}