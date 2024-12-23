---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Invoke-KustoQuery.md
schema: 2.0.0
---

# Invoke-KustoQuery

## SYNOPSIS

Invokes a KQL or T-SQL query over an Azure Data Explorer Cluster.

## SYNTAX

```powershell
Invoke-KustoQuery
    [-Query] <String>
    [[-Database] <String>]
    [-OutputType <OutputType>]
    [-RequestProperties <ClientRequestProperties>]
    [<CommonParameters>]
```

## DESCRIPTION

The `Invoke-KustoQuery` cmdlet allows you to run
[Kusto Query Language (KQL)](https://learn.microsoft.com/en-us/kusto/query/?view=microsoft-fabric) queries or
[T-SQL](https://learn.microsoft.com/en-us/kusto/query/t-sql?view=microsoft-fabric) queries on the tables on a
specified Database of your Azure Data Explorer Cluster.

## EXAMPLES

### Example 1: Run a KQL Query

```powershell
Invoke-KustoQuery 'search * | summarize count() by $table'
```

This examples shows how to get the log count summarized by each table.

### Example 2: Run a KQL Query on specified Database

```powershell
Invoke-KustoQuery 'myTable | take 10' -Database myDb
```

### Example 3: Run a KQL Query with specified request properties

```powershell
$requestProps = New-KustoClientRequestProperties -NoTruncation -ServerTimeout '00:05:00'
Invoke-KustoQuery 'myTable | project fooProp, barProp' -RequestProperties $requestProps
```

This example demonstrates how you can specify request properties for your query.
See also [`New-KustoClientRequestProperties`](New-KustoClientRequestProperties.md) for more details.

- In this case `-NoTruncation` is particularly useful to overcome the 500k row limit on your query.
- `-ServerTimeout '00:05:00'` allows the query to run for at least 5 minutes before timing out.

## PARAMETERS

### -Database

This non mandatory parameter determines which Database in your Cluster will be targetted by your ingest command.

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
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query

The KQL or T-SQL query you want to run against a specified Database in your Cluster.

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

[Kusto Query Language (KQL)](https://learn.microsoft.com/en-us/kusto/query/?view=microsoft-fabric)

[T-SQL](https://learn.microsoft.com/en-us/kusto/query/t-sql?view=microsoft-fabric)

[Send T-SQL queries via the REST API](https://learn.microsoft.com/en-us/kusto/api/rest/t-sql?view=microsoft-fabric)
