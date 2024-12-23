---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Invoke-KustoControlCommand.md
schema: 2.0.0
---

# Invoke-KustoControlCommand

## SYNOPSIS

Invokes management commands over an Azure Data Explorer Cluster.

## SYNTAX

```powershell
Invoke-KustoControlCommand
    [-Command] <String>
    [[-Database] <String>]
    [-OutputType <OutputType>]
    [-RequestProperties <ClientRequestProperties>]
    [<CommonParameters>]
```

## DESCRIPTION

The `Invoke-KustoControlCommand` cmdlet allows you to invoke management commands also known as control commands over an Azure Data Explorer Cluster. For detailed information on this topic check out [__Management commands overview__](https://learn.microsoft.com/en-us/kusto/management/?view=microsoft-fabric).

## EXAMPLES

### Example 1: Invoke a control command

```powershell
Invoke-KustoControlCommand '
.create table MyLogs(
    Level:string,
    Timestamp:datetime,
    UserId:string,
    TraceId:string,
    Message:string,
    ProcessId:int32)'
```

This example shows how to create a new Table on a database specified by [`Connect-Kusto`](Connect-Kusto.md).

### Example 2: Invoke a control command over a specified Database

```powershell
Invoke-KustoControlCommand -Database myDb '
.show tables details
| where TotalRowCount > 0
| extend TotalExtentSize = format_bytes(TotalExtentSize)
| extend TotalOriginalSize = format_bytes(TotalOriginalSize)
| extend HotExtentSize = format_bytes(HotExtentSize)
| extend HotOriginalSize = format_bytes(HotOriginalSize)
| project-away
    Folder,
    DocString,
    AuthorizedPrincipals,
    RetentionPolicy,
    CachingPolicy,
    ShardingPolicy,
    MergePolicy,
    StreamingIngestionPolicy,
    IngestionBatchingPolicy,
    RowOrderPolicy,
    TableId'
```

This example demonstrates how to get information on all tables in the `myDb` Database.

## PARAMETERS

### -Command

The control command to invoke.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Database

This non mandatory parameter determines which Database in your Cluster will be targetted by your control command.

> [!NOTE]
>
> If not supplied, the Database used will be the one specified when you called [`Connect-Kusto`](Connect-Kusto.md).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputType

Determines the output type this cmdlet will produce. __The default value is `PSObject`__.

```yaml
Type: OutputType
Parameter Sets: (All)
Aliases:
Accepted values: PSObject, Json, Csv, DataTable, Html

Required: False
Position: Named
Default value: PSObject
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequestProperties

Request properties control how a query or command executes and returns results. If no `ClientRequestProperties` object is supplied this cmdlet will use default properties.

> [!NOTE]
>
> You can create new request properties using [New-KustoClientRequestProperties](New-KustoClientRequestProperties.md).

```yaml
Type: ClientRequestProperties
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters

This cmdlet supports the common parameters.
For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Management.Automation.PSObject

### System.String

### System.Data.DataTable

The output type from this cmdlet is determined by the `-OutputType` parameter.
By default, this cmdlet outputs `PSObject`.

## NOTES

## RELATED LINKS

[__Management commands overview__](https://learn.microsoft.com/en-us/kusto/management/?view=microsoft-fabric)

[__Kusto Data ClientRequestProperties class__](https://learn.microsoft.com/en-us/kusto/api/netfx/client-request-properties?view=microsoft-fabric)

[__Request properties__](https://learn.microsoft.com/en-us/kusto/api/rest/request-properties?view=microsoft-fabric#supported-request-properties)
