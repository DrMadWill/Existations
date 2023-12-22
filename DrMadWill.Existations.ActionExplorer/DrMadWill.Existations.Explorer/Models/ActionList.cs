namespace DrMadWill.Extensions.Explorer.Models;

public class ActionList
{
    public string Controller { get; set; }
    public IList<ActionProp> SystemsActions { get; set; } = new List<ActionProp>();
    public IList<ActionProp> ExternalActions { get; set; } = new List<ActionProp>();
    public IList<ActionProp> UserActions { get; set; } = new List<ActionProp>();

    public IList<ActionProp> AllAction => SystemsActions.Concat(ExternalActions).Concat(UserActions).ToList();
}

