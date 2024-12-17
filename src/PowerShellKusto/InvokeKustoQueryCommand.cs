using System;
using System.Data;
using System.Management.Automation;
using Kusto.Cloud.Platform.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using Newtonsoft.Json.Linq;

namespace PowerShellKusto;

[Cmdlet(VerbsLifecycle.Invoke, "KustoQuery")]
[OutputType(typeof(string), ParameterSetName = [AsJsonSet])]
[OutputType(typeof(PSObject))]
public sealed class InvokeKustoQueryCommand : PSCmdlet
{
    private const string AsJsonSet = "AsJson";

    [Parameter(Mandatory = true, Position = 0)]
    public string Query { get; set; } = null!;

    [Parameter(ParameterSetName = AsJsonSet)]
    public SwitchParameter AsJson { get; set; }

    protected override void EndProcessing()
    {
        KustoConnectionDetails connection = ConnectKustoCommand.GetConnectionDetails(this);

        try
        {
            using ICslQueryProvider provider = KustoClientFactory.CreateCslQueryProvider(connection.Builder);
            using IDataReader reader = provider.ExecuteQuery(Query, connection.Properties);

            if (AsJson.IsPresent)
            {
                WriteObject(reader.ToJsonString());
                return;
            }

            foreach (JObject jObject in reader.ToJObjects())
            {
                WriteObject(jObject.ToPSObject());
            }
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "QueryError", ErrorCategory.ReadError, null);
            WriteError(error);
        }
    }
}
