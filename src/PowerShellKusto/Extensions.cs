using System.Collections.Generic;
using System.Management.Automation;
using Newtonsoft.Json.Linq;

namespace PowerShellKusto;

internal static class Extensions
{
    internal static PSObject ToPSObject(this JObject entries)
    {
        PSObject result = new(entries.Count);
        foreach (KeyValuePair<string, JToken?> entry in entries)
        {
            switch (entry.Value)
            {
                case JArray jArray:
                    object?[] collection = PopulateFromJArray(jArray);
                    result.Properties.Add(new PSNoteProperty(entry.Key, collection));
                    break;

                case JObject jObject:
                    PSObject psobject = ToPSObject(jObject);
                    result.Properties.Add(new PSNoteProperty(entry.Key, psobject));
                    break;

                case JValue jValue:
                    result.Properties.Add(new PSNoteProperty(entry.Key, jValue.Value));
                    break;
            }
        }

        return result;
    }

    private static object?[] PopulateFromJArray(JArray jArray)
    {
        object?[] result = new object[jArray.Count];
        for (int index = 0; index < jArray.Count; index++)
        {
            JToken? element = jArray[index];
            switch (element)
            {
                case JArray subJArray:
                    object?[] subCollection = PopulateFromJArray(subJArray);
                    result[index] = subCollection;
                    break;

                case JObject jObject:
                    PSObject psobject = ToPSObject(jObject);
                    result[index] = psobject;
                    break;

                case JValue jValue:
                    result[index] = jValue.Value;
                    break;
            }
        }

        return result;
    }
}
