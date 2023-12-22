using System.Reflection;
using DrMadWill.Extensions.Explorer.Enums;
using DrMadWill.Extensions.Explorer.Models;
namespace DrMadWill.Extensions.Explorer;
public static class ActionExplore
{
    public static List<ActionList> GetActionExplore(this Type baseController)
    {
        var controllers = baseController.Assembly.GetTypes().Where(s => s.IsAssignableTo(baseController));
        if (controllers is null) throw new NotSupportedException();
        var results = new List<ActionList>();
        foreach (var controller in controllers)
        {
            var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(SysDefinitionAttribute)));
            var explore = new ActionList();
            foreach (var action in actions)
            {
                var attribute = action.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(SysDefinitionAttribute)) as SysDefinitionAttribute;
                var endPoint = new ActionProp(controller.Name.Replace("Controller", "") + "/" + action.Name, attribute.Definition, attribute.Code, attribute.Type.ToString());
                switch (attribute.DefinitionType)
                {
                    case ActionDefinitionType.System:
                        explore.SystemsActions.Add(endPoint);
                        break;
                    case ActionDefinitionType.External:
                        explore.ExternalActions.Add(endPoint);
                        break;
                    case ActionDefinitionType.User:
                        explore.UserActions.Add(endPoint);
                        break;
                }
            }
            results.Add(explore);
        }
        return results;
    }

}