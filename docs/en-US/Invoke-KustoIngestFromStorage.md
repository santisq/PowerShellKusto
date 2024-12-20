---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Invoke-KustoIngestFromStorage.md
schema: 2.0.0
---

# Invoke-KustoIngestFromStorage

## SYNOPSIS

Ingests local or blob storage files into Azure Data Explorer.

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

The `Invoke-KustoIngestFromStorage` cmdlet can be used to ingest local or blob storage files into a table
on your Azure Data Explorer Cluster.
For ingestion from a [__Stream__](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) see [`Invoke-KustoIngestFromStream`](Invoke-KustoIngestFromStream.md).
For more details on Kusto ingestion, checkout
[__Ingest from storage__](https://learn.microsoft.com/en-us/kusto/management/data-ingestion/ingest-from-storage?view=microsoft-fabric).

## EXAMPLES

### Example 1: Ingest a local Csv

```powershell
Invoke-KustoIngestFromStorage .\myCsvFile.csv -Table myTable -Database myDb -IgnoreFirstRecord
```

### Example 2: Ingest a Csv from Blob Storage

```powershell
$uri = 'https://myStorageAccount.blob.core.windows.net/my-container/myCsvFile.csv?sp=.....'
Invoke-KustoIngestFromStorage $uri -Table myTable -Database myDb -IgnoreFirstRecord
```

This example demonstrates how you can ingest into `myTable` directly from a Storage Account
using a SAS Key and URI.

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
> This example specifies a format of `multijson` instead `json` because a _JSON Array_ is classified as multijson, while _JSON Lines_ adhere to the `json` format.
> See [__The JSON format__](https://learn.microsoft.com/en-us/azure/data-explorer/ingest-json-formats?tabs=kusto-query-language#the-json-format) for more details.

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
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Format

This parameter determines the format of the file to be ingested. The default value is __`csv`__.

```yaml
Type: DataSourceFormat
Parameter Sets: (All)
Aliases:
Accepted values: csv, tsv, scsv, sohsv, psv, txt, raw, tsve, w3clogfile, apacheavro, orc, sstream, parquet, avro, multijson, singlejson, json

Required: False
Position: Named
Default value: csv
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreFirstRecord

This switch indicates that ingestion should ignore the first record of a file.
This property is useful for files in `CSV` and similar formats,
if the first record in the file are the column names.

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

This optional parameter indicates how to map data from the source file to the actual columns in the table.
You can define the format value with the relevant mapping type.

To create a new mapping object, checkout [`New-KustoIngestionMapping`](New-KustoIngestionMapping.md) and [`New-KustoColumnMapping`](New-KustoColumnMapping.md) documentations.

See [__data mappings__](https://learn.microsoft.com/en-us/kusto/management/mappings?view=microsoft-fabric) and [__Class `KustoIngestionProperties`__](https://learn.microsoft.com/en-us/kusto/api/netfx/kusto-ingest-client-reference?view=microsoft-fabric#class-kustoingestionproperties) for more information.

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

Determines the total retry service calls when there is an ingestion failure. __The default retry value is 3__.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 3
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

Specifies the path or URI to the file to ingest.

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

Determines the time to wait before retrying. __The default value is 1 second__.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: [timespan] '00:00:01'
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table

Specifies the database table in your Cluster to ingest into.

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

### CommonParameters

This cmdlet supports the common parameters.
For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Kusto.Ingest.IngestionStatus

## NOTES

## RELATED LINKS

[__Ingest from storage__](https://learn.microsoft.com/en-us/kusto/management/data-ingestion/ingest-from-storage?view=microsoft-fabric)

[__Data mappings__](https://learn.microsoft.com/en-us/kusto/management/mappings?view=microsoft-fabric)

[__The JSON format__](https://learn.microsoft.com/en-us/azure/data-explorer/ingest-json-formats?tabs=kusto-query-language#the-json-format)
