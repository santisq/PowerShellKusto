using System;
using System.Data;
using System.Management.Automation;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsLifecycle.Invoke, "KustoQuery", DefaultParameterSetName = AsJsonSet)]
[OutputType(typeof(string), ParameterSetName = [AsJsonSet, AsCsvSet])]
[OutputType(typeof(DataSet), ParameterSetName = [AsDataSetSet])]
[OutputType(typeof(PSObject))]
public sealed class InvokeKustoQueryCommand : KustoReaderCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Query { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    protected override void EndProcessing()
    {
        try
        {
            using ICslQueryProvider provider = KustoClientFactory.CreateCslQueryProvider(Builder);
            using IDataReader reader = Database is not null
                ? provider.ExecuteQuery(Database, Query, Properties)
                : provider.ExecuteQuery(Query, Properties);

            HandleReader(reader);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "QueryError", ErrorCategory.ReadError, null);
            WriteError(error);
        }
    }
}
