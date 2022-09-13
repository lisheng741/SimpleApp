using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.EventBus;
using Simple.Common.Models;

namespace Simple.Services.EventHandlers;

public class RequestEventHandler : IEventHandler<RequestEvent>
{
    private readonly SimpleDbContext _context;

    public RequestEventHandler(SimpleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RequestEvent @event)
    {
        SysLogOperating logOperating = new SysLogOperating()
        {
            Account = @event.Account,
            Name = @event.Name,
            IsSuccess = @event.IsSuccess,
            Message = @event.Message,
            Result = @event.Result,
            Browser = @event.Browser,
            OperatingSystem = @event.OperatingSystem,
            Ip = @event.Ip,
            Url = @event.Url,
            Path = @event.Path,
            ClassName = @event.ClassName,
            MethodName = @event.MethodName,
            RequestMethod = @event.RequestMethod,
            Body = @event.Body,
            ElapsedTime = @event.ElapsedTime,
            OperatingTime = @event.OperatingTime
        };

        _context.Add(logOperating);
        await _context.SaveChangesAsync();
    }
}
