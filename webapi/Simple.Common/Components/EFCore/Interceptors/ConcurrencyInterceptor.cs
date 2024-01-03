using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Simple.Common.EFCore.Interceptors
{
    public class ConcurrencyInterceptor : ISaveChangesInterceptor
    {
        public ConcurrencyInterceptor() { }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}
