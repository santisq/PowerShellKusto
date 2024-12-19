using System;
using System.ComponentModel;
using System.Management.Automation;
using Kusto.Data.Common;
using Kusto.Ingest;

namespace PowerShellKusto;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class KustoIngestionCommandBase : KustoCommandBase
{
    private KustoIngestionProperties? _ingestionProperties;

    private IngestClientOptions? _ingestionOptions;

    protected KustoIngestionProperties? IngestionProperties { get => _ingestionProperties; }

    protected IngestClientOptions? IngestionOptions { get => _ingestionOptions; }

    [Parameter(Mandatory = true, Position = 1)]
    public string Table { get; set; } = null!;

    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public IngestionMapping? Mapping { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public DataSourceFormat Format { get; set; } = DataSourceFormat.csv;

    [Parameter]
    public SwitchParameter IgnoreFirstRecord { get; set; }

    [Parameter]
    [ValidateRange(1, int.MaxValue)]
    public int MaxRetries { get; set; } = 3;

    [Parameter]
    [ValidateTimespan]
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);

    protected override void BeginProcessing()
    {
        base.BeginProcessing();

        _ingestionProperties = new KustoIngestionProperties(
            databaseName: Database ?? Builder?.InitialCatalog,
            tableName: Table)
        {
            Format = Format,
            IgnoreFirstRecord = IgnoreFirstRecord,
        };

        if (Mapping is not null)
        {
            _ingestionProperties.IngestionMapping = Mapping;
        }

        _ingestionOptions = new IngestClientOptions()
        {
            MaxServiceCallsRetries = MaxRetries,
            MaxStorageRetries = MaxRetries,
            ServiceCallsRetryDelay = RetryDelay
        };
    }
}
