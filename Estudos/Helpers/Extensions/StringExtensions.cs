using System.Collections.Generic;
using System.Linq;

namespace Estudos.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<int> SplitToInt(this string data)
        {
            if (string.IsNullOrEmpty(data))
                return Enumerable.Empty<int>();

            var separators = new [] { ',', ':', ';', '-', '_', '.', ' '};

            return data
                .Split(separators)
                .Select(x => 
                {
                    int.TryParse(x.Trim(), out var value);
                    return value;
                });
        }
    }
}