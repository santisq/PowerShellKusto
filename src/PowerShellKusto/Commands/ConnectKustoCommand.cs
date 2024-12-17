using System;
using System.Management.Automation;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Kusto.Data;
using Kusto.Data.Common;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommunications.Connect, "Kusto", DefaultParameterSetName = CredentialSet)]
[OutputType(typeof(void))]
public sealed class ConnectKustoCommand : PSCmdlet
{
    private const string CredentialSet = "Credential";

    private const string IdentitySet = "Identity";

    private const string CertificateSet = "Certificate";

    private const string CertificateThumbprintSet = "CertificateThumbprint";

    [ThreadStatic]
    private static KustoConnectionDetails? s_connectionDetails;

    [Parameter(Mandatory = true, Position = 0)]
    public string ClusterUri { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CredentialSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    public string? Authority { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CredentialSet)]
    [Credential]
    public PSCredential Credential { get; set; } = null!;

    [Parameter(ParameterSetName = IdentitySet)]
    public SwitchParameter Identity { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    public string AppId { get; set; } = null!;

    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    public X509Certificate2 Certificate { get; set; } = null!;

    [Parameter(ParameterSetName = CertificateSet)]
    public SwitchParameter UseTrustedIssuer { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    public string Thumbprint { get; set; } = null!;

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

        KustoConnectionStringBuilder builder = Database is not null
            ? new(ClusterUri, Database)
            : new(ClusterUri);

        try
        {
            s_connectionDetails = ParameterSetName switch
            {
                IdentitySet => new KustoConnectionDetails(
                    builder.WithAadSystemManagedIdentity(),
                    RequestProperties),

                CredentialSet => new KustoConnectionDetails(
                    builder.WithAadApplicationKeyAuthentication(
                        applicationClientId: Credential.UserName,
                        applicationKey: Credential.GetNetworkCredential().Password,
                        authority: Authority),
                    RequestProperties),

                CertificateSet => new KustoConnectionDetails(
                    builder.WithAadApplicationCertificateAuthentication(
                        applicationClientId: AppId,
                        applicationCertificate: Certificate,
                        authority: Authority,
                        sendX5c: UseTrustedIssuer),
                    RequestProperties),

                CertificateThumbprintSet => new KustoConnectionDetails(
                    builder.WithAadApplicationThumbprintAuthentication(
                        applicationClientId: AppId,
                        applicationCertificateThumbprint: Thumbprint,
                        authority: Authority),
                    RequestProperties),

                _ => null
            };
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(
                exception, "AuthFailed", ErrorCategory.AuthenticationError, null);

            ThrowTerminatingError(error);
        }
    }

    internal static KustoConnectionDetails GetConnectionDetails(PSCmdlet cmdlet)
    {
        if (s_connectionDetails is null)
        {
            ErrorRecord error = new(
                new AuthenticationException("Authentication required. Please call 'Connect-Kusto'."),
                "AuthRequired",
                ErrorCategory.AuthenticationError,
                null);

            cmdlet.ThrowTerminatingError(error);
        }

        return (KustoConnectionDetails)s_connectionDetails;
    }
}
