---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Set-KustoBatchingPolicy.md
schema: 2.0.0
---

# Set-KustoBatchingPolicy

## SYNOPSIS

Sets ingestion batching policy on a Kusto Database or Table.

## SYNTAX

```powershell
Set-KustoBatchingPolicy
    [[-Table] <String>]
    [[-Database] <String>]
    [-MaximumBatchingTimeSpan <TimeSpan>]
    [-MaximumNumberOfItems <Int32>]
    [-MaximumRawDataSizeMB <Int32>]
    [-OutputType <OutputType>]
    [-RequestProperties <ClientRequestProperties>]
    [<CommonParameters>]
```

## DESCRIPTION

The `Set-KustoBatchingPolicy` cmdlet is used to alter the batching policy of a Database or specific Table on an Azure Data Explorer Cluster. See [__Ingestion batching policy__](https://learn.microsoft.com/en-us/kusto/management/batching-policy?view=microsoft-fabric) for more details.

## EXAMPLES

### Example 1: Set new ingestion batching policy for a Table

```powershell
$params = @{
    MaximumBatchingTimeSpan = '00:00:30'
    MaximumNumberOfItems    = 500
    MaximumRawDataSizeMB    = 1024
}
Set-KustoBatchingPolicy -Table myTable -Database myDb @params
```

This example alters the ingestion batching policy of `myTable` on `myDb` Database.

### Example 2: Set new ingestion batching policy for a Database

```powershell
$params = @{
    MaximumBatchingTimeSpan = '00:00:30'
    MaximumNumberOfItems    = 500
    MaximumRawDataSizeMB    = 1024
}
Set-KustoBatchingPolicy -Database myDb @params
```

> [!TIP]
>
> When `-Table` isn't specified, the ingestion batching policy is altered at Database level.

## PARAMETERS

### -Database

Specifies the name of the Database for which to alter the ingestion batching policy.

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

### -MaximumBatchingTimeSpan

The time limit after which a batch is sealed. __The default value is 5 minutes__.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: [timespan] '00:05:00'
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaximumNumberOfItems

The number of files defined as the limit after which a batch is sealed. This setting should only be set in scenarios where you can control the data units, such as blobs or files. In message-based scenarios, such as Event Hubs, IoT Hub, and Azure Cosmos DB change feed, consider using the Time and Size settings to control batching. __The default value is 500 items__.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 500
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaximumRawDataSizeMB

The size limit after which a batch is sealed. __The default value is 1024 MB__.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 1024
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

### -Table

Specifies the Table for which to alter the ingestion batching policy.

> [!NOTE]
>
> This parameter is optional. If not specified, the ingestion batching policy is altered on Database level.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
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

## NOTES

## RELATED LINKS

[__Ingestion batching policy__](https://learn.microsoft.com/en-us/kusto/management/batching-policy?view=microsoft-fabric)

[__.alter database policy ingestionbatching command__](https://learn.microsoft.com/en-us/kusto/management/alter-database-ingestion-batching-policy?view=microsoft-fabric)

[__Create a table's ingestion batching policy with the table batching policy wizard__](https://docs.azure.cn/en-us/data-explorer/table-batching-policy-wizard)
