using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Simple.Common.EventBus;
using Simple.Common.Models;

namespace Simple.Common.Filters;

/// <summary>
/// 异常过滤器
/// </summary>
public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {

    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        OnException(context);
        return Task.CompletedTask;
    }
}
