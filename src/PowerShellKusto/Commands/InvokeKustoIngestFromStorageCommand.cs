using System;
using System.Management.Automation;
using Kusto.Ingest;
using Microsoft.PowerShell.Commands;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsLifecycle.Invoke, "KustoIngestFromStorage")]
[OutputType(typeof(IKustoIngestionResult))]
public sealed class InvokeKustoIngestFromStorageCommand : KustoIngestionCommandBase
{

    [Parameter(Mandatory = true, Position = 0)]
    [Alias("Uri")]
    public string Path { get; set; } = null!;

    protected override void EndProcessing()
    {
        Path = ResolvePath(Path);

        try
        {
            using IKustoIngestClient client = KustoIngestFactory.CreateDirectIngestClient(
                kcsb: Builder,
                autoCorrectEndpoint: true,
                ingestClientOptions: IngestionOptions);

            IKustoIngestionResult result = client.IngestFromStorage(
                uri: Path,
                ingestionProperties: IngestionProperties);

            WriteObject(result);
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(exception, "IngestionError", ErrorCategory.WriteError, null);
            WriteError(error);
        }
    }

    private string ResolvePath(string path)
    {
        path = SessionState.Path.GetUnresolvedProviderPathFromPSPath(
            path, out ProviderInfo provider, out _);

        ThrowIfInvalidProvider(path, provider);
        return path;
    }

    private void ThrowIfInvalidProvider(string path, ProviderInfo provider)
    {
        if (provider.ImplementingType == typeof(FileSystemProvider))
        {
            return;
        }

        ArgumentException exception = new(
            $"The resolved path '{path}' is not a FileSystem path but '{provider.Name}'.");
        ErrorRecord error = new(exception, "InvalidProvider", ErrorCategory.InvalidArgument, path);
        ThrowTerminatingError(error);
    }
}
