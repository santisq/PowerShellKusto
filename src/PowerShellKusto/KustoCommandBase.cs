using System.ComponentModel;
using System.Management.Automation;
using Kusto.Data;
using PowerShellKusto.Commands;

namespace PowerShellKusto;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class KustoCommandBase : PSCmdlet
{
    protected KustoConnectionStringBuilder? Builder { get; private set; }

    protected override void BeginProcessing()
    {
        Builder = ConnectKustoCommand.GetConnectionBuilder(this);
    }
}
