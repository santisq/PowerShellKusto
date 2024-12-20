using System;
using System.Management.Automation;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Kusto.Data;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommunications.Connect, "Kusto", DefaultParameterSetName = UserPromptSet)]
[OutputType(typeof(void))]
public sealed class ConnectKustoCommand : PSCmdlet
{
    private const string ClientCredentialSet = "ClientCredential";

    private const string IdentitySet = "Identity";

    private const string CertificateSet = "Certificate";

    private const string CertificateThumbprintSet = "CertificateThumbprint";

    private const string UserPromptSet = "UserPrompt";

    private const string AccessTokenSet = "AccessToken";

    [ThreadStatic]
    private static KustoConnectionStringBuilder? s_connectionSb;

    [Parameter(Mandatory = true, Position = 0)]
    public Uri Cluster { get; set; } = null!;

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string? Database { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = ClientCredentialSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    [Parameter(ParameterSetName = UserPromptSet)]
    [Alias("TenantId")]
    public string? Authority { get; set; }

    [Parameter(ParameterSetName = UserPromptSet)]
    public string? UserId { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = ClientCredentialSet)]
    [Credential]
    public PSCredential ClientSecretCredential { get; set; } = null!;

    [Parameter(ParameterSetName = IdentitySet)]
    public SwitchParameter Identity { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    [Parameter(ParameterSetName = IdentitySet)]
    [Alias("ApplicationId")]
    public Guid? ClientId { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CertificateSet)]
    public X509Certificate2 Certificate { get; set; } = null!;

    [Parameter(ParameterSetName = CertificateSet)]
    public SwitchParameter UseTrustedIssuer { get; set; }

    [Parameter(Mandatory = true, ParameterSetName = CertificateThumbprintSet)]
    public string Thumbprint { get; set; } = null!;

    [Parameter(ParameterSetName = AccessTokenSet)]
    [ValidateNotNull]
    public SecureString? AccessToken { get; set; }

    [Parameter(ParameterSetName = AccessTokenSet)]
    [ValidateSet("User", "Application")]
    public string TokenType { get; set; } = "User";

    protected override void EndProcessing()
    {
        try
        {
            KustoConnectionStringBuilder builder = Database is not null
                ? new(Cluster.ToString(), Database)
                : new(Cluster.ToString());

            s_connectionSb = ParameterSetName switch
            {
                AccessTokenSet => TokenType == "User"
                    ? builder.WithAadUserTokenAuthentication(AccessToken.ToPlainText())
                    : builder.WithAadApplicationTokenAuthentication(AccessToken.ToPlainText()),

                IdentitySet => ClientId is not null
                    ? builder.WithAadUserManagedIdentity(ClientId.ToString())
                    : builder.WithAadSystemManagedIdentity(),

                ClientCredentialSet => builder.WithAadApplicationKeyAuthentication(
                    applicationClientId: ClientSecretCredential.UserName,
                    applicationKey: ClientSecretCredential.GetNetworkCredential().Password,
                    authority: Authority),

                CertificateSet => builder.WithAadApplicationCertificateAuthentication(
                    applicationClientId: ClientId.ToString(),
                    applicationCertificate: Certificate,
                    authority: Authority,
                    sendX5c: UseTrustedIssuer),

                CertificateThumbprintSet => builder.WithAadApplicationThumbprintAuthentication(
                    applicationClientId: ClientId.ToString(),
                    applicationCertificateThumbprint: Thumbprint,
                    authority: Authority),

                _ => builder.WithAadUserPromptAuthentication(
                    authority: Authority,
                    userId: UserId)
            };
        }
        catch (Exception exception)
        {
            ErrorRecord error = new(
                exception, "AuthFailed", ErrorCategory.AuthenticationError, null);

            ThrowTerminatingError(error);
        }
    }

    internal static KustoConnectionStringBuilder GetConnectionBuilder(PSCmdlet cmdlet)
    {
        if (s_connectionSb is null)
        {
            ErrorRecord error = new(
                new AuthenticationException("Authentication required. Please call 'Connect-Kusto'."),
                "AuthRequired",
                ErrorCategory.AuthenticationError,
                null);

            cmdlet.ThrowTerminatingError(error);
        }

        return s_connectionSb;
    }
}
