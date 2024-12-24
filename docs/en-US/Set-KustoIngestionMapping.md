---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Set-KustoIngestionMapping.md
schema: 2.0.0
---

# Set-KustoIngestionMapping

## SYNOPSIS

Creates or updates an ingestion mapping that can be associated with a specific format and a specific table or database.

## SYNTAX

```powershell
Set-KustoIngestionMapping
    [-Name] <String>
    [[-Table] <String>]
    [[-Database] <String>]
    -Mapping <IngestionMapping>
    [-Force]
    [-OutputType <OutputType>]
    [-RequestProperties <ClientRequestProperties>]
    [<CommonParameters>]
```

## DESCRIPTION

The `Set-KustoIngestionMapping` cmdlet can be used to create or update an ingestion mapping that can be associated with a specific format and a specific table or database. For more information see [__.create ingestion mapping command__](https://learn.microsoft.com/en-us/kusto/management/create-ingestion-mapping-command?view=microsoft-fabric)
and [__.create-or-alter ingestion mapping command__](https://learn.microsoft.com/en-us/kusto/management/create-or-alter-ingestion-mapping-command?view=microsoft-fabric).

## EXAMPLES

### Example 1: Creates a new ingestion mapping on a Table

```powershell
$mapping = New-KustoIngestionMapping -Columns $columns -Kind Json
Set-KustoIngestionMapping myNewMapping -Table myTable -Mapping $mapping
```

This example demonstrates how to create a new `Json` mapping with name `myNewMapping` on `myTable` in a Database specified by `Connect-Kusto -Database`.

### Example 2: Creates a new ingestion mapping on a Database

```powershell
$mapping = New-KustoIngestionMapping -Columns $columns
Set-KustoIngestionMapping myNewMapping -Database myDb -Mapping $mapping
```

This example demonstrates how to create a new `Csv` mapping with name `myNewMapping` on `myDb`.

> [!TIP]
>
> When `-Table` isn't specified, the mapping is created at Database level.

### Example 3: Update an ingestion mapping on a Database

```powershell
$mapping = New-KustoIngestionMapping -Columns $columns
Set-KustoIngestionMapping myNewMapping -Database myDb -Mapping $mapping -Force
```

## PARAMETERS

### -Database

Specifies the Kusto Database where the new ingestion mapping is being created.

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

### -Mapping

This parameter indicates how to map data from the source file to the actual columns in the table.
You can define the format value with the relevant mapping type.

To create a new mapping object, checkout [`New-KustoIngestionMapping`](New-KustoIngestionMapping.md) and [`New-KustoColumnMapping`](New-KustoColumnMapping.md) documentations.

See [__data mappings__](https://learn.microsoft.com/en-us/kusto/management/mappings?view=microsoft-fabric) and [__Class `KustoIngestionProperties`__](https://learn.microsoft.com/en-us/kusto/api/netfx/kusto-ingest-client-reference?view=microsoft-fabric#class-kustoingestionproperties) for more information.

```yaml
Type: IngestionMapping
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

The name for the ingestion mapping.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
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

### -Force

If a mapping with same name in the given scope already exists, `.create` fails. Use this switch to execute a `.create-or-alter` control command instead.

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

Specifies the Table where the new ingestion mapping is being created.

> [!NOTE]
>
> This parameter is optional. If not specified, the ingestion mapping is created on Database level.

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

[__.create ingestion mapping command__](https://learn.microsoft.com/en-us/kusto/management/create-ingestion-mapping-command?view=microsoft-fabric)

[__.create-or-alter ingestion mapping command__](https://learn.microsoft.com/en-us/kusto/management/create-or-alter-ingestion-mapping-command?view=microsoft-fabric)
