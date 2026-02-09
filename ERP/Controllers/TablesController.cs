using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

public class TablesController : Controller
{
  public IActionResult Basic() => View();
}
