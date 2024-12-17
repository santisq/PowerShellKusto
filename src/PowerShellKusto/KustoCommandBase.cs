using System.ComponentModel;
using System.Management.Automation;
using Kusto.Data;
using Kusto.Data.Common;
using PowerShellKusto.Commands;

namespace PowerShellKusto;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class KustoCommandBase : PSCmdlet
{
    private KustoConnectionDetails _connectionDetails;

    protected KustoConnectionStringBuilder Builder { get => _connectionDetails.Builder; }

    protected ClientRequestProperties Properties { get => _connectionDetails.Properties; }

    protected override void BeginProcessing()
    {
        _connectionDetails = ConnectKustoCommand.GetConnectionDetails(this);
    }
}
