using DrMadWill.Extensions.Explorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace DrMadWill.Extensions.Explorer.Attributes;

public class AccessValidationAttribute : ActionFilterAttribute
{
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // get controller name
        var controllerName = context.Controller.GetType().Name;
        // Cast the context.ActionDescriptor to ControllerActionDescriptor to access more specific information.
        if (context.ActionDescriptor is not ControllerActionDescriptor descriptor) return;
        // Retrieve attributes applied to the action method.
        var actionDefinition =
            descriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .FirstOrDefault(a => a.GetType() == typeof(ActionDefinitionAttribute)) as ActionDefinitionAttribute;
        
        if(actionDefinition == null) return;
        var hash = context.HttpContext.User.Claims
            .Where(s => s.Type == ("Access" + controllerName.Replace("Controller","")))?.Select(s => s.Value).FirstOrDefault();
        
        if (!string.IsNullOrEmpty(hash) && !string.IsNullOrEmpty(ActionAccessConfig.Key) 
                                         && (ExplorerHelper.ValidationHash(ActionAccessConfig.Key, 
                                             ExplorerHelper.GetCode(controllerName,actionDefinition.Operation.ToString(),actionDefinition.SpecialCode )
                                             , hash)))
            base.OnActionExecuting(context);
        else
            context.Result = new UnauthorizedResult();


    }
}