using System.Management.Automation;
using Kusto.Data.Common;
using Kusto.Data.Ingestion;
using Kusto.Ingest;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommon.New, "KustoIngestionMapping")]
[OutputType(typeof(IngestionMapping))]
public sealed class NewKustoIngestionMappingCommand : PSCmdlet
{
    [Parameter(Position = 0)]
    [ValidateNotNullOrEmpty]
    public ColumnMapping[]? Columns { get; set; }

    [Parameter(Position = 1)]
    public IngestionMappingKind Kind { get; set; } = IngestionMappingKind.Unknown;

    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? Reference { get; set; }

    protected override void EndProcessing()
    {
        IngestionMapping mapping = new()
        {
            IngestionMappingKind = Kind
        };

        if (Reference is not null)
        {
            mapping.IngestionMappingReference = Reference;
        }

        if (Columns is not null)
        {
            mapping.IngestionMappings = Columns;
        }

        WriteObject(mapping);
    }
}
