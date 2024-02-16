using DrMadWill.Extensions.Explorer.Enums;

namespace DrMadWill.Extensions.Explorer.Models;

public record ControllerAccessProp(string Controller,AccessOperation Operation,string Code);