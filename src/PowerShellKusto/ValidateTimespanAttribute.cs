using System;
using System.Management.Automation;

namespace PowerShellKusto;

public sealed class ValidateTimespan : ValidateEnumeratedArgumentsAttribute
{
    protected override void ValidateElement(object element)
    {
        TimeSpan timeSpan = (TimeSpan)element;
        if (timeSpan > TimeSpan.Zero)
        {
            return;
        }

        throw new ValidationMetadataException(
            $"Argument '{timeSpan}' is out of range. Timespan must be greater than zero.");
    }
}
