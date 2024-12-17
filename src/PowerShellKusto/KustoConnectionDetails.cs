using Kusto.Data;
using Kusto.Data.Common;

namespace PowerShellKusto;

internal record struct KustoConnectionDetails(
    KustoConnectionStringBuilder Builder,
    ClientRequestProperties Properties);
