using System.Management.Automation;

internal sealed class DynamicParamExcludeHeader
{
    [Parameter]
    public SwitchParameter ExcludeHeaders { get; set; }
}

internal sealed class DynamicParamTitle
{
    [Parameter(Mandatory = true)]
    public string Title { get; set; } = null!;
}
