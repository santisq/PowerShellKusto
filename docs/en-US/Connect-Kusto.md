---
external help file: PowerShellKusto.dll-Help.xml
Module Name: PowerShellKusto
online version: https://github.com/santisq/PowerShellKusto/blob/main/docs/en-US/Connect-Kusto.md
schema: 2.0.0
---

# Connect-Kusto

## SYNOPSIS

Establishes a connection to a Kusto service endpoint.

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
    -ClientId <Guid>
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
    -ClientId <Guid>
    -Thumbprint <String>
    [<CommonParameters>]
```

### Identity

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    [-Identity]
    [-ClientId <Guid>]
    [<CommonParameters>]
```

### AccessToken

```powershell
Connect-Kusto
    -Cluster <Uri>
    [[-Database] <String>]
    [-AccessToken <SecureString>]
    [-TokenType <String>]
    [<CommonParameters>]
```

## DESCRIPTION

The `Connect-Kusto` cmdlet is the main entry point for the cmdlets in this module and it's used to establish a connection with your Azure Data Explorer. The authentication methods offered by this cmdlet are:

- User prompt
- System or User Managed Identity
- Certificate via `X509Certificate2` or Thumbprint
- Secret
- User and Application Access Token

## EXAMPLES

### Example 1: Interactive authentication

```powershell
Connect-Kusto https://myCluster.zone.kusto.windows.net
```

Using this method the cmdlets in this module will prompt for authentication.

### Example 2: Client Id and Secret

```powershell
$connectKustoSplat = @{
    Cluster                = 'https://myCluster.zone.kusto.windows.net'
    Authority              = 'myTenantId or Primary Domain'
    ClientSecretCredential = [pscredential]::new(
        'myClientId',
        (ConvertTo-SecureString mySecret -AsPlainText))
}
Connect-Kusto @connectKustoSplat
```

> [!NOTE]
>
> For demo purposes, this example uses a hardcoded secret.
> Ideally, you should get your credential object via Key Vault or `Get-Credential`

### Example 3: System Managed Identity

```powershell
Connect-Kusto https://myCluster.zone.kusto.windows.net -Identity
```

### Example 4: User Managed Identity

```powershell
$connectKustoSplat = @{
    Identity = $true
    ClientId = '697183e1-a4b0-457a-aa71-9a6e3df5f029'
    Cluster  = 'https://myCluster.zone.kusto.windows.net'
}
Connect-Kusto @connectKustoSplat
```

### Example 5: `X509Certificate2` Certificate

```powershell
$connectKustoSplat = @{
    Certificate = Get-ChildItem Cert:\CurrentUser\My\$myCertThumbprint
    ClientId    = '697183e1-a4b0-457a-aa71-9a6e3df5f029'
    Cluster     = 'https://myCluster.zone.kusto.windows.net'
    Authority   = 'myTenantId or Primary Domain'
}
Connect-Kusto @connectKustoSplat
```

### Example 6: Certificate Thumbprint

```powershell
$connectKustoSplat = @{
    Thumbprint = $myCertThumbprint
    ClientId   = '697183e1-a4b0-457a-aa71-9a6e3df5f029'
    Cluster    = 'https://myCluster.zone.kusto.windows.net'
    Authority  = 'myTenantId or Primary Domain'
}
Connect-Kusto @connectKustoSplat
```

### Example 7: Access Token

```powershell
Connect-AzAccount
$token = Get-AzAccessToken -ResourceUrl https://api.kusto.windows.net -AsSecureString

$connectKustoSplat = @{
    Cluster     = 'https://myCluster.zone.kusto.windows.net'
    AccessToken = $token.Token
}
Connect-Kusto @connectKustoSplat
```

This example demonstrates how to request an Access Token on behalf of a User to the `https://api.kusto.windows.net` API.
For simplicity, this example uses [`Get-AzAccessToken`](https://learn.microsoft.com/en-us/powershell/module/az.accounts/get-azaccesstoken) however the use of `Az.Accounts` Module isn't mandatory. See [__Get an access token__](https://learn.microsoft.com/en-us/kusto/api/rest/authentication#get-an-access-token) for more details.

## PARAMETERS

### -Authority

The Tenant Id or Primary Domain where your Azure Data Explorer Cluster is located.

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

An X.509 certificate supplied during invocation.

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

This non mandatory parameter determines which Database in your Cluster will be targetted on queries and ingestion.

> [!TIP]
>
> The `Invoke-*` and `Set-*` cmdlets also offer supplying this value if it wasn't provided during connection.

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

Connects using a System or User Managed Identity.

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

The thumbprint of your certificate. The Certificate will be retrieved from the current user's certificate store.

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

Uses Trusted Issuer feature of Entra ID.

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

The Client Id of your Application.

```yaml
Type: Guid
Parameter Sets: Certificate, CertificateThumbprint
Aliases: ApplicationId

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: Guid
Parameter Sets: Identity
Aliases: ApplicationId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSecretCredential

The PSCredential object provides the application ID and client secret for service principal credentials.

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

The Azure Data Explorer Url to connect to.

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId

Performs user authentication with the indicated user name.

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

### -AccessToken

Specifies an Application or User bearer token.

> [!NOTE]
>
> Access tokens do timeout and you'll have to handle their refresh.

```yaml
Type: SecureString
Parameter Sets: ApplicationToken
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TokenType

Specifies if the bearer token is requested on behalf of a `User` or `Application`.

```yaml
Type: String
Parameter Sets: AccessToken
Aliases:
Accepted values: User, Application

Required: False
Position: Named
Default value: User
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

[__Kusto connection strings__](https://learn.microsoft.com/en-us/kusto/api/connection-strings/kusto)

[__`Get-AzAccessToken`__](https://learn.microsoft.com/en-us/powershell/module/az.accounts/get-azaccesstoken)

[__Get an access token__](https://learn.microsoft.com/en-us/kusto/api/rest/authentication#get-an-access-token)
