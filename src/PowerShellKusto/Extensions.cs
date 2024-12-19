using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Text;
using Kusto.Cloud.Platform.Data;
using Newtonsoft.Json.Linq;

namespace PowerShellKusto;

internal static class Extensions
{
    internal static string ToPlainText(this SecureString? secureString) =>
        new NetworkCredential(string.Empty, secureString).Password;

    internal static Dictionary<string, string> ToDictionary(this Hashtable hashtable) =>
        hashtable
            .Cast<DictionaryEntry>()
            .ToDictionary(
                e => LanguagePrimitives.ConvertTo<string>(e.Key),
                e => LanguagePrimitives.ConvertTo<string>(e.Value));

    internal static DataTable ToDataTable(this IDataReader reader) => reader.ToDataSet().Tables[0];

    internal static string ToCsvString(this IDataReader reader, bool includeHeaders = true)
    {
        using MemoryStream mem = new();
        using (StreamWriter writer = new(mem))
        {
            reader.WriteAsCsv(includeHeaders, writer);
        }

        return Encoding.UTF8.GetString(mem.ToArray());
    }

    internal static string ToHtmlString(this IDataReader reader, string title)
    {
        using MemoryStream mem = new();
        using (StreamWriter writer = new(mem))
        {
            reader.WriteAsHtml(title, writer);
        }

        return Encoding.UTF8.GetString(mem.ToArray());
    }

    internal static IEnumerable<PSObject> ToEnumerablePSObject(this IDataReader reader)
    {
        foreach (JObject jObject in reader.ToJObjects())
        {
            yield return ToPSObject(jObject);
        }
    }

    private static PSObject ToPSObject(JObject entries)
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
