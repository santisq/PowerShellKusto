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
    public string Name { get; set; } = null!;

    [Parameter(Position = 2)]
    public string? Table { get; set; }

    [Parameter(Mandatory = true)]
    public IngestionMapping Mapping { get; set; } = null!;

    [Parameter]
    public SwitchParameter Force { get; set; }

    protected override void EndProcessing()
    {
        try
        {
            Database ??= Builder?.InitialCatalog;
            string command = Table is null
                ? GetDatabaseCommand()
                : GetTableCommand();

            using ICslAdminProvider client = KustoClientFactory.CreateCslAdminProvider(Builder);
            using IDataReader reader = RequestProperties is null
                ? client.ExecuteControlCommand(Database, command)
                : client.ExecuteControlCommand(Database, command, RequestProperties);

            HandleReader(reader);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "CommandError", ErrorCategory.NotSpecified, null);
            WriteError(error);
        }
    }

    private string GetTableCommand() => Force
        ? CslCommandGenerator.GenerateTableMappingCreateOrAlterCommand(
            mappingKind: Mapping.IngestionMappingKind,
            tableName: Table,
            mappingName: Name,
            mapping: Mapping.IngestionMappings)
        : CslCommandGenerator.GenerateTableMappingCreateCommand(
            mappingKind: Mapping.IngestionMappingKind,
            entityName: Table,
            mappingName: Name,
            mapping: Mapping.IngestionMappings);

    private string GetDatabaseCommand() => Force
        ? CslCommandGenerator.GenerateDatabaseMappingCreateOrAlterCommand(
            mappingKind: Mapping.IngestionMappingKind,
            entityName: Database,
            mappingName: Name,
            mapping: Mapping.IngestionMappings)
        : CslCommandGenerator.GenerateDatabaseMappingCreateCommand(
            mappingKind: Mapping.IngestionMappingKind,
            entityName: Database,
            mappingName: Name,
            mapping: Mapping.IngestionMappings);
}
