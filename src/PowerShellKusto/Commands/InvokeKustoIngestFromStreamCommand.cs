using System;
using System.IO;
using System.Management.Automation;
using Kusto.Ingest;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsLifecycle.Invoke, "KustoIngestFromStream")]
[OutputType(typeof(IKustoIngestionResult))]
public sealed class InvokeKustoIngestFromStreamCommand : KustoIngestionCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    public Stream Stream { get; set; } = null!;

    [Parameter]
    public SwitchParameter LeaveOpen { get; set; }

    protected override void EndProcessing()
    {
        try
        {
            using IKustoIngestClient client = KustoIngestFactory.CreateDirectIngestClient(
                kcsb: Builder,
                autoCorrectEndpoint: true,
                ingestClientOptions: IngestionOptions);

            IKustoIngestionResult result = client.IngestFromStream(
                stream: Stream,
                ingestionProperties: IngestionProperties);

            WriteObject(result);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "IngestionError", ErrorCategory.WriteError, null);
            WriteError(error);
        }
    }
}
