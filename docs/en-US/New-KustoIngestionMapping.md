---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/New-KustoIngestionMapping.md
schema: 2.0.0
---

# New-KustoIngestionMapping

## SYNOPSIS

Creates a `IngestionMapping` object.

## SYNTAX

```powershell
New-KustoIngestionMapping
    [[-Kind] <IngestionMappingKind>]
    [-Reference <String>]
    [[-Columns] <ColumnMapping[]>]
    [<CommonParameters>]
```

## DESCRIPTION

The `New-KustoIngestionMapping` is used to create a new object of type `IngestionMapping` that can be later on passed as argument to the [`Invoke-KustoIngestFromStorage`](Invoke-KustoIngestFromStorage.md), [`Invoke-KustoIngestFromStream`](Invoke-KustoIngestFromStream.md) and [`Set-KustoIngestionMapping`](Set-KustoIngestionMapping.md) commands.

## EXAMPLES

### Example 1: Create a new Ingestion Mapping object

```powershell
$columns = @(
    New-KustoColumnMapping ....
    New-KustoColumnMapping ....
    New-KustoColumnMapping ....)

$mapping = New-KustoIngestionMapping -Columns $columns -Kind Json
```

## PARAMETERS

### -Columns

Specifies an array of `ColumnMapping` objects to be used with the ingestion mapping. See [`New-KustoColumnMapping`](New-KustoColumnMapping.md) for details on how to create new column mappings.

```yaml
Type: ColumnMapping[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Kind

Specifies the type of mapping. __Default value is `Csv`__.

```yaml
Type: IngestionMappingKind
Parameter Sets: (All)
Aliases:
Accepted values: Unknown, Csv, Json, Avro, Parquet, SStream, Orc, ApacheAvro, W3CLogFile

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Reference

A value that indicates how to map data from the source file to the actual columns in the table using a named mapping policy object.

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

### Kusto.Ingest.IngestionMapping

## NOTES

## RELATED LINKS

[.create ingestion mapping command](https://learn.microsoft.com/en-us/kusto/management/create-ingestion-mapping-command?view=azure-data-explorer&preserve-view=true)

[Ingestion properties](https://learn.microsoft.com/en-us/kusto/ingestion-properties?view=microsoft-fabric#ingestion-properties)
