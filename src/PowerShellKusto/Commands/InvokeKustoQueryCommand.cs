using System;
using System.Data;
using System.Management.Automation;
using Kusto.Cloud.Platform.Data;
using Kusto.Cloud.Platform.Utils;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsLifecycle.Invoke, "KustoQuery")]
[OutputType(typeof(string), ParameterSetName = [AsJsonSet, AsCsvSet])]
[OutputType(typeof(PSObject))]
public sealed class InvokeKustoQueryCommand : PSCmdlet
{
    private const string AsJsonSet = "AsJson";

    private const string AsCsvSet = "AsCsv";

    [Parameter(Mandatory = true, Position = 0)]
    public string Query { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    [Parameter(ParameterSetName = AsJsonSet)]
    public SwitchParameter AsJson { get; set; }

    [Parameter(ParameterSetName = AsCsvSet)]
    public SwitchParameter AsCsv { get; set; }

    [Parameter(ParameterSetName = AsCsvSet)]
    public SwitchParameter ExcludeHeaders { get; set; }

    protected override void EndProcessing()
    {
        KustoConnectionDetails connection = ConnectKustoCommand.GetConnectionDetails(this);
        (KustoConnectionStringBuilder builder, ClientRequestProperties properties) = connection;

        try
        {
            using ICslQueryProvider provider = KustoClientFactory.CreateCslQueryProvider(builder);
            using IDataReader reader = Database is not null
                ? provider.ExecuteQuery(Database, Query, properties)
                : provider.ExecuteQuery(Query, properties);

            switch (ParameterSetName)
            {
                case AsJsonSet:
                    WriteObject(reader.ToJsonString());
                    return;

                case AsCsvSet:
                    WriteObject(reader.ToCsvString(!ExcludeHeaders.IsPresent));
                    return;

                default:
                    WriteObject(
                        reader.ToEnumerablePSObject(),
                        enumerateCollection: true);
                    return;
            }
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "QueryError", ErrorCategory.ReadError, null);
            WriteError(error);
        }
    }
}
