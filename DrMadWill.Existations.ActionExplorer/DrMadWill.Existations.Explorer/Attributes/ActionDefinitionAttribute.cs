using DrMadWill.Extensions.Explorer.Enums;

namespace DrMadWill.Extensions.Explorer.Attributes;
public class ActionDefinitionAttribute : Attribute
{
    /// <summary>
    /// Using AccessOperation For Create,Update,Delete And Read but not access Special 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="definition"></param>
    public ActionDefinitionAttribute(AccessOperation operation ,string definition)
    {
        if (operation == AccessOperation.Special) 
            throw new NotImplementedException("you are definition AccessOperation.Special but SpecialCode not define.");
        Definition = definition;
        Operation = operation;
        SpecialCode = string.Empty;
    }

    public ActionDefinitionAttribute(AccessOperation operation, string specialCode,string definition)
    {
        Operation = operation;
        Definition = definition;
        if (Operation == AccessOperation.Special)
            specialCode = string.Empty;
        SpecialCode = specialCode;
    }

    /// <summary>
    /// Operation is AccessOperation.Special.
    /// </summary>
    /// <param name="specialCode">string | special code using in access code generate</param>
    public ActionDefinitionAttribute(string specialCode)
    {
        SpecialCode = specialCode;
        Operation = AccessOperation.Special;
        Definition = string.Empty;
    } 
    
    /// <summary>
    /// Operation is AccessOperation.Special
    /// </summary>
    /// <param name="specialCode">string | special code using in access code generate</param>
    /// <param name="definition">Auction definition string </param>
    public ActionDefinitionAttribute(string specialCode,string definition)
    {
        SpecialCode = specialCode;
        Operation = AccessOperation.Special;
        Definition = definition;
    }  
    
    public string Definition { get; private set; }
    public AccessOperation Operation { get; private set; }
    public string SpecialCode { get; private set; }

}