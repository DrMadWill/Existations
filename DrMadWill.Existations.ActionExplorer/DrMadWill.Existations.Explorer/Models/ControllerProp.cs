namespace DrMadWill.Extensions.Explorer.Models;

public class ControllerProp
{
    public string Controller { get; set; }
    public IList<ActionProp> Actions { get; set; }
    public IList<ControllerAccessProp> Accesses { get; set; }

    public ControllerProp(string controller, IList<ActionProp> actions,IList<ControllerAccessProp> accesses)
    {
        Controller = controller;
        Actions = actions;
        Accesses = accesses;
    }

    public ControllerProp()
    {
        Actions = new List<ActionProp>();
        Accesses = new List<ControllerAccessProp>();
    }
}

