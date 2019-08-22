using System;
using System.Collections.Generic;

namespace Utilities
{
   public static class ExtensionCommonFunc
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var o in list)
            {
                action(o);
            }
        }

        public static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
}
