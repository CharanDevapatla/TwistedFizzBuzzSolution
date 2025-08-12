using System.Threading.Tasks;
using TwistedFizzBuzz;

namespace TwistedFizzBuzzConsole
{
    internal static class Program
    {
        private static async Task Main()
        {
            var specRules = new FizzBuzzRules(new()
            {
                (5,  "Fizz"),
                (9,  "Buzz"),
                (27, "Bar")
            });

            var specEngine = new FizzBuzzGenerator(specRules);

            foreach (var line in specEngine.GenerateForRange(-20, 127))
            {
                Console.WriteLine(line);
            }


            Console.WriteLine();
            Console.WriteLine("=== API Rules Demo (1..30) ===");

            try
            {
                const string apiUrl = "https://pie-healthy-swift.glitch.me/";
                var rulesFromApi = await ApiRulesLoader.GetRulesFromApiAsync(apiUrl);
                var apiEngine = new FizzBuzzGenerator(rulesFromApi);

                foreach (var line in apiEngine.GenerateForRange(1, 30))
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API demo skipped] Could not load rules: {ex.Message}");
            }
        }
    }
}

