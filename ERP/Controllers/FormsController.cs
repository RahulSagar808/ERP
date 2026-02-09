using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;
public class FormsController : Controller
{
  public IActionResult BasicInputs() => View();
  public IActionResult InputGroups() => View();
}
