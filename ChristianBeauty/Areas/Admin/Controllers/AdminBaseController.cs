using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.HttpHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    [PermissionChecker]
    [Area("Admin")]
    public class AdminBaseController : Controller { }
}
