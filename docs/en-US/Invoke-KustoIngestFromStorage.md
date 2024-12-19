---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version:
schema: 2.0.0
---

# Invoke-KustoIngestFromStorage

## SYNOPSIS

{{ Fill in the Synopsis }}

## SYNTAX

```powershell
Invoke-KustoIngestFromStorage
    [-Path] <String>
    [-Table] <String>
    [-Database <String>]
    [-Mapping <IngestionMapping>]
    [-Format <DataSourceFormat>]
    [-IgnoreFirstRecord]
    [-MaxRetries <Int32>]
    [-RetryDelay <TimeSpan>]
    [<CommonParameters>]
```

## DESCRIPTION

{{ Fill in the Description }}

## EXAMPLES

### Example 1

```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Database

{{ Fill Database Description }}

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

### -Format

{{ Fill Format Description }}

```yaml
Type: DataSourceFormat
Parameter Sets: (All)
Aliases:
Accepted values: csv, tsv, scsv, sohsv, psv, txt, raw, tsve, w3clogfile, apacheavro, orc, sstream, parquet, avro, multijson, singlejson, json

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreFirstRecord

{{ Fill IgnoreFirstRecord Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Mapping

{{ Fill Mapping Description }}

```yaml
Type: IngestionMapping
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxRetries

{{ Fill MaxRetries Description }}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

{{ Fill Path Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases: Uri

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RetryDelay

{{ Fill RetryDelay Description }}

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

### -Table

{{ Fill Table Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProgressAction

{{ Fill ProgressAction Description }}

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

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

### Kusto.Ingest.IKustoIngestionResult

## NOTES

## RELATED LINKS
