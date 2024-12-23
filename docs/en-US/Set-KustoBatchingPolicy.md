---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Set-KustoBatchingPolicy.md
schema: 2.0.0
---

# Set-KustoBatchingPolicy

## SYNOPSIS

{{ Fill in the Synopsis }}

## SYNTAX

```powershell
Set-KustoBatchingPolicy
    [-Table] <String>
    [[-Database] <String>]
    [-MaximumBatchingTimeSpan <TimeSpan>]
    [-MaximumNumberOfItems <Int32>]
    [-MaximumRawDataSizeMB <Int32>]
    [-OutputType <OutputType>]
    [-RequestProperties <ClientRequestProperties>]
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
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaximumBatchingTimeSpan

{{ Fill MaximumBatchingTimeSpan Description }}

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

### -MaximumNumberOfItems

{{ Fill MaximumNumberOfItems Description }}

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

### -MaximumRawDataSizeMB

{{ Fill MaximumRawDataSizeMB Description }}

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

### -OutputType

{{ Fill OutputType Description }}

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

{{ Fill RequestProperties Description }}

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

{{ Fill Table Description }}

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
