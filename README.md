<h1 align="center">PowerShellKusto</h1>

<div align="center">
<sub>

PowerShell Module for Azure Data Explorer management, query and ingestion

</sub>
<br/>

[![build][build_badge]][build_ref]
[![PowerShell Gallery][gallery_badge]][gallery_ref]
[![LICENSE][license_badge]][license_ref]

</div>

PowerShellKusto is an abstraction over [`Microsoft.Azure.Kusto.Data`][kustodata] and [`Microsoft.Azure.Kusto.Ingest`][kustoingest], to simplify the process of Azure Data Explorer management, query and ingestion.

## Commands

The Module currently has the following cmdlets and more will be added in the future.

If you'd like to see a cmdlet for a specific task, please sumbit an [Issue][newissue]!

### [`Connect-Kusto`](./docs/en-US/Connect-Kusto.md)

Main entry point for the cmdlets in this module and it's used to establish a connection with your Azure Data Explorer Cluster.

The available authentication methods are:

- User prompt
- System or User Managed Identity
- Certificate via `X509Certificate2` or Thumbprint
- Secret
- User and Application Access Token

### [`Invoke-KustoControlCommand`](./docs/en-US/Invoke-KustoControlCommand.md)

Allows you to invoke management commands also known as control commands over an Azure Data Explorer Cluster. For detailed information on this topic check out [__Management commands overview__](https://learn.microsoft.com/en-us/kusto/management/?view=microsoft-fabric).

### [`Invoke-KustoIngestFromStorage`](./docs/en-US/Invoke-KustoIngestFromStorage.md)

Can be used to ingest local or blob storage files into a table on your Azure Data Explorer Cluster.

### [`Invoke-KustoIngestFromStream`](./docs/en-US/Invoke-KustoIngestFromStream.md)

Similar to [`Invoke-KustoIngestFromStorage`](./docs/en-US/Invoke-KustoIngestFromStorage.md), but the source is
[__Stream__](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) instead of a Path or URI.

### [`Invoke-KustoQuery`](./docs/en-US/Invoke-KustoQuery.md)

Allows you to run
[Kusto Query Language (KQL)](https://learn.microsoft.com/en-us/kusto/query/?view=microsoft-fabric) queries or
[T-SQL](https://learn.microsoft.com/en-us/kusto/query/t-sql?view=microsoft-fabric) queries on a
specified Database of your Azure Data Explorer Cluster.

### [`New-KustoClientRequestProperties`](./docs/en-US/New-KustoClientRequestProperties.md)

Creates a new `ClientRequestProperties` object
to manage the interaction between client and service. This object can be later on passed as argument to the request cmdlets: [`Invoke-KustoControlCommand`](./docs/en-US/Invoke-KustoControlCommand.md), [`Invoke-KustoQuery`](./docs/en-US/Invoke-KustoQuery.md),
[`Set-KustoBatchingPolicy`](./docs/en-US/Set-KustoBatchingPolicy.md) and [`Set-KustoIngestionMapping`](./docs/en-US/Set-KustoIngestionMapping.md).

### [`New-KustoColumnMapping`](./docs/en-US/New-KustoColumnMapping.md)

Creates a new object of type `ColumnMapping`, this object can be later on passed as argument to the [`New-KustoIngestionMapping`](./docs/en-US/New-KustoIngestionMapping.md) cmdlet.

### [`New-KustoIngestionMapping`](./docs/en-US/New-KustoIngestionMapping.md)

Creates a new object of type `IngestionMapping` that can be later on passed as argument to the [`Invoke-KustoIngestFromStorage`](./docs/en-US/Invoke-KustoIngestFromStorage.md), [`Invoke-KustoIngestFromStream`](./docs/en-US/Invoke-KustoIngestFromStream.md) and [`Set-KustoIngestionMapping`](./docs/en-US/Set-KustoIngestionMapping.md) commands.

### [`Set-KustoBatchingPolicy`](./docs/en-US/Set-KustoBatchingPolicy.md)

Alters the [__Ingestion batching policy__](https://learn.microsoft.com/en-us/kusto/management/batching-policy?view=microsoft-fabric) of a Database or specific Table on an Azure Data Explorer Cluster.

### [`Set-KustoIngestionMapping`](./docs/en-US/Set-KustoIngestionMapping.md)

[Creates](https://learn.microsoft.com/en-us/kusto/management/create-ingestion-mapping-command?view=microsoft-fabric) or [updates](https://learn.microsoft.com/en-us/kusto/management/create-or-alter-ingestion-mapping-command?view=microsoft-fabric) an ingestion mapping that can be associated with a specific format and a specific table or database.

## Installation

### Gallery

The module is available through the [PowerShell Gallery][gallery_ref]:

```powershell
Install-Module PowerShellKusto -Scope CurrentUser
```

### Source

```powershell
git clone 'https://github.com/santisq/PowerShellKusto.git'
Set-Location ./PowerShellKusto
./build.ps1
```

## Requirements

This module is compatible only with [__PowerShell v7.2.0__][psgithub] or above.

## Contributing

Contributions are welcome, if you wish to contribute, fork this repository and submit a pull request with the changes.

[build_badge]: https://github.com/santisq/PowerShellKusto/actions/workflows/ci.yml/badge.svg
[build_ref]: https://github.com/santisq/PowerShellKusto/actions/workflows/ci.yml
[gallery_badge]: https://img.shields.io/powershellgallery/dt/PowerShellKusto?color=%23008FC7
[gallery_ref]: https://www.powershellgallery.com/packages/PowerShellKusto
[license_badge]: https://img.shields.io/github/license/santisq/PowerShellKusto
[license_ref]: https://github.com/santisq/PowerShellKusto/blob/main/LICENSE
[psgithub]: https://github.com/PowerShell/PowerShell
[kustodata]: https://www.nuget.org/packages/Microsoft.Azure.Kusto.Data/
[kustoingest]: https://www.nuget.org/packages/Microsoft.Azure.Kusto.Ingest/
[newissue]: https://github.com/santisq/PowerShellKusto/issues/new/choose
