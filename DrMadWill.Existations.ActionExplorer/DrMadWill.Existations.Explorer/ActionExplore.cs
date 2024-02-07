using System.Reflection;
using DrMadWill.Extensions.Explorer.Models;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DrMadWill.Extensions.Explorer;
public static class ActionExplore
{
    public static List<ActionList> GetActionExplore(this Type baseController,string key)
    {
        var controllers = baseController.Assembly.GetTypes().Where(s => s.IsAssignableTo(baseController));
        if (controllers is null) throw new NotSupportedException();
        var results = new List<ActionList>();
        foreach (var controller in controllers)
        {
            var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m =>  m.IsPublic && m.DeclaringType == controller);
            var explore = new ActionList();
            explore.Controller = controller.Name.Replace("Controller","").ToUpper();
            foreach (var action in actions)
            {
                var attribute = action.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(SysDefinitionAttribute)) as SysDefinitionAttribute;
                
                var httpMethod = action.GetCustomAttributes()
                    .Where(attr => attr is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>()
                    .SelectMany(attr => attr.HttpMethods)
                    .Distinct()
                    .FirstOrDefault();
                
                var endPoint = 
                    new ActionProp
                        (controller.Name.Replace("Controller",""),action.Name,httpMethod ?? "",
                            attribute.Operation,
                            attribute?.Definition ?? "");
                
                explore.Actions.Add(endPoint);
                if (explore.Accesses.Any(a => a.Operation == attribute?.Operation) && attribute != null)
                    continue;
                
                explore.Accesses.Add(new ControllerAccessProp(controller.Name.Replace("Controller",""),attribute.Operation,
                    ExplorerHelper.GenerateHash(key,$"{controller.Name}.{attribute.Operation.ToString()}"))); 
            }
            results.Add(explore);
        }
        return results;
    }

   

}