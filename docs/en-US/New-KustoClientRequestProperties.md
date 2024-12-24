---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/New-KustoClientRequestProperties.md
schema: 2.0.0
---

# New-KustoClientRequestProperties

## SYNOPSIS

Creates a new `ClientRequestProperties` object.

## SYNTAX

```powershell
New-KustoClientRequestProperties
    [-Application <String>]
    [-User <String>]
    [-ClientRequestId <String>]
    [-Parameters <Hashtable>]
    [-Options <Hashtable>]
    [-NoTruncation]
    [-ServerTimeout <TimeSpan>]
    [<CommonParameters>]
```

## DESCRIPTION

The `New-KustoClientRequestProperties` cmdlet can be used to create a new `ClientRequestProperties` object
to manage the interaction between client and service. This object can be later on passed as argument to the request cmdlets: [`Invoke-KustoControlCommand`](Invoke-KustoControlCommand.md), [`Invoke-KustoQuery`](Invoke-KustoQuery.md),
[`Set-KustoBatchingPolicy`](Set-KustoBatchingPolicy.md) and [`Set-KustoIngestionMapping`](Set-KustoIngestionMapping.md).

The object contains the following information:

- [Request properties](https://learn.microsoft.com/en-us/kusto/api/netfx/client-request-properties?view=microsoft-fabric#request-properties): A mapping of specific options for customizing request behavior.
- [Query parameters](https://learn.microsoft.com/en-us/kusto/api/netfx/client-request-properties?view=microsoft-fabric#query-parameters): A mapping of user-declared parameters that allow for secure query customization.
- [Named properties](https://learn.microsoft.com/en-us/kusto/api/netfx/client-request-properties?view=microsoft-fabric#named-properties): Client request ID, application details, and user data, primarily used for debugging and tracing.

## EXAMPLES

### Example 1: Run a KQL Query with specified request properties

```powershell
$requestProps = New-KustoClientRequestProperties -NoTruncation -ServerTimeout '00:05:00'
Invoke-KustoQuery 'myTable | project fooProp, barProp' -RequestProperties $requestProps
```

This example demonstrates how you can specify request properties for your query.

- In this case `-NoTruncation` is particularly useful to overcome the 500k row limit on your query.
- `-ServerTimeout '00:05:00'` allows the query to run for at least 5 minutes before timing out.

### Example 2: Create a new `ClientRequestProperties` object with supported request properties

```powershell
$requestProps = New-KustoClientRequestProperties -Options @{
    query_language   = 'sql'
    request_app_name = 'myApp'
    norequesttimeout = $true
}
Invoke-KustoQuery 'SELECT top(10) * FROM MyTable' -RequestProperties $requestProps
```

> [!TIP]
>
> Out of the supported request properties, this cmdlet offers `-NoTruncation` (`notruncation`) and `-ServerTimeout` (`servertimeout`) as a Parameter.
> The `-Options` Parameter is used to add extra options to your request that aren't by default available.  
> See [__Supported request properties__](https://learn.microsoft.com/en-us/kusto/api/rest/request-properties?view=microsoft-fabric#supported-request-properties) for the additional options.

## PARAMETERS

### -Application

The name of the client application that makes the request. This value is used for tracing.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientRequestId

An ID used to identify the request. This specification is helpful for debugging and may be required for specific scenarios like query cancellation.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoTruncation

Disables truncation of query results returned to the caller.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options

Specifies a Hashtable of additional [Supported request properties](https://learn.microsoft.com/en-us/kusto/api/rest/request-properties?view=microsoft-fabric#supported-request-properties) that aren't by default available as a Parameter.

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parameters

Specifies a Hashtable of [query parameters declaration statement](https://learn.microsoft.com/en-us/kusto/query/query-parameters-statement?view=microsoft-fabric) that are used to declare parameters for a Kusto Query Language (KQL) query.

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerTimeout

Overrides the default request timeout.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User

The identity of the user that makes the request. This value is used for tracing.

```yaml
Type: String
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

### Kusto.Data.Common.ClientRequestProperties

## NOTES

## RELATED LINKS

[Kusto Data ClientRequestProperties class](https://learn.microsoft.com/en-us/kusto/api/netfx/client-request-properties?view=microsoft-fabric)

[Query parameters declaration statement](https://learn.microsoft.com/en-us/kusto/query/query-parameters-statement?view=microsoft-fabric)

[Request properties](https://learn.microsoft.com/en-us/kusto/api/rest/request-properties?view=microsoft-fabric)
