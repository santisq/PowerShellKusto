using System;
using System.Data;
using System.Management.Automation;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using Kusto.Ingest;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommon.Set, "KustoIngestionMapping")]
[OutputType(typeof(PSObject), typeof(string), typeof(DataTable))]
public sealed class SetKustoIngestionMappingCommand : KustoReaderCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    public string MappingName { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    public string Table { get; set; } = null!;

    [Parameter(Position = 2)]
    public string? Database { get; set; }

    [Parameter(Mandatory = true)]
    public IngestionMapping IngestionMapping { get; set; } = null!;

    [Parameter]
    public SwitchParameter RemoveOldestIfRequired { get; set; }

    protected override void EndProcessing()
    {
        try
        {
            string? database = Database ?? Builder?.InitialCatalog;
            using ICslAdminProvider client = KustoClientFactory.CreateCslAdminProvider(Builder);
            string command = CslCommandGenerator.GenerateTableMappingCreateCommand(
                mappingKind: IngestionMapping.IngestionMappingKind,
                entityName: Table,
                mappingName: MappingName,
                mapping: IngestionMapping.IngestionMappings,
                removeOldestIfRequired: RemoveOldestIfRequired);

            using IDataReader reader = RequestProperties is null
                ? client.ExecuteControlCommand(database, command)
                : client.ExecuteControlCommand(database, command, RequestProperties);

            HandleReader(reader);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "CommandError", ErrorCategory.NotSpecified, null);
            WriteError(error);
        }
    }
}
