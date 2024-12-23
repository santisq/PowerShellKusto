---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/New-KustoIngestionMapping.md
schema: 2.0.0
---

# New-KustoColumnMapping

## SYNOPSIS

Creates a `ColumnMapping` object.

## SYNTAX

```powershell
New-KustoColumnMapping
    [-Name] <String>
    [[-Type] <ColumnType>]
    [[-Properties] <Hashtable>]
    [<CommonParameters>]
```

## DESCRIPTION

The `New-KustoColumnMapping` cmdlet is used to create a new object of type `ColumnMapping`, this object can be later on passed as argument to the [`New-KustoIngestionMapping`](New-KustoIngestionMapping.md) cmdlet. See [__Ingestion mappings__](https://learn.microsoft.com/en-us/kusto/management/mappings?view=microsoft-fabric) for more details.

## EXAMPLES

### Example 1: CSV mapping

```powershell
$columns = @(
    New-KustoColumnMapping event_time -Properties @{ Ordinal = 0 }
    New-KustoColumnMapping event_name -Properties @{ Ordinal = 1 }
    New-KustoColumnMapping event_type -Properties @{ Ordinal = 2 }
    New-KustoColumnMapping ingestion_time -Properties @{ ConstValue = '2023-01-01T10:32:00' }
    New-KustoColumnMapping source_location -Properties @{ Transform = 'SourceLocation' })
```

Demonstrates how to create columns for a new [__CSV mapping__](https://learn.microsoft.com/en-us/kusto/management/csv-mapping?view=microsoft-fabric).

### Example 2: JSON Mapping

```powershell
$columns = @(
    New-KustoColumnMapping event_timestamp -Properties @{Path = '$.Timestamp' }
    New-KustoColumnMapping event_name -Properties @{ Path = '$.Event.Name' }
    New-KustoColumnMapping event_type -Properties @{ Path = '$.Event.Type' }
    New-KustoColumnMapping source_uri -Properties @{ Transform = 'SourceLocation' }
    New-KustoColumnMapping source_line -Properties @{ Transform = 'SourceLineNumber' }
    New-KustoColumnMapping event_time -Properties @{ Path = '$.Timestamp'; Transform = 'DateTimeFromUnixMilliseconds' }
    New-KustoColumnMapping ingestion_time -Properties @{ ConstValue = '2021-01-01T10:32:00' }
    New-KustoColumnMapping full_record -Properties @{ Path = '$' })
```

Demonstrates how to create columns for a new [__JSON mapping__](https://learn.microsoft.com/en-us/kusto/management/json-mapping?view=microsoft-fabric).

## PARAMETERS

### -Name

Specifies the Target column name in the table.

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

### -Properties

Property-bag containing properties specific for each mapping as described in each specific mapping type page. 

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type

Specifies the Datatype with which to create the mapped column if it doesn't already exist in the table.

```yaml
Type: ColumnType
Parameter Sets: (All)
Aliases:
Accepted values: bool, datetime, decimal, dynamic, guid, int, long, double, string, timespan

Required: False
Position: 1
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

### Kusto.Data.Common.ColumnMapping

## NOTES

## RELATED LINKS

[__Ingestion mappings__](https://learn.microsoft.com/en-us/kusto/management/mappings?view=microsoft-fabric)
