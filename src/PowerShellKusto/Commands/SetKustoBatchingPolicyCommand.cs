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
    [Parameter(Position = 0)]
    [ValidateNotNullOrEmpty]
    public string? Table { get; set; }

    [Parameter]
    [ValidateTimespan]
    public TimeSpan MaximumBatchingTimeSpan { get; set; } = TimeSpan.FromMinutes(5);

    [Parameter]
    [ValidateRange(1, int.MaxValue)]
    public int MaximumNumberOfItems { get; set; } = 500;

    [Parameter]
    [ValidateRange(1, int.MaxValue)]
    public int MaximumRawDataSizeMB { get; set; } = 1024;

    protected override void EndProcessing()
    {
        try
        {
            Database ??= Builder?.InitialCatalog;
            IngestionBatchingPolicy policy = new(
                    maximumBatchingTimeSpan: MaximumBatchingTimeSpan,
                    maximumNumberOfItems: MaximumNumberOfItems,
                    maximumRawDataSizeMB: MaximumRawDataSizeMB);

            string command = Table is null
                ? CslCommandGenerator.GenerateDatabaseAlterIngestionBatchingPolicyCommand(
                    databaseName: Database,
                    ingestionBatchingPolicy: policy)
                : CslCommandGenerator.GenerateTableAlterIngestionBatchingPolicyCommand(
                    databaseName: Database,
                    tableName: Table,
                    ingestionBatchingPolicy: policy);

            using ICslAdminProvider client = KustoClientFactory.CreateCslAdminProvider(Builder);
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
