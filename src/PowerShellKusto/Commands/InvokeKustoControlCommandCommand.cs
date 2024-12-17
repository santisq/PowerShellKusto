using System;
using System.Data;
using System.Management.Automation;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsLifecycle.Invoke, "KustoControlCommand")]
[OutputType(typeof(string), ParameterSetName = [AsJsonSet, AsCsvSet])]
[OutputType(typeof(DataSet), ParameterSetName = [AsDataSetSet])]
[OutputType(typeof(PSObject))]
public sealed class InvokeKustoControlCommandCommand : KustoReaderCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Command { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    protected override void EndProcessing()
    {
        try
        {
            using ICslAdminProvider provider = KustoClientFactory.CreateCslAdminProvider(Builder);
            using IDataReader reader = Database is not null
                ? provider.ExecuteControlCommand(Database, Command, Properties)
                : provider.ExecuteControlCommand(Command, Properties);

            HandleReader(reader);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "CommandError", ErrorCategory.SecurityError, null);
            WriteError(error);
        }
    }
}
