---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version:
schema: 2.0.0
---

# Connect-Kusto

## SYNOPSIS

{{ Fill in the Synopsis }}

## SYNTAX

### UserPrompt (Default)

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    [-Authority <String>]
    [-UserId <String>]
    [<CommonParameters>]
```

### ClientCredential

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    -Authority <String>
    -ClientSecretCredential <PSCredential>
    [<CommonParameters>]
```

### Certificate

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    -Authority <String>
    -ClientId <String>
    -Certificate <X509Certificate2>
    [-UseTrustedIssuer]
    [<CommonParameters>]
```

### CertificateThumbprint

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    -Authority <String>
    -ClientId <String>
    -Thumbprint <String>
    [<CommonParameters>]
```

### Identity

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    [-Identity]
    [-ClientId <String>]
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

### -Authority

{{ Fill Authority Description }}

```yaml
Type: String
Parameter Sets: UserPrompt
Aliases: TenantId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: ClientCredential, Certificate, CertificateThumbprint
Aliases: TenantId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Certificate

{{ Fill Certificate Description }}

```yaml
Type: X509Certificate2
Parameter Sets: Certificate
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Identity

{{ Fill Identity Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Identity
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Thumbprint

{{ Fill Thumbprint Description }}

```yaml
Type: String
Parameter Sets: CertificateThumbprint
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseTrustedIssuer

{{ Fill UseTrustedIssuer Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Certificate
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientId

{{ Fill ClientId Description }}

```yaml
Type: String
Parameter Sets: Certificate, CertificateThumbprint
Aliases: ApplicationId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: Identity
Aliases: ApplicationId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSecretCredential

{{ Fill ClientSecretCredential Description }}

```yaml
Type: PSCredential
Parameter Sets: ClientCredential
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Cluster

{{ Fill Cluster Description }}

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId

{{ Fill UserId Description }}

```yaml
Type: String
Parameter Sets: UserPrompt
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

### None

This cmdlet produces no output.

## NOTES

## RELATED LINKS
