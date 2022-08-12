using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Services.Models;

public class PageModel
{
    public virtual string? Filter { get; set; }

    public virtual string Sort { get; set; } = "Sort";

    public virtual int PageIndex { get; set; } = 0;

    public virtual int PageSize { get; set; } = 10;
}

public class PageResultModel
{

}
