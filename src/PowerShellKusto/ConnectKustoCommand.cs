using System;
using System.Management.Automation;
using System.Security.Authentication;
using Kusto.Data;
using Kusto.Data.Common;

namespace PowerShellKusto;

[Cmdlet(VerbsCommunications.Connect, "Kusto")]
public sealed class ConnectKustoCommand : PSCmdlet
{
    private const string CredentialSet = "Credential";

    private const string IdentitySet = "Identity";

    private static KustoConnectionDetails? s_connectionDetails;


    [Parameter(Mandatory = true, Position = 0)]
    public string Cluster { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    public string Database { get; set; } = null!;

    [Parameter(Mandatory = true, ParameterSetName = CredentialSet)]
    [Credential]
    public PSCredential Credential { get; set; } = null!;

    [Parameter(Mandatory = true, ParameterSetName = CredentialSet)]
    public string Authority { get; set; } = null!;

    [Parameter(ParameterSetName = IdentitySet)]
    public SwitchParameter Identity { get; set; }

    [Parameter]
    public ClientRequestProperties? RequestProperties { get; set; }

    [Parameter]
    public TimeSpan ServerTimeout { get; set; } = TimeSpan.FromMinutes(10);

    [Parameter]
    public SwitchParameter NoTruncation { get; set; }

    protected override void EndProcessing()
    {
        RequestProperties ??= new ClientRequestProperties([
            new(ClientRequestProperties.OptionServerTimeout, ServerTimeout),
            new(ClientRequestProperties.OptionNoTruncation, NoTruncation.IsPresent)],
            null);

        KustoConnectionStringBuilder builder = new(Cluster, Database);

        try
        {
            s_connectionDetails = ParameterSetName switch
            {
                IdentitySet => new KustoConnectionDetails(
                    builder.WithAadSystemManagedIdentity(),
                    RequestProperties),

                CredentialSet => new KustoConnectionDetails(
                    builder.WithAadApplicationKeyAuthentication(
                        Credential.UserName,
                        Credential.GetNetworkCredential().Password,
                        Authority),
                    RequestProperties),

                _ => null
            };
        }
        catch (Exception ex)
        {
            ErrorRecord error = new(
                ex, "AuthFailed", ErrorCategory.AuthenticationError, null);

            ThrowTerminatingError(error);
        }
    }

    internal static void AssertConnected(PSCmdlet cmdlet)
    {
        if (s_connectionDetails is null)
        {
            ErrorRecord error = new(
                new AuthenticationException("Authentication required. Please call 'Connect-Kust'."),
                "AuthRequired",
                ErrorCategory.AuthenticationError,
                null);

            cmdlet.ThrowTerminatingError(error);
        }
    }
}
