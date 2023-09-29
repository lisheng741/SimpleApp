using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Common.Result;

public interface IApiResultProvider
{
    IActionResult ProcessActionResult(IActionResult actionResult);

    IActionResult ProcessApiResultException(ApiResultException resultException);
}
