using System.ComponentModel;
using System.Data;
using System.Management.Automation;
using Kusto.Cloud.Platform.Data;
using Kusto.Data.Common;

namespace PowerShellKusto;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class KustoReaderCommandBase : KustoCommandBase, IDynamicParameters
{
    private DynamicParamTitle? _titleParam;

    private DynamicParamExcludeHeader? _excludeHeaderParam;

    private string Title
    {
        get => _titleParam?.Title ?? string.Empty;
    }

    private bool ExcludeHeaders
    {
        get => _excludeHeaderParam is { ExcludeHeaders.IsPresent: not true };
    }

    [Parameter(Position = 1)]
    public OutputType OutputType { get; set; } = OutputType.PSObject;

    public object? GetDynamicParameters()
    {
        switch (OutputType)
        {
            case OutputType.Html:
                _titleParam = new DynamicParamTitle();
                return _titleParam;

            case OutputType.Csv:
                _excludeHeaderParam = new DynamicParamExcludeHeader();
                return _excludeHeaderParam;

            default:
                return null;
        }
    }

    protected void HandleReader(IDataReader reader)
    {
        switch (OutputType)
        {
            case OutputType.Json:
                WriteObject(reader.ToJsonString());
                return;

            case OutputType.Csv:
                WriteObject(reader.ToCsvString(ExcludeHeaders));
                return;

            case OutputType.DataTable:
                WriteObject(reader.ToDataTable());
                return;

            case OutputType.Html:
                WriteObject(reader.ToHtmlString(Title));
                return;

            default:
                WriteObject(
                    reader.ToEnumerablePSObject(),
                    enumerateCollection: true);
                return;
        }
    }
}
