
using DrMadWill.Extensions.Explorer.Enums;

namespace DrMadWill.Extensions.Explorer.Models;

public record ActionProp(string Controller,string Action,string Type,AccessOperation? Operation,string Definition);