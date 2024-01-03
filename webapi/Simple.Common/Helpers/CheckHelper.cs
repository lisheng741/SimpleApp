using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Helpers;

public static class CheckHelper
{
    public static T NotNull<T>(T? obj) where T : class
    {
        if (obj == null) throw new ArgumentNullException();
        return obj;
    }

    public static T NotNull<T>(T? obj, string objName) where T : class
    {
        if (obj == null) throw new ArgumentNullException(objName);
        return obj;
    }
}
