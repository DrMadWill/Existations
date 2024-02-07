namespace DrMadWill.Extensions.Explorer.Models;

public class ActionList
{
    public string Controller { get; set; }
    public IList<ActionProp> Actions { get; set; }
    public IList<ControllerAccessProp> Accesses { get; set; }

    public ActionList(string controller, IList<ActionProp> actions,IList<ControllerAccessProp> accesses)
    {
        Controller = controller;
        Actions = actions;
        Accesses = accesses;
    }

    public ActionList()
    {
        Actions = new List<ActionProp>();
        Accesses = new List<ControllerAccessProp>();
    }
}

