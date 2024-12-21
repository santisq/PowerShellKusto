using System;
using System.Data;
using System.Management.Automation;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommon.Set, "KustoBatchingPolicy")]
[OutputType(typeof(PSObject), typeof(string), typeof(DataTable))]
public sealed class SetKustoBatchingPolicyCommand : KustoReaderCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Table { get; set; } = null!;

    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    [Parameter]
    [ValidateTimespan]
    public TimeSpan MaximumBatchingTimeSpan { get; set; } = TimeSpan.FromSeconds(10);

    [Parameter]
    [ValidateRange(1, int.MaxValue)]
    public int MaximumNumberOfItems { get; set; } = 100;

    [Parameter]
    [ValidateRange(1, int.MaxValue)]
    public int MaximumRawDataSizeMB { get; set; } = 1024;

    protected override void EndProcessing()
    {
        try
        {
            using ICslAdminProvider client = KustoClientFactory.CreateCslAdminProvider(Builder);
            string command = CslCommandGenerator.GenerateTableAlterIngestionBatchingPolicyCommand(
                databaseName: Database ?? Builder?.InitialCatalog,
                tableName: Table,
                ingestionBatchingPolicy: new IngestionBatchingPolicy(
                    maximumBatchingTimeSpan: MaximumBatchingTimeSpan,
                    maximumNumberOfItems: MaximumNumberOfItems,
                    maximumRawDataSizeMB: MaximumRawDataSizeMB));

            using IDataReader reader = RequestProperties is null
                ? client.ExecuteControlCommand(command)
                : client.ExecuteControlCommand(command, RequestProperties);

            HandleReader(reader);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "CommandError", ErrorCategory.NotSpecified, null);
            WriteError(error);
        }
    }
}
