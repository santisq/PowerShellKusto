using System.Management.Automation;

namespace PowerShellKusto;

[Cmdlet(VerbsCommunications.Connect, "Kusto")]
public sealed class ConnectKustoCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Cluster { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    public string Database { get; set; } = null!;

    [Parameter(Mandatory = true, ParameterSetName = "Credential")]
    [Credential]
    public PSCredential Credential { get; set; } = null!;

    [Parameter(Mandatory = true, ParameterSetName = "Credential")]
    public Guid TenantId { get; set; }

    [Parameter(ParameterSetName = "Identity")]
    public SwitchParameter Identity { get; set; }

    protected override void EndProcessing()
    {

    }
}
