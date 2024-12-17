using System.ComponentModel;
using System.Data;
using System.Management.Automation;
using Kusto.Cloud.Platform.Data;
using Kusto.Data.Common;

namespace PowerShellKusto;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class KustoReaderCommandBase : KustoCommandBase
{
    protected const string AsJsonSet = "AsJson";

    protected const string AsCsvSet = "AsCsv";

    protected const string AsDataSetSet = "AsDataSet";

    [Parameter(ParameterSetName = AsJsonSet)]
    public SwitchParameter AsJson { get; set; }

    [Parameter(ParameterSetName = AsCsvSet)]
    public SwitchParameter AsCsv { get; set; }

    [Parameter(ParameterSetName = AsCsvSet)]
    public SwitchParameter ExcludeHeaders { get; set; }

    [Parameter(ParameterSetName = AsDataSetSet)]
    public SwitchParameter AsDataSet { get; set; }

    protected void HandleReader(IDataReader reader)
    {
        switch (ParameterSetName)
        {
            case AsJsonSet:
                WriteObject(reader.ToJsonString());
                return;

            case AsCsvSet:
                WriteObject(reader.ToCsvString(!ExcludeHeaders.IsPresent));
                return;

            case AsDataSetSet:
                WriteObject(reader.ToDataSet().Tables[0]);
                return;

            default:
                WriteObject(
                    reader.ToEnumerablePSObject(),
                    enumerateCollection: true);
                return;
        }
    }
}
