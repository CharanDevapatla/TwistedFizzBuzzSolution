using TwistedFizzBuzz;

namespace FizzBuzzConsole
{
    internal static class Program
    {
        private static void Main()
        {
            // Classic rules: 3 -> "Fizz", 5 -> "Buzz"
            var rules = new FizzBuzzRules(new List<(long Divisor, string Token)>
            {
                (3, "Fizz"),
                (5, "Buzz")
            });

            var engine = new FizzBuzzGenerator(rules);

            foreach (var line in engine.GenerateForRange(1, 100))
            {
                Console.WriteLine(line);
            }
        }
    }
}
