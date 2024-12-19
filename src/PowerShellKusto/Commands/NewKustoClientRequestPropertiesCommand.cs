using System;
using System.Collections;
using System.Management.Automation;
using Kusto.Data.Common;

namespace PowerShellKusto.Commands;

[Cmdlet(VerbsCommon.New, "KustoClientRequestProperties")]
[OutputType(typeof(ClientRequestProperties))]
public sealed class NewKustoClientRequestPropertiesCommand : PSCmdlet
{
    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? Application { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? User { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public string? ClientRequestId { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public Hashtable? Parameters { get; set; }

    [Parameter]
    [ValidateNotNullOrEmpty]
    public Hashtable? Options { get; set; }

    [Parameter]
    public SwitchParameter NoTruncation { get; set; }

    [Parameter]
    [ValidateTimespan]
    public TimeSpan ServerTimeout { get; set; } = TimeSpan.FromSeconds(60);

    protected override void EndProcessing()
    {
        ClientRequestProperties properties = new()
        {
            Application = Application,
            User = User,
            ClientRequestId = ClientRequestId
        };

        if (Parameters is not null)
        {
            properties.SetParameters(Parameters.ToDictionary());
        }

        if (Options is not null)
        {
            foreach (DictionaryEntry entry in Options)
            {
                properties.SetOption(
                    LanguagePrimitives.ConvertTo<string>(entry.Key),
                    entry.Value);
            }
        }

        properties.SetOption(ClientRequestProperties.OptionNoTruncation, NoTruncation);
        properties.SetOption(ClientRequestProperties.OptionServerTimeout, ServerTimeout);
        WriteObject(properties);
    }
}
