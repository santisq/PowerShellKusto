using System.Management.Automation;

internal sealed class DynamicParamNoHeader
{
    [Parameter]
    public SwitchParameter NoHeader { get; set; }
}

internal sealed class DynamicParamTitle
{
    [Parameter(Mandatory = true)]
    public string Title { get; set; } = null!;
}
