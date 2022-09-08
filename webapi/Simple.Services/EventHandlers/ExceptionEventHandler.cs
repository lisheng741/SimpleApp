using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.EventBus;
using Simple.Common.Models;

namespace Simple.Services.EventHandlers;

public class ExceptionEventHandler : IEventHandler<ExceptionEvent>
{
    private readonly SimpleDbContext _context;

    public ExceptionEventHandler(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ExceptionEvent @event)
    {
        SysLogException logException = new SysLogException()
        {
            Account = @event.Account,
            EventId = @event.Id,
            Name = @event.Name,
            Message = @event.Message,
            ClassName = @event.ClassName,
            MethodName = @event.MethodName,
            ExceptionSource = @event.ExceptionSource,
            StackTrace = @event.StackTrace,
            Parameters = @event.Parameters,
            ExceptionTime = @event.ExceptionTime,
        };

        _context.Add(logException);
        await _context.SaveChangesAsync();
    }
}
