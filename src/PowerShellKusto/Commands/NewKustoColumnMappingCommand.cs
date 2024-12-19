using System.Collections;
using System.Management.Automation;
using Kusto.Data.Common;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommon.New, "KustoColumnMapping")]
[OutputType(typeof(ColumnMapping))]
public sealed class NewKustoColumnMappingCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Name { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public ColumnType? Type { get; set; }

    [Parameter(Position = 2)]
    [ValidateNotNullOrEmpty]
    public Hashtable? Properties { get; set; }

    protected override void EndProcessing()
    {
        ColumnMapping column = new()
        {
            ColumnName = Name,
            ColumnType = Type.ToString()
        };

        if (Properties is not null)
        {
            column.Properties = Properties.ToDictionary();
        }

        WriteObject(column);
    }
}
