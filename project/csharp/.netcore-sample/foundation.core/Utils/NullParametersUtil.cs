using System;
using System.Collections.Generic;
using Edu.Ntu.Foundation.Core.Constants;

namespace Edu.Ntu.Foundation.Core.Utils
{
    public static class NullParametersUtil
    {
        public static void Check<T>(T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(ErrorMessageConstants.NULL_REFERENCE);
            }
        }

        public static bool IsEmptyList<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
               return true;
            }
            return false;
        }
    }
}