
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    static class Class1
    {
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> sourceSequence, int interval)
        {
            int index = 0;
            foreach (T item in sourceSequence)
            {
                if (index++ % interval == 0)
                    yield return item;
            }
        }
    }
}
