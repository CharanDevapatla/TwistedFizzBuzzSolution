using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using System.Text.Json;

namespace TwistedFizzBuzz
{
    public static class ApiRulesLoader
    {
        public static async Task<FizzBuzzRules> GetRulesFromApiAsync(
            string url,
            HttpClient? httpClient = null,
            CancellationToken cancellationToken = default)
        {
            httpClient ??= new HttpClient();

            using var stream = await httpClient.GetStreamAsync(url, cancellationToken);
            using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                var list = new List<(long, string)>();
                foreach (var item in root.EnumerateArray())
                {
                    if (item.TryGetProperty("divisor", out var divProp) &&
                        item.TryGetProperty("token", out var tokProp) &&
                        divProp.TryGetInt32(out var div) &&
                        tokProp.ValueKind == JsonValueKind.String)
                    {
                        list.Add((div, tokProp.GetString()!));
                    }
                }
                if (list.Count == 0) throw new InvalidOperationException("No valid rules found in array.");
                return new FizzBuzzRules(list);
            }
            else if (root.ValueKind == JsonValueKind.Object)
            {
                var list = new List<(long, string)>();
                foreach (var prop in root.EnumerateObject())
                {
                    if (long.TryParse(prop.Name, out var div) &&
                        prop.Value.ValueKind == JsonValueKind.String)
                    {
                        list.Add((div, prop.Value.GetString()!));
                    }
                }
                if (list.Count == 0) throw new InvalidOperationException("No valid rules found in object.");
                return new FizzBuzzRules(list);
            }

            throw new InvalidOperationException("Unrecognized API response format.");
        }
    }
}
