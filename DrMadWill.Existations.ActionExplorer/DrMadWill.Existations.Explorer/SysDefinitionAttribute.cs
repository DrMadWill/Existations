using DrMadWill.Extensions.Explorer;
using DrMadWill.Extensions.Explorer.Enums;
namespace DrMadWill.Extensions.Explorer;
public class SysDefinitionAttribute : Attribute
{
    public SysDefinitionAttribute(string definition, string code, ActionType type, ActionDefinitionType definitionType)
    {
        Definition = definition;
        Code = code;
        Type = type;
        DefinitionType = definitionType;
    }
    public string Definition { get; set; }
    public string Code { get; set; }
    public ActionType Type { get; set; }
    public ActionDefinitionType DefinitionType { get; set; }
}