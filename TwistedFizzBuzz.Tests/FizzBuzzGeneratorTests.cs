using TwistedFizzBuzz;
using FluentAssertions;

namespace TwistedFizzBuzz.Tests
{
    public class FizzBuzzGeneratorTests
    {
        [Fact]
        public void Classic_FizzBuzz_1_to_15()
        {
            var rules = new FizzBuzzRules(new() { (3, "Fizz"), (5, "Buzz") });
            var gen = new FizzBuzzGenerator(rules);

            var actual = gen.GenerateForRange(1, 15).ToArray();

            actual[0].Should().Be("1");          
            actual[2].Should().Be("Fizz");       
            actual[4].Should().Be("Buzz");       
            actual[14].Should().Be("FizzBuzz");
        }

        [Fact]
        public void NonSequential_List_With_Negatives_Works()
        {
            var rules = new FizzBuzzRules(new() { (3, "Fizz"), (5, "Buzz") });
            var gen = new FizzBuzzGenerator(rules);

            var input = new long[] { -5, 6, 300, 12, 15 };
            var actual = gen.GenerateForList(input).ToArray();

            actual.Should().Equal("Buzz", "Fizz", "FizzBuzz", "Fizz", "FizzBuzz");
        }

        [Fact]
        public void Custom_Rules_Concatenate_In_Given_Order()
        {
            var rules = new FizzBuzzRules(new() { (5, "Fizz"), (9, "Buzz"), (27, "Bar") });
            var gen = new FizzBuzzGenerator(rules);

            gen.GenerateForList(new long[] { 135 })  
               .Single().Should().Be("FizzBuzzBar");
        }

        [Fact]
        public void Throws_When_No_Rules()
        {
            var act = () => new FizzBuzzGenerator(new FizzBuzzRules(new List<(long, string)>()));
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Throws_When_Divisor_Is_Zero()
        {
            var rules = new FizzBuzzRules(new() { (0, "Oops") });
            var act = () => new FizzBuzzGenerator(rules);
            act.Should().Throw<ArgumentException>();
        }
    }
}

