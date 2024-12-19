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
    -Cluster <String>
    [[-Database] <String>]
    [-UserPrompt]
    [-RequestProperties <ClientRequestProperties>]
    [-ServerTimeout <TimeSpan>]
    [-NoTruncation]
    [<CommonParameters>]
```

### Credential

```powershell
Connect-Kusto
    -Cluster <String>
    [[-Database] <String>]
    -Authority <String>
    -ClientSecretCredential <PSCredential>
    [-RequestProperties <ClientRequestProperties>]
    [-ServerTimeout <TimeSpan>]
    [-NoTruncation]
    [<CommonParameters>]
```

### Certificate

```powershell
Connect-Kusto
    -Cluster <String>
    [[-Database] <String>]
    -Authority <String>
    -ClientId <String>
    -Certificate <X509Certificate2>
    [-UseTrustedIssuer]
    [-RequestProperties <ClientRequestProperties>]
    [-ServerTimeout <TimeSpan>]
    [-NoTruncation]
    [<CommonParameters>]
```

### CertificateThumbprint

```powershell
Connect-Kusto
    -Cluster <String>
    [[-Database] <String>]
    -Authority <String>
    -ClientId <String>
    -Thumbprint <String>
    [-RequestProperties <ClientRequestProperties>]
    [-ServerTimeout <TimeSpan>]
    [-NoTruncation]
    [<CommonParameters>]
```

### Identity

```powershell
Connect-Kusto
    -Cluster <String>
    [[-Database] <String>]
    [-Identity]
    [-ClientId <String>]
    [-RequestProperties <ClientRequestProperties>]
    [-ServerTimeout <TimeSpan>]
    [-NoTruncation]
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
Parameter Sets: Credential, Certificate, CertificateThumbprint
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

### -NoTruncation

{{ Fill NoTruncation Description }}

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

### -ServerTimeout

{{ Fill ServerTimeout Description }}

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

### -UserPrompt

{{ Fill UserPrompt Description }}

```yaml
Type: SwitchParameter
Parameter Sets: UserPrompt
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
Parameter Sets: Credential
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
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
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

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

This cmdlet produces no output.

## NOTES

## RELATED LINKS
