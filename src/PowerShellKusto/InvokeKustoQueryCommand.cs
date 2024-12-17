using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Kusto.Cloud.Platform.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using Microsoft.PowerShell.Commands;
using Newtonsoft.Json.Linq;

namespace PowerShellKusto;

[Cmdlet(VerbsLifecycle.Invoke, "KustQuery", DefaultParameterSetName = ToJsonSet)]
[OutputType(typeof(string), ParameterSetName = [ToJsonSet])]
[OutputType(typeof(PSObject), ParameterSetName = [ToEnumerableSet])]
public sealed class InvokeKustoQueryCommand : PSCmdlet
{
    private const string ToEnumerableSet = "ToEnumerable";

    private const string ToJsonSet = "ToJson";

    [ThreadStatic]
    private static MethodInfo? s_methodInfo;

    [Parameter(Mandatory = true, Position = 0)]
    public string Query { get; set; } = null!;

    [Parameter(ParameterSetName = ToEnumerableSet)]
    public SwitchParameter ToEnumerableArray { get; set; }

    [Parameter(ParameterSetName = ToEnumerableSet)]
    [ValidateNotNull]
    public Type? OutputType { get; set; }

    [Parameter(ParameterSetName = ToJsonSet)]
    public SwitchParameter ToJson { get; set; }

    protected override void EndProcessing()
    {
        KustoConnectionDetails connection = ConnectKustoCommand.GetConnectionDetails(this);

        try
        {
            using ICslQueryProvider provider = KustoClientFactory.CreateCslQueryProvider(connection.Builder);
            using IDataReader reader = provider.ExecuteQuery(Query, connection.Properties);

            if (ToJson.IsPresent)
            {
                WriteObject(reader.ToJsonString());
                return;
            }

            if (!ToEnumerableArray.IsPresent)
            {
                IEnumerable<JObject> jObjects = reader.ToJObjects();
            }

            if (OutputType is null)
            {
                WriteObject(
                    reader.ToEnumerableObjectArray(true),
                    enumerateCollection: true);

                return;
            }

            foreach (object item in ToEnumerable(OutputType, reader))
            {
                WriteObject(item);
            }

        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "QueryError", ErrorCategory.ReadError, null);
            WriteError(error);
        }
    }

    private static IEnumerable ToEnumerable(
        Type outputType,
        IDataReader reader)
    {
        s_methodInfo ??= typeof(ExtendedDataReader).GetMethod(
            name: "ConvertTo",
            genericParameterCount: 1,
            types: [typeof(IDataReader)]);

        object? enumerable = s_methodInfo!
            .MakeGenericMethod(outputType)
            .Invoke(null, [reader]);

        return enumerable is null
            ? Enumerable.Empty<object>()
            : (IEnumerable)enumerable;
    }
}
