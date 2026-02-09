using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

public class ExtendedUiController : Controller
{
  public IActionResult PerfectScrollbar() => View();
  public IActionResult TextDivider() => View();
}
