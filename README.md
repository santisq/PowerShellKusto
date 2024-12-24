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

PowerShellKusto is an abstraction over [`Microsoft.Azure.Kusto.Data`][kustodata] and [`Microsoft.Azure.Kusto.Ingest`][kustoingest], to simplify the process of Azure Data Explorer management, query and ingestion. The cmdlet currently has the following cmdlets and more will be added in the future. If you'd like to see a cmdlet for a specific task, please sumbit a [new Issue][newissue]!

- `Connect-Kusto`: This cmdlet is the main entry point for the cmdlets in this module and it's used to establish a connection with your Azure Data Explorer Cluster.
- `Invoke-KustoControlCommand`: 
- `Invoke-KustoIngestFromStorage`: 
- `Invoke-KustoIngestFromStream`: 
- `Invoke-KustoQuery`: 
- `New-KustoClientRequestProperties`: 
- `New-KustoColumnMapping`: 
- `New-KustoIngestionMapping`: 
- `Set-KustoBatchingPolicy`: 
- `Set-KustoIngestionMapping`: 

## Documentation

Check out [__the docs__](./docs/en-US/Use-Object.md) for information about how to use this Module.

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
