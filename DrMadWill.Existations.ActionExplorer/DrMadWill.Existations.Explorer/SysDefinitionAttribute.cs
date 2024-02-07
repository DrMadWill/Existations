using DrMadWill.Extensions.Explorer.Enums;

namespace DrMadWill.Extensions.Explorer;
public class SysDefinitionAttribute : Attribute
{
    public SysDefinitionAttribute(Crud operation ,string definition)
    {
        Definition = definition;
        Operation = operation;
    }

    public SysDefinitionAttribute(Crud operation)
    {
        Definition = "";
        Operation = operation;
    }
    public string Definition { get; set; }
    public Crud Operation { get; set; }
    
}