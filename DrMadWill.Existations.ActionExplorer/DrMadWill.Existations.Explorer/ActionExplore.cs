using System.Reflection;
using DrMadWill.Extensions.Explorer.Attributes;
using DrMadWill.Extensions.Explorer.Enums;
using DrMadWill.Extensions.Explorer.Models;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DrMadWill.Extensions.Explorer;
public static class ActionExplore
{
    public static List<ControllerProp> GetActionExplore(this Type baseController,string key)
    {
        var controllers = baseController.Assembly.GetTypes().Where(s => s.IsAssignableTo(baseController));
        if (controllers is null) throw new NotSupportedException();
        var explorer = new List<ControllerProp>();
        foreach (var controller in controllers)
        {
            var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m =>  m.IsPublic && m.DeclaringType == controller);
            var controllerProp = new ControllerProp();
            controllerProp.Controller = controller.Name.Replace("Controller","").ToUpper();
            foreach (var action in actions)
            {
                var attribute = action.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(ActionDefinitionAttribute)) as ActionDefinitionAttribute;
                
                var httpMethod = action.GetCustomAttributes()
                    .Where(attr => attr is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>()
                    .SelectMany(attr => attr.HttpMethods)
                    .Distinct()
                    .FirstOrDefault();
                
                var endPoint = 
                    new ActionProp
                        (controller.Name.Replace("Controller",""),action.Name,httpMethod ?? "",
                            attribute?.Operation,
                            attribute?.Definition ?? "");
                
                controllerProp.Actions.Add(endPoint);
                if (controllerProp.Accesses.Any(a => a.Operation == attribute?.Operation) && attribute != null)
                    continue;
                
                controllerProp.Accesses.Add(new ControllerAccessProp(controller.Name.Replace("Controller",""),attribute.Operation,
                    ExplorerHelper.GenerateHash(key, $"{controller.Name}.{attribute.Operation.ToString()}.{attribute.SpecialCode}" ))); 
            }
            explorer.Add(controllerProp);
        }
        return explorer;
    }

   

}