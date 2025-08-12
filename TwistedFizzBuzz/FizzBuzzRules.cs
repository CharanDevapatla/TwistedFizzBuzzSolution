using System.Collections.Generic;
using System.Linq;

namespace TwistedFizzBuzz
{
    public sealed record FizzBuzzRules(List<(long Divisor, string Token)> Rules)
    {
        public static FizzBuzzRules FromDictionary(IDictionary<long, string> rules) =>
            new(rules.Select(kv => ((long)kv.Key, kv.Value)).ToList());
    }
}
