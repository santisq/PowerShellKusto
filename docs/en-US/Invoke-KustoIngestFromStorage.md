---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Invoke-KustoIngestFromStorage.md
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

### Example 1: Ingest a local Csv file

```powershell
Invoke-KustoIngestFromStorage .\myCsvFile.csv -Table myTable -Database myDb -IgnoreFirstRecord
```

### Example 2: Ingest a Csv from Blob Storage

```powershell
$uri = 'https://myStorageAccount.blob.core.windows.net/my-container/myCsvFile.csv?sp=.....'
Invoke-KustoIngestFromStorage $uri -Table myTable -Database myDb -IgnoreFirstRecord
```

This example demonstrates how you can ingest into `myTable` directly from a Storage Account using a SAS Key and URI link.

### Example 3: Ingest a local Json

```powershell
# create a new Json to ingest
Get-Process | Select-Object Id, Name | ConvertTo-Json | Set-Content myJson.json
# create a new table
Invoke-KustoControlCommand '.create table MyJsonTable(Id:int, Name:string)' -Database myDb
# ingest the Json
Invoke-KustoIngestFromStorage .\myJson.json -Table MyJsonTable -Database myDb -Format multijson
```

> [!NOTE]
>
> The reason `multijson` format instead of `json` is used in this example is because a Json Array is considered as
> `multijson` whereas _JSON Lines_ corresponds to the `json` format. See [__The JSON format__](https://learn.microsoft.com/en-us/azure/data-explorer/ingest-json-formats?tabs=kusto-query-language#the-json-format) for more details.

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

### Kusto.Ingest.IngestionStatus

## NOTES

## RELATED LINKS
