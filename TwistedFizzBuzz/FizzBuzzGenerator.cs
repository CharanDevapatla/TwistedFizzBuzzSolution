using System.Text;

namespace TwistedFizzBuzz
{
    public sealed class FizzBuzzGenerator : IFizzBuzzGenerator
    {
        private readonly FizzBuzzRules _rules;

        public FizzBuzzGenerator(FizzBuzzRules rules)
        {
            if (rules is null || rules.Rules.Count == 0)
                throw new ArgumentException("At least one rule is required.", nameof(rules));

            if (rules.Rules.Any(r => r.Divisor == 0))
                throw new ArgumentException("Divisor cannot be zero.", nameof(rules));

            _rules = rules;
        }

        public IEnumerable<string> GenerateForRange(long startInclusive, long endInclusive)
        {
            var step = startInclusive <= endInclusive ? 1 : -1;
            for (long n = startInclusive; ; n += step)
            {
                yield return Format(n);
                if (n == endInclusive) break;
            }
        }

        public IEnumerable<string> GenerateForList(IEnumerable<long> numbers)
        {
            foreach (var n in numbers)
                yield return Format(n);
        }

        private string Format(long n)
        {
            var sb = new StringBuilder();

            foreach (var (divisor, token) in _rules.Rules)
            {
                if (n % divisor == 0)
                    sb.Append(token);
            }

            return sb.Length == 0 ? n.ToString() : sb.ToString();
        }
    }
}
