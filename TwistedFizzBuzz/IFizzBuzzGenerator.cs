using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistedFizzBuzz
{
    public interface IFizzBuzzGenerator
    {
        IEnumerable<string> GenerateForRange(long startInclusive, long endInclusive);
        IEnumerable<string> GenerateForList(IEnumerable<long> numbers);
    }
}
